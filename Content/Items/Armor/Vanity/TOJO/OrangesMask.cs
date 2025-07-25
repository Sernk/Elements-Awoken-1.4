using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Armor.Vanity.TOJO
{
    [AutoloadEquip(EquipType.Head)]
    public class OrangesMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 36;
            Item.rare = 9;
            Item.vanity = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ThatOneJuicyOrange's Mask");
            // Tooltip.SetDefault("So juicy");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
    }
}
