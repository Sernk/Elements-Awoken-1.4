using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops
{
    public abstract class RelicItem : ModItem
    {
        public abstract int RelicType { get; }
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.rare = ItemRarityID.Master;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.maxStack = 9999;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.createTile = RelicType;
            Item.master = true;
            Item.value = Terraria.Item.buyPrice(0, 5);
        }
    }
}