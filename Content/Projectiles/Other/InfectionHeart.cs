using ElementsAwoken.Content.Items.BossDrops.Azana;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Other
{
    public class InfectionHeart : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.velocity *= 0.5f;
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.3f, 0.4f);

            Projectile.velocity *= 0.95f;

            int maxConvert = Main.expertMode ? MyWorld.awakenedMode ? 75 : 65 : 60;
            int maxDist = 300;
            for (int k = 0; k < Main.maxItems; k++)
            {
                Item other = Main.item[k];
                if ((other.Name.Contains("Ore") || Const.VanillaOreIDs().Contains(other.type)) && other.type != ModContent.ItemType<DiscordantOre>() && other.maxStack > 1 && other.active && Vector2.Distance(other.Center,Projectile.Center) < maxDist)
                {
                    if (other.stack + Projectile.ai[0] >= maxConvert)
                    {
                        int diff = other.stack - (int)Projectile.ai[0];
                        if (diff > maxConvert) diff = maxConvert;
                        other.stack -= diff;
                        int item = Item.NewItem(Const.Proj(Projectile), other.Center, ModContent.ItemType<DiscordantOre>(), diff);
                        if (Main.netMode == 1 && item >= 0)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                        }
                        Projectile.ai[0] = maxConvert;
                    }
                    else
                    {
                        int stack = other.stack;
                        other.SetDefaults(ModContent.ItemType<DiscordantOre>());
                        other.stack = stack;
                        Projectile.ai[0] += other.stack;
                    }
                    for (int p = 0; p < 10; p++)
                    {
                        Dust d = Main.dust[Dust.NewDust(Projectile.Center + (other.Center - Projectile.Center) * Main.rand.NextFloat() - new Vector2(4, 4), 0, 0, 127)];
                        d.noGravity = true;
                        d.velocity *= 0.04f;
                        d.scale *= 0.8f;
                    }
                }
            }
            if (Projectile.ai[0] >= maxConvert)
            {
                Projectile.Kill();
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item95, Projectile.Center);
            {
                int numDusts = 50;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (Vector2.One * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                    Vector2 velocity = position - Projectile.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, 127, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 6f;
                }
            }
            {
                int numDusts = 20;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (Vector2.One * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                    Vector2 velocity = position - Projectile.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, 127, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                }
            }
            if (Projectile.ai[0] == 0 && Projectile.owner == Main.myPlayer)
            {
                int item = Item.NewItem(Const.Proj(Projectile), Projectile.Center, ModContent.ItemType<Content.Items.BossDrops.Azana.InfectionHeart>());
                if (Main.netMode == 1 && item >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                }
            }
        }
    }
}