﻿using ElementsAwoken.Content.Items.ItemSets.Putrid;
using ElementsAwoken.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Tiles.Crafting
{
    public class Putrifier : ModTile
    {
        public int soundCD = 0;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            AddMapEntry(new Color(39, 78, 96), CreateMapEntryName());
            EAU.DSCursor(Type);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(GetInstance<PutrifierEntity>().Hook_AfterPlacement, -1, 0, true);
            TileObjectData.addTile(Type);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            GetInstance<PutrifierEntity>().Kill(i, j);
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.netMode != 0)
            {
                Item iron = null;
                Item fragment = null;
                for (int k = 0; k < Main.maxItems; k++)
                {
                    Item item = Main.item[k];
                    if (Vector2.Distance(item.Center, new Vector2(i* 16, j * 16)) <= 50 && item.active)
                    {
                        if (item.type == ItemType<SunFragment>())
                        {
                            fragment = item;
                            ChewDust(item);
                        }
                        if (item.type == ItemID.IronOre || item.type == ItemID.LeadOre)
                        {
                            iron = item;
                            ChewDust(item);
                        }
                    }
                }
                soundCD--;
                if (iron != null && fragment != null)
                {
                    if (iron.active && fragment.active)
                    {
                        if (iron.stack > 0 && fragment.stack > 0)
                        {
                            if (iron.stack > 1)
                            {
                                iron.stack--;
                                if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, iron.whoAmI, 1f);
                            }
                            else
                            {
                                iron.active = false;
                                if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, iron.whoAmI, 1f);
                            }
                            if (fragment.stack > 1)
                            {
                                fragment.stack--;
                                if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, fragment.whoAmI, 1f);
                            }
                            else
                            {
                                fragment.active = false;
                                if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, fragment.whoAmI, 1f);
                            }
                            int ore = Item.NewItem(Main.LocalPlayer.GetSource_FromThis(), i* 16, j * 16, 32, 16, ItemType<PutridOre>());
                            if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendData(MessageID.SyncItem, -1, -1, null, ore, 1f);
                            if (soundCD <= 0)
                            {
                                SoundEngine.PlaySound(SoundID.NPCDeath13, new Vector2(i* 16, j * 16));
                                soundCD = 20;
                            }
                        }
                    }
                }
            }
        }
        private void ChewDust(Item item)
        {
            if (Main.rand.Next(20) == 0)
            {
                int dust = Dust.NewDust(item.Center, item.width, item.height, 2, 0f, 0f, 100, new Color(14, 122, 82));
            }
        }
    }
}