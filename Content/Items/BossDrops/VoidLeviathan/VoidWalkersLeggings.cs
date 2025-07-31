using ElementsAwoken.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Legs)]
    public class VoidWalkersLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.defense = 18;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
            float speedBoost = MathHelper.Lerp(1.5f, 1f, (float)player.statLife / (float)player.statLifeMax2);
            player.moveSpeed *= speedBoost;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 6);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}