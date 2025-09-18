using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Content.Projectiles.Turrets
{
    public abstract class TurretBase : ModProjectile
    {
        private float aiState
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        private float shootTimer
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        protected float maxRange = 600f;
        protected float deadBottomAngle = 0.75f;
        protected float projSpeed = 24f;
        protected float shootCDAmount = 120;
        protected string baseTex = "ElementsAwoken/Content/Projectiles/Turrets/RifleSentryBase";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC npc)
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.position += Projectile.velocity;
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        // based off ballista code
        public override void AI()
        {
            if (aiState == 0f)
            {
                Projectile.direction = (Projectile.spriteDirection = Main.player[Projectile.owner].direction);
                aiState = 1f;
                shootTimer = 0f;
                Projectile.netUpdate = true;
                if (Projectile.direction == -1)
                {
                    Projectile.rotation = 3.14159274f;
                }
            }
            if (aiState == 1f)
            {
                bool flag = false;
                if (shootTimer < shootCDAmount)
                {
                    shootTimer += 1f;
                }
                else
                {
                    flag = true;
                }
                int num8 = FindTarget(maxRange, deadBottomAngle, Projectile.Center);
                if (num8 != -1)
                {
                    Vector2 vector = (Main.npc[num8].Center - Projectile.Center).SafeNormalize(Vector2.UnitY);
                    Projectile.rotation = Projectile.rotation.AngleLerp(vector.ToRotation(), 0.08f);
                    if (Projectile.rotation > 1.57079637f || Projectile.rotation < -1.57079637f)
                    {
                        Projectile.direction = -1;
                    }
                    else
                    {
                        Projectile.direction = 1;
                    }
                    if (flag && Projectile.owner == Main.myPlayer)
                    {
                        Projectile.direction = Math.Sign(vector.X);
                        aiState = 2f;
                        Projectile.netUpdate = true;
                    }
                }
                else
                {
                    float targetAngle = 0f;
                    if (Projectile.direction == -1)
                    {
                        targetAngle = 3.14159274f;
                    }
                    Projectile.rotation = Projectile.rotation.AngleLerp(targetAngle, 0.05f);
                }
            }
            else if (aiState == 2f)
            {
                if (shootTimer >= shootCDAmount)
                {
                    Vector2 vector2 = new Vector2((float)Projectile.direction, 0f); // shoot whichever way its facing
                    int targetIndex = FindTarget(maxRange, deadBottomAngle, Projectile.Center);
                    if (targetIndex != -1)
                    {
                        vector2 = (Main.npc[targetIndex].Center - Projectile.Center).SafeNormalize(Vector2.UnitX * (float)Projectile.direction);
                    }
                    Projectile.rotation = vector2.ToRotation();
                    if (Projectile.rotation > 1.57079637f || Projectile.rotation < -1.57079637f)
                    {
                        Projectile.direction = -1;
                    }
                    else
                    {
                        Projectile.direction = 1;
                    }
                    if (Projectile.owner == Main.myPlayer)
                    {
                        if (targetIndex != -1)
                        {
                            Shoot(Main.npc[targetIndex]);
                        }
                    }
                }
                aiState = 1f;
                shootTimer = 0;
            }
            Projectile.spriteDirection = Projectile.direction;
            Projectile.tileCollide = true;
            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;

            ExtraAI();
        }
        public virtual void ExtraAI()
        {
        }
        private int FindTarget(float shot_range, float deadBottomAngle, Vector2 shootingSpot)
        {
            int num = -1;
            NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
            {
                for (int i = 0; i < 1; i++)
                {
                    if (ownerMinionAttackTargetNPC.CanBeChasedBy(this, true))
                    {
                        float num2 = Vector2.Distance(shootingSpot, ownerMinionAttackTargetNPC.Center);
                        if (num2 <= shot_range)
                        {
                            Vector2 vector = (ownerMinionAttackTargetNPC.Center - shootingSpot).SafeNormalize(Vector2.UnitY);
                            if ((Math.Abs(vector.X) >= Math.Abs(vector.Y) * deadBottomAngle || vector.Y <= 0f) && (num == -1 || num2 < Vector2.Distance(shootingSpot, Main.npc[num].Center)) && Collision.CanHitLine(shootingSpot, 0, 0, ownerMinionAttackTargetNPC.Center, 0, 0))
                            {
                                num = ownerMinionAttackTargetNPC.whoAmI;
                            }
                        }
                    }
                }
                if (num != -1)
                {
                    return num;
                }
            }
            for (int j = 0; j < 200; j++)
            {
                NPC nPC = Main.npc[j];
                if (nPC.CanBeChasedBy(this, true))
                {
                    float num3 = Vector2.Distance(shootingSpot, nPC.Center);
                    if (num3 <= shot_range)
                    {
                        Vector2 vector2 = (nPC.Center - shootingSpot).SafeNormalize(Vector2.UnitY);
                        if ((Math.Abs(vector2.X) >= Math.Abs(vector2.Y) * deadBottomAngle || vector2.Y <= 0f) && (num == -1 || num3 < Vector2.Distance(shootingSpot, Main.npc[num].Center)) && Collision.CanHitLine(shootingSpot, 0, 0, nPC.Center, 0, 0))
                        {
                            num = j;
                        }
                    }
                }
            }
            return num;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // base
            {
                Texture2D baseTexture = ModContent.Request<Texture2D>(baseTex).Value;
                Vector2 baseOrigin = baseTexture.Size() * new Vector2(0.5f, 1f);
                baseOrigin.Y -= 2f;
                Vector2 drawPos = Projectile.Bottom - Main.screenPosition + new Vector2(0,Projectile.gfxOffY);
                EAU.Sb.Draw(baseTexture, drawPos, null, Projectile.GetAlpha(lightColor), 0f, baseOrigin, Projectile.scale, SpriteEffects.None & SpriteEffects.FlipHorizontally, 0f);
            }
            //head
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (Projectile.spriteDirection == -1) spriteEffects = SpriteEffects.FlipVertically;
                Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, TextureAssets.Projectile[Projectile.type].Value.Height * 0.5f);
                Vector2 drawPos = Projectile.Top - Main.screenPosition;
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, Projectile.GetAlpha(lightColor), Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0f);
            }
            return false;
        }
        public virtual void Shoot(NPC target) // use this to decide how it shoots 
        {
        }
    }
}