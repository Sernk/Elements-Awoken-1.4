using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Crafting
{
    public class ElementalForge : ModTile
    {
        public override void SetStaticDefaults()
        {
			Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(133, 133, 133), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);
			AdjTiles = new int[] { TileID.Furnaces };
			AnimationFrameHeight = 38;
			DustType = 6;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 6)
			{
				frameCounter = 0;
				frame++;
				if (frame > 5)
				{
					frame = 0;
				}
			}
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = (Main.DiscoR / 255f) / 2;
            g = (Main.DiscoG / 255f) / 2;
            b = (Main.DiscoB / 255f) / 2; 
        }
    }
}