using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EAUtilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.HiveCrate
{
    public class HoneyCocoon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 1;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var EALocalization = ModContent.GetInstance<EALocalization>();
            var list = ElementsAwoken.specialAbility.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = EALocalization.HoneyCocoon; 
            string baseTooltip = $"{EALocalization.HoneyCocoon1} ({hotkey}) {EALocalization.HoneyCocoon2.Replace("<Special Ability Unbound>", hotkey)}\n";          
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
            modPlayer.honeyCocoon = true;
        }
    }
}