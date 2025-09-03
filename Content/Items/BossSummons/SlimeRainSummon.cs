using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class SlimeRainSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.maxStack = 9999;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.rare = 1;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummonToolTips>().AwakenedSummonItem = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !Main.slimeRain;          
        }
        public override bool? UseItem(Player player)
        {
            Main.NewText(ModContent.GetInstance<EALocalization>().SlimeRainSummon, 175, 75, 255);
            Main.slimeRainTime = (double)Main.rand.Next(32400, 54000);
            Main.slimeRain = true;
            Main.slimeRainKillCount = 0;

            Main.StartSlimeRain(true); // doesnt want to work by itsself
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 30);
            recipe.AddIngredient(ItemID.PinkGel, 5);
            recipe.AddIngredient(ItemID.Bottle, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
