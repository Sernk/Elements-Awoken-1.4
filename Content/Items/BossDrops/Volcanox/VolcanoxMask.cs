using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Volcanox
{
    [AutoloadEquip(EquipType.Head)]
    public class VolcanoxMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.rare = 1;
            Item.vanity = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
    }
}