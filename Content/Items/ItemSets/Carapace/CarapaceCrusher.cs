using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Carapace
{
    public class CarapaceCrusher : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.damage = 8;
            Item.pick = 45;
            Item.knockBack = 6f;
            Item.useTime = 39;
            Item.useAnimation = 39;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.rare = 0;
            Item.GetGlobalItem<ItemsGlobal>().miningRadius = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}