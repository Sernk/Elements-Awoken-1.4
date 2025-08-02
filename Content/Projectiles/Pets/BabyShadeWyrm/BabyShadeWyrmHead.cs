using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Pets.BabyShadeWyrm
{
    public class BabyShadeWyrmHead : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projPet[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.timeLeft *= 5;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if ((int)Main.time % 120 == 0)
            {
                Projectile.netUpdate = true;
            }
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }

            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead)
            {
                modPlayer.babyShadeWyrm = false;
            }
            if (modPlayer.babyShadeWyrm)
            {
                Projectile.timeLeft = 2;
            }

            Vector2 center14 = player.Center;
            int num1049 = 10;
            int num1053 = -1;
            if (Projectile.Distance(center14) > 2000f)
            {
                Projectile.Center = center14;
                Projectile.netUpdate = true;
            }
            if (num1053 != -1)
            {
                NPC nPC15 = Main.npc[num1053];
                Vector2 vector148 = nPC15.Center - Projectile.Center;
                float num1057 = (float)(vector148.X > 0f).ToDirectionInt();
                float num1058 = (float)(vector148.Y > 0f).ToDirectionInt();
                float scaleFactor15 = 0.4f;
                if (vector148.Length() < 600f)
                {
                    scaleFactor15 = 0.6f;
                }
                if (vector148.Length() < 300f)
                {
                    scaleFactor15 = 0.8f;
                }
                if (vector148.Length() > nPC15.Size.Length() * 0.75f)
                {
                    Projectile.velocity += Vector2.Normalize(vector148) * scaleFactor15 * 1.5f;
                    if (Vector2.Dot(Projectile.velocity, vector148) < 0.25f)
                    {
                        Projectile.velocity *= 0.8f;
                    }
                }
                float num1059 = 30f;
                if (Projectile.velocity.Length() > num1059)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num1059;
                }
            }
            else
            {
                float num1060 = 0.2f;
                Vector2 vector149 = center14 - Projectile.Center;
                if (vector149.Length() < 200f)
                {
                    num1060 = 0.12f;
                }
                if (vector149.Length() < 140f)
                {
                    num1060 = 0.06f;
                }
                if (vector149.Length() > 100f)
                {
                    if (Math.Abs(center14.X - Projectile.Center.X) > 20f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num1060 * (float)Math.Sign(center14.X - Projectile.Center.X);
                    }
                    if (Math.Abs(center14.Y - Projectile.Center.Y) > 10f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num1060 * (float)Math.Sign(center14.Y - Projectile.Center.Y);
                    }
                }
                else if (Projectile.velocity.Length() > 2f)
                {
                    Projectile.velocity *= 0.96f;
                }
                if (Math.Abs(Projectile.velocity.Y) < 1f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - 0.1f;
                }
                float num1061 = 15f;
                if (Projectile.velocity.Length() > num1061)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num1061;
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            int direction = Projectile.direction;
            Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : -1));
            if (direction != Projectile.direction)
            {
                Projectile.netUpdate = true;
            }
            float num1062 = MathHelper.Clamp(Projectile.localAI[0], 0f, 50f);
            Projectile.position = Projectile.Center;
            Projectile.scale = 1f + num1062 * 0.01f;
            Projectile.width = (Projectile.height = (int)((float)num1049 * Projectile.scale));
            Projectile.Center = Projectile.position;
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 42;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                    return;
                }
            }         
        }
    }
}