using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class FusionP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 480f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 18f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Slow, 200);
            target.AddBuff(BuffID.OnFire, 200);
            target.AddBuff(BuffID.VortexDebuff, 200);
            target.AddBuff(BuffID.Frostburn, 200);
            target.AddBuff(BuffID.Wet, 200);
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 200);
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 63, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            if (Main.rand.Next(7) == 0)
            {
                int type = ModContent.ProjectileType<FusionOrb1>();
                switch (Main.rand.Next(6))
                {
                    case 0: type = ModContent.ProjectileType<FusionOrb1>(); break;
                    case 1: type = ModContent.ProjectileType<FusionOrb2>(); break;
                    case 2: type = ModContent.ProjectileType<FusionOrb3>(); break;
                    case 3: type = ModContent.ProjectileType<FusionOrb4>(); break;
                    case 4: type = ModContent.ProjectileType<FusionOrb5>(); break;
                    case 5: type = ModContent.ProjectileType<FusionOrb6>(); break;
                    default: break;
                }
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-50, 50) * 0.25f, Main.rand.Next(-50, 50) * 0.25f, type, Projectile.damage, 0, Projectile.owner);
            }
        }
    }
}