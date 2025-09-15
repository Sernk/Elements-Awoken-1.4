using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles
{
    public abstract class BedClass : ModTile
    {
        public abstract int ColorR { get; }
        public abstract int ColorG { get; }
        public abstract int ColorB { get; }
        public abstract int Icon { get; }

        public const int NextStyleHeight = 38;
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.IsValidSpawnPoint[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.CoordinateHeights = [16, 16];
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(ColorR, ColorG, ColorB), CreateMapEntryName());
            AdjTiles = [TileID.Beds];
            TileID.Sets.CanBeSleptIn[Type] = true;
        }
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }
        public override void ModifySmartInteractCoords(ref int width, ref int height, ref int frameWidth, ref int frameHeight, ref int extraY)
        {
            width = 2;
            height = 2;
        }
        public override void ModifySleepingTargetInfo(int i, int j, ref TileRestingInfo info)
        {
            info.VisualOffset.Y += 4f;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 1;
        }
        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            int spawnX = (i - (tile.TileFrameX / 18)) + (tile.TileFrameX >= 72 ? 5 : 2);
            int spawnY = j + 2;

            if (tile.TileFrameY % NextStyleHeight != 0)
            {
                spawnY--;
            }

            if (!Player.IsHoveringOverABottomSideOfABed(i, j))
            {
                if (player.IsWithinSnappngRangeToTile(i, j, PlayerSleepingHelper.BedSleepingMaxDistance))
                {
                    player.GamepadEnableGrappleCooldown();
                    player.sleeping.StartSleeping(player, i, j);
                }
            }
            else
            {
                player.FindSpawn();

                if (player.SpawnX == spawnX && player.SpawnY == spawnY)
                {
                    player.RemoveSpawn();
                    Main.NewText(Language.GetTextValue("Game.SpawnPointRemoved"), byte.MaxValue, 240, 20);
                }
                else if (Player.CheckSpawn(spawnX, spawnY))
                {
                    player.ChangeSpawn(spawnX, spawnY);
                    Main.NewText(Language.GetTextValue("Game.SpawnPointSet"), byte.MaxValue, 240, 20);
                }
            }
            return true;
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;

            if (!Player.IsHoveringOverABottomSideOfABed(i, j))
            {
                if (player.IsWithinSnappngRangeToTile(i, j, PlayerSleepingHelper.BedSleepingMaxDistance))
                {
                    player.noThrow = 2;
                    player.cursorItemIconEnabled = true;
                    player.cursorItemIconID = Icon;
                }
            }
            else
            {
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = Icon;
            }
        }
    }
}