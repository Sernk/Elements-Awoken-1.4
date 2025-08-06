using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class HungryMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.tileCollide = false;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.timeLeft = 18000;
            Projectile.alpha = 30;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/Flails/HungryFlailChain").Value;

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
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                    Projectile.frame = 0;
            }
            return true;
        }
    
        public override void AI()
        {
               Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead || !player.active || !modPlayer.hellHeart) Projectile.Kill();
            ProjectileUtils.PushOtherEntities(Projectile);

            float targetX = Projectile.position.X;
            float targetY = Projectile.position.Y;
            float targetDist = 900f;
            bool attacking = false;
            int maxDist = 500;
            if (Projectile.ai[1] != 0f)
            {
                maxDist = 700;
            }
            if (Math.Abs(Projectile.Center.X - player.Center.X) + Math.Abs(Projectile.Center.Y - player.Center.Y) > (float)maxDist)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.tileCollide = true;
                NPC ownerMinionAttackTargetNPC10 = Projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC10 != null && ownerMinionAttackTargetNPC10.CanBeChasedBy(this))
                {
                    float num1059 = ownerMinionAttackTargetNPC10.position.X + (float)(ownerMinionAttackTargetNPC10.width / 2);
                    float num1058 = ownerMinionAttackTargetNPC10.position.Y + (float)(ownerMinionAttackTargetNPC10.height / 2);
                    float num1057 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num1059) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num1058);
                    if (num1057 < targetDist && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC10.position, ownerMinionAttackTargetNPC10.width, ownerMinionAttackTargetNPC10.height))
                    {
                        targetDist = num1057;
                        targetX = num1059;
                        targetY = num1058;
                        attacking = true;
                    }
                }
                if (!attacking)
                {
                    for (int num1056 = 0; num1056 < 200; num1056++)
                    {
                        if (Main.npc[num1056].CanBeChasedBy(this))
                        {
                            float num1055 = Main.npc[num1056].position.X + (float)(Main.npc[num1056].width / 2);
                            float num1054 = Main.npc[num1056].position.Y + (float)(Main.npc[num1056].height / 2);
                            float num1053 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num1055) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num1054);
                            if (num1053 < targetDist && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num1056].position, Main.npc[num1056].width, Main.npc[num1056].height))
                            {
                                targetDist = num1053;
                                targetX = num1055;
                                targetY = num1054;
                                attacking = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Projectile.tileCollide = false;
            }
            if (!attacking)
            {
                Projectile.friendly = true;
                float num1051 = 8f;
                if (Projectile.ai[0] == 1f)
                {
                    num1051 = 12f;
                }
                Vector2 vector301 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num1050 = player.Center.X - vector301.X;
                if (player.velocity.X != 0)
                {
                    num1050 = player.Center.X + 300 * Math.Sign(player.velocity.X) - vector301.X;
                    num1051 = Math.Abs(player.velocity.X * 1.4f);
                }
                float num1049 = player.Center.Y - vector301.Y - 60f;
                float num1048 = (float)Math.Sqrt(num1050 * num1050 + num1049 * num1049);
                if (num1048 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                }
                if (num1048 > 2000f)
                {
                    Projectile.position.X = player.Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = player.Center.Y - (float)(Projectile.width / 2);
                }
                if (num1048 > 70f)
                {
                    num1048 = num1051 / num1048;
                    num1050 *= num1048;
                    num1049 *= num1048;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num1050) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num1049) / 21f;
                }
                else
                {
                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X = -0.15f;
                        Projectile.velocity.Y = -0.05f;
                    }
                    Projectile.velocity *= 1.01f;
                }
                Projectile.friendly = false;
                Projectile.rotation = Projectile.velocity.X * 0.05f;
                if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                }
                return;
            }
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] -= 1f;
            }
            if (Projectile.ai[1] == 0f)
            {
                Projectile.friendly = true;
                float num1043 = 8f;
                Vector2 vector300 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num1042 = targetX - vector300.X;
                float num1041 = targetY - vector300.Y;
                float num1040 = (float)Math.Sqrt(num1042 * num1042 + num1041 * num1041);
                if (num1040 < 100f)
                {
                    num1043 = 10f;
                }
                num1040 = num1043 / num1040;
                num1042 *= num1040;
                num1041 *= num1040;
                float speed = 14f;
                //if (targetDist < 20) speed = 4f;
                Projectile.velocity.X = (Projectile.velocity.X * speed + num1042) / 15f;
                Projectile.velocity.Y = (Projectile.velocity.Y * speed + num1041) / 15f;
            }
            else
            {
                Projectile.friendly = false;
                if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 10f)
                {
                    Projectile.velocity *= 1.05f;
                }
            }
            Projectile.rotation = Projectile.velocity.X * 0.05f;
            if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
            {
                Projectile.spriteDirection = -Projectile.direction;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (player.statLife < player.statLifeMax2)
            {
                int heal = Main.rand.Next(1, 2);
                player.statLife += heal;
                player.HealEffect(heal);
            }
            Projectile.ai[1] = 9f; // this is to make them not stick to the enemy so hard
        }
    }
}