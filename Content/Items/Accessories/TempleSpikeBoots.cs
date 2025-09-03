using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class TempleSpikeBoots : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 7;
            Item.value = Item.sellPrice(0, 2, 50, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.templeSpikeBoots = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SpikeBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SunFragment>(), 5);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 25);
            recipe.AddIngredient(ItemID.WoodenSpike, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<SpikeBoots>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}