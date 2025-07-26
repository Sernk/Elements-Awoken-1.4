using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Tiles.Lab
{
    public class Computer : ModTile
    {
        public int entryNo = 0; // no drive
        public int noDetectTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleWrapLimit = 2; //not really necessary but allows me to add more subtypes of chairs below the example chair texture
            TileObjectData.newTile.StyleMultiplier = 2; //same as above
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight; //allows me to place example chairs facing the same way as the player
            TileObjectData.addAlternate(1); //facing right will use the second texture style
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(98, 214, 177), CreateMapEntryName());
            TileID.Sets.DisableSmartCursor[Type] = true;
            //adjTiles = new int[] { TileID.Chairs };

        }
        public override bool RightClick(int i, int j)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            #region drive detection
            float distance = 3 * 16;
            Point topLeft = ((new Vector2(i * 16, j * 16) - new Vector2(distance, distance)) / 16).ToPoint();
            Point bottomRight = ((new Vector2(i * 16 - 16, j * 16 - 16) + new Vector2(distance, distance)) / 16).ToPoint();

            // draws dust where the detection zone is 
            /*for (int d = 0; d < 3; d++)
            {
                int dust = Dust.NewDust(new Vector2(topLeft.X * 16, topLeft.Y * 16), (bottomRight.X - topLeft.X) * 16, (bottomRight.Y - topLeft.Y) * 16, 57, 0f, 0f, 100);
                Main.dust[dust].velocity *= 0.01f;
            }*/
            // needs to check all of the tiles 
            for (int k = topLeft.X; k <= bottomRight.X; k++)
            {
                for (int l = topLeft.Y; l <= bottomRight.Y; l++)
                {
                    Tile t = Framing.GetTileSafely(k, l);
                    if (t.TileType == TileType<Drives.WastelandDrive>())
                    {
                        entryNo = 1;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.InfernaceDrive>())
                    {
                        entryNo = 2;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.ScourgeFighterDrive>())
                    {
                        entryNo = 3;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.RegarothDrive>())
                    {
                        entryNo = 4;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.CelestialDrive>())
                    {
                        entryNo = 5;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.ObsidiousDrive>())
                    {
                        entryNo = 6;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.PermafrostDrive>())
                    {
                        entryNo = 7;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.AqueousDrive>())
                    {
                        entryNo = 8;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.GuardianDrive>())
                    {
                        entryNo = 9;
                        modPlayer.guardianEntryNo++;
                        if (modPlayer.guardianEntryNo > 1)
                        {
                            modPlayer.guardianEntryNo = 0;
                        }
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.VolcanoxDrive>())
                    {
                        entryNo = 10;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.VoidLeviathanDrive>())
                    {
                        entryNo = 11;
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.AzanaDrive>())
                    {
                        entryNo = 12;
                        modPlayer.azanaEntryNo++;
                        if (modPlayer.azanaEntryNo > 1)
                        {
                            modPlayer.azanaEntryNo = 0;
                        }
                        noDetectTimer = 20;
                    }
                    else if (t.TileType == TileType<Drives.AncientsDrive>())
                    {
                        entryNo = 13;
                        modPlayer.ancientsEntryNo++;
                        if (modPlayer.ancientsEntryNo > 1)
                        {
                            modPlayer.ancientsEntryNo = 0;
                        }
                        noDetectTimer = 20;
                    }
                    else
                    {
                        if (noDetectTimer <= 0)
                        {
                            entryNo = 0;
                        }
                    }
                }
            }
            #endregion
            Main.playerInventory = false;
            //player.talkNPC = -1;
            player.sign = -1;
            modPlayer.inComputer = true;
            modPlayer.computerPos = new Vector2(i, j);
            modPlayer.computerTextNo = entryNo;

            return true;
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ItemType<Items.Placeable.Computer>();
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            noDetectTimer--;
        }
    }
}
