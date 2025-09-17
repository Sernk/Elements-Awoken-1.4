using ElementsAwoken.Content.Items.Placeable.Darkstone;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Tiles.Darkstone
{
    public class DarkstoneDoorClosed : DoorCloseClass
    {
        public override int ColorR => 51;
        public override int ColorG => 51;
        public override int ColorB => 51;
        public override int DorOpenType => ModContent.TileType<DarkstoneDoorOpen>();
        public override int Icon => ModContent.ItemType<DarkstoneDoor>();
    }
}