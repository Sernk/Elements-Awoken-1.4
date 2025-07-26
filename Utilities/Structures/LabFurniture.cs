using ElementsAwoken.Content.Tiles.Lab;
using ElementsAwoken.Content.Tiles.Lab.Drives;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Utilities.Structures
{
    public class LabFurniture
    {
        private static readonly int[,] StructureArray = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,2,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };

        public static void StructureGen(int xPosO, int yPosO, bool mirrored, int driveNo)
        {
            int driveType = ModContent.TileType<WastelandDrive>();
            switch (driveNo)
            {
                case 0:
                    driveType = ModContent.TileType<InfernaceDrive>();
                    break;
                case 1:
                    driveType = ModContent.TileType<ScourgeFighterDrive>();
                    break;
                case 2:
                    driveType = ModContent.TileType<RegarothDrive>();
                    break;
                case 3:
                    driveType = ModContent.TileType<CelestialDrive>();
                    break;
                case 4:
                    driveType = ModContent.TileType<ObsidiousDrive>();
                    break;
                case 5:
                    driveType = ModContent.TileType<PermafrostDrive>();
                    break;
                case 6:
                    driveType = ModContent.TileType<AqueousDrive>();
                    break;
                case 7:
                    driveType = ModContent.TileType<GuardianDrive>();
                    break;
                case 8:
                    driveType = ModContent.TileType<VolcanoxDrive>();
                    break;
                case 9:
                    driveType = ModContent.TileType<VoidLeviathanDrive>();
                    break;
                case 10:
                    driveType = ModContent.TileType<AzanaDrive>();
                    break;
            }

            for (int i = 0; i < StructureArray.GetLength(1); i++)
            {
                for (int j = 0; j < StructureArray.GetLength(0); j++)
                {
                    if(mirrored)
                    {
                        if (TileCheckSafe((int)(xPosO + StructureArray.GetLength(1) - i), (int)(yPosO + j)))
                        {
                            if (StructureArray[j, i] == 1) // Desk
                            {
                                // origin in bottom middle
                                WorldGen.Place3x2(xPosO + StructureArray.GetLength(1) - i, yPosO + j, (ushort)ModContent.TileType<Desk>(), 1);
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i + 1, yPosO + j - 2, (ushort)ModContent.TileType<Computer>(), false, 0, 1, -1, 0);
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i - 1, yPosO + j - 2, driveType, false, 0);
                            }
                            if (StructureArray[j, i] == 2) // Chair
                            {
                                // origin in bottom right
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i + 1, yPosO + j, (ushort)ModContent.TileType<OfficeChair>(), false, 0, 1, -1, 1);
                            }
                            if (StructureArray[j, i] == 3) // Lab Light
                            {
                                WorldGen.PlaceObject(xPosO + StructureArray.GetLength(1) - i, yPosO + j, ModContent.TileType<LabLight>(), false, 0);
                            }
                            if (StructureArray[j, i] == 4) // Door
                            {
                                WorldGen.PlaceDoor(xPosO + StructureArray.GetLength(1) - i, yPosO + j, 10, 20);
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
                            if (StructureArray[j, i] == 1) // Desk
                            {
                                WorldGen.Place3x2(xPosO + i, yPosO + j, (ushort)ModContent.TileType<Desk>(), 0);
                                WorldGen.PlaceObject(xPosO + i, yPosO + j - 2, (ushort)ModContent.TileType<Computer>(), false , 0, 1, -1, 1);
                                WorldGen.PlaceObject(xPosO + i + 1, yPosO + j - 2, driveType, false, 0);
                            }
                            if (StructureArray[j, i] == 2) // Chair
                            {
                                WorldGen.PlaceObject(xPosO + i, yPosO + j, (ushort)ModContent.TileType<OfficeChair>(), false, 0, 1, -1, 0);
                            }
                            if (StructureArray[j, i] == 3) // Lab Light
                            {
                                WorldGen.PlaceObject(xPosO + i, yPosO + j, ModContent.TileType<LabLight>(), false, 0);
                            }
                            if (StructureArray[j, i] == 4) // Door
                            {
                                WorldGen.PlaceDoor(xPosO + i, yPosO + j, 10, 20);
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
        
        //Making sure tiles arent out of bounds
        private static bool TileCheckSafe(int i, int j)
        {
            if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY)
                return true;
            return false;
        }
    }
}