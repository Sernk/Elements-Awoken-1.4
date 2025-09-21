using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Held.Procedural
{
    public class WandHeld : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            string texLoc = "";
            if (Projectile.ai[0] == 0) texLoc = "Normal";
            if (Projectile.ai[0] == 1) texLoc = "Desert";
            if (Projectile.ai[0] == 2) texLoc = "Fire";
            if (Projectile.ai[0] == 3) texLoc = "Sky";
            if (Projectile.ai[0] == 4) texLoc = "Frost";
            if (Projectile.ai[0] == 5) texLoc = "Water";
            if (Projectile.ai[0] == 6) texLoc = "Void";
            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Items/Weapons/Procedural/" + texLoc + Projectile.ai[1]).Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            EAU.Sb.Draw(tex, drawPos, null, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            string texLoc = "";
            if (Projectile.ai[0] == 0) texLoc = "Normal";
            if (Projectile.ai[0] == 1) texLoc = "Desert";
            if (Projectile.ai[0] == 2) texLoc = "Fire";
            if (Projectile.ai[0] == 3) texLoc = "Sky";
            if (Projectile.ai[0] == 4) texLoc = "Frost";
            if (Projectile.ai[0] == 5) texLoc = "Water";
            if (Projectile.ai[0] == 6) texLoc = "Void";
            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Items/Weapons/Procedural/" + texLoc + Projectile.ai[1]).Value;

            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > player.HeldItem.useAnimation) Projectile.Kill();

            Vector2 offset = Projectile.velocity;
            offset.Normalize(); // makes the value = 1
            offset *= tex.Width / 3;

            Vector2 vector24 = player.RotatedRelativePoint(player.MountedCenter, true) + offset.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2));
            Projectile.direction = player.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            Projectile.position.X = vector24.X - (float)(Projectile.width / 2);
            Projectile.position.Y = vector24.Y - (float)(Projectile.height / 2);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (float)(Math.PI / 4);
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
        }
    }
}
 