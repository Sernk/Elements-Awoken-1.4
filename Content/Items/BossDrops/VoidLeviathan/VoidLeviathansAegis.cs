using ElementsAwoken.EASystem;
using ElementsAwoken.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Shield)]
    public class VoidLeviathansAegis : ModItem
    {
        public int damageTaken = 0;
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var EALocalization = ModContent.GetInstance<EALocalization>();
            var list = ElementsAwoken.dash2.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = EALocalization.VoidLeviathansAegis;

            string baseTooltip = $"{EALocalization.VoidLeviathansAegis1} ({ hotkey }) {EALocalization.VoidLeviathansAegis2.Replace("<Dash Unbound>", hotkey)}\n";
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
            modPlayer.vleviAegis = true;

            player.statLifeMax2 += 50;
            if (player.statLife < player.statLifeMax2 / 2)
            {
                player.statDefense += 1 + (int)(player.statDefense * 1.05);
            }
        }
    }
}