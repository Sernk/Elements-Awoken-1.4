using ElementsAwoken.Content.Buffs.TileBuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
namespace ElementsAwoken.Content.Tiles.Statues
{
    public class AmadisBuff : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 2;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(144, 148, 144));
            DustType = 11;
            EAU.DSCursor(Type);
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.player[Main.myPlayer]; 
            if (closer && player.FindBuffIndex(BuffType<StatueBuffBurst>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffGenihWat>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffRanipla>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffOrange>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffOinite>()) == -1)
            {
                player.AddBuff(BuffType<StatueBuffAmadis>(), 60, true);
            }
        }
    }
}