using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Content.Projectiles.Drills
{
    public class MatterManipulator : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.9f);
            Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0.4f, 0.2f, 0.9f);

            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= 60f)
            {
                Projectile.localAI[0] = 0f;
            }
            if (Vector2.Distance(vector, Projectile.Center) >= 5f)
            {
                float num8 = Projectile.localAI[0] / 60f;
                if (num8 > 0.5f)
                {
                    num8 = 1f - num8;
                }
                Vector3 arg_548_0 = new Vector3(0f, 1f, 0.7f);
                Vector3 value3 = new Vector3(0f, 0.7f, 1f);
                Vector3 vector6 = Vector3.Lerp(arg_548_0, value3, 1f - num8 * 2f) * 0.5f;
                if (Vector2.Distance(vector, Projectile.Center) >= 30f)
                {
                    Vector2 vector7 = Projectile.Center - vector;
                    vector7.Normalize();
                    vector7 *= Vector2.Distance(vector, Projectile.Center) - 30f;
                    DelegateMethods.v3_1 = vector6 * 0.8f;
                    Utils.PlotTileLine(Projectile.Center - vector7, Projectile.Center, 8f, DelegateMethods.CastLightOpen);
                }
                Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, vector6.X, vector6.Y, vector6.Z);
            }
            if (Main.myPlayer == Projectile.owner)
            {
                if (Projectile.localAI[1] > 0f)
                {
                    Projectile.localAI[1] -= 1f;
                }
                if (!player.channel || player.noItems || player.CCed)
                {
                    Projectile.Kill();
                }
                else if (Projectile.localAI[1] == 0f)
                {
                    Vector2 vector8 = vector;
                    Vector2 vector9 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector8;
                    if (player.gravDir == -1f)
                    {
                        vector9.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector8.Y;
                    }
                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].HasTile)
                    {
                        vector9 = new Vector2((float)Player.tileTargetX, (float)Player.tileTargetY) * 16f + Vector2.One * 8f - vector8;
                        Projectile.localAI[1] = 2f;
                    }
                    vector9 = Vector2.Lerp(vector9, Projectile.velocity, 0.7f);
                    if (float.IsNaN(vector9.X) || float.IsNaN(vector9.Y))
                    {
                        vector9 = -Vector2.UnitY;
                    }
                    float num9 = 30f;
                    if (vector9.Length() < num9)
                    {
                        vector9 = Vector2.Normalize(vector9) * num9;
                    }
                    int tileBoost = player.inventory[player.selectedItem].tileBoost;
                    int num10 = -Player.tileRangeX - tileBoost + 1;
                    int num11 = Player.tileRangeX + tileBoost - 1;
                    int num12 = -Player.tileRangeY - tileBoost;
                    int num13 = Player.tileRangeY + tileBoost - 1;
                    int num14 = 12;
                    bool flag2 = false;
                    if (vector9.X < (float)(num10 * 16 - num14))
                    {
                        flag2 = true;
                    }
                    if (vector9.Y < (float)(num12 * 16 - num14))
                    {
                        flag2 = true;
                    }
                    if (vector9.X > (float)(num11 * 16 + num14))
                    {
                        flag2 = true;
                    }
                    if (vector9.Y > (float)(num13 * 16 + num14))
                    {
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        Vector2 vector10 = Vector2.Normalize(vector9);
                        float num15 = -1f;
                        if (vector10.X < 0f && ((float)(num10 * 16 - num14) / vector10.X < num15 || num15 == -1f))
                        {
                            num15 = (float)(num10 * 16 - num14) / vector10.X;
                        }
                        if (vector10.X > 0f && ((float)(num11 * 16 + num14) / vector10.X < num15 || num15 == -1f))
                        {
                            num15 = (float)(num11 * 16 + num14) / vector10.X;
                        }
                        if (vector10.Y < 0f && ((float)(num12 * 16 - num14) / vector10.Y < num15 || num15 == -1f))
                        {
                            num15 = (float)(num12 * 16 - num14) / vector10.Y;
                        }
                        if (vector10.Y > 0f && ((float)(num13 * 16 + num14) / vector10.Y < num15 || num15 == -1f))
                        {
                            num15 = (float)(num13 * 16 + num14) / vector10.Y;
                        }
                        vector9 = vector10 * num15;
                    }
                    if (vector9.X != Projectile.velocity.X || vector9.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector9;
                }
            }

            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Vector2 vector63 = Projectile.position + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Vector2 vector64 = Main.player[Projectile.owner].RotatedRelativePoint(player.MountedCenter, true) + Vector2.UnitY * Main.player[Projectile.owner].gfxOffY;
            Vector2 vector65 = vector63 + Main.screenPosition - vector64;
            Vector2 value42 = Vector2.Normalize(vector65);
            float num296 = -5f;
            float num297 = num296 + 30f;

            // actual drill
            Texture2D texture2D38 = TextureAssets.Item[Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].type].Value;
            Vector2 arg_10665_2 = vector64 - Main.screenPosition + value42 * num296;
            Rectangle? sourceRectangle2 = null;
            Main.spriteBatch.Draw(texture2D38, arg_10665_2, sourceRectangle2, Color.White, Projectile.rotation + 1.57079637f + ((spriteEffects == SpriteEffects.None) ? 3.14159274f : 0f), new Vector2((float)((spriteEffects == SpriteEffects.None) ? 0 : texture2D38.Width), (float)texture2D38.Height / 2f) + Vector2.UnitY * 1f, Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].scale, spriteEffects, 0f);

            Vector2 thing = Projectile.velocity;
            thing.Normalize(); // makes the value = 1
            thing *= 20f;
            Vector2 yAdd = new Vector2(0, 0);
            if (player.direction == 1)
            {
                yAdd.Y = 6;
            }
            else
            {
                yAdd.Y = -6;
            }

            Texture2D texture = ModContent.Request<Texture2D>(EAU.ModifyProjTexture("Drills/ManipulatorChain")).Value;
            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position + thing.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2)) - yAdd;
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}