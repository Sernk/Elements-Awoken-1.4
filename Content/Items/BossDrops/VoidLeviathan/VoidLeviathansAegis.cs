using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
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
            var e = ModContent.GetInstance<EALocalization>();
            var list = ElementsAwoken.dash2.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = e.VoidLeviathansAegis;

            string[] lines = [e.VoidLeviathansAegis1, e.VoidLeviathansAegis2, e.VoidLeviathansAegis3 + e.VoidLeviathansAegis4 + " " + hotkey, e.VoidLeviathansAegis5, e.VoidLeviathansAegis6, e.VoidLeviathansAegis7];

            tooltips.RemoveAll(t => t.Mod == "Terraria" && t.Name.StartsWith("Tooltip"));

            for (int i = 0; i < lines.Length; i++)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip" + (i + 1), lines[i]));
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