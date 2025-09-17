using ElementsAwoken.Content.Items.Tech.Accessories.Tier4;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Accessories.Tier7
{
    public class HyperDrive : ModItem
    {
        public bool hasShot = false;
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 10;    
            Item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ItemType<BoostDrive>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey;
            var list = ElementsAwoken.specialAbility.GetAssignedKeys();
            if (list.Count > 0) hotkey = list[0];
            else hotkey = GetInstance<EALocalization>().ToySlimeClaw;

            string baseTooltip = string.Format(GetInstance<EALocalization>().HyperDrive, hotkey);
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
            modPlayer.boostDrive = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemType<BoostDrive>(), 1);
            recipe.AddIngredient(ItemType<HeatSink>(), 3);
            recipe.AddIngredient(ItemType<Microcontroller>(), 1);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}