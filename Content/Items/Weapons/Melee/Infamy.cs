using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class Infamy : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 54;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 12;
            Item.useTurn = true;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cutlass, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
