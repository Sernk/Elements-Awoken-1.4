using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    public class DesertNeck : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 3;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            //ArmorIDs.Head.Sets.DrawHead[Item.bodySlot] = false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed *= 1.25f;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.25f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertEssence>(), 4);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 25);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 10);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}