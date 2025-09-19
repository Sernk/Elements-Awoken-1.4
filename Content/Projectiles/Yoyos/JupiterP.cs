using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class JupiterP : ModProjectile
    {
        public float timer = 30;
        public bool hasGas = false;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 410f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 18f;
        }
        public override void AI()
        {
            int maxDist = 200;
            float gravStength = 0.075f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                bool immune = false;
                foreach (int k in ElementsAwoken.instakillImmune)
                {
                    if (npc.type == k)
                    {
                        immune = true;
                    }
                }
                if (!immune && npc.CanBeChasedBy(this) && !npc.boss && npc.lifeMax < 10000 && Vector2.Distance(npc.Center, Projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(Projectile.Center.X - npc.Center.X, Projectile.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity.X += toTarget.X * gravStength;
                    npc.velocity.Y += toTarget.Y * gravStength * 5;
                }
            }
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item item = Main.item[i];
                if (item.active && Vector2.Distance(item.Center, Projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(Projectile.Center.X - item.Center.X, Projectile.Center.Y - item.Center.Y);
                    toTarget.Normalize();
                    item.velocity += toTarget * gravStength;
                }
            }
            if (!hasGas && Projectile.localAI[0] >= 20) // 0 is a timer
            {
                int swirlCount = 8;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    int orbital = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<JupiterGasOrbit>(), Projectile.damage, Projectile.knockBack, 0, l * distance, Projectile.whoAmI);
                    Projectile Orbital = Main.projectile[orbital];
                }
                hasGas = true;
            }
        }
    }
}