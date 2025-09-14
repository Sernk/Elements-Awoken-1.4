using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Pets
{
    public class LilOrange : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Penguin);
            Projectile.height = 32;
            Projectile.width = 28;
            AIType = ProjectileID.Penguin;
            Main.projFrames[Projectile.type] = 3;
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
            if (player.dead) modPlayer.lilOrange = false;
            if (modPlayer.lilOrange) Projectile.timeLeft = 2;
        }
    }
}