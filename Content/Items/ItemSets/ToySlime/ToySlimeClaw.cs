using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.ToySlime
{
    public class ToySlimeClaw : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ModContent.RarityType<EARarity.Awakened>();
            Item.accessory = true;
            Item.GetGlobalItem<EARaritySettings>().awakened = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var e = ModContent.GetInstance<EALocalization>();
            var list = ElementsAwoken.specialAbility.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = e.ToySlimeClaw;
            string[] Line = [e.ToySlimeClaw1, e.ToySlimeClaw2 + " " + hotkey + " " + e.ToySlimeClaw3, e.ToySlimeClaw4];

            tooltips.RemoveAll(t => t.Mod == "Terraria" && t.Name.StartsWith("Tooltip"));

            for (int i = 0; i < Line.Length; i++)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip" + (i + 1), Line[i]));
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.toySlimeClaw = true;
        }
    }
}