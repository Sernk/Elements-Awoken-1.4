using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Shield)]
    public class SkyShield : ModItem
    { 
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
            Item.rare = 6;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.GetJumpState(ExtraJump.CloudInABottle).Enable();
            player.wingTimeMax += 50;
            if (Main.player[Main.myPlayer].ZoneSkyHeight)
            {
                player.statDefense += 10;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.CloudinaBottle, 1);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}