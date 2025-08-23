using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.StatIncreases
{
    public class LunarStar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.consumable = true;    
            Item.useStyle = 4;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item4;
            Item.rare = 10;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        //public override void SetStaticDefaults()
        //{
        //    DisplayName.SetDefault("Lunar Star");
        //    Tooltip.SetDefault("Condense your mana with raw lunar power\nFighters of The Calamity need not apply\nIncreases mana by 100");
        //}
        //public override bool CanUseItem(Player player)
        //{
        //    bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
        //    return !calamityEnabled && player.statManaMax == 200 && player.GetModPlayer<MyPlayer>().lunarStarsUsed < 1;
        //}

        //public override bool UseItem(Player player)
        //{
        //    player.statManaMax2 += 100;
        //    player.statMana += 100;
        //    if (Main.myPlayer == player.whoAmI)
        //    {
        //        player.ManaEffect(100);
        //    }
        //    player.GetModPlayer<MyPlayer>().lunarStarsUsed += 1;
        //    return true;
        //}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 25);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.LunarBar, 40);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            //if (ModLoader.GetMod("CalamityMod") == null) recipe.Register();
        }
    }
}
