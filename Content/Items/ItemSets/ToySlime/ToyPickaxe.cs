using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.ToySlime
{
    public class ToyPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 60;         
            Item.damage = 11;
            Item.pick = 95;
            Item.knockBack = 4.5f;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
