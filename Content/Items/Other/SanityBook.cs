using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Other
{
    public class SanityBook : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Orange;
        }
        public override bool CanRightClick() => true;
        public override void RightClick(Player player)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();

            if (MyWorld.awakenedMode)
            {
                if (!modPlayer.openSanityBook) modPlayer.openSanityBook = true;
                else modPlayer.openSanityBook = false;
            }
            else modPlayer.openSanityBook = false;
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddRecipeGroup(EARecipeGroups.EvilBar, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddOnCraftCallback(EAOnCraft.SanityBookOnCraft);
            recipe.Register();
        }
    }
}