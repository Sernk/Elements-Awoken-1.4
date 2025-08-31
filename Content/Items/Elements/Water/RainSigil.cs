using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Water
{
    public class RainSigil : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item66;
            Item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {
            if (MyWorld.radiantRain && !MyWorld.completedRadiantRain)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().RainSigil ,Color.HotPink);
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool? UseItem(Player player)
        {
            if (Main.raining)
            {
                Main.rainTime = 0;
                Main.raining = false;
                Main.maxRaining = 0f;
                CombatText.NewText(player.getRect(), Color.Aqua, ModContent.GetInstance<EALocalization>().RainSigil1, true, false);
            }
            else if (!Main.raining)
            {
                CombatText.NewText(player.getRect(), Color.Aqua, ModContent.GetInstance<EALocalization>().RainSigil2, true, false);

                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0) Main.rainTime += Main.rand.Next(0, num2);
                if (Main.rand.Next(4) == 0) Main.rainTime += Main.rand.Next(0, num2 * 2);
                if (Main.rand.Next(5) == 0) Main.rainTime += Main.rand.Next(0, num2 * 2);
                if (Main.rand.Next(6) == 0) Main.rainTime += Main.rand.Next(0, num2 * 3);
                if (Main.rand.Next(7) == 0) Main.rainTime += Main.rand.Next(0, num2 * 4);
                if (Main.rand.Next(8) == 0) Main.rainTime += Main.rand.Next(0, num2 * 5);
                float num3 = 1f;
                if (Main.rand.Next(2) == 0) num3 += 0.05f;
                if (Main.rand.Next(3) == 0) num3 += 0.1f;
                if (Main.rand.Next(4) == 0) num3 += 0.15f;
                if (Main.rand.Next(5) == 0) num3 += 0.2f;
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                Main.maxRaining = Main.rand.NextFloat(0.2f,0.8f);
                MyWorld.prevTickRaining = true; // to stop radiant rain from happening from this
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}
