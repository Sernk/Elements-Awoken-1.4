using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.StatIncreases
{
    public class ExtraAcc : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.consumable = true;
            Item.width = 18;
            Item.height = 18;
            Item.useStyle = 4;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item4;
            Item.useAnimation = 30;
            Item.rare = ModContent.RarityType<EARarity.Awakened>();
            Item.GetGlobalItem<EARaritySettings>().awakened = true;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<MyPlayer>().extraAccSlot && MyWorld.awakenedMode; 
        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<MyPlayer>().extraAccSlot = true;
            return true;
        }
        public override bool OnPickup(Player player)
        {
            if (player.GetModPlayer<MyPlayer>().extraAccSlot) return false;
            else return true;
        }
        //public override void AddRecipes()
        //{
        //    ModRecipe recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.DemonHeart, 1);
        //    recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 25);
        //    recipe.AddIngredient(null, "VoidAshes", 12);
        //    recipe.AddIngredient(null, "VoidEssence", 15);
        //    recipe.AddTile(null, "ChaoticCrucible");
        //    recipe.SetResult(this);
        //    //if (ModLoader.GetMod("CalamityMod") == null) recipe.AddRecipe();
        //}
    }
}
