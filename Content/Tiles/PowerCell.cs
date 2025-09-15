using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Tiles
{
    public class PowerCell : ModTile
    {
        public Point16 tilePoint = new Point16();
        public PowerCellEntity myEntity = null;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            EAU.DSCursor(Type);
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(GetInstance<PowerCellEntity>().Hook_AfterPlacement, -1, 0, true);
            AddMapEntry(new Color(154, 214, 213));
            AnimationFrameHeight = 90;
            TileObjectData.addTile(Type);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            tilePoint = new Point16(i, j - 1); // it finds the bottom right while we want top left
            foreach (TileEntity current in TileEntity.ByID.Values)
            {
                if (current.type == TileEntityType<PowerCellEntity>())
                {
                    if (current.Position == tilePoint)
                    {
                        myEntity = (PowerCellEntity)current;
                    }
                }
            }
            if (myEntity != null)
            {
                Tile tile = Main.tile[i, j];
                if (tile.TileFrameY == 0)
                {
                    Texture2D barTexture = ModContent.Request<Texture2D>("ElementsAwoken/Content/Tiles/PowerCellOverlay").Value;
                    Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                    if (Main.drawToScreen)
                    {
                        zero = Vector2.Zero;
                    }
                    int barHeight = (int)(barTexture.Height * ((float)myEntity.energy / (float)myEntity.maxEnergy));
                    Rectangle barDest = new Rectangle(i * 16 - (int)Main.screenPosition.X + (int)zero.X, j * 16 - (int)Main.screenPosition.Y + (int)zero.Y + 6 + barTexture.Height - barHeight, barTexture.Width, barHeight);
                    Rectangle barLength = new Rectangle(0, 0, barTexture.Width, barHeight);
                    spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            if (myEntity != null)
            {
                player.cursorItemIconText = "Energy: " + myEntity.energy;
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
            }
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            GetInstance<PowerCellEntity>().Kill(i, j);
        }        
    }
}