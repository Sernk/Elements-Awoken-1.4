using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Tiles
{
    public class AutoDriller : ModTile
    {
        public Point16 tilePoint = new Point16();
        public AutoDrillerEntity myEntity = null;
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            EAU.DSCursor(Type);
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(GetInstance<AutoDrillerEntity>().Hook_AfterPlacement, -1, 0, true);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.Origin = new Point16(2, 3);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            AddMapEntry(new Color(154, 214, 213));
            AnimationFrameHeight = 90;
            TileObjectData.addTile(Type);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            tilePoint = new Point16(i - 4, j - 4); // it finds the bottom right while we want top left
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (myEntity == null)
            {
                if (!Main.gameMenu)
                {
                    foreach (TileEntity current in TileEntity.ByID.Values)
                    {
                        if (current.type == TileEntityType<AutoDrillerEntity>())
                        {
                            if (current.Position == tilePoint)
                            {
                                myEntity = (AutoDrillerEntity)current;
                            }
                        }
                    }
                }
            }
            else
            {

                if (myEntity.enabled)
                {
                    frame = 1;
                }
                else
                {
                    frame = 0;
                }
            }
        }
        public override bool CanPlace(int i, int j)
        {
            Tile anchorLeft = Framing.GetTileSafely(i - 2, j + 2);
            Tile anchorRight = Framing.GetTileSafely(i + 2, j + 2);

            if (Main.tileSolid[anchorLeft.TileType] && anchorLeft.HasTile && Main.tileSolid[anchorRight.TileType] && anchorRight.HasTile)
            {
                return base.CanPlace(i, j);
            }
            return false;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            GetInstance<AutoDrillerEntity>().Kill(i, j);
        }        
    }
}