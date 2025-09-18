using Terraria;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Global
{
    public class GlobalTiles : GlobalTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSpelunker[TileID.LunarOre] = true;
            TileID.Sets.Ore[TileID.LunarOre] = true;
            Main.tileOreFinderPriority[TileID.LunarOre] = 1000;
            
            if (!Main.dedServ)
            {
                Lang._mapLegendCache[MapHelper.TileToLookup(TileID.LunarOre, 0)] = Lang.GetItemName(ItemID.LunarOre);
            }
        }
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            Player player = Main.LocalPlayer;
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (player.position.Y > Main.maxTilesY * .25f * 16)
            {
                modPlayer.mineTileCooldown = modPlayer.mineTileCooldownMax;
            }
            return base.CanKillTile(i, j, type, ref blockDamaged);
        }
        public static int GetTileMinPick(int type)
        {
            if (type == 37) return 50;
            else if ((type == 22 || type == 204)) return 55;
            else if (type == 25  || type == 203 || type == 117 || type == 404 || type == 56 || type == 58 || Main.tileDungeon[type]) return 65;
            else if (type == 107 || type == 221) return 100;
            else if (type == 108 || type == 222) return 110;
            else if (type == 111 || type == 223) return 150;
            else if (type == 211) return 200;
            else if (type == 226 || type == 237 || type == 408) return 210;
            else
            {
                ModTile modTile = TileLoader.GetTile(type);
                if (modTile != null) return modTile.MinPick;
            }
            return 0;
        }
    }
}