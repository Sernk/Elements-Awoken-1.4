using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Radia
{
    public class RadiantGlove : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.damage = 510;
            Item.knockBack = 8f;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.shoot = ProjectileType<RadiantStarThrown>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item9, position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Radia>(), 16);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}