using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Youtuber
{
    public class GnomesBane : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 84;
            Item.height = 84;
            Item.damage = 170;
            Item.knockBack = 5;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<GiantGnome>();
            Item.shootSpeed = 18f;
            Item.GetGlobalItem<EATooltip>().youtuber = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ClayBlock, 50);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}