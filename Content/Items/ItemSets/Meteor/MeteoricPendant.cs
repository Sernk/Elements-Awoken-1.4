using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Meteor
{
    public class MeteoricPendant : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.rare = 1;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().meteoricPendant = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}