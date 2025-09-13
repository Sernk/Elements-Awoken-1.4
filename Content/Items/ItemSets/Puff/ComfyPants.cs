using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Puff
{
    [AutoloadEquip(EquipType.Legs)]
    public class ComfyPants : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 2, 0);
            Item.rare = 1;
            Item.defense = 1;
        }
        public override void UpdateEquip(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.puffFall = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Puffball>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
