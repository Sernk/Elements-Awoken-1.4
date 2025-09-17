using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.VoidStone
{
    public class VoidClock : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(51, 51, 51), CreateMapEntryName());
            AdjTiles = new int[] { TileID.GrandfatherClocks };
            TileObjectData.newTile.DrawYOffset = 2;
        }
        public override bool RightClick(int x, int y)
        {
            {
                string text = "AM";
                double time = Main.time;
                if (!Main.dayTime)
                {
                    time += 54000.0;
                }
                time = time / 86400.0 * 24.0;
                time = time - 7.5 - 12.0;
                if (time < 0.0)
                {
                    time += 24.0;
                }
                if (time >= 12.0)
                {
                    text = "PM";
                }
                int intTime = (int)time;
                double deltaTime = time - intTime;
                deltaTime = ((int)(deltaTime * 60.0));
                string text2 = string.Concat(deltaTime);
                if (deltaTime < 10.0)
                {
                    text2 = "0" + text2;
                }
                if (intTime > 12)
                {
                    intTime -= 12;
                }
                if (intTime == 0)
                {
                    intTime = 12;
                }
                var newText = string.Concat("Time: ", intTime, ":", text2, " ", text);
                Main.NewText(newText, 255, 240, 20);
            }
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.SceneMetrics.HasClock = true;
            }
        }
    }
}