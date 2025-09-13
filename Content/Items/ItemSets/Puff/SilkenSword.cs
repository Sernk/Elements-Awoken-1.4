using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Puff
{
    public class SilkenSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 20;
            Item.useTurn = true;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddIngredient(ModContent.ItemType<Puffball>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}