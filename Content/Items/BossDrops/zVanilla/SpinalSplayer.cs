using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla
{
    public class SpinalSplayer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 46;
            Item.knockBack = 2;
            Item.useTime = 42;
            Item.useAnimation = 42;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item61;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.shoot = 10;
            Item.shootSpeed = 25f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            if (Main.rand.Next(8) == 0)
            {
                int numberProjectiles = Main.rand.Next(3, 6);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<SpinalEye>(), damage, knockBack, player.whoAmI);
                }
                SoundEngine.PlaySound(SoundID.NPCDeath13, position);
            }
            else
            {
                Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<Spine>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}