using ElementsAwoken.Content.Items.Placeable.VoidStone;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles.VoidStone
{
    public class VoidDoorClosed : DoorCloseClass
    {
        public override int ColorR => 51;
        public override int ColorG => 51;
        public override int ColorB => 51;
        public override int DorOpenType => ModContent.TileType<VoidDoorOpen>();
        public override int Icon => ModContent.ItemType<VoidDoor>();
    }
}