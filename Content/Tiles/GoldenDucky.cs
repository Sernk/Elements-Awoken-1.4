using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles
{
    public class GoldenDucky : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileTable[Type] = true;
            Main.tileLighted[Type] = false;
            EAU.DSCursor(Type);
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            AddMapEntry(new Color(244, 237, 39));
            TileObjectData.addTile(Type);
        }
    }
}