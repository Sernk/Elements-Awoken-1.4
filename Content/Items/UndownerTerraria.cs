using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items
{
    public class UndownerTerraria : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = ModContent.RarityType<EARarity.BETATEST>();
            Item.GetGlobalItem<EARaritySettings>().betatest = true;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.mouseLeft)
            {
                foreach (var set in EAList.BossFlagsSet)
                {
                    set(false);
                }      
                Main.NewText("Все сброшено", EAColors.Purpal);
            }
            else if (Main.mouseRight)
            {
                foreach (var set in EAList.BossFlagsSet)
                {
                    set(true);
                }
                Main.NewText("Все босы были убиты", EAColors.Purpal);
            }
            return true;
        }
    }
}