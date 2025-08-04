using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class BladeOfTheNight : ModItem
    {
        public int swingNum = 1;
        public int hitNum = 1;

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 200;
            Item.knockBack = 8;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.shoot = ModContent.ProjectileType<ExtinctionBall>();
            Item.shootSpeed = 20f;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (hitNum == 10) modifiers.SourceDamage *= 10;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Item.playerIndexTheItemIsReservedFor] = 3;
            hitNum++;
            if (hitNum > 10) hitNum = 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float speedX = speed.X;
            float speedY = speed.Y;
            swingNum++;
            if (swingNum > 30)
            {
                SoundEngine.PlaySound(SoundID.Item113, player.position);

                int current = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, speedX * 0.75f, speedY * 0.75f, ModContent.ProjectileType<VoidLeviathanProjHead>(), damage, 0f, Main.myPlayer);

                int previous = current;
                for (int k = 0; k < 12; k++)
                {
                    previous = current;
                    current = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<VoidLeviathanProjBody>(), damage, 0f, Main.myPlayer, previous);
                }
                previous = current;
                current = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<VoidLeviathanProjTail>(), damage, 0f, Main.myPlayer, previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;

                int numDusts = 36;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 dustPos = (Vector2.One * new Vector2((float)player.width / 2f, (float)player.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + player.Center;
                    Vector2 velocity = dustPos - player.Center;
                    Dust dust = Main.dust[Dust.NewDust(dustPos + velocity, 0, 0, EAU.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default, 1.4f)];
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.velocity = Vector2.Normalize(velocity) * 3f;
                    dust.fadeIn = 1.3f;
                }

                swingNum = 1;
            }
            int numProj = 3;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
                Projectile proj = Main.projectile[Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<VoidSinewave>(), damage, knockback, player.whoAmI)];
                proj.localAI[0] = Main.rand.NextFloat();
                proj.DamageType = DamageClass.Melee;
            }
            if (swingNum % 2 == 0)
            {
                int numProj2 = Main.rand.Next(1, 4);
                for (int i = 0; i < numProj2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * Main.rand.NextFloat(0.75f, 1f), perturbedSpeed.Y * Main.rand.NextFloat(0.75f, 1f), ModContent.ProjectileType<VoidOrb>(), damage, knockback, player.whoAmI);
                }
            }
            return false;
        }
    }
}