using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class WarriorSlice : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 36;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 15;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            NPC parent = Main.npc[(int)Projectile.ai[0]];
            Projectile.Center = parent.Center + new Vector2(30 * parent.direction, 0);
        }
    }
}