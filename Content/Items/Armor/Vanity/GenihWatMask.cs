using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class GenihWatMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.rare = 9;
            Item.vanity = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
    }
}