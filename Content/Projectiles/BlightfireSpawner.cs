using ElementsAwoken.Content.Items.ItemSets.Blightfire;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BlightfireSpawner : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public int outOfLava = 120;
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 3600;
            Projectile.alpha = 255;
            Projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blightfire");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 75, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust].velocity *= 0.6f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;

            if (Projectile.lavaWet)
            {
                Projectile.velocity.Y -= 0.05f;
            }
            else
            {
                Projectile.velocity *= Main.rand.NextFloat(0.9f, 0.95f);
                outOfLava--;
            }
            if (outOfLava <= 0)
            {
                Projectile.Kill();
            }
        }
        public override void OnKill(int timeLeft) => Item.NewItem(EAU.Proj(Projectile), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<Blightfire>());
    }
}