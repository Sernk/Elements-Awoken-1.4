using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.HiveCrate
{
    public class HiveCrate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.createTile = TileType<Tiles.HiveCratePlaced>();
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            var source = player.GetSource_OpenItem(Type);
            if (Main.rand.Next(3) == 0)
            {
                int item = Main.rand.Next(2);
                if (NPC.downedQueenBee) item = Main.rand.Next(3);
                switch (item)
                {
                    case 0:
                        item = ItemType<Honeysuckle>();
                        break;
                    case 1:
                        item = ItemType<Honeycrest>();
                        break;
                    case 2:
                        item = ItemType<HoneyCocoon>();
                        break;
                    default:
                        item = ItemType<HoneyCocoon>();
                        break;
                }
                player.QuickSpawnItem(source, item);
            }
            player.OpenFishingCrate(4000);
        }
    }
}