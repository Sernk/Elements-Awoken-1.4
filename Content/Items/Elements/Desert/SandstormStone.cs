using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    public class SandstormStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item66;
            Item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ZoneDesert)
            {
                return true;
            }
            return false;
        }
        public override bool? UseItem(Player player)
        {
            if (Sandstorm.Happening == true)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().SandstormStone, 227, 200, 93);
                Sandstorm.Happening = false;
                Sandstorm.TimeLeft = 0;
                SandstormStuff();
                return true;
            }
            else if (Sandstorm.Happening == false)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().SandstormStone1, 227, 200, 93);
                Sandstorm.Happening = true;
                Sandstorm.TimeLeft = (int)(3600.0 * (8.0 + (double)Main.rand.NextFloat() * 16.0));
                SandstormStuff();
                return true;
            }
            return false;
        }
        public static void SandstormStuff()
        {
            Sandstorm.IntendedSeverity = !Sandstorm.Happening ? (Main.rand.Next(3) != 0 ? Main.rand.NextFloat() * 0.3f : 0.0f) : 0.4f + Main.rand.NextFloat();
            if (Main.netMode == 1)
                return;
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