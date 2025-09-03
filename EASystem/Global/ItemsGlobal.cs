using ElementsAwoken.Content.Items.Accessories.Emblems;
using ElementsAwoken.Content.Items.BossDrops.zVanilla.Awakened;
using ElementsAwoken.Content.Items.Donator.Crow;
using ElementsAwoken.Content.Items.Testing;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.Loot;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Global
{
    public class ItemsGlobal : GlobalItem
    {
        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;

        public int miningRadius = 0;

        public override bool CanUseItem(Item item, Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.wispForm) return false;
            if (modPlayer.voidBlood && (item.potion || item.type == ItemID.RegenerationPotion)) return false;
            if (modPlayer.cantROD && item.type == ItemID.RodofDiscord) return false;
            if ((ElementsAwoken.encounter == 1 || modPlayer.cantMagicMirror) && (item.type == ItemID.CellPhone || item.type == ItemID.MagicMirror || item.type == ItemID.IceMirror || item.type == ItemID.RecallPotion || item.type == ItemID.WormholePotion)) return false;     
            if (MyWorld.credits && item.type != ModContent.ItemType<CreditsSetup>())return false;
            if (modPlayer.meteoricPendant && item.CountsAsClass(DamageClass.Magic))
            {
                Item startItem = new();
                startItem.SetDefaults(item.type);
                startItem.Prefix(item.prefix);
                item.useTime = (int)(startItem.useTime * 0.8f);
                item.useAnimation = (int)(startItem.useAnimation * 0.8f);
                startItem.TurnToAir();
            }
            if (!modPlayer.meteoricPendant && item.CountsAsClass(DamageClass.Magic))
            {
                Item startItem = new();
                startItem.SetDefaults(item.type);
                startItem.Prefix(item.prefix);
                item.useTime = startItem.useTime;
                item.useAnimation = startItem.useAnimation;
                startItem.TurnToAir();
            }
            return base.CanUseItem(item, player);
        }
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i)
                    {
                        if (item.type == ItemID.SorcererEmblem && player.armor[i].type == ModContent.ItemType<NebulaEmblem>()) return false;
                        if (item.type == ItemID.WarriorEmblem && player.armor[i].type == ModContent.ItemType<SolarEmblem>()) return false;
                        if (item.type == ItemID.RangerEmblem && player.armor[i].type == ModContent.ItemType<VortexEmblem>()) return false;
                        if (item.type == ItemID.SummonerEmblem && player.armor[i].type == ModContent.ItemType<StardustEmblem>()) return false;
                    }
                }
            }
            return base.CanEquipAccessory(item, player, slot, modded);
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) // Как будто должно быть что падает с боссов
        {
            LeadingConditionRule _AwakenedMode = new(new EAIDRC.AwakenedModeActive());
            if (item.type == ItemID.WallOfFleshBossBag) itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ThrowerEmblem>(), 4));
            if (item.type == ItemID.PlanteraBossBag) itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrematedChaos>(), 6));
            if (item.type == ItemID.KingSlimeBossBag) _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SlimeBooster>()));
            if (item.type == ItemID.EyeOfCthulhuBossBag) _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GreatLens>()));
            if (item.type == ItemID.EaterOfWorldsBossBag) _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CorruptedFang>()));
            if (item.type == ItemID.BrainOfCthulhuBossBag) _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BleedingHeart>()));
            if (item.type == ItemID.QueenBeeBossBag) _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CrystalNectar>()));
            if (item.type == ItemID.SkeletronBossBag) _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FadedCloth>()));
            if (item.type == ItemID.WallOfFleshBossBag) _AwakenedMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HellishFleshHeart>()));
            itemLoot.Add(_AwakenedMode);
            if (item.type == ItemID.FloatingIslandFishingCrate) itemLoot.Add(ItemDropRule.Common(ItemID.SkyMill, 3));
        }
        public override void RightClick(Item item, Player player) { }
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.JungleSpores)
            {
                item.ammo = ItemID.JungleSpores;
                item.shoot = ModContent.ProjectileType<BioLightning>();
                item.consumable = true;
            }
            if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (item.type == ItemID.SpaceGun)
                {
                    item.useTime = 19;
                    item.useAnimation = 19;
                    item.shootSpeed = 8f;
                }
                if (item.type == ItemID.LastPrism) item.damage = 90;
            }
        }
    }
    public class AOEPick : ModPlayer
    {
        public override void PostItemCheck()
        {
            if (!Player.HeldItem.IsAir)
            {
                Item item = Player.HeldItem;
                bool flag18 = Player.position.X / 16f - Player.tileRangeX - item.tileBoost <= Player.tileTargetX && (Player.position.X + Player.width) / 16f + Player.tileRangeX + item.tileBoost - 1f >= Player.tileTargetX && Player.position.Y / 16f - Player.tileRangeY - item.tileBoost <= Player.tileTargetY && (Player.position.Y + Player.height) / 16f + Player.tileRangeY + item.tileBoost - 2f >= Player.tileTargetY;
                if (Player.noBuilding) flag18 = false;
                if (flag18)
                {
                    if (item.GetGlobalItem<ItemsGlobal>().miningRadius > 0)
                    {
                        if (Player.toolTime == 0 && Player.itemAnimation > 0 && Player.controlUseItem)
                        {
                            if (item.pick > 0)
                            {
                                for (int i = -item.GetGlobalItem<ItemsGlobal>().miningRadius; i <= item.GetGlobalItem<ItemsGlobal>().miningRadius; i++)
                                {
                                    for (int j = -item.GetGlobalItem<ItemsGlobal>().miningRadius; j <= item.GetGlobalItem<ItemsGlobal>().miningRadius; j++)
                                    {
                                        if ((i != 0 || j != 0) && !Main.tileAxe[Main.tile[Player.tileTargetX + i, Player.tileTargetY + j].TileType] && !Main.tileHammer[Main.tile[Player.tileTargetX + i, Player.tileTargetY + j].TileType]) Player.PickTile(Player.tileTargetX + i, Player.tileTargetY + j, item.pick);
                                    }
                                }
                                Player.itemTime = (int)(item.useTime * Player.pickSpeed);
                            }                         
                            Player.poundRelease = false;
                        }
                        if (Player.releaseUseItem) Player.poundRelease = true;                      
                    }
                }
            }
        }
    }
}