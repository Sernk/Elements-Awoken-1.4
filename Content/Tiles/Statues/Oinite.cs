using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Statues
{
    public class Oinite : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 2;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(144, 148, 144));
            DustType = 11;
            EAU.DSCursor(Type);
        }
    }
}