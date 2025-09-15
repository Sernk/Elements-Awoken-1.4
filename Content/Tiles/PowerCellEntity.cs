using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Tiles
{
    public class PowerCellEntity : ModTileEntity
    {
        public int energy = 0;
        public int maxEnergy = 300;
        public override void Update()
        {
            Player player = Main.LocalPlayer;
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();

            Vector2 tileCenter = new Vector2(Position.X * 16, Position.Y * 16);
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }

            Rectangle mouse = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 2, 2);
            Rectangle tileRect = new Rectangle(Position.X * 16, Position.Y * 16, 5 * 16, 5 * 16);
            if (Main.mouseRight && mouse.Intersects(tileRect))
            {
                if (modPlayer.energy > 0 && energy < maxEnergy)
                {
                    energy++;
                    modPlayer.energy--;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag["energy"] = energy;
        }
        public override void LoadData(TagCompound tag)
        {
            energy = tag.GetInt("energy");
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(energy);
        }
        public override void NetReceive(BinaryReader reader)
        {
            energy = reader.ReadInt32();
        }
        public override bool IsTileValidForEntity(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.HasTile && tile.TileType == TileType<PowerCell>() && tile.TileFrameX == 0 && tile.TileFrameY == 0;
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            //Main.NewText("i " + i + " j " + j + " t " + type + " s " + style + " d " + direction);
            if (Main.netMode == 1)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j + 1, 2);
                NetMessage.SendData(87, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                return -1;
            }
            return Place(i, j);
        }
    }
}