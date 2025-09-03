using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Effects;
using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.Tiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.EASystem
{
    public class AwakenedPlayer : ModPlayer
    {
        public bool openSanityBook = false;

        public int sanity = 30;
        public int sanityMax = 150;
        public int sanityIncreaser = 150;

        public int sanityRegen = 0;
        public int sanityRegenCount = 0;
        public int sanityRegenTime = 0;

        public int craftWeaponCooldown = 0;

        public int sanityArrow = 0;
        public int sanityArrowFrame = 0;

        public int sanityGlitch = 0;
        public int sanityGlitchCooldown = 0;
        public int sanityGlitchFrame = 0;

        public List<int> sanityDrains = new List<int>();
        public List<string> sanityDrainsName = new List<string>();
        public List<int> sanityRegens = new List<int>();
        public List<string> sanityRegensName = new List<string>();

        public int bossIncreaseSanityCD = 0;

        public int mineTileCooldown = 0;
        public int mineTileCooldownMax = 3600 * 3;
        public int miningCounter = 0;

        public int nurseCooldown = 0;

        public int aleCD = 0;
        public override void ResetEffects()
        {
            sanityIncreaser = 150;

            sanityRegen = 0;

            sanityDrains = new List<int>();
            sanityDrainsName = new List<string>();
            sanityRegens = new List<int>();
            sanityRegensName = new List<string>();
        }
        public override void PostUpdateMiscEffects()
        {
            nurseCooldown--;
            if (!MyWorld.awakenedMode)
            {
                sanity = sanityMax;
            }
            PlayerUtils playerUtils = Player.GetModPlayer<PlayerUtils>();
            MyPlayer modPlayer = Player.GetModPlayer<MyPlayer>();

            sanityMax = sanityIncreaser;
            craftWeaponCooldown--;
            aleCD--;
            if (MyWorld.awakenedMode)
            {
                // if (sanity > 0)
                {
                    if (Player.statLife < Player.statLifeMax2 * 0.25f)
                    {
                        int sanityRegenLoss = (int)Math.Round(MathHelper.Lerp(4, 1, Player.statLife / (Player.statLifeMax2 * 0.25f)));
                        sanityRegen -= sanityRegenLoss;

                        AddSanityDrain(sanityRegenLoss, "Low Health");
                    }
                    // in the dark
                    if (playerUtils.playerLight < 0.2)
                    {
                        int sanityRegenLoss = (int)Math.Round(MathHelper.Lerp(3, 1, playerUtils.playerLight / 0.2f));
                        sanityRegen -= sanityRegenLoss;

                        AddSanityDrain(sanityRegenLoss, "Darkness");
                    }
                    if (Main.bloodMoon)
                    {
                        sanityRegen--;
                        AddSanityDrain(1, "Blood Moon");
                    }
                    if (MyWorld.darkMoon)
                    {
                        sanityRegen -= 2;
                        AddSanityDrain(2, "Dark Moon");
                    }
                    if (MyWorld.voidInvasionUp && Main.time >= 16220 && !Main.dayTime)
                    {
                        sanityRegen -= 3;
                        AddSanityDrain(2, "Dawn of the Void");
                    }
                    if (Player.ZoneUnderworldHeight)
                    {
                        sanityRegen -= 2;
                        AddSanityDrain(2, "In Hell");
                    }
                    if (Player.ZoneSkyHeight && !modPlayer.cosmicalusArmor)
                    {
                        sanityRegen -= 1;
                        AddSanityDrain(1, "In Space");
                    }
                    if (miningCounter > 3600 * 10)
                    {
                        if (mineTileCooldown > mineTileCooldownMax - 300)
                        {
                            sanityRegen -= 3;// first 5 seconds after mining a tile reduces sanity
                            AddSanityDrain(3, "Mining For Too Long");
                        }
                    }
                    if (NPC.AnyNPCs(NPCID.MoonLordCore))
                    {
                        Player.AddBuff(ModContent.BuffType<EldritchHorror>(), 2);
                    }
                }
                {
                    for (int i = 0; i < 22; i++)
                    {
                        if (Main.vanityPet[Player.buffType[i]])
                        {
                            sanityRegen++;
                            AddSanityRegen(1, "Pet");
                            break;
                        }
                    }
                    #region flowers and campfires nearby
                    int distance = 15 * 16;
                    Point topLeft = ((Player.position - new Vector2(distance, distance)) / 16).ToPoint();
                    Point bottomRight = ((Player.BottomRight + new Vector2(distance, distance)) / 16).ToPoint();
                    int x = 0;
                    int y = 0;
                    Tile closest = Main.tile[x, y];
                    Vector2 closestPos = new Vector2();
                    Tile closestVoidite = Main.tile[x, y]; ;
                    Vector2 voiditePos = new Vector2();
                    for (int i = topLeft.X; i <= bottomRight.X; i++)
                    {
                        for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                        {
                            Tile t = Framing.GetTileSafely(i, j);
                            if (CheckValidSanityTile(t))
                            {
                                Vector2 tileCenter = new Vector2(i * 16, j * 16);
                                if (closest != null)
                                {
                                    if (Vector2.Distance(tileCenter, Player.Center) < Vector2.Distance(closestPos, Player.Center))
                                    {
                                        closest = t;
                                        closestPos = new Vector2(i * 16, j * 16);
                                    }
                                }
                                else
                                {
                                    closest = t;
                                    closestPos = new Vector2(i * 16, j * 16);
                                }

                            }
                            if (t.TileType == ModContent.TileType<Voidite>())
                            {
                                Vector2 tileCenter = new Vector2(i * 16, j * 16);
                                if (closestVoidite != null)
                                {
                                    if (Vector2.Distance(tileCenter, Player.Center) < Vector2.Distance(voiditePos, Player.Center))
                                    {
                                        closestVoidite = t;
                                        voiditePos = new Vector2(i * 16, j * 16);
                                    }
                                }
                                else
                                {
                                    closestVoidite = t;
                                    voiditePos = new Vector2(i * 16, j * 16);
                                }
                            }
                        }
                    }
                    if (Vector2.Distance(closestPos, Player.Center) < distance && CheckValidSanityTile(closest))
                    {
                        int amount = (int)Math.Round(MathHelper.Lerp(3, 1, Vector2.Distance(closestPos, Player.Center) / distance));
                        sanityRegen += amount;
                        string type = "Nice Object";
                        if (closest.TileType == TileID.Campfire)
                        {
                            type = "Campfire";
                        }
                        if (closest.TileType == TileID.Fireplace)
                        {
                            type = "Fireplace";
                        }
                        if (closest.TileType == TileID.FireflyinaBottle)
                        {
                            type = "Firefly in a Bottle";
                        }
                        if (closest.TileType == TileID.Sunflower)
                        {
                            type = "Sunflower";
                        }
                        if (closest.TileType == TileID.PlanterBox)
                        {
                            type = "Planter Box";
                        }
                        AddSanityRegen(amount, "Nearby " + type);
                    }
                    if (Vector2.Distance(voiditePos, Player.Center) < distance && closestVoidite != null)
                    {
                        int amount = (int)Math.Round(MathHelper.Lerp(5, 1, Vector2.Distance(voiditePos, Player.Center) / distance));
                        sanityRegen -= amount;
                        AddSanityDrain(amount, "Voidite");
                    }
                    #endregion

                    int townSanityRegen = 0;
                    int numNPCs = CountNearbyTownNPCs();
                    if (numNPCs > 5) townSanityRegen++;
                    if (numNPCs > 10) townSanityRegen++;
                    if (numNPCs > 15) townSanityRegen++;
                    if (numNPCs > 20) townSanityRegen++;
                    if (numNPCs > 25) townSanityRegen++;
                    if (townSanityRegen > 0)
                    {
                        sanityRegen += townSanityRegen;
                        AddSanityRegen(townSanityRegen, "In a Town");
                    }

                    if (miningCounter < 3600 * 10)
                    {
                        if (mineTileCooldown > mineTileCooldownMax - 300)
                        {
                            sanityRegen += 3;
                            AddSanityRegen(3, "Mining");
                        }
                    }
                }

                if (sanity < sanityMax * 0.25f && sanity > sanityMax * 0.1f)
                {
                    Player.GetDamage(DamageClass.Generic) *= 0.9f;
                }
                if (sanity < sanityMax * 0.1f)
                {
                    modPlayer.screenshakeAmount = 2f;
                    if (sanity != 0)
                    {
                        Player.GetDamage(DamageClass.Generic) *= 0.75f;
                    }
                    else
                    {
                        Player.GetDamage(DamageClass.Generic) *= 0.5f;
                    }
                    if (Main.rand.Next(1200) == 0)
                    {
                        int choice = Main.rand.Next(2);
                        if (choice == 0)
                        {
                            Player.AddBuff(BuffID.Darkness, 600);
                        }
                        else if (choice == 1)
                        {
                            Player.AddBuff(BuffID.Confused, 120);
                        }
                    }
                }
                if (!MyWorld.credits)
                {
                    sanityRegenCount = Math.Abs(sanityRegen);
                    sanityRegenTime -= sanityRegenCount;
                    if (sanityRegenTime <= 0)
                    {
                        sanityRegenTime = 450;
                        sanity += Math.Sign(sanityRegen);
                    }
                    if (sanity > sanityMax)
                    {
                        sanity = sanityMax;
                    }
                    if (sanity < 0)
                    {
                        sanity = 0;
                    }
                }
                if (mineTileCooldown > 0)
                {
                    mineTileCooldown--;
                    miningCounter++;
                }
                if (mineTileCooldown <= 0)
                {
                    miningCounter = 0;
                }
                InsanityOverlay.Update();

                #region glitch anim
                sanityGlitchCooldown--;
                if (sanityGlitchCooldown <= 0)
                {
                    sanityGlitchCooldown = 120;
                }
                if (sanityGlitchCooldown <= 12)
                {
                    sanityGlitch--;
                    if (sanityGlitch <= 0)
                    {
                        sanityGlitchFrame++;
                        sanityGlitch = 3;
                    }
                }
                else
                {
                    sanityGlitchFrame = 0;
                }
                if (sanityGlitchFrame > 4)
                {
                    sanityGlitchFrame = 1;
                }
                #endregion
                #region arrow anim
                sanityArrow--;
                if (sanityArrow <= 0)
                {
                    sanityArrowFrame++;
                    sanityArrow = 5;
                }
                if (sanityArrowFrame > 12)
                {
                    sanityArrowFrame = 0;
                }
                #endregion
            }
        }
        private bool CheckValidSanityTile(Tile t)
        {
            if (t.TileType == TileID.Campfire || t.TileType == TileID.Fireplace || t.TileType == TileID.FireflyinaBottle || t.TileType == TileID.Sunflower || t.TileType == TileID.PlanterBox)
            {
                return true;
            }
            return false;
        }
        private int CountNearbyTownNPCs()
        {
            int num = 0;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.townNPC && Vector2.Distance(Player.Center, nPC.Center) <= 2000)
                {
                    num++;
                }
            }
            return num;
        }
        public override void OnRespawn()
        {
            sanity = sanityMax / 2;
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (MyWorld.awakenedMode)
            {
                if (target.life <= 0)
                {
                    if (target.damage == 0 && NPCID.Sets.TownCritter[target.type])
                    {
                        ElementsAwoken.DebugModeText("reduced sanity by 3");
                        sanity -= 3;
                    }
                    if (target.townNPC)
                    {
                        ElementsAwoken.DebugModeText("reduced sanity by 15");
                        sanity -= 15;
                    }
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (MyWorld.awakenedMode)
            {
                if (target.life <= 0)
                {
                    if (target.damage == 0 && NPCID.Sets.TownCritter[target.type])
                    {
                        ElementsAwoken.DebugModeText("reduced sanity by 3");
                        sanity -= 3;
                    }
                    if (target.townNPC)
                    {
                        ElementsAwoken.DebugModeText("reduced sanity by 15");
                        sanity -= 15;
                    }
                }
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            int damage =  hurtInfo.Damage = 1;
            if (MyWorld.awakenedMode && damage > 0)
            {
                    sanity -= 1;
            }
        }
        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            int damage = hurtInfo.Damage = 1;
            if (MyWorld.awakenedMode && damage > 0)
            {
                sanity -= 1;
            }
        }
        public override void UpdateDead()
        {
            nurseCooldown = 0;
        }
        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            if (MyWorld.awakenedMode)
            {
                if (nurseCooldown > 0)
                {
                    var p = ModContent.GetInstance<EALocalization>();
                    int nurseCDSeconds = nurseCooldown / 60;
                    chatText = p.Nurse + " " + nurseCDSeconds + " " + p.Nurse1;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            nurseCooldown = 30 * 60;
        }
        public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
        {
            if (MyWorld.awakenedMode)
            {
                int newPrice = (int)((Player.statLifeMax2 * 0.75f) - Player.statLife);
                for (int j = 0; j < 22; j++)
                {
                    int debuff = Player.buffType[j];
                    if (Main.debuff[debuff] && Player.buffTime[j] > 5 && debuff != 28 && debuff != 34 && debuff != 87 && debuff != 89 && debuff != 21 && debuff != 86 && debuff != 199)
                    {
                        newPrice += 1000;
                    }
                }
                newPrice = (int)(newPrice * GetNursePriceScale());
                price = newPrice;
            }
        }
        private float GetNursePriceScale()
        {
            float scale = 0.5f;
            if (NPC.downedSlimeKing) scale += 0.25f;
            if (NPC.downedBoss1) scale += 0.25f;
            if (MyWorld.downedWasteland) scale += 0.25f;
            if (NPC.downedBoss2) scale += 0.25f;
            if (NPC.downedBoss3) scale += 0.5f;
            if (MyWorld.downedInfernace) scale += 0.5f;
            if (Main.hardMode) scale += 4f;
            if (NPC.downedMechBossAny) scale += 2f;
            if (MyWorld.downedScourgeFighter) scale += 1f;
            if (MyWorld.downedRegaroth) scale += 2f;
            if (NPC.downedPlantBoss) scale += 2f;
            if (MyWorld.downedPermafrost) scale += 2f;
            if (MyWorld.downedObsidious) scale += 1f;
            if (NPC.downedFishron) scale += 2f;
            if (MyWorld.downedAqueous) scale += 2f;
            if (NPC.downedMoonlord) scale += 10f;
            if (MyWorld.downedGuardian) scale += 3f;
            if (MyWorld.downedVolcanox) scale += 3f;
            if (MyWorld.downedVoidLeviathan) scale += 3f;
            if (MyWorld.downedAzana || MyWorld.sparedAzana) scale += 3f;
            if (MyWorld.downedAncients) scale += 3f;
            return scale;
        }
        public override void PostSellItem(NPC vendor, Item[] shopInventory, Item item)
        {
            PlayerUtils playerUtils = Player.GetModPlayer<PlayerUtils>();

            if (MyWorld.awakenedMode)
            {
                if (playerUtils.salesLastMin < 10)
                {
                    sanity++;
                }
            }
        }
        public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item)
        {
            PlayerUtils playerUtils = Player.GetModPlayer<PlayerUtils>();

            if (MyWorld.awakenedMode)
            {
                if (playerUtils.buysLastMin < 10)
                {
                    sanity++;
                }
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag["sanity"] = sanity;
        }
        public override void LoadData(TagCompound tag)
        {
            sanity = tag.GetInt("sanity");
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)ElementsAwoken.ElementsAwokenMessageType.AwakenedSync);
            packet.Write((byte)Player.whoAmI);
            packet.Write(sanity);
            packet.Send(toWho, fromWho);
        }
        public void AddSanityDrain(int amount, string type)
        {
            sanityDrains.Add(amount);
            sanityDrainsName.Add(type);
        }
        public void AddSanityRegen(int amount, string type)
        {
            sanityRegens.Add(amount);
            sanityRegensName.Add(type);
        }
    }
    class SanityNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            Player player = Main.player[Main.myPlayer];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();
            if (MyWorld.awakenedMode)
            {
                if (npc.boss)
                {
                    if (playerUtils.bossesKilledLastFiveMin < 3)
                    {
                        modPlayer.sanity += modPlayer.sanityMax / 5;
                    }
                    else
                    {
                        modPlayer.sanity -= 8;
                    }
                }
            }
        }
    }
    class ReduceSanityItem : GlobalItem
    {
        public int reduceSanityCD = 0;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            ReduceSanityItem myClone = (ReduceSanityItem)base.Clone(item, itemClone);
            myClone.reduceSanityCD = reduceSanityCD;
            return myClone;
        }
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            Player player = Main.player[item.playerIndexTheItemIsReservedFor];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (MyWorld.awakenedMode)
            {
                reduceSanityCD--;
                if (item.type == ItemID.GuideVoodooDoll && Collision.LavaCollision(item.position, item.width, item.height) && Main.netMode != NetmodeID.MultiplayerClient && NPC.AnyNPCs(NPCID.Guide) && reduceSanityCD <= 0)
                {
                    reduceSanityCD = 5;
                    modPlayer.sanity -= 20;
                    ElementsAwoken.DebugModeText("voodoll drain");
                }
            }
        }
        public override bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
        {
            if (MyWorld.awakenedMode)
            {
                reforgePrice = (int)(reforgePrice * 1.5f);
            }
            return base.ReforgePrice(item, ref reforgePrice, ref canApplyDiscount);
        }
        public override bool? UseItem(Item item, Player player)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();
            if (MyWorld.awakenedMode)
            {
                if (item.buffType != 0 && item.useStyle == 2 && item.consumable && item.type != ModContent.ItemType<SanityRegenerationPotion>() && item.type != ItemID.Ale)
                {
                    if (playerUtils.potionsConsumedLastMin > 5)
                    {
                        modPlayer.sanity -= 3;
                    }
                }
            }
            if (modPlayer.aleCD <= 0 && item.type == ItemID.Ale)
            {
                modPlayer.sanity += 3;
                modPlayer.aleCD = 60 * 30;
            }
            return base.UseItem(item, player);
        }
    }
}