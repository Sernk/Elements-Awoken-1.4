using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class ChaoticImpaler : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 22;
            Item.knockBack = 2.25f;
            Item.damage = 190;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.rare = RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.UseSound = SoundID.Item5;
            Item.shoot = 10;
            Item.shootSpeed = 18f;
            Item.useAmmo = 40;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(4));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<ChaosLaser>(), damage, knockBack, player.whoAmI);
            }
            if (Main.rand.Next(5) == 0)
            {
                SoundEngine.PlaySound(SoundID.Item92.WithPitchOffset(-0.3f), position);
                int numberProjectiles2 = 2;
                for (int i = 0; i < numberProjectiles2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(3));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<ChaoticBlast>(), damage * 3, knockBack, player.whoAmI);
                }
            }
            return false;
        }
    }
}