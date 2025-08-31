using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Global
{
    public class ItemEnergy : GlobalItem
    {
        public int energy = 0;
        public ItemEnergy()
        {
            energy = 0;
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            ItemEnergy myClone = (ItemEnergy)base.Clone(item, itemClone);
            myClone.energy = energy;
            return myClone;
        }
        public override bool CanUseItem(Item item, Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (modPlayer.energy < energy)
            {
                return false;
            }
            return base.CanUseItem(item, player);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (energy > 0)
            {
                modPlayer.energy -= energy;
            }
            return base.Shoot(item, player, source, position, speed, type, damage, knockback);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            ItemEnergy modItem = item.GetGlobalItem<ItemEnergy>();
            var EALocalization = ModContent.GetInstance<EALocalization>();
            if (modItem.energy > 0)
            {
                TooltipLine tip = new TooltipLine(Mod, "Elements Awoken:Energy", EALocalization.ItemEnergy + " " + energy + " " + EALocalization.ItemEnergy1);
                tooltips.Add(tip);
            }
        }
    }
}