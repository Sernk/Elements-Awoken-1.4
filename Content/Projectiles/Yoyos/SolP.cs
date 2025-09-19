using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class SolP : ModProjectile
    {
        public bool hasOrbital = false;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 480f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 20f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Jupiter");
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.3f, 0.6f);

            int maxDist = 200;
            float gravStength = 0.1f;
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
            if (!hasOrbital && Projectile.localAI[0] >= 20) // 0 is a timer
            {
                int swirlCount = 17;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    int orbital = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<SolOrbit>(), Projectile.damage, Projectile.knockBack, 0, l * distance, Projectile.whoAmI);
                    Projectile Orbital = Main.projectile[orbital];
                }
                hasOrbital = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => target.AddBuff(BuffID.OnFire, 180, false);
    }
}