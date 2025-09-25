using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Pets
{
    public class VoidCrawler : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Penguin);
            Projectile.height = 20;
            Projectile.width = 34;
            AIType = ProjectileID.Penguin;
            Main.projFrames[Projectile.type] = 6;
            Main.projPet[Projectile.type] = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.penguin = false; // Relic from aiType
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead) modPlayer.voidCrawler = false;
            if (modPlayer.voidCrawler) Projectile.timeLeft = 2;
        }
    }
}