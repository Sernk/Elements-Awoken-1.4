using ElementsAwoken.Content.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class InfernalFury : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 86;
            Projectile.height = 86;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60000;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.timeLeft = 60000;
            }
            Lighting.AddLight(player.Center, 0.75f, 0.3f, 0.1f);
            Projectile.Center = player.Center;

            Projectile.rotation += Projectile.ai[0] == 1 ? -0.075f : 0.05f;        
            Projectile.scale = Projectile.ai[0] == 1 ? 1.35f : 0.9f;

            bool hasChalice = false;
            int maxAccessoryIndex = 5 + player.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<InfernalChalice>())
                {
                    hasChalice = true;
                }
            }
            if (!hasChalice)
            {
                Projectile.Kill();
            }
        }
    }
}