using ElementsAwoken.Content.Items.Placeable.Darkstone;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles.Darkstone
{
    public class DarkstoneDoorOpen : DoorOpenClass
	{
        public override int ColorR => 51;
        public override int ColorG => 51;
        public override int ColorB => 51;
        public override int DorCloseType => ModContent.TileType<DarkstoneDoorClosed>();
        public override int Icon => ModContent.ItemType<DarkstoneDoor>();
        public override int DropItem => ModContent.ItemType<DarkstoneDoor>();
    }
}