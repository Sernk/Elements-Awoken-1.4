using ElementsAwoken.Content.Items.Placeable.Darkstone;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles.Darkstone
{
    public class DarkstoneDoorOpen : DorOpenClass
	{
        public override int ColorR => 51;
        public override int ColorG => 51;
        public override int ColorB => 51;
        public override int DorCloseType => ModContent.TileType<DarkstoneDoorClosed>();
        public override int Icon => ModContent.ItemType<DarkstoneDoor>();
        public override bool CanDrop(int i, int j) => true;
        public override IEnumerable<Item> GetItemDrops(int i, int j) { yield return new Item(ModContent.ItemType<DarkstoneDoor>()); }
    }
}