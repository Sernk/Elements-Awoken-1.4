using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Flails
{
    public class DyingAzureP : ModProjectile
    {
        private int deathTimer = 400;
        private float withdrawSpeed = 8f; // speed it goes in

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Melee;
            //projectile.aiStyle = 13;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dying Azure");
        }
        public override void AI()
        {
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
            }
            else
            {
                if (Projectile.alpha == 0)
                {
                    if (Projectile.position.X + (float)(Projectile.width / 2) > Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2)) Main.player[Projectile.owner].ChangeDir(1);
                    else Main.player[Projectile.owner].ChangeDir(-1);
                }
                //if (projectile.type == 481)
                {
                    if (Projectile.ai[0] == 0f)  Projectile.extraUpdates = 0;
                    else Projectile.extraUpdates = 1;
                }
                Vector2 vector15 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num172 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector15.X;
                float num173 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector15.Y;
                float num174 = (float)Math.Sqrt((double)(num172 * num172 + num173 * num173));
                if (Projectile.ai[0] == 0f)
                {
                    if (num174 > 700f) Projectile.ai[0] = 1f;
                    Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                    Projectile.ai[1] = Projectile.ai[1] += 1f;
                    if (Projectile.ai[1] > 5f) Projectile.alpha = 0;
                    Projectile.ai[1] = 8f;
                    if (Projectile.ai[1] >= 10f)
                    {
                        Projectile.ai[1] = 15f;
                        Projectile.velocity.Y = Projectile.velocity.Y += 0.3f;
                    }
                }
                else if (Projectile.ai[0] == 1f)
                {
                    Projectile.tileCollide = false;
                    Projectile.rotation = (float)Math.Atan2((double)num173, (double)num172) - 1.57f;
                    if (num174 < 50f) Projectile.Kill();
                    num174 = withdrawSpeed / num174;
                    num172 *= num174;
                    num173 *= num174;
                    Projectile.velocity.X = num172;
                    Projectile.velocity.Y = num173;
                }
            }
            deathTimer--;
            if (deathTimer <= 0)  withdrawSpeed = 20f;
            if (deathTimer > 50)
            {
                if (Main.rand.Next(18) == 0)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ModContent.ProjectileType<ChaosFireball>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
            }
            ProjectileUtils.PushOtherEntities(Projectile);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 127 }, Projectile.damage, "melee");
            Projectile.ai[0] = 1;
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 127 }, Projectile.damage, "melee");
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(EAU.ModifyProjTexture("Flails/DyingAzureChain")).Value;

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y)) flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y)) flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0) flag = false;
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }       
    }
}