using ElementsAwoken.Content.Items.Banners;
using ElementsAwoken.Content.Items.Banners.Elementals;
using ElementsAwoken.Content.Items.Banners.Mortemite;
using ElementsAwoken.Content.NPCs.Elementals;
using ElementsAwoken.Content.NPCs.GiantVampireBat;
using ElementsAwoken.Content.NPCs.InfernoSpirit;
using ElementsAwoken.Content.NPCs.ItemSets.Drakonite.Lesser;
using ElementsAwoken.Content.NPCs.ItemSets.Floral;
using ElementsAwoken.Content.NPCs.ItemSets.Mortemite;
using ElementsAwoken.Content.NPCs.ItemSets.Puff;
using ElementsAwoken.Content.NPCs.ItemSets.Stellarium;
using ElementsAwoken.Content.NPCs.VampireBat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Content.Tiles.Banners
{
    public class MonsterBanner : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            DustType = -1;
            EAU.DSCursor(Type);
            AddMapEntry(new Color(13, 88, 130), Language.GetText("MapObject.Banner"));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int style = frameX / 18;
            int item;
            switch (style)
            {
                case 0:
                    item = ModContent.ItemType<GiantTickBanner>();
                    break;
                case 1:
                    item = ModContent.ItemType<SkyCrawlerBanner>();
                    break;
                case 2:
                    item = ModContent.ItemType<MortemWalkerBanner>();
                    break;
                case 3:
                    item = ModContent.ItemType<DragonBatBanner>();
                    break;
                case 4:
                    item = ModContent.ItemType<DragonSlimeBanner>();
                    break;
                case 5:
                    item = ModContent.ItemType<DragonWarriorBanner>();
                    break;
                case 6:
                    item = ModContent.ItemType<DrakoniteElementalBanner>();
                    break;
                //case 7:
                //    item = ModContent.ItemType<ChargetteBanner>();
                //    break;
                //case 8:
                //    item = ModContent.ItemType<ElectrodeBanner>();
                //    break;
                case 9:
                    item = ModContent.ItemType<FlyingJawBanner>();
                    break;
                case 10:
                    item = ModContent.ItemType<PetalClasperBanner>();
                    break;
                //case 11:
                //    item = ModContent.ItemType<StrangeBulbBanner>();
                //    break;
                case 12:
                    item = ModContent.ItemType<DesertElementalBanner>();
                    break;
                case 13:
                    item = ModContent.ItemType<FireElementalBanner>();
                    break;
                case 14:
                    item = ModContent.ItemType<SkyElementalBanner>();
                    break;
                case 15:
                    item = ModContent.ItemType<FrostElementalBanner>();
                    break;
                case 16:
                    item = ModContent.ItemType<WaterElementalBanner>();
                    break;
                case 17:
                    item = ModContent.ItemType<VoidElementalBanner>();
                    break;
                case 18:
                    item = ModContent.ItemType<VampireBatBanner>();
                    break;
                case 19:
                    item = ModContent.ItemType<GiantVampireBatBanner>();
                    break;
                case 20:
                    item = ModContent.ItemType<InfernoSpiritBanner>();
                    break;
                case 21:
                    item = ModContent.ItemType<GiantTickBanner>();
                    break;
                case 22:
                    item = ModContent.ItemType<SkyCrawlerBanner>();
                    break;
                //case 23:
                //    item = ModContent.ItemType<PuffBanner>();
                //    break;
                //case 24:
                //    item = ModContent.ItemType<SpikedPuffBanner>();
                //    break;
                case 25:
                    item = ModContent.ItemType<StellarBatBanner>();
                    break;
                case 26:
                    item = ModContent.ItemType<StellarEntityBanner>();
                    break;
                default:
                    return;
            }
            Item.NewItem(Main.LocalPlayer.GetSource_FromThis(), i * 16, j * 16, 16, 48, item);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Player player = Main.LocalPlayer;
                int style = Main.tile[i, j].TileFrameX / 18;
                int type;
                switch (style)
                {
                    case 0:
                        type = ModContent.NPCType<GiantTick>();
                        break;
                    case 1:
                        type = ModContent.NPCType<SkyCrawler>();
                        break;
                    case 2:
                        type = ModContent.NPCType<MortemWalker>();
                        break;
                    case 3:
                        type = ModContent.NPCType<DragonBat>();
                        break;
                    case 4:
                        type = ModContent.NPCType<DragonSlime>();
                        break;
                    case 5:
                        type = ModContent.NPCType<DragonWarrior>();
                        break;
                    case 6:
                        type = ModContent.NPCType<DrakoniteElemental>();
                        break;
                    //case 7:
                    //    type = ModContent.NPCType<Chargette>();
                    //    break;
                    //case 8:
                    //    type = ModContent.NPCType<Electrode>();
                    //    break;
                    case 9:
                        type = ModContent.NPCType<FlyingJaw>();
                        break;
                    case 10:
                        type = ModContent.NPCType<PetalClasper>();
                        break;
                    //case 11:
                    //    type = ModContent.NPCType<StrangeBulb>();
                    //    break;
                    case 12:
                        type = ModContent.NPCType<DesertElemental>();
                        break;
                    case 13:
                        type = ModContent.NPCType<FireElemental>();
                        break;
                    case 14:
                        type = ModContent.NPCType<SkyElemental>();
                        break;
                    case 15:
                        type = ModContent.NPCType<FrostElemental>();
                        break;
                    case 16:
                        type = ModContent.NPCType<WaterElemental>();
                        break;
                    case 17:
                        type = ModContent.NPCType<VoidElemental>();
                        break;
                    case 18:
                        type = ModContent.NPCType<VampireBat>();
                        break;
                    case 19:
                        type = ModContent.NPCType<GiantVampireBat>();
                        break;
                    case 20:
                        type = ModContent.NPCType<InfernoSpirit>();
                        break;
                    case 21:
                        type = ModContent.NPCType<GiantTick>();
                        break;
                    case 22:
                        type = ModContent.NPCType<SkyCrawler>();
                        break;
                    //case 23:
                    //    type = ModContent.NPCType<Puff>();
                    //    break;
                    case 24:
                        type = ModContent.NPCType<SpikedPuff>();
                        break;
                    case 25:
                        type = ModContent.NPCType<StellarBat>();
                        break;
                    case 26:
                        type = ModContent.NPCType<StellarEntity>();
                        break;
                    default:
                        return;
                }
                Main.SceneMetrics.NPCBannerBuff[type] = true;
                Main.SceneMetrics.hasBanner = true;
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
    }
}