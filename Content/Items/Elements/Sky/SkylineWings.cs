using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Wings)]
    public class SkylineWings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(110, 10f, 1f);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 110;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)			
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 10f;
            acceleration *= 2.5f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(ItemID.Feather, 8);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}