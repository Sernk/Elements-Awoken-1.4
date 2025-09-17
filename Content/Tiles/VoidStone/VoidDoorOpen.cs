using ElementsAwoken.Content.Items.Placeable.VoidStone;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles.VoidStone
{
    public class VoidDoorOpen : DoorOpenClass
	{
        public override int ColorR => 51;
        public override int ColorG => 51;
        public override int ColorB => 51;
        public override int DorCloseType => ModContent.TileType<VoidDoorClosed>();
        public override int Icon => ModContent.ItemType<VoidDoor>();
        public override int DropItem => ModContent.ItemType<VoidDoor>();
	}
}