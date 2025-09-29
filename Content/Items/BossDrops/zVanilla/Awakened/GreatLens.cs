using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla.Awakened
{
    public class GreatLens : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 2, 50, 0);
            Item.accessory = true;
            Item.rare = ModContent.RarityType<EARarity.Awakened>();
            Item.GetGlobalItem<EARaritySettings>().awakened = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var E = ModContent.GetInstance<EALocalization>();
            string hotkey;
            var list = ElementsAwoken.specialAbility.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = E.GreatLens;

            string baseTooltip = $"{E.GreatLens1} ({hotkey}) {E.GreatLens2 } \n{E.GreatLens3}";
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.Text = baseTooltip;
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.greatLens = true;
        }
    }
}