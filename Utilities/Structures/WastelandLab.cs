using Terraria;
using Terraria.ID;

namespace ElementsAwoken.Utilities.Structures
{
    public class WastelandLab
    {
        private static readonly int[,] StructureArray = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,6,6,6,6,6,6,6,6,6,6,6,3,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,6,8,8,8,8,8,8,8,8,8,6,3,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,6,8,7,7,7,8,7,7,7,8,6,3,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,6,8,7,7,7,8,7,7,7,8,6,3,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,6,8,6,6,6,8,6,6,6,8,6,3,0,0},
            {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,9,9,9,9,9,2,3,3,3},
            {3,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,3},
            {3,6,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,6,3},
            {3,6,8,7,7,7,8,7,7,7,8,7,7,7,8,7,7,7,8,7,7,7,8,7,7,7,8,6,3},
            {9,6,8,7,7,7,8,7,7,7,8,7,7,7,8,7,7,7,8,7,7,7,8,7,7,7,8,6,9},
            {9,6,8,6,6,6,8,6,6,6,8,6,6,6,8,6,6,6,8,6,6,6,8,6,6,6,8,6,9},
            {9,6,8,6,6,6,8,6,6,6,8,6,6,6,8,6,6,6,8,6,6,6,8,1,6,6,8,6,9},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        };

        public static void StructureGen(int xPosO, int yPosO, bool mirrored)
        {
            for (int i = 0; i < StructureArray.GetLength(1); i++)
            {
                for (int j = 0; j < StructureArray.GetLength(0); j++)
                {
                    if(mirrored)
                    {
                        if (TileCheckSafe((int)(xPosO + StructureArray.GetLength(1) - i), (int)(yPosO + j)))
                        {
                            if (StructureArray[j, i] == 1) // Locker
                            {
                                MyWorld.PlaceLabLocker(xPosO + StructureArray.GetLength(1) - i -1, yPosO + j, 177);
                                WorldGen.KillWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j, WallID.PlatinumBrick, true);
                            }
                            if (StructureArray[j, i] == 2) // Platinum Brick
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j, TileID.PlatinumBrick, true, true);
                            }
                            if (StructureArray[j, i] == 3) // Marble Block
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j, TileID.MarbleBlock, true, true, -1, 13);
                            }
                            if (StructureArray[j, i] == 4) // platform
                            {
                            }
                            if (StructureArray[j, i] == 5) // Drive
                            {
                            }
                            if (StructureArray[j, i] == 6) // Platinum Brick Wall
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                Main.tile[xPosO + StructureArray.GetLength(1) - i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j, WallID.PlatinumBrick, true);
                            }
                            if (StructureArray[j, i] == 7) // martian wall
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                Main.tile[xPosO + StructureArray.GetLength(1) - i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j, WallID.MartianConduit, true);
                            }
                            if (StructureArray[j, i] == 8) // luminite wall
                            {
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                Main.tile[xPosO + StructureArray.GetLength(1) - i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + StructureArray.GetLength(1) - i, yPosO + j, WallID.LunarBrickWall, true);
                            }
                            if (StructureArray[j, i] == 9) // destroy everything >:)
                            {
                                Main.tile[xPosO + StructureArray.GetLength(1) - i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillTile(xPosO + StructureArray.GetLength(1) - i, yPosO + j);
                            }
                        }
                    }
                    else
                    {
                        if (TileCheckSafe((int)(xPosO + i), (int)(yPosO + j)))
                        {
                            if (StructureArray[j, i] == 1)
                            {
                                MyWorld.PlaceLabLocker(xPosO + i, yPosO + j, 177);

                                WorldGen.KillWall(xPosO + i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + i, yPosO + j, WallID.PlatinumBrick, true);
                            }
                            if (StructureArray[j, i] == 2)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, TileID.PlatinumBrick, true, true);
                            }
                            if (StructureArray[j, i] == 3)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, TileID.MarbleBlock, true, true);
                            }
                            if (StructureArray[j, i] == 4)
                            {
                            }
                            if (StructureArray[j, i] == 5)
                            {
                            }
                            if (StructureArray[j, i] == 6)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                Main.tile[xPosO + i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillWall(xPosO + i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + i, yPosO + j, WallID.PlatinumBrick, true);
                            }
                            if (StructureArray[j, i] == 7)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                Main.tile[xPosO + i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillWall(xPosO + i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + i, yPosO + j, WallID.MartianConduit, true);
                            }
                            if (StructureArray[j, i] == 8)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                Main.tile[xPosO + i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillWall(xPosO + i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + i, yPosO + j, WallID.LunarBrickWall, true);
                            }
                            if (StructureArray[j, i] == 9)
                            {
                                Main.tile[xPosO + i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                            }
                        }
                    }
                }
            }
        }
        private static bool TileCheckSafe(int i, int j)
        {
            if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY)
                return true;
            return false;
        }
    }
}