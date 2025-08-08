using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.CosmicObserver
{
    public class ObserverSpell : ModProjectile
    {
        public float rotSpeed = 0.05f;
        public float innerRot = 0f;
        public float middleRot = 0f;
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10000;
            Projectile.light = 0.4f;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            NPC parent = Main.npc[(int)Projectile.ai[1]];
            Player P = Main.player[Main.myPlayer];
            Projectile.Center = parent.Center;
            if (Projectile.localAI[0] == 0)
            {
                Projectile.scale = 0.2f;
                Projectile.localAI[0]++;
            }

            if (rotSpeed <= 0.3)
            {
                rotSpeed += 0.0005f;
            }
            Projectile.rotation += rotSpeed;
            innerRot -= rotSpeed;
            middleRot += rotSpeed;

            if (Projectile.scale < 1f)
            {
                Projectile.scale += 0.005f;
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 180)
            {
                Projectile.Kill();
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                float rotation = (float)Math.Atan2(Projectile.Center.Y - P.Center.Y, Projectile.Center.X - P.Center.X);
                float speed = 15f;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ModContent.ProjectileType<ObserverFireball>(), Projectile.damage, 0f, 0);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, TextureAssets.Projectile[Projectile.type].Value.Height * 0.5f);
            Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);

            EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, Color.White, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            EAU.Sb.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/CosmicObserver/ObserverSpell1").Value, drawPos, null, Color.White, innerRot, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            EAU.Sb.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/CosmicObserver/ObserverSpell2").Value, drawPos, null, Color.White, middleRot, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}