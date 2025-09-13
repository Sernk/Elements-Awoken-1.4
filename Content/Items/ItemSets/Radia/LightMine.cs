using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Radia
{
    public class LightMine : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 30;
            Item.width = 28;
            Item.damage = 350;
            Item.knockBack = 3.5f;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.UseSound = SoundID.Item9;
            Item.shoot = ProjectileType<RadiantStarMine>();
            Item.shootSpeed = 1f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 4;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(source, position + Main.rand.NextVector2Square(-400,400), Main.rand.NextVector2Square(-3, 3), type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Weapons.Magic.Tomes.FrostMine>());
            recipe.AddIngredient(ItemType<Radia>(), 16);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}