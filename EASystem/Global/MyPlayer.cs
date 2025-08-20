using ElementsAwoken.Content.Buffs;
using ElementsAwoken.Content.Buffs.Cooldowns;
using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Buffs.Prompts;
using ElementsAwoken.Content.Buffs.TileBuffs;
using ElementsAwoken.Content.Dusts;
using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.Content.Items.Ancient;
using ElementsAwoken.Content.Items.Armor.Vanity.TOJO;
using ElementsAwoken.Content.Items.Consumable;
using ElementsAwoken.Content.Items.ItemSets.HiveCrate;
using ElementsAwoken.Content.Items.Other;
using ElementsAwoken.Content.Items.Tools;
using ElementsAwoken.Content.Items.Youtuber;
using ElementsAwoken.Content.Mounts;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.Content.NPCs.Critters;
using ElementsAwoken.Content.NPCs.Projectiles;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.Content.Projectiles.Other;
using ElementsAwoken.EASystem.UI.UIIIII;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.EASystem.Global
{
    /// <summary>
    /// DustID.PinkFlame => DustID.Firework_Pink 
    /// This is maybe not true
    /// </summary>
    public class MyPlayer : ModPlayer, ILocalizedModType
    {
        public string LocalizationCategory => "MyPlayerLocalization";

        int PinkFlame = DustID.Firework_Pink;

        public bool voidBlood = false;
        public int generalTimer = 0;

        public bool glassHeart = false;

        public int sansUseCD = 0;
        public int sansNote = 0;

        public bool talkToAzana = false;

        public int saveAmmo = 0;

        public int toySlimed = 0;
        public int toySlimedID = -1;

        #region minions
        public bool fireElemental = false;
        public bool miniatureSandStorm = false;
        public bool babyPuff = false;
        public bool happyCloud = false;
        public bool bubble = false;
        public bool enchantedTrio = false;
        public bool gWorm = false;
        public bool soulSkull = false;
        public bool aqueousMinions = false;
        public bool bloodDiamond = false;
        public bool iceAxe = false;
        public bool eyeballMinion = false;
        public bool scorpionMinion = false;
        public bool hearthMinion = false;
        public bool energySpirit = false;
        public bool icicleMinion = false;
        public bool deathwatcher = false;
        public bool phantomHook = false;
        public bool volcanicTentacle = false;
        public bool abyssCultist = false;
        public bool miniDragon = false;
        public bool azanaMinions = false;
        public bool coalescedOrb = false;
        public bool cosmicObserver = false;
        public bool kirovAirship = false;
        public bool wokeMinion = false;
        public bool corruptPenguin = false;
        public bool toyRobot = false;
        public bool miniVlevi = false;
        public bool corroder = false;
        public bool crystalEntity = false;
        public bool putridRipper = false;
        public bool globule = false;
        #endregion   
        #region pets
        public bool voidCrawler = false;
        public bool lilOrange = false;
        public bool woke = false;
        public bool royalEye = false;
        public bool possessedHand = false;
        public bool babyShadeWyrm = false;
        public bool turboDoge = false;
        public bool wyvernPet = false;
        public bool chamchamRat = false;
        public bool stellate = false;
        #endregion
        #region debuffs
        public bool iceBound = false;
        public bool endlessTears = false;
        public bool extinctionCurse = false;
        public bool handsOfDespair = false;
        public bool dragonfire = false;
        public bool discordDebuff = false;
        public bool brokenWings = false;
        public bool chaosBurn = false;
        public bool acidBurn = false;
        public bool superSlow = false;
        public int behemothGazeTimer = 0;
        public int leviathanDist = 0;
        public bool starstruck = false;
        public int starstruckCounter = 0;
        #endregion   
        #region buffs
        public bool extinctionCurseImbue = false;
        public bool discordantPotion = false;
        public bool superSpeed = false;
        public bool vilePower = false;
        public bool hellFury = false;
        #endregion
        #region other
        public bool dashCooldown = false;
        public bool venomSample = false;
        public bool ancientDecayWeapon = false;
        public bool medicineCooldown = false;
        public bool lightningCloud = false;
        public bool lightningCloudHidden = false;
        public float lightningCloudCharge = 0;
        public bool frozenGauntlet = false;
        public bool cantFly = false;
        public bool cantROD = false;
        public bool cantMagicMirror = false;
        public bool cantGrapple = false;
        public bool puffFall = false;
        public bool replenishRing = false;
        public bool neovirtuoBonus = false;
        public bool immortalResolve = false;
        public bool chaosRing = false;
        public bool voidLantern = false;
        public bool heartContainer = false;
        public bool noRespawnTime = false;
        public bool flare = false;
        public int flareShieldCD = 0;
        public bool scourgeDrive = false;
        public bool scourgeSpeed = false;
        public bool spikeBoots = false;
        public bool templeSpikeBoots = false;
        public bool sonicArm = false;
        public bool nyanBoots = false;
        public bool voidBoots = false;
        public bool theAntidote = false;
        public bool cosmicGlass = false;
        public int cosmicGlassCD = 0;
        public bool sufferWithMe = false;
        public bool strangeUkulele = false;
        public bool crystallineLocket = false;
        public bool eaMagmaStone = false;
        public bool meteoricPendant = false;
        public int crystallineLocketCrit = 0;
        public bool prismPolish = false;
        public bool fireAcc = false;
        public int fireAccCD = 0;
        public int boostDrive = 0;
        public int boostDriveTimer = 0;

        public bool honeyCocoon = false;
        public int honeyCocooned = 0;
        public int honeyCocoonDamage = 0;

        public bool wispForm = false;
        public bool forceWisp = false;
        public int wispDust = 0;

        public int noDamageCounter = 0;
        //amulet of despair
        public int voidEnergyCharge = 0;
        public int voidEnergyTimer = 0;
        //infinity guantlet and stones
        public int overInfinityCharged = 0;
        public bool infinityDeath = false;
        #endregion
        #region credits
        public Vector2 desiredScPos = new Vector2();
        public Vector2 playerStartPos = new Vector2();
        public Vector2[] creditPoints = new Vector2[20];
        public int pointsNotFound = 0;
        public int startTime = 0;
        public bool startDayTime = false;
        public bool screenTransition = false;
        public float screenTransAlpha = 0f;
        public float screenTransTimer = 0f;
        public int screenTransDuration = 60; // in frames
        public int screenDuration = 60 * 9;
        public int escHeldTimer = 0;
        #endregion
        #region skyline whirlwind
        public bool skylineFlying = false;
        public float skylineAlpha = 0f;
        public int skylineFrameTimer = 0;
        public int skylineFrame = 0;
        #endregion

        public bool[] mysteriousPotionsDrank;
        #region armor bonuses
        public int empyreanCloudCD = 0;
        public bool oceanicArmor = false;
        public bool voidArmor = false;
        public int voidArmorHealCD = 0;
        public bool dragonmailGreathelm = false;
        public bool dragonmailHood = false;
        public bool dragonmailMask = false;
        public bool dragonmailVisage = false;
        public bool elementalArmor = false;
        public bool elementalArmorCooldown = false;
        public bool superbaseballDemon = false;
        public bool gelticConqueror = false;
        public bool crowsArmor = false;
        public int crowsArmorCooldown = 0;
        public bool toyArmor = false;
        public int toyArmorCooldown = 0;
        public bool forgedArmor = false;
        public bool flingToShackle = false;
        public int forgedShackled = 0;
        public int shackleFlingCooldown = 0;
        public int voidWalkerArmor = 0; // for multiple helms
        public int voidWalkerCooldown = 0;
        public int voidWalkerAura = 0;
        public bool voidWalkerChest = false;
        public int voidWalkerRegen = 0;
        public bool energyWeaverArmor = false;
        public int energyWeaverTimer = 0;
        public bool cosmicalusArmor = false;
        public bool mechArmor = false;
        public int mechArmorCD = 0;
        public bool awokenWood = false;
        public bool arid = false;
        public int aridTimer = 0;
        public float aridFalling = 0;
        public bool putridArmour = false;
        #endregion
        // zones
        public static bool zoneTemple = false;
        // dash & hypothermia
        public bool ninjaDash = false;
        public bool viridiumDash = false;
        public bool canGetHypo = false;
        public int hypoChillTimer = 0;
        public int dashDustTimer = 0;
        #region timers and cooldowns
        public int neovirtuoTimer = 0;
        public float chaosBoost = 0;
        public float chaosDamageBoost = 0;
        public int masterSwordCharge = 0;
        public float masterSwordCountdown = 0;
        public float immortalResolveCooldown = 0;
        public float hellsReflectionTimer = 0;
        public float hellsReflectionCD = 0;
        public float voidPortalCooldown = 0;
        #endregion

        public int voidTimeChangeTime = 0;


        // aegis
        public bool vleviAegis = false;
        public int vleviAegisDamage = 0;
        public int vleviAegisBoost = 0;
        public int aegisDashTimer = 0;
        public int aegisDashCooldown = 0;
        public int aegisDashDir = 1;
        // computer
        public bool inComputer = false;
        public Vector2 computerPos = new Vector2();
        public int computerTextNo = 0;
        public int guardianEntryNo = 0;
        public int azanaEntryNo = 0;
        public int ancientsEntryNo = 0;
        public string computerText = "";
        // toy slime
        public int toySlimeChanceTimer = 0;
        public int observerChanceTimer = 0;
        // damage modifiers
        public float damageTaken = 1f;
        #region stat increases

        public int voidHeartsUsed = 0;
        public int chaosHeartsUsed = 0;
        public int lunarStarsUsed = 0;
        public int statManaMax3 = 0;

        public int shieldHearts = 0;
        public int shieldLife = 0;

        public bool extraAccSlot = false;

        public bool voidCompressor = false;
        #endregion

        // oinite statue
        public bool oiniteStatue = false;
        public bool[] oiniteDoubledBuff = new bool[Player.MaxBuffs];
        public bool[] discordPotBuff = new bool[Player.MaxBuffs];
        // info
        public int buffDPSCount = 0;
        public int buffDPS = 0;
        public bool alchemistTimer = false;
        public bool[] hideEAInfo = new bool[3];
        public bool dryadsRadar = false;
        public string nearbyEvil = "No evil";
        public bool rainMeter = false;
        //encounters
        public string encounterText = "";
        public int encounterTextAlpha = 0;
        public int encounterTextTimer = 0;
        public bool finalText = false;

        // screenshake
        public float screenshakeAmount = 0; // dont go above 15 unless you want to have a seizure
        public int screenshakeTimer = 0;

        //double tapping down
        public bool doubleTappedDown = false;
        public int doubleDownWindow = 0;

        // left and right arrows
        public bool controlLeftArrow = false;
        public bool controlRightArrow = false;

        public int eaDash = 0;
        public int eaDashTime = 0;
        public int eaDashDelay = 0;

        // awakened mode itmes
        public bool toySlimeClaw = false;
        public int toySlimeClawCD = 0;
        public bool toySlimeClawSliding = false;

        public bool slimeBooster = false;

        public bool greatLens = false;
        public int greatLensTimer = 0;

        public bool bleedingHeart = false;

        public bool crystalNectar = false;

        public bool fadedCloth = false;

        public bool hellHeart = false;

        public bool icyHeart = false;
        public int icyHeartTimer = 0;
        public float icyHeartDR = 1;

        public int aeroflakHits = 0;
        public int aeroflakTimer = 0;

        public bool abyssalMatter = false;
        public int abyssalRage = 0;

        public bool radiantCrown = false;

        public Vector2 archaicProtectionPos = new Vector2();
        public int archaicProtectionTimer = 0;

        bool crit = false;

        #region Biomes
        public bool useLeviathan = false;
        #endregion
        public override void Initialize()
        {
            mysteriousPotionsDrank = new bool[10];
            voidHeartsUsed = 0;
            chaosHeartsUsed = 0;
            lunarStarsUsed = 0;
        }
        public override void ResetEffects()
        {
            talkToAzana = false;
            saveAmmo = 0;
            #region minions
            fireElemental = false;
            miniatureSandStorm = false;
            babyPuff = false;
            bubble = false;
            happyCloud = false;
            enchantedTrio = false;
            gWorm = false;
            soulSkull = false;
            aqueousMinions = false;
            bloodDiamond = false;
            venomSample = false;
            iceAxe = false;
            eyeballMinion = false;
            scorpionMinion = false;
            hearthMinion = false;
            energySpirit = false;
            icicleMinion = false;
            deathwatcher = false;
            phantomHook = false;
            volcanicTentacle = false;
            abyssCultist = false;
            miniDragon = false;
            azanaMinions = false;
            coalescedOrb = false;
            cosmicObserver = false;
            kirovAirship = false;
            wokeMinion = false;
            corruptPenguin = false;
            toyRobot = false;
            miniVlevi = false;
            corroder = false;
            crystalEntity = false;
            putridRipper = false;
            globule = false;
            #endregion
            #region pets
            lilOrange = false;
            voidCrawler = false;
            woke = false;
            royalEye = false;
            possessedHand = false;
            babyShadeWyrm = false;
            turboDoge = false;
            wyvernPet = false;
            chamchamRat = false;
            stellate = false;
            #endregion
            #region debuffs
            iceBound = false;
            endlessTears = false;
            extinctionCurse = false;
            handsOfDespair = false;
            dragonfire = false;
            discordDebuff = false;
            brokenWings = false;
            chaosBurn = false;
            acidBurn = false;
            superSlow = false;
            starstruck = false;
            #endregion

            glassHeart = false;

            dashCooldown = false;
            medicineCooldown = false;
            frozenGauntlet = false;
            ancientDecayWeapon = false;
            lightningCloud = false;
            lightningCloudHidden = false;

            extinctionCurseImbue = false;
            discordantPotion = false;
            vilePower = false;
            superSpeed = false;
            hellFury = false;

            cantFly = false;
            cantROD = false;
            cantMagicMirror = false;
            cantGrapple = false;
            puffFall = false;
            replenishRing = false;
            neovirtuoBonus = false;
            immortalResolve = false;
            chaosRing = false;
            voidLantern = false;
            noRespawnTime = false;
            flare = false;
            scourgeDrive = false;
            spikeBoots = false;
            templeSpikeBoots = false;
            sonicArm = false;
            nyanBoots = false;
            voidBoots = false;
            skylineFlying = false;
            vleviAegis = false;
            theAntidote = false;
            cosmicGlass = false;
            sufferWithMe = false;
            strangeUkulele = false;
            crystallineLocket = false;
            eaMagmaStone = false;
            meteoricPendant = false;
            prismPolish = false;
            fireAcc = false;
            boostDrive = 0;
            wispForm = false;
            honeyCocoon = false;

            oceanicArmor = false;
            voidArmor = false;
            dragonmailGreathelm = false;
            dragonmailHood = false;
            dragonmailMask = false;
            dragonmailVisage = false;
            elementalArmor = false;
            elementalArmorCooldown = false;
            superbaseballDemon = false;
            gelticConqueror = false;
            crowsArmor = false;
            toyArmor = false;
            forgedArmor = false;
            voidWalkerArmor = 0;
            voidWalkerChest = false;
            energyWeaverArmor = false;
            cosmicalusArmor = false;
            mechArmor = false;
            arid = false;
            putridArmour = false;

            oiniteStatue = false;

            alchemistTimer = false;
            dryadsRadar = false;
            rainMeter = false;
            nearbyEvil = "No evil";

            damageTaken = 1f;
            //if (!calamityEnabled)
            //{
            //    Player.statLifeMax2 += (voidHeartsUsed * 10) + (chaosHeartsUsed * 10);
            //    Player.statManaMax2 += lunarStarsUsed * 100;

            //    if (Main.expertMode)
            //    {
            //        if (Player.extraAccessory)
            //        {
            //            Player.extraAccessorySlots = 1;
            //            if (extraAccSlot)
            //            {
            //                Player.extraAccessorySlots = 2;
            //            }
            //        }
            //    }
            //}
            shieldHearts = shieldLife / 5;
            Player.statLifeMax2 += shieldLife;
            buffDPS = buffDPSCount;
            buffDPSCount = 0;
            eaDash = 0;
            toySlimeClaw = false;
            slimeBooster = false;
            greatLens = false;
            bleedingHeart = false;
            crystalNectar = false;
            fadedCloth = false;
            hellHeart = false;
            icyHeart = false;
            abyssalMatter = false;
            radiantCrown = false;
        }
        //public override void CopyClientState(ModPlayer clientClone)
        //{
        //    MyPlayer clone = clientClone as MyPlayer;
        //}
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)ElementsAwoken.ElementsAwokenMessageType.StarHeartSync);
            packet.Write((byte)Player.whoAmI);
            packet.Write(voidHeartsUsed);
            packet.Write(chaosHeartsUsed);
            packet.Write(lunarStarsUsed);
            packet.Send(toWho, fromWho);
        }
        public override void SaveData(TagCompound tag)
        {
            var list = new List<bool>(mysteriousPotionsDrank);
            tag["mysteriousPotionsDrankList"] = list;
            tag["voidHeartsUsed"] = voidHeartsUsed;
            tag["chaosHeartsUsed"] = chaosHeartsUsed;
            tag["lunarStarsUsed"] = lunarStarsUsed;
            tag["voidCompressor"] = voidCompressor;
            tag["extraAccSlot"] = extraAccSlot;
            tag["voidBlood"] = voidBlood;
        }
        public override void LoadData(TagCompound tag)
        {
            voidHeartsUsed = tag.GetInt("voidHeartsUsed");
            chaosHeartsUsed = tag.GetInt("chaosHeartsUsed");
            lunarStarsUsed = tag.GetInt("lunarStarsUsed");
            voidCompressor = tag.GetBool("voidCompressor");
            extraAccSlot = tag.GetBool("extraAccSlot");
            voidBlood = tag.GetBool("voidBlood");
            if (tag.ContainsKey("mysteriousPotionsDrankList"))
            {
                var list = tag.GetList<bool>("mysteriousPotionsDrankList");
                mysteriousPotionsDrank = new List<bool>(list).ToArray();
            }
        }
        public void TryGettingDevArmor()
        {
            var source = Main.LocalPlayer.GetSource_FromThis();
            Player.TryGettingDevArmor(source);
            if (Main.rand.NextBool(20))
            {
                if (Main.rand.NextBool(20)) Player.QuickSpawnItem(source, ItemType<KawaiiOrangesMask>());
                else Player.QuickSpawnItem(source, ItemType<OrangesMask>());
                Player.QuickSpawnItem(source, ItemType<OrangesBreastplate>());
                Player.QuickSpawnItem(source, ItemType<OrangesLeggings>());
            }
        }
        public override void PreUpdateMovement()
        {
            if (arid && !Main.tileSolid[Framing.GetTileSafely((int)(Player.Bottom.X / 16), (int)(Player.Bottom.Y / 16)).TileType])
            {
                aridFalling = Player.velocity.Y;
            }
        }
        public override void Load()
        {
            _ = this.GetLocalization("Worold.Corruption").Value;
            _ = this.GetLocalization("Worold.andCrimson").Value;
            _ = this.GetLocalization("Worold.Crimson").Value;
            _ = this.GetLocalization("Worold.Allevils").Value;
            _ = this.GetLocalization("Worold.andHallowed").Value;
            _ = this.GetLocalization("Worold.Hallowed").Value;

            _ = this.GetLocalization("MasterSwordCharge.Discharged").Value;

            _ = this.GetLocalization("Encounter.Said").Value;
            _ = this.GetLocalization("Encounter.Said1").Value;
            _ = this.GetLocalization("Encounter.Said2").Value;
            _ = this.GetLocalization("Encounter.Said3").Value;
            _ = this.GetLocalization("Encounter.Said4").Value;
            _ = this.GetLocalization("Encounter.Said5").Value;
            _ = this.GetLocalization("Encounter.Said6").Value;
            _ = this.GetLocalization("Encounter.Said7").Value;
            _ = this.GetLocalization("Encounter.Said8").Value;
            _ = this.GetLocalization("Encounter.Said9").Value;
            _ = this.GetLocalization("Encounter.Said10").Value;

            _ = this.GetLocalization("Prompts.void").Value;
            _ = this.GetLocalization("Prompts.void1").Value;
            _ = this.GetLocalization("Prompts.void2").Value;
            _ = this.GetLocalization("Prompts.void3").Value;
            _ = this.GetLocalization("Prompts.void4").Value;
            _ = this.GetLocalization("Prompts.void5").Value;

            _ = this.GetLocalization("UponEnteringTheWorld.Music").Value;
            _ = this.GetLocalization("UponEnteringTheWorld.Music1").Value;
            _ = this.GetLocalization("UponEnteringTheWorld.Music2").Value;

            _ = this.GetLocalization("ComputerText.Text").Value;
            _ = this.GetLocalization("ComputerText.Text1").Value;
            _ = this.GetLocalization("ComputerText.Text2").Value;
            _ = this.GetLocalization("ComputerText.Text3").Value;
            _ = this.GetLocalization("ComputerText.Text4").Value;
            _ = this.GetLocalization("ComputerText.Text5").Value;
            _ = this.GetLocalization("ComputerText.Text6").Value;
            _ = this.GetLocalization("ComputerText.Text7").Value;
            _ = this.GetLocalization("ComputerText.Text8").Value;
            _ = this.GetLocalization("ComputerText.Text9").Value;
            _ = this.GetLocalization("ComputerText.Text10").Value;
            _ = this.GetLocalization("ComputerText.Text11").Value;
            _ = this.GetLocalization("ComputerText.Text12").Value;
            _ = this.GetLocalization("ComputerText.Text13").Value;
            _ = this.GetLocalization("ComputerText.Text14").Value;
            _ = this.GetLocalization("ComputerText.Text15").Value;
            _ = this.GetLocalization("ComputerText.Text16").Value;
            _ = this.GetLocalization("ComputerText.Text17").Value;

            _ = this.GetLocalization("PlayerDeath.Death").Value;
            _ = this.GetLocalization("PlayerDeath.Death1").Value;

            _ = this.GetLocalization("Other.Other1").Value;
            _ = this.GetLocalization("Other.Other2").Value;

            _ = this.GetLocalization("NurseHeal.Heal").Value;
            _ = this.GetLocalization("NurseHeal.Heal1").Value;
        }
        public override void PostUpdateMiscEffects()
        {
            var source = Main.LocalPlayer.GetSource_FromThis();

            EAWallSlide();
            generalTimer++;
            skylineFrameTimer++;
            cosmicGlassCD--;
            toySlimeClawCD--;
            doubleDownWindow--;
            hellsReflectionTimer--;
            hellsReflectionCD--;
            voidPortalCooldown--;
            toyArmorCooldown--;
            crowsArmorCooldown--;
            crystallineLocketCrit--;
            voidArmorHealCD--;
            abyssalRage--;
            immortalResolveCooldown--;
            toySlimeChanceTimer--;
            observerChanceTimer--;
            greatLensTimer--;
            icyHeartTimer++;
            aeroflakTimer--;
            mechArmorCD--;
            toySlimed--;
            honeyCocooned--;

            if (honeyCocooned <= 0)
            {
                honeyCocoonDamage = 0;
            }
            else if (honeyCocooned > 0)
            {
                Player.velocity.X = 0;
                CantMove();
            }
            if (glassHeart)
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (Player.buffType[l] == BuffID.ShadowDodge)
                    {
                        Player.DelBuff(l);
                        break;
                    }
                }
            }
            if (wispForm) Player.noKnockback = false;
            if (arid)
            {
                if (Player.controlJump && aridTimer < 600)
                {
                    aridTimer++;
                    if (Player.velocity.Y > -4)
                    {
                        Player.velocity.Y -= 0.6f;
                        Dust dust = Main.dust[Dust.NewDust(Player.BottomLeft - new Vector2(0 , 6), Player.width, 6, 32, 0f, 0f)];
                        dust.velocity = new Vector2(Main.rand.NextFloat(-1,1), Main.rand.NextFloat(3,5));
                    }
                    if (Player.velocity.Y < 4)
                    {
                        Player.fallStart = (int)Player.position.Y / 16;
                    }
                }
                if (Player.velocity.Y == 0)
                {
                    aridTimer = 0;
                }
            }
            if (boostDriveTimer > 0 && boostDrive > 0)
            {
                boostDriveTimer--;
                if (boostDrive == 1)
                {
                    Player.moveSpeed *= 1.5f;
                    Player.accRunSpeed *= 1.5f;
                    Player.runAcceleration *= 2f;
                    Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, 226, 0f, 0f, 100, default(Color), 1.4f)];
                    dust.noGravity = true;
                }
                else if (boostDrive == 2)
                {
                    Player.moveSpeed *= 2.5f;
                    Player.accRunSpeed *= 2.5f;
                    Player.runAcceleration *= 3f;
                    Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, 205, 0f, 0f, 100, default(Color), 1.4f)];
                    dust.noGravity = true;
                }
            }
            if (putridArmour)
            {
                int num = Player.velocity.Y == 0 ? 30 : 60;
                if (generalTimer % num == 0)
                {
                    Vector2 startVec = Player.Center + Main.rand.NextVector2Square(-60, 60);
                    for (int i = 0; i < 6; i++)
                    {
                        float distance = i < 3 ? 30 : 15;
                        Projectile proj = Main.projectile[Projectile.NewProjectile(source, startVec + Main.rand.NextVector2Square(-distance, distance), Vector2.Zero, ProjectileType<PutridCloud>(), 0, 0, Player.whoAmI, i < 3 ? 0 : 1)];
                        proj.localAI[0] = Main.rand.NextBool() ? -1 : 1;
                        proj.rotation = Main.rand.NextFloat((float)Math.PI * 2);
                    }
                }
            }
            if (toySlimedID != -1)
            {
                CantMove();
                if (Main.npc[toySlimedID].active)
                {
                    Player.Center = Main.npc[toySlimedID].Center;
                    Player.immune = true;
                    if (toySlimed <= 0)
                    {
                        toySlimedID = -1;
                        Player.velocity.X = Main.rand.NextFloat(-8, 8);
                        Player.velocity.Y = Main.rand.NextFloat(-12, -20);
                        SoundEngine.PlaySound(SoundID.Item95, Player.Center);
                    }
                    Player.AddBuff(BuffID.Slimed, 20);
                }
                else toySlimedID = -1;
            }
            if (flare) flareShieldCD--;
            if (aeroflakTimer <= 0) aeroflakHits = 0;
            if (bleedingHeart)
            {
                if ((Player.dashDelay != 0 || eaDashDelay != 0) && Player.velocity != Vector2.Zero)
                {
                    if (Main.rand.NextBool(8))
                    {
                        Projectile.NewProjectile(source, Player.Center, Main.rand.NextVector2Square(-6, 6), ProjectileType<BloodbathDashP>(), 30, 6f, Player.whoAmI);
                    }
                }
            }
            if (icyHeart)
            {
                icyHeartDR = MathHelper.Lerp(1f, 0f, MathHelper.Clamp(icyHeartTimer, 0f, 1200f) / 1200f);
                int numDust = (int)MathHelper.Lerp(1, 20, MathHelper.Clamp(icyHeartTimer, 0f, 1200f) / 1200f);
                int width = (int)(Player.width * 1.2f);
                int height = (int)(Player.height * 1.1f);
                for (int i = 0; i < numDust; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * width, (float)Math.Cos(angle) * height);
                    Dust dust = Main.dust[Dust.NewDust(Player.Center + offset - Vector2.One * 4, 0, 0, 135, 0, 0, 100)];
                    dust.noGravity = true;
                    dust.velocity = Player.velocity;
                }
            }
            if (abyssalMatter)
            {
                if (Main.rand.NextBool(60))
                {
                    Projectile.NewProjectile(source, Player.Center + Main.rand.NextVector2Square(-300,300), Main.rand.NextVector2Square(-1, 1), ProjectileType<AbyssalPortal>(), 300, 6f, Player.whoAmI);
                }
                if (abyssalRage > 0)
                {
                    if (Player.dead || !Player.active) abyssalRage = 0;
                    Player.GetDamage(DamageClass.Generic) *= 1.3f;
                    Player.moveSpeed *= 1.5f;
                    Player.accRunSpeed *= 1.5f;

                    int num = GetInstance<Config>().lowDust ? 1 : 3;
                    for (int l = 0; l < num; l++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.Firework_Pink, 0f, 0f, 100, default(Color), 1.4f)];
                        dust.noGravity = true;
                    }

                    if (abyssalRage % 60 == 0)
                    {
                        Vector2 speed = Main.rand.NextVector2Square(-2.5f, 2.5f);
                        float randAi0 = Main.rand.Next(10, 80) * 0.001f;
                        if (Main.rand.Next(2) == 0) randAi0 *= -1f;
                        float randAi1 = Main.rand.Next(10, 80) * 0.001f;
                        if (Main.rand.Next(2) == 0) randAi1 *= -1f;
                        Projectile.NewProjectile(source, Player.Center, speed, ProjectileType<AbyssalTentacle>(), 300, 6f, Player.whoAmI, randAi0, randAi1);
                    }
                }
            }
            if (!starstruck)  starstruckCounter = 0;
            if (voidBlood && Player.statLife < Player.statLifeMax2 * 0.3f && generalTimer % 60 == 0)
            {
                int damage = 10;
                if (NPC.downedBoss1) damage = 15;
                if (NPC.downedBoss3) damage = 20;
                if (Main.hardMode) damage = 25;
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) damage = 30;
                if (NPC.downedPlantBoss) damage = 35;
                if (NPC.downedAncientCultist) damage = 40;
                if (NPC.downedMoonlord) damage = 60;
                Projectile proj = Main.projectile[Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), Mod.Find<ModProjectile>("VoidBlood").Type, damage, 0f, Main.myPlayer, 0f, 0f)];
            }
            if (!NPC.AnyNPCs(NPCType<VoidLeviathanHead>())) behemothGazeTimer = 0;
            if (behemothGazeTimer > 600)
            {
                float amount = MathHelper.Clamp((float)(leviathanDist - 3000) / 9000f, 0, 1);
                Player.accRunSpeed *= MathHelper.Lerp(0, 1.5f, amount);
                Player.moveSpeed *= MathHelper.Lerp(0, 2, amount);
                Player.statDefense += (int)(Player.statDefense * MathHelper.Lerp(1, 0.2f, amount));
                Player.GetDamage(DamageClass.Generic) *= MathHelper.Lerp(1, 0.2f, amount);
            }
            else if (behemothGazeTimer > 0)
            {
                int num = (int)MathHelper.Lerp(1, 8, (float)(leviathanDist - 3000) / 9000f);
                if (GetInstance<Config>().lowDust) num = 1;
                for (int l = 0; l < num; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.Firework_Pink, 0f, 0f, 100, default(Color), 1.4f)];
                    dust.noGravity = true;
                }
            }
            if (superSpeed)
            {
                Player.moveSpeed *= 3;
                Player.runAcceleration *= 10;
            }
            if (superSlow)
            {
                Player.moveSpeed *= 0.1f;
            }
            #region dashes
            if (eaDashDelay > 0)
            {
                eaDashDelay--;
            }
            if (eaDashDelay > 0)
            {
                float maxDashSpeed = 15f;
                float maxSpeed = Math.Max(Player.accRunSpeed, Player.maxRunSpeed);
                float slowdown1 = 0.985f;
                float slowdown2 = 0.94f;
                if (eaDash == 1 || eaDash == 2)
                {
                    int dustID = 127;
                    if (eaDash == 2)
                    {
                        dustID = 63;
                        maxDashSpeed = 18f;
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        int num12;
                        if (Player.velocity.Y == 0f)
                        {
                            num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)Player.height - 4f), Player.width, 8, dustID, 0f, 0f, 100, default(Color), 2f);
                        }
                        else
                        {
                            num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)(Player.height / 2) - 8f), Player.width, 16, dustID, 0f, 0f, 100, default(Color), 2f);
                        }
                        Dust dust = Main.dust[num12];
                        dust.velocity *= 0.1f;
                        dust.scale *= 1f + (float)Main.rand.NextFloat(-0.4f, 0.4f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                        dust.noGravity = true;
                        if (eaDash == 2) dust.color = Main.DiscoColor;
                    }
                }
                if (eaDash > 0)
                {
                    Player.vortexStealthActive = false;
                    if (Player.velocity.X > maxDashSpeed || Player.velocity.X < -maxDashSpeed)
                    {
                        Player.velocity.X = Player.velocity.X * slowdown1;
                    }
                    if (Player.velocity.X > maxSpeed || Player.velocity.X < -maxSpeed)
                    {
                        Player.velocity.X = Player.velocity.X * slowdown2;
                    }
                    if (Player.velocity.X < 0f)
                    {
                        Player.velocity.X = -maxDashSpeed;
                    }
                    if (Player.velocity.X > 0f)
                    {
                        Player.velocity.X = maxDashSpeed;
                    }
                }
            }
            else if (eaDash > 0 && !Player.mount.Active)
            {
                int dashDir = 0;
                bool flag = false;
                if (eaDashTime > 0) eaDashTime--;
                if (eaDashTime < 0) eaDashTime++;
                if (eaDashDelay <= 0)
                {
                    if (Player.controlRight && Player.releaseRight)
                    {
                        if (eaDashTime > 0)
                        {
                            dashDir = 1;
                            flag = true;
                            eaDashTime = 0;
                        }
                        else eaDashTime = 15;
                    }
                    else if (Player.controlLeft && Player.releaseLeft)
                    {
                        if (eaDashTime < 0)
                        {
                            dashDir = -1;
                            flag = true;
                            eaDashTime = 0;
                        }
                        else eaDashTime = -15;
                    }
                }
                if (flag)
                {
                    int dashDelayAmount = 40;
                    if (eaDash == 1 || eaDash == 2)
                    {
                        int dustID = 127;
                        if (eaDash == 2)
                        {
                            dustID = 63;
                        }
                        Player.velocity.X = 26f * (float)dashDir;
                        Point point = (Player.Center + new Vector2((float)(dashDir * Player.width / 2 + 2), Player.gravDir * (float)(-(float)Player.height) / 2f + Player.gravDir * 2f)).ToTileCoordinates();
                        Point point2 = (Player.Center + new Vector2((float)(dashDir * Player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                        {
                            Player.velocity.X = Player.velocity.X / 2f;
                        }
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, dustID, 0f, 0f, 100, default(Color), 2f)];
                            dust.position.X = dust.position.X + (float)Main.rand.Next(-5, 6);
                            dust.position.Y = dust.position.Y + (float)Main.rand.Next(-5, 6);
                            dust.velocity *= 3f;
                            dust.scale *= 1f + (float)Main.rand.NextFloat(-0.4f, 0.4f);
                            dust.shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                            dust.noGravity = true;
                            if (eaDash == 2) dust.color = Main.DiscoColor;
                        }
                        eaDashDelay = dashDelayAmount;
                    }
                }
            }
            #endregion
            if (archaicProtectionTimer > 0)
            {
                archaicProtectionTimer--;
                Player.immune = true;
                CantMove();
                Player.velocity = Vector2.Zero;
                if (archaicProtectionPos == Vector2.Zero) archaicProtectionPos = Player.Center;
                else Player.Center = archaicProtectionPos;
                if (Player.ownedProjectileCounts[ProjectileType<ArchaicProtection>()] == 0)
                {
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ProjectileType<ArchaicProtection>(), 0, 0f, Player.whoAmI);
                }

            }
            else archaicProtectionPos = Vector2.Zero;

            if (brokenWings) Player.wingTimeMax = 1;
            string Corruption = this.GetLocalization("Worold.Corruption").Value;
            string AndCrimson = this.GetLocalization("Worold.andCrimson").Value;
            string Crimson = this.GetLocalization("Worold.Crimson").Value;
            string Allevils = this.GetLocalization("Worold.Allevils").Value;
            string andHallowed = this.GetLocalization("Worold.andHallowed").Value;
            string Hallowed = this.GetLocalization("Worold.Hallowed").Value;
            string and = " " + AndCrimson;
            string and2 = "," + " " + Crimson;
            string and3 = " " + andHallowed;
            if (dryadsRadar)
            {
                if (MyWorld.corruptionTiles > 0)
                {
                    nearbyEvil = Corruption;
                }
                if (MyWorld.crimsonTiles > 0)
                {
                    if (MyWorld.corruptionTiles > 0 && MyWorld.hallowedTiles == 0)
                    {
                        nearbyEvil += and;
                    }
                    else if (MyWorld.corruptionTiles > 0)
                    {
                        nearbyEvil += and2;
                    }
                    else
                    {
                        nearbyEvil = Crimson;
                    }
                }
                if (MyWorld.hallowedTiles > 0)
                {
                    if (MyWorld.corruptionTiles > 0 && MyWorld.crimsonTiles > 0)
                    {
                        nearbyEvil = Allevils;
                    }
                    else if (MyWorld.corruptionTiles > 0 || MyWorld.crimsonTiles > 0)
                    {
                        nearbyEvil += and3;
                    }
                    else
                    {
                        nearbyEvil = Hallowed;
                    }
                }
            }
            if (forgedShackled > 0)
            {
                if (Player.ownedProjectileCounts[ProjectileType<ShackledBase>()] == 0)
                {
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ProjectileType<ShackledBase>(), 0, 0f, Player.whoAmI, Player.whoAmI, 0f);
                }
                Player.moveSpeed *= 3;
                shackleFlingCooldown--;
                forgedShackled--;
            }
            if (flingToShackle)
            {
                if (forgedShackled <= 0)
                {
                    flingToShackle = false;
                }
                for (int l = 0; l < 5; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, 6, 0f, 0f, 100, default(Color), 1.4f)];
                    dust.noGravity = true;
                }
                CollideWithNPCs(Player.getRect(), 40, 10f, 30, 6);
                shackleFlingCooldown = 60;
            }
            if (skylineAlpha > 0)
            {
                if (skylineFrameTimer % 6 == 0) skylineFrame++;
                if (skylineFrame > 3) skylineFrame = 0;
            }
            if (shieldLife > 100) shieldLife = 100;
            if (shieldLife > 0 && Main.time % 60 == 0) shieldLife--;
            if (Player.statLife <= Player.statLifeMax2 - 5 && shieldHearts > 0) shieldHearts--;
            if (Player.mount.Active && Player.mount.Type == MountType<ElementalDragonBunny>() && Math.Abs(Player.velocity.X) > Player.mount.DashSpeed - Player.mount.RunSpeed / 3f)
            {
                Rectangle rect = Player.getRect();
                if (Player.direction == 1)
                {
                    rect.Offset(Player.width - 1, 0);
                }
                rect.Width = 2;
                rect.Inflate(6, 12);
                float damage = 60;
                float knockback = 10f;
                int nPCImmuneTime = 30;
                int playerImmuneTime = 6;
                CollideWithNPCs(rect, damage, knockback, nPCImmuneTime, playerImmuneTime);
            }
            if (crystallineLocketCrit > 0)
            {
                Player.GetCritChance(DamageClass.Magic) = 100;
                Player.GetCritChance(DamageClass.Melee) = 100;
                Player.GetCritChance(DamageClass.Ranged) = 100;
                Player.GetCritChance(DamageClass.Throwing) = 100;
                int dustID = DustType<AncientRed>();
                switch (Main.rand.Next(4))
                {
                    case 0:
                        dustID = DustType<AncientRed>();
                        break;
                    case 1:
                        dustID = DustType<AncientGreen>();
                        break;
                    case 2:
                        dustID = DustType<AncientBlue>();
                        break;
                    case 3:
                        dustID = DustType<AncientPink>();
                        break;
                    default:
                        dustID = DustType<AncientRed>();
                        break;
                }
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, dustID, 0f, 0f, 100, default(Color), 1.5f)];
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                    dust.fadeIn = 1.3f;
                }
            }
            if (lightningCloud)
            {
                if (!lightningCloudHidden)
                {
                    if (Player.ownedProjectileCounts[ProjectileType<LightningCloud>()] == 0)
                    {
                        Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y - 50, 0f, 0f, ProjectileType<LightningCloud>(), 0, 0f, Player.whoAmI, 0f, 0f);
                    }
                    if (Player.ownedProjectileCounts[ProjectileType<LightningCloudSwirl>()] == 0 && lightningCloudCharge >= 300)
                    {
                        int orbitalCount = 3;
                        for (int l = 0; l < orbitalCount; l++)
                        {
                            int distance = 360 / orbitalCount;
                            int orbital = Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ProjectileType<LightningCloudSwirl>(), 0, 0f, Player.whoAmI, l * distance, Player.whoAmI);
                        }
                    }
                }
                if (lightningCloudCharge < 300)
                {
                    lightningCloudCharge++;
                    int rand = 20;
                    if (lightningCloudCharge >= 60)
                    {
                        rand = 15;
                    }
                    if (lightningCloudCharge >= 120)
                    {
                        rand = 12;
                    }
                    if (lightningCloudCharge >= 180)
                    {
                        rand = 8;
                    }
                    if (lightningCloudCharge >= 240)
                    {
                        rand = 5;
                    }
                    if (Main.rand.Next(rand) == 0)
                    {
                        int num5 = Dust.NewDust(Player.position, Player.width, Player.height, 15, 0f, 0f, 200, default(Color), 0.5f);
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].velocity *= 0.75f;
                        Main.dust[num5].fadeIn = 1.3f;
                        Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                        Main.dust[num5].velocity = vector;
                        vector.Normalize();
                        vector *= 34f;
                        Main.dust[num5].position = Player.Center - vector;
                    }
                }
                if (lightningCloudCharge > 300)
                {
                    lightningCloudCharge = 300;
                }
            }
            if (scourgeDrive)
            {
                float pVelX = Player.velocity.X;

                if (pVelX < 0)
                {
                    pVelX *= -1;
                }
                scourgeSpeed = pVelX >= 12;
                if (scourgeSpeed)
                {
                    Player.GetDamage(DamageClass.Magic) *= 1.25f;
                    Player.GetDamage(DamageClass.Ranged) *= 1.25f;
                    Player.GetDamage(DamageClass.Melee) *= 1.25f;
                    Player.GetDamage(DamageClass.Summon) *= 1.25f;
                    Player.GetDamage(DamageClass.Throwing) *= 1.25f;
                }
            }
            if (MyWorld.aggressiveEnemies)
            {
                Player.aggro += 500;
            }
            if (Player.FindBuffIndex(BuffType<StatueBuffGenihWat>()) == -1)
            {
                MyWorld.aggressiveEnemies = false;
                MyWorld.swearingEnemies = false;
            }
            if (superbaseballDemon)
            {
                if (Player.ownedProjectileCounts[ProjectileType<Demon>()] < 1)
                {
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ProjectileType<Demon>(), 30, 1.25f, Main.myPlayer, 0f, 0f);
                }
            }
            if (MyWorld.voidInvasionUp)
            {
                if (!Main.dayTime && Main.time > 16220)
                {
                    if (voidLantern)
                    {
                        Lighting.AddLight(Player.Center, 60f, 12f, 0.0f);

                    }
                    else
                    {
                        Lighting.AddLight(Player.Center, 20f, 4f, 0.0f);
                    }
                }
            }
            if (masterSwordCharge > 0)
            {
                if (masterSwordCharge > 50)
                {
                    masterSwordCharge = 50;
                }
                masterSwordCountdown--;
                if (masterSwordCountdown <= 0)
                {
                    masterSwordCharge = 0;
                    masterSwordCountdown = 0;
                }
                if (masterSwordCountdown == 1)
                {
                    CombatText.NewText(Player.getRect(), Color.Red, this.GetLocalization("MasterSwordCharge.Discharged").Value, true, false);
                }
            }
            if (empyreanCloudCD < 0)
            {
                Projectile.NewProjectile(source, Player.Center.X + Main.rand.Next(-120, 120), Player.Center.Y - Main.rand.Next(15, 50), 0f, 0f, ProjectileType<AeroStorm>(), 30, 1.25f, Main.myPlayer, 0f, 0f);
                empyreanCloudCD = Main.rand.Next(60, 300);
            }
            if (ninjaDash)
            {
                for (int l = 0; l < 1; l++)
                {
                    int dust;
                    if (Player.velocity.Y == 0f)
                    {
                        dust = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)Player.height - 4f), Player.width, 8, 31, 0f, 0f, 100, default(Color), 1.4f);
                    }
                    else
                    {
                        dust = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)(Player.height / 2) - 8f), Player.width, 16, 31, 0f, 0f, 100, default(Color), 1.4f);
                    }
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                }
                dashDustTimer--;
                if (dashDustTimer <= 0)
                {
                    ninjaDash = false;
                }
            }
            if (viridiumDash)
            {
                for (int l = 0; l < 1; l++)
                {
                    int dust = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)Player.height - 4f), Player.width, 8, 222, 0f, 0f, 100, default(Color), 1.4f);
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Projectile.NewProjectile(source, Player.Center.X, Player.Bottom.Y, 0f, 0f, ProjectileType<ViridiumBomb>(), 0, 0f, Player.whoAmI, 0.0f, 0.0f);
                }
                dashDustTimer--;
                if (dashDustTimer <= 0)
                {
                    viridiumDash = false;
                }
            }
            if (vleviAegis)
            {
                aegisDashCooldown--;
                if (vleviAegisDamage >= 500)
                {
                    vleviAegisBoost = 900;
                    vleviAegisDamage = 0;
                }
            }
            if (vleviAegisBoost > 0)
            {
                vleviAegisBoost--;

                Player.moveSpeed *= 1.4f;

                Player.GetDamage(DamageClass.Magic) *= 1.2f;
                Player.GetDamage(DamageClass.Melee) *= 1.2f;
                Player.GetDamage(DamageClass.Summon) *= 1.2f;
                Player.GetDamage(DamageClass.Ranged) *= 1.2f;
                Player.GetDamage(DamageClass.Throwing) *= 1.2f;

                if (Player.ownedProjectileCounts[ProjectileType<VleviAegisSwirl>()] == 0)
                {
                    int orbitalCount = 3;
                    for (int l = 0; l < orbitalCount; l++)
                    {
                        int distance = 360 / orbitalCount;
                        int orbital = Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ProjectileType<VleviAegisSwirl>(), 0, 0f, Main.myPlayer, l * distance, Player.whoAmI);
                    }
                    for (int l = 0; l < orbitalCount; l++)
                    {
                        int distance = 360 / orbitalCount;
                        int orbital = Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ProjectileType<VleviAegisSwirl>(), 0, 0f, Main.myPlayer, l * distance, Player.whoAmI);
                        Main.projectile[orbital].localAI[0] = 1;
                    }
                    SoundEngine.PlaySound(SoundID.Item113, Player.position);
                }
            }
            if (aegisDashTimer > 0)
            {
                aegisDashTimer--;

                Player.velocity.X = 25 * aegisDashDir;
                Player.velocity.Y = 0;

                Player.direction = aegisDashDir;

                Player.controlJump = false;
                for (int l = 0; l < 2; l++)
                {
                    Vector2 position = Player.Center + Vector2.Normalize(Player.velocity) * 10f;
                    Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.Firework_Pink, 0f, 0f, 0, default(Color), 1.5f)];
                    dust.position = position;
                    dust.velocity = Player.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * 0.33f + Player.velocity / 4f;
                    dust.velocity.X -= Player.velocity.X / 10f * l;
                    dust.position += Player.velocity.RotatedBy(1.5707963705062866, default(Vector2));
                    dust.fadeIn = 0.5f;
                    dust.noGravity = true;
                    Dust dust1 = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.Firework_Pink, 0f, 0f, 0, default(Color), 1.5f)];
                    dust1.position = position;
                    dust1.velocity = Player.velocity.RotatedBy(-1.5707963705062866, default(Vector2)) * 0.33f + Player.velocity / 4f;
                    dust1.velocity.X -= Player.velocity.X / 10f * l;
                    dust1.position += Player.velocity.RotatedBy(-1.5707963705062866, default(Vector2));
                    dust1.fadeIn = 0.5f;
                    dust1.noGravity = true;
                }
            }
            if (voidEnergyCharge > 0)
            {
                if (voidEnergyCharge < 2200)
                {
                    int num = (int)MathHelper.Lerp(10f, 1f, voidEnergyCharge / 2200f);
                    if (Main.rand.Next(num) == 0)
                    {
                        int num5 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Firework_Pink, 0f, 0f, 200, default(Color), 0.5f);
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].velocity *= 0.75f;
                        Main.dust[num5].fadeIn = 1.3f;
                        Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                        Main.dust[num5].velocity = vector;
                        vector.Normalize();
                        vector *= 34f;
                        Main.dust[num5].position = Player.Center - vector;
                    }
                }
                else if (voidEnergyCharge > 2200 && voidEnergyCharge < 3600)
                {
                    int num = (int)MathHelper.Lerp(1f, 3f, (voidEnergyCharge - 2200) / (3600f - 2200f));
                    for (int i = 0; i < num; i++)
                    {
                        int num5 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Firework_Pink, 0f, 0f, 200, default(Color), 0.5f);
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].velocity *= 0.75f;
                        Main.dust[num5].fadeIn = 1.3f;
                        Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                        Main.dust[num5].velocity = vector;
                        vector.Normalize();
                        vector *= 34f;
                        Main.dust[num5].position = Player.Center - vector;
                    }
                }
                else
                {
                    EyeDust(Player, DustID.Firework_Pink);
                }
            }
            if (voidEnergyTimer > 0)
            {
                Player.moveSpeed *= 0.2f;
                Player.GetDamage(DamageClass.Magic) *= 1.2f;
                Player.GetDamage(DamageClass.Melee) *= 1.2f;
                Player.GetDamage(DamageClass.Summon) *= 1.2f;
                Player.GetDamage(DamageClass.Ranged) *= 1.2f;
                Player.GetDamage(DamageClass.Throwing) *= 1.2f;
                voidEnergyTimer--;

                if (Main.rand.Next(4) == 0)
                {
                    Vector2 perturbedSpeed = new Vector2(5f, 5f).RotatedByRandom(MathHelper.ToRadians(360));
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<VoidSinewave>(), 300, 0f, 0);
                }
            }
            if (energyWeaverArmor)
            {
                energyWeaverTimer--;
                if (energyWeaverTimer <= 0)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC nPC = Main.npc[i];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Player.Center, nPC.Center) <= 250)
                        {
                            float Speed = 9f;
                            float rotation = (float)Math.Atan2(Player.Center.Y - nPC.Center.Y, Player.Center.X - nPC.Center.X);
                            if (energyWeaverTimer <= 0)
                            {
                                Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                                Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, speed.X, speed.Y, ProjectileType<EnergyWeaverBeam>(), 60, 5f, Main.myPlayer);
                                energyWeaverTimer = 90;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int dust = Main.rand.Next(2) == 0 ? 135 : 242;
                        int num5 = Dust.NewDust(Player.position, Player.width, Player.height, dust, 0f, 0f, 200, default(Color), 0.5f);
                        Main.dust[num5].noGravity = true;
                        Main.dust[num5].velocity *= 0.75f;
                        Main.dust[num5].fadeIn = 1.3f;

                        Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                        Main.dust[num5].velocity = vector;
                        vector.Normalize();
                        vector *= 34f;
                        Main.dust[num5].position = Player.Center - vector;
                    }
                }
            }
            if (overInfinityCharged > 0)
            {
                int num5 = Dust.NewDust(Player.position, Player.width, Player.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.5f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.75f;
                Main.dust[num5].fadeIn = 1.3f;

                Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                Main.dust[num5].velocity = vector;
                vector.Normalize();
                vector *= 34f;
                Main.dust[num5].position = Player.Center - vector;

                if (overInfinityCharged > 1800)
                {
                    infinityDeath = true;
                }
                else
                {
                    infinityDeath = false;
                }
                if (infinityDeath)
                {
                    Player.lifeRegen -= 150;
                }
            }
            else
            {
                infinityDeath = false;
            }
            if (Player.FindBuffIndex(BuffType<ChaosShield>()) == -1 && !Player.dead)
            {
                chaosBoost = 0;
                chaosDamageBoost = 0;
            }
            if (chaosBoost > 0)
            {
                chaosDamageBoost = 1f + (chaosBoost / 5000);
                Player.GetDamage(DamageClass.Magic) *= chaosDamageBoost;
                Player.GetDamage(DamageClass.Melee) *= chaosDamageBoost;
                Player.GetDamage(DamageClass.Summon) *= chaosDamageBoost;
                Player.GetDamage(DamageClass.Ranged) *= chaosDamageBoost;
                Player.GetDamage(DamageClass.Throwing) *= chaosDamageBoost;
            }
            if (voidWalkerAura > 0)
            {
                voidWalkerAura--;
                voidWalkerCooldown = 1800;
                EyeDust(Player, PinkFlame);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (!nPC.friendly && !nPC.boss && Vector2.Distance(Player.Center, nPC.Center) <= 2000)
                    {
                        nPC.AddBuff(BuffID.Confused, 30);
                        nPC.AddBuff(BuffType<ExtinctionCurse>(), 30);
                    }
                }
            }
            if (voidWalkerCooldown > 0)
            {
                voidWalkerCooldown--;
            }
            if (voidWalkerRegen > 0)
            {
                voidWalkerRegen--;
                Player.lifeRegen += 75;
            }
            #region encounters
            if (ElementsAwoken.encounter != 0)
            {
                string Said = this.GetLocalization("Encounter.Said").Value;
                string Said1 = this.GetLocalization("Encounter.Said1").Value;
                string Said2 = this.GetLocalization("Encounter.Said2").Value;
                string Said3 = this.GetLocalization("Encounter.Said3").Value;
                string Said4 = this.GetLocalization("Encounter.Said4").Value;
                string Said5 = this.GetLocalization("Encounter.Said5").Value;
                string Said6 = this.GetLocalization("Encounter.Said6").Value;
                string Said7 = this.GetLocalization("Encounter.Said7").Value;
                string Said8 = this.GetLocalization("Encounter.Said8").Value;
                string Said9 = this.GetLocalization("Encounter.Said9").Value;
                string Said10 = this.GetLocalization("Encounter.Said10").Value;

                if (ElementsAwoken.encounter == 1)
                {
                    screenshakeAmount = 5f;
                }
                if (ElementsAwoken.encounter >= 2)
                {
                    if (Player.active && !Player.dead) Player.AddBuff(BuffID.Darkness, 60);
                    UISystemSettings.VoidSay = true;
                    UISystemSettings.VoidTime++;
                }
                if (UISystemSettings.VoidTime == 400)
                {
                    UISystemSettings.VoidTime++;
                    encounterTextTimer = 300;
                    encounterTextAlpha = 0;
                    if (ElementsAwoken.encounter == 1) encounterText = Said;
                    else if (ElementsAwoken.encounter == 2) encounterText = Said1;
                    else if (ElementsAwoken.encounter == 3) encounterText = Said2;
                }
                else if (UISystemSettings.VoidTime == 800)
                {
                    UISystemSettings.VoidTime++;
                    encounterTextTimer = 300;
                    encounterTextAlpha = 0;
                    if (ElementsAwoken.encounter == 1) encounterText = Said3;
                    else if (ElementsAwoken.encounter == 2) encounterText = Said4;
                    else if (ElementsAwoken.encounter == 3) encounterText = Said5;
                }
                else if (UISystemSettings.VoidTime == 1100)
                {
                    UISystemSettings.VoidTime++;
                    encounterTextTimer = 300;
                    encounterTextAlpha = 0;
                    if (ElementsAwoken.encounter == 1) encounterText = Said6;
                    else if (ElementsAwoken.encounter == 2) encounterText = Said7;
                    else if (ElementsAwoken.encounter == 3) encounterText = Said8;
                }
                else if (UISystemSettings.VoidTime == 1400)
                {
                    UISystemSettings.VoidTime++;
                    if (ElementsAwoken.encounter != 1)
                    {
                        encounterTextTimer = 300;
                        encounterTextAlpha = 0;
                    }
                    if (ElementsAwoken.encounter == 2) encounterText = Said9;
                    else if (ElementsAwoken.encounter == 3)
                    {
                        encounterText = Said10;
                        UISystemSettings.VoidColor = Color.DarkRed;
                        SoundEngine.PlaySound(SoundID.Zombie105, Player.position);
                        finalText = true;
                    }
                }
                else if (UISystemSettings.VoidTime == 1700)
                {
                    encounterText = "";
                }
                if (UISystemSettings.VoidTime > 300)
                {
                    if (encounterTextAlpha > 255) encounterTextAlpha = 255;
                    else encounterTextAlpha += (int)Math.Ceiling(255f / 60f);
                }
                else
                {
                    if (encounterTextAlpha < 0) encounterTextAlpha = 0;
                    else encounterTextAlpha -= (int)Math.Ceiling(255f / 60f);
                }
            }
            #endregion

            ComputerText();

            if (MyWorld.credits)
            {
                Player.immune = true;
                Player.statLife = Player.statLifeMax2;
                for (int i = 0; i < 22; i++)
                {
                    Player.DelBuff(i);
                }
                Player.hideMisc[0] = true;
                Player.hideMisc[1] = true;
                Player.mount._active = false;

                CantMove();
                Player.controlInv = false;
                Player.controlMap = false;
                Player.releaseInventory = false;
                Main.playerInventory = false;
                Main.inputTextEnter = false;
                Main.menuMode = 0;
                for (int k = 0; k < Main.maxProjectiles; k++)
                {
                    Main.projectile[k].Kill();
                }
                for (int k = 0; k < Main.dust.Length; k++)
                {
                    Dust dust = Main.dust[k];
                    if (Vector2.Distance(dust.position, Player.Center) < 90)
                        dust.active = false;
                }
                // find keypoints
                if (creditPoints[0].X == 0)
                {
                    for (int x = 0; x < Main.maxTilesX; ++x)
                    {
                        for (int y = 0; y < Main.maxTilesY; ++y)
                        {
                            //temple
                            if (creditPoints[1].X != 0 && creditPoints[2].X != 0 && creditPoints[3].X != 0 && creditPoints[4].X != 0 && creditPoints[5].X != 0 && creditPoints[6].X != 0 && creditPoints[7].X != 0 && creditPoints[8].X != 0)
                                break;
                            if (Main.tile[x, y] == null) continue;
                            else if (Main.tile[x, y].TileType == 237 && creditPoints[1].X == 0) // altar
                            {
                                creditPoints[1] = new Vector2(x * 16, y * 16);
                            }
                            // jungle with hive
                            else if (Main.tile[x, y].TileType == 225 && creditPoints[2].X == 0) // hive
                            {
                                creditPoints[2] = new Vector2(x * 16, y * 16);
                            }
                            // spidernest
                            else if (Main.tile[x, y].WallType == 62 && creditPoints[3].X == 0) // spider wall
                            {
                                creditPoints[3] = new Vector2(x * 16, y * 16);
                            }
                            // sky island
                            else if (Main.tile[x, y].TileType == 202 && creditPoints[4].X == 0) // sunplate
                            {
                                creditPoints[4] = new Vector2(x * 16, y * 16);
                            }
                            // hallow
                            else if (Main.tile[x, y].TileType == 110 && creditPoints[5].X == 0) // hallowed grass
                            {
                                creditPoints[5] = new Vector2(x * 16, y * 16);
                            }
                            // evil
                            else if (Main.tile[x, y].TileType == 201 && creditPoints[6].X == 0) // crim grass
                            {
                                if (WorldGen.crimson) creditPoints[6] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].TileType == 24 && creditPoints[6].X == 0) // corrupt grass bits
                            {
                                if (!WorldGen.crimson) creditPoints[6] = new Vector2(x * 16, y * 16);
                            }
                            // mushroom 
                            else if (Main.tile[x, y].TileType == 70 && creditPoints[7].X == 0) // mushroom grass
                            {
                                creditPoints[7] = new Vector2(x * 16, y * 16);
                            }
                            //snow
                            else if (Main.tile[x, y].TileType == 147 && creditPoints[8].X == 0)
                            {
                                creditPoints[8] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].TileType == TileID.Marble && creditPoints[12].X == 0 && x > Main.spawnTileX - 200)
                            {
                                creditPoints[12] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].TileType == TileID.Granite && creditPoints[13].X == 0 && x > Main.spawnTileX - 200)
                            {
                                creditPoints[13] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].TileType == TileID.LivingMahogany && creditPoints[14].X == 0)
                            {
                                creditPoints[14] = new Vector2(x * 16, y * 16);
                            }
                            else if (Main.tile[x, y].TileType == TileID.LeafBlock && creditPoints[15].X == 0 && x > Main.spawnTileX - 200)
                            {
                                creditPoints[15] = new Vector2(x * 16, y * 16);
                            }
                            else continue;
                        }
                    }
                    creditPoints[9] = new Vector2(Main.spawnTileX * 16 + 600, (Main.maxTilesY - 200) * 16); // hell
                    creditPoints[10] = new Vector2(Main.dungeonX * 16, Main.dungeonY * 16); // dungeon
                    creditPoints[11] = new Vector2(1800, (float)Main.worldSurface * 16 - 16 * 150); // ocean

                    Player.FindSpawn();
                    creditPoints[0] = new Vector2(Player.SpawnX * 16, Player.SpawnY * 16);
                    if (creditPoints[0].X < 0 || creditPoints[0].Y < 0) creditPoints[0] = new Vector2(Main.spawnTileX * 16, Main.spawnTileY * 16);
                }
                // spawn
                if (MyWorld.creditsCounter == 0)
                {
                    playerStartPos = Player.Center;
                    startTime = (int)Main.time;
                    screenTransition = true;
                }
                if (MyWorld.creditsCounter == screenTransDuration / 2)
                {
                    desiredScPos = creditPoints[0] - Vector2.One * 50;
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC nPC = Main.npc[k];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0) Main.npc[k].active = false;
                    }
                }
                if (MyWorld.creditsCounter > screenTransDuration / 2)
                {
                    Player.Center = desiredScPos;
                    Main.dayTime = true;
                    Main.time = 27000;
                }
                if (MyWorld.creditsCounter > screenTransDuration / 2 && MyWorld.creditsCounter < screenDuration + screenTransDuration / 2) desiredScPos += new Vector2(1, 1);

                int creditsLength = screenDuration * 11 + 300;
                if (MyWorld.creditsCounter >= screenDuration && MyWorld.creditsCounter < creditsLength)
                {
                    int screenNum = pointsNotFound + (int)Math.Floor((decimal)(MyWorld.creditsCounter / screenDuration));
                    if (creditPoints[screenNum].X == 0)
                    {
                        pointsNotFound++;
                        screenNum = pointsNotFound + (int)Math.Floor((decimal)(MyWorld.creditsCounter / screenDuration));
                    }
                    Vector2 scroll = new Vector2(1, -1);
                    if (screenNum == 1) scroll = new Vector2(0, -1);
                    else if (screenNum == 2) scroll = new Vector2(1, 1);
                    else if (screenNum == 3) scroll = new Vector2(1, -1);
                    else if (screenNum == 4) scroll = new Vector2(-1, 1);
                    else if (screenNum == 5) scroll = new Vector2(1, 1);
                    else if (screenNum == 6) scroll = new Vector2(0, 1);
                    else if (screenNum == 7) scroll = new Vector2(1, 0);
                    else if (screenNum == 8) scroll = new Vector2(-0.5f, 2);
                    else if (screenNum == 9) scroll = new Vector2(1, 1);
                    else if (screenNum == 10) scroll = new Vector2(0, 1);
                    else if (screenNum == 11) scroll = new Vector2(1, 0);
                    else if (screenNum == 12) scroll = new Vector2(1, 0.5f);
                    else if (screenNum == 13) scroll = new Vector2(1, -0.25f);
                    else if (screenNum == 14) scroll = new Vector2(2, 1);
                    else if (screenNum == 15) scroll = new Vector2(0.2f, 1);
                    CreditsScroll(screenNum, scroll, screenDuration);
                }

                Keys[] pressedKeys = Main.keyState.GetPressedKeys();

                bool escPressed = false;
                for (int j = 0; j < pressedKeys.Length; j++)
                {
                    Keys key = pressedKeys[j];
                    if (key == Keys.Escape) escPressed = true;
                }
                if (escPressed && escHeldTimer <= 60) escHeldTimer++;
                if (!escPressed && escHeldTimer > 0) escHeldTimer--;
                if (escHeldTimer > 60) MyWorld.creditsCounter = creditsLength - 1;

                MyWorld.creditsCounter++;
                if (MyWorld.creditsCounter > creditsLength)
                {
                    screenTransition = true;
                    if (MyWorld.creditsCounter - creditsLength == screenTransDuration / 2)
                    {
                        MyWorld.credits = false;
                        MyWorld.creditsCounter = 0;
                        Player.hideMisc[0] = false;
                        Player.hideMisc[1] = false;
                        desiredScPos = Player.Center;
                        Player.Center = playerStartPos;
                        Main.time = startTime;
                        Main.dayTime = startDayTime;
                    }
                }
            }
            else
            {
                MyWorld.creditsCounter = 0;
                escHeldTimer = 0;
            }
            if (screenTransition)
            {
                screenTransTimer += (float)(Math.PI / screenTransDuration);
                screenTransAlpha = (float)Math.Sin(screenTransTimer);
                if (screenTransTimer >= Math.PI)
                {
                    screenTransTimer = 0;
                    screenTransition = false;
                }
            }
            #region PROMPTS!!
            if (!GetInstance<Config>().promptsDisabled)
            {
                bool activeBoss = false;
                bool inTown = false;
                for (int i = 0; i < Main.npc.Length; ++i)
                {
                    NPC nPC = Main.npc[i];

                    if (nPC.townNPC && nPC.active && Vector2.Distance(Player.Center, nPC.Center) <= 2000)
                    {
                        inTown = true;
                    }
                    if (nPC.boss && nPC.active)
                    {
                        activeBoss = true;
                    }
                }
                bool underground = Player.Center.Y / 16 > Main.maxTilesY * 0.225;

                if (!activeBoss)
                {
                    // only happens after 30 minutes
                    if (MyWorld.desertPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        Player.AddBuff(BuffType<ScorpionBreakout>(), 60);
                        // spawn code in the scorpion code
                    }
                    if (MyWorld.firePrompt > ElementsAwoken.bossPromptDelay)    
                    {
                        Player.AddBuff(BuffType<InfernacesWrath>(), 60);
                        if (!underground)
                        {
                            float num13 = MathHelper.Lerp(0.2f, 0.35f, 0.5f);
                            float num14 = MathHelper.Lerp(0.5f, 0.7f, 0.5f);
                            Vector2 speed = new Vector2(Main.windSpeedCurrent * 10f, 3f);
                            if (Main.rand.Next(3) == 0 && !GetInstance<Config>().lowDust)
                            {
                            }
                            if (Main.rand.Next(120) == 0 && !inTown)
                            {
                                int add2 = Main.rand.Next(1000, 2000); // y
                                int add1 = 0;
                                float ai0 = Main.rand.NextFloat(1, 2); // x

                                int choice = Main.rand.Next(2);
                                if (choice == 0)
                                {
                                    add1 = Main.rand.Next(650, 1500);
                                }
                                if (choice == 1)
                                {
                                    add1 = Main.rand.Next(-1500, -650);
                                }

                                int npc = NPC.NewNPC(source, (int)Player.Center.X + add1, (int)Player.Center.Y - add2, NPCType<SolarFragment>(), Player.whoAmI); // type 519

                                Vector2 newVelocity = -Vector2.UnitY.RotatedByRandom(0.78539818525314331) * (7f + Main.rand.NextFloat() * 5f);
                                Main.npc[npc].velocity = newVelocity;
                                if (!Main.expertMode)
                                {
                                    Main.npc[npc].damage = 30;
                                }
                                else
                                {
                                    Main.npc[npc].damage = 60;
                                }
                            }
                        }
                    }
                    if (MyWorld.skyPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        Player.AddBuff(BuffType<DarkenedSkies>(), 60);
                        if (!underground)
                        {
                            if (Main.raining && Main.rainTime > 0 && Main.rand.Next(250) == 0 && !inTown)
                            {
                                int add2 = Main.rand.Next(1000, 2000);
                                int add1 = 0;
                                float ai0 = Main.rand.NextFloat(1, 2);

                                int choice = Main.rand.Next(2);
                                if (choice == 0)
                                {
                                    add1 = Main.rand.Next(300, 1500);
                                }
                                if (choice == 1)
                                {
                                    add1 = Main.rand.Next(-1500, -300);
                                }
                                Projectile.NewProjectile(source, Player.Center.X + add1, Player.Center.Y - add2, 0f, 6f, 580, 40, 10f, Player.whoAmI, ai0, 0.0f);
                            }
                        }
                    }
                    if (MyWorld.frostPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        hypoChillTimer--;
                        //dividing by 16 gets the TILE position
                        Point topLeft = ((Player.position - new Vector2(112f, 112f)) / 16).ToPoint();
                        Point bottomRight = ((Player.BottomRight + new Vector2(112f, 112f)) / 16).ToPoint();

                        bool nearHotTile = false;
                        for (int i = topLeft.X; i <= bottomRight.X; i++)
                        {
                            for (int j = topLeft.Y; j <= bottomRight.Y; j++)
                            {
                                Tile t = Framing.GetTileSafely(i, j);
                                if (HotTile(t) || (t.LiquidType == LiquidID.Lava))
                                {
                                    nearHotTile = true;
                                }
                            }
                        }
                        if (!nearHotTile)
                        {
                            Player.AddBuff(BuffType<Hypothermia>(), 30);
                            if (hypoChillTimer <= 0)
                            {
                                Player.AddBuff(BuffID.Chilled, Main.rand.Next(60, 600));
                                hypoChillTimer = Main.rand.Next(900, 1200);
                            }
                        }
                        if (MyWorld.hailStormTime > 0 && !Player.ZoneDesert)
                        {
                            if (!underground)
                            {
                                Player.AddBuff(BuffID.WindPushed, 60);
                                if (Main.rand.Next(5) == 0)
                                {
                                    Vector2 speed = new Vector2(Main.windSpeedCurrent * 12f, 14f);
                                    int damage = Main.expertMode ? 30 : 45;
                                    Projectile.NewProjectile(source, Player.Center.X + Main.rand.Next(-2000, 2000), Player.Center.Y - 1000, speed.X * 8, speed.Y, ProjectileType<HailPellet>(), damage, 10f, Player.whoAmI);
                                }
                            }
                        }
                    }
                    if (MyWorld.waterPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        Player.AddBuff(BuffType<StormSurge>(), 60);

                        if (!Main.raining && Main.rand.Next(5000) == 0)
                        {
                            Main.raining = true;
                            Main.rainTime = 18000;
                        }

                        if (!underground)
                        {
                            if (Player.ZoneBeach)
                            {
                                if (Main.rand.Next(100) == 0)
                                {
                                    float ai0 = Main.rand.NextFloat(1, 2);
                                    int add1 = 0;
                                    int choice = Main.rand.Next(2);
                                    if (choice == 0)
                                    {
                                        add1 = Main.rand.Next(750, 2000);
                                    }
                                    if (choice == 1)
                                    {
                                        add1 = Main.rand.Next(-2000, -750);
                                    }
                                    Projectile.NewProjectile(source, Player.Center.X + add1, Player.Center.Y - 300, 0f, 6f, ProjectileType<WaternadoBolt>(), 90, 10f, Player.whoAmI, ai0, 0.0f);
                                }
                            }
                            else if (!inTown)
                            {
                                if (Main.rand.Next(3000) == 0)
                                {
                                    float ai0 = Main.rand.NextFloat(1, 2);
                                    int add1 = 0;
                                    int choice = Main.rand.Next(2);
                                    if (choice == 0)
                                    {
                                        add1 = Main.rand.Next(750, 2000);
                                    }
                                    if (choice == 1)
                                    {
                                        add1 = Main.rand.Next(-2000, -750);
                                    }
                                    Projectile.NewProjectile(source, Player.Center.X + add1, Player.Center.Y - 300, 0f, 6f, ProjectileType<WaternadoBolt>(), 90, 10f, Player.whoAmI, ai0, 0.0f);
                                }
                            }
                        }
                    }
                    if (MyWorld.voidPrompt > ElementsAwoken.bossPromptDelay)
                    {
                        Player.AddBuff(BuffType<Psychosis>(), 60);

                        string Void = this.GetLocalization("Prompts.void").Value;
                        string Void1 = this.GetLocalization("Prompts.void1").Value;
                        string Void2 = this.GetLocalization("Prompts.void2").Value;
                        string Void3 = this.GetLocalization("Prompts.void3").Value;
                        string Void4 = this.GetLocalization("Prompts.void4").Value;
                        string Void5 = this.GetLocalization("Prompts.void5").Value;

                        if (Main.rand.Next(20) == 0)
                        {
                            int num1 = Dust.NewDust(Player.position, Player.width, Player.height, 14);
                            Main.dust[num1].scale = 1.5f;
                            Main.dust[num1].velocity *= 3f;
                            Main.dust[num1].noGravity = true;
                        }

                        if (Main.rand.Next(2000) == 0)
                        {
                            int choice = Main.rand.Next(12);
                            if (choice == 0)
                            {
                                Main.NewText(Void, Color.Purple.R, Color.Purple.G, Color.Purple.B);
                                Main.NewText(Void1, Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B);
                                SoundEngine.PlaySound(SoundID.Roar, Player.Center);
                            }
                            else if (choice == 1) Main.NewText(Void2, Color.PaleGreen.R, Color.PaleGreen.G, Color.PaleGreen.B);
                            else if (choice == 2) Main.NewText(Void3, Color.Red.R, Color.Red.G, Color.Red.B);
                            else if (choice == 3) Main.NewText(Void4, Color.Red.R, Color.Red.G, Color.Red.B);
                            else if (choice == 4) SoundEngine.PlaySound(SoundID.NPCDeath62, Player.Center);
                            else if (choice == 5) SoundEngine.PlaySound(SoundID.NPCDeath59, Player.Center);
                            else if (choice == 6) SoundEngine.PlaySound(SoundID.NPCDeath51, Player.Center);
                            else if (choice == 7) SoundEngine.PlaySound(SoundID.Roar, Player.Center);
                            else if (choice == 8) SoundEngine.PlaySound(SoundID.ScaryScream, Player.Center);
                            else if (choice == 9) SoundEngine.PlaySound(SoundID.Zombie105, Player.Center);
                            else if (choice == 10) SoundEngine.PlaySound(SoundID.Zombie104, Player.Center);
                            else if (choice == 11)
                            {
                                int guide = NPC.FindFirstNPC(NPCID.Guide);
                                if (guide >= 0 && Main.rand.Next(5) == 0)
                                {
                                    Main.NewText(Main.npc[guide].GivenName + Void5, Color.Red.R, Color.Red.G, Color.Red.B);
                                }
                            }
                        }
                        if (Main.rand.Next(1500) == 0)
                        {
                            int add1 = 0;
                            int choice = Main.rand.Next(2);
                            if (choice == 0) add1 = Main.rand.Next(750, 2000);
                            else if (choice == 1) add1 = Main.rand.Next(-2000, -750);

                            int type = 0;
                            int choice2 = Main.rand.Next(6);
                            if (choice2 == 0) type = 524; // ghoul
                            else if (choice2 == 1) type = 524; // ghoul
                            else if (choice2 == 2) type = 258; // ladybug
                            else if (choice2 == 3) type = 93; // giant bat
                            else if (choice2 == 4) type = 78; // mummy
                            else if (choice2 == 5) type = 34; // cursed skull
                            NPC npc = Main.npc[NPC.NewNPC(source, (int)Player.Center.X + add1, (int)Player.Center.Y - 800, type)];
                            npc.color = new Color(66, 66, 66);
                            npc.alpha = 200;
                            npc.damage = 0;
                            npc.lifeMax = 10;
                            npc.life = 10;
                            npc.DeathSound = SoundID.NPCDeath6;
                        }
                    }
                    
                }
            }  
            #endregion
        }
        private static bool HotTile(Tile t)
        {
            if (t.TileType == TileID.Campfire ||
                t.TileType == TileID.Fireplace) return true;
            return false;
        }
        public override void PostUpdate()
        {
            if (skylineFlying)
            {
                if (skylineAlpha < 1) skylineAlpha += 0.05f;
            }
            else
            {
                if (skylineAlpha > 0) skylineAlpha -= 0.05f;
            }
        }
        public override void PostUpdateBuffs()
        {
            if (oiniteStatue)
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (Player.buffTime[l] <= 0)
                    {
                        oiniteDoubledBuff[l] = false;
                        continue;
                    }
                    if (!oiniteDoubledBuff[l])
                    {
                        if (Player.buffType[l] != BuffID.PotionSickness &&
                            Player.buffType[l] != BuffID.ManaSickness &&
                            !Main.buffNoTimeDisplay[l])
                            Player.buffTime[l] *= 2;
                        oiniteDoubledBuff[l] = true;      
                    }
                }
            }
            else
            {
                for (int l = 0; l < Player.MaxBuffs; l++)
                {
                    if (oiniteDoubledBuff[l])
                    {
                        if (Player.buffType[l] != BuffID.PotionSickness &&
                            Player.buffType[l] != BuffID.ManaSickness &&
                            !Main.buffNoTimeDisplay[l])
                            Player.buffTime[l] /= 2;
                        oiniteDoubledBuff[l] = false; 
                    }
                }
            }
        }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath)
            {
                yield return new Item(ItemType<ElementalCapsule>());
                yield return new Item(ItemType<MysticGemstone>());
                yield return new Item(ItemType<VoidbloodHeart>());
            }
        }
        public override void OnEnterWorld()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                string Music = this.GetLocalization("UponEnteringTheWorld.Music").Value;
                string Music1 = this.GetLocalization("UponEnteringTheWorld.Music1").Value; 
                string Music2 = this.GetLocalization("UponEnteringTheWorld.Music2").Value; 
                if (!InstalledPack())
                {
                    Main.NewText(Music, Color.Red.R, Color.Red.G, Color.Red.B);
                    Main.NewText(Music1, Color.Purple.R, Color.Purple.G, Color.Purple.B);
                    Main.NewText(Music2, Color.Purple.R, Color.Purple.G, Color.Purple.B);
                }
                //Mod yabhb = ModLoader.GetMod("ExtensibleInventory");
                //if (yabhb != null)
                //{
                //    Main.NewText("You have 'Extensible Inventory' enabled. The energy system will not work with this mod.", Color.Red.R, Color.Red.G, Color.Red.B);
                //}
            }
            observerChanceTimer = 0;
            toySlimeChanceTimer = 0;

            encounterTextAlpha = 0;
            encounterTextTimer = 0;
        }

        private bool InstalledPack()
        {
            //if (ElementsAwoken.eaMusicEnabled)
            //{
            //    return true;
            //}
            //if (ElementsAwoken.eaRetroMusicEnabled)
            //{
            //    return true;
            //}
            return false;
        }
        public override void PostItemCheck()
        {
            var source = Main.LocalPlayer.GetSource_FromThis();
            Item item = Player.inventory[Player.selectedItem];
            if (item.type == ItemType<LavaLeecher>() || item.type == ItemType<FireFlooder>())
            {
                if (!Main.GamepadDisableCursorItemIcon)
                {
                    Player.cursorItemIconEnabled = true;
                    Main.ItemIconCacheUpdate(item.type);
                }
                if (Player.itemTime == 0 && Player.itemAnimation > 0 && Player.controlUseItem)
                {
                    if (item.type == ItemType<LavaLeecher>() && Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidType == 1)
                    {
                        int num233 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidType;
                        int num234 = 0;
                        for (int num235 = Player.tileTargetX - 1; num235 <= Player.tileTargetX + 1; num235++)
                        {
                            for (int num236 = Player.tileTargetY - 1; num236 <= Player.tileTargetY + 1; num236++)
                            {
                                if ((int)Main.tile[num235, num236].LiquidType == num233)
                                {
                                    num234 += (int)Main.tile[num235, num236].LiquidAmount;
                                }
                            }
                        }
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidAmount > 0 && (num234 > 100 || item.type == ItemType<LavaLeecher>()))
                        {
                            int liquidType = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidType;

                            SoundEngine.PlaySound(SoundID.SplashWeak, Player.position);
                            Player.itemTime = item.useTime;
                            int num237 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidAmount;
                            Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidAmount = 0;
                            Tile tile1 = Main.tile[Player.tileTargetX, Player.tileTargetY];
                            tile1.LiquidType = LiquidID.Lava;//??(false);
                            Tile tile2 = Main.tile[Player.tileTargetX, Player.tileTargetY];
                            tile2.LiquidType = LiquidID.Honey;//??(false);
                            WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, false);
                            if (Main.netMode == 1)
                            {
                                NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                            }
                            else
                            {
                                Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);
                            }
                            for (int xPos = Player.tileTargetX - 1; xPos <= Player.tileTargetX + 1; xPos++)
                            {
                                for (int yPos = Player.tileTargetY - 1; yPos <= Player.tileTargetY + 1; yPos++)
                                {
                                    if (num237 < 256 && (int)Main.tile[xPos, yPos].LiquidType == num233)
                                    {
                                        int num240 = (int)Main.tile[xPos, yPos].LiquidAmount; // which liquid type
                                        if (num240 + num237 > 255)
                                        {
                                            num240 = 255 - num237;
                                        }
                                        num237 += num240;
                                        Tile tile = Main.tile[xPos, yPos];
                                        tile.LiquidAmount -= (byte)num240;
                                        tile.LiquidType = liquidType;
                                        if (Main.tile[xPos, yPos].LiquidAmount == 0)
                                        {
                                            tile.LiquidType = LiquidID.Lava; //(false);
                                            tile.LiquidType = LiquidID.Honey; //(false);
                                        }
                                        WorldGen.SquareTileFrame(xPos, yPos, false);
                                        if (Main.netMode == 1)
                                        {
                                            NetMessage.sendWater(xPos, yPos);
                                        }
                                        else
                                        {
                                            Liquid.AddWater(xPos, yPos);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (item.type == ItemType<FireFlooder>() && Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidAmount < 200 && (!Main.tile[Player.tileTargetX, Player.tileTargetY].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].TileType] || Main.tileSolidTop[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].TileType]))
                    {
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidAmount == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidType == 1)
                        {
                            SoundEngine.PlaySound(SoundID.SplashWeak, Player.position);
                            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
                            tile.LiquidType = 1;
                            Main.tile[Player.tileTargetX, Player.tileTargetY].LiquidAmount = 255;
                            WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
                            Player.itemTime = item.useTime;
                            if (Main.netMode == 1)
                            {
                                NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                            }
                        }
                    }
                }
            }
            if (item.type == 1991 || item.type == 3183)
            {
                Rectangle rectangle = new Rectangle((int)Player.itemLocation.X, (int)Player.itemLocation.Y, 32, 32);

                for (int n = 0; n < Main.npc.Length; n++)
                {
                    NPC bunny = Main.npc[n];
                    if (bunny.active && bunny.type == NPCType<MysticBunny>())
                    {
                        Rectangle value10 = new Rectangle((int)bunny.position.X, (int)bunny.position.Y, bunny.width, bunny.height);
                        if (rectangle.Intersects(value10) && (bunny.noTileCollide || Player.CanHit(bunny)))
                        {
                            if (!bunny.active)
                            {
                                return;
                            }
                            if (Main.rand.Next(29) == 0)
                            {
                                new Item().SetDefaults(ItemType<PiohsPresent>(), false);
                                Item.NewItem(source, (int)Player.Center.X, (int)Player.Center.Y, 0, 0, ItemType<PiohsPresent>(), 1, false, 0, true, false);
                            }
                            else if (Main.rand.Next(9) == 0)
                            {
                                new Item().SetDefaults(ItemID.FuzzyCarrot, false);
                                Item.NewItem(source, (int)Player.Center.X, (int)Player.Center.Y, 0, 0, ItemID.FuzzyCarrot, 1, false, 0, true, false);
                            }
                            else
                            {
                                Vector2 vector = bunny.Center - new Vector2(20f);
                                Utils.PoofOfSmoke(vector);
                                NetMessage.SendData(106, -1, -1, null, (int)vector.X, vector.Y, 0f, 0f, 0, 0, 0);
                            }
                            bunny.active = false;
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n, 0f, 0f, 0f, 0, 0, 0);
                        }
                    }
                }
            }
        }
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (Main.rand.NextBool(3) && attempt.inHoney) itemDrop = ItemType<MajesticHivefish>();
            if (Main.rand.Next(100) < (10 + (Player.cratePotion ? 10 : 0)))
            {
                if (attempt.inHoney) itemDrop = ItemType<HiveCrate>();
            }
        }
        private void CreditsScroll(int screenNum, Vector2 scroll, int screenDuration)
        {
            int counterN = screenDuration * (screenNum - pointsNotFound);
            if (MyWorld.creditsCounter == counterN) screenTransition = true;
            if (MyWorld.creditsCounter - counterN == screenTransDuration / 2) desiredScPos = creditPoints[screenNum];
            if (MyWorld.creditsCounter - counterN > screenTransDuration / 2) Player.Center = desiredScPos;
            if (MyWorld.creditsCounter - counterN > screenTransDuration / 2 && MyWorld.creditsCounter > counterN && MyWorld.creditsCounter < screenDuration * (screenNum + 1) + screenTransDuration / 2) desiredScPos += scroll;
        }
        private void ComputerText()
        {
            string ComputerText = this.GetLocalization("ComputerText.Text").Value;
            string ComputerText1 = this.GetLocalization("ComputerText.Text1").Value; 
            string ComputerText2 = this.GetLocalization("ComputerText.Text2").Value;
            string ComputerText3 = this.GetLocalization("ComputerText.Text3").Value;
            string ComputerText4 = this.GetLocalization("ComputerText.Text4").Value;
            string ComputerText5 = this.GetLocalization("ComputerText.Text5").Value;
            string ComputerText6 = this.GetLocalization("ComputerText.Text6").Value;
            string ComputerText7 = this.GetLocalization("ComputerText.Text7").Value;
            string ComputerText8 = this.GetLocalization("ComputerText.Text8").Value;
            string ComputerText9 = this.GetLocalization("ComputerText.Text9").Value;
            string ComputerText10 = this.GetLocalization("ComputerText.Text10").Value;
            string ComputerText11 = this.GetLocalization("ComputerText.Text11").Value;
            string ComputerText12 = this.GetLocalization("ComputerText.Text12").Value;
            string ComputerText13 = this.GetLocalization("ComputerText.Text13").Value;
            string ComputerText14 = this.GetLocalization("ComputerText.Text14").Value;
            string ComputerText15 = this.GetLocalization("ComputerText.Text15").Value;
            string ComputerText16 = this.GetLocalization("ComputerText.Text16").Value;
            string ComputerText17 = this.GetLocalization("ComputerText.Text17").Value;

            int num16 = (int)(((double)Player.position.X + (double)Player.width * 0.5) / 16.0);
            int num17 = (int)(((double)Player.position.Y + (double)Player.height * 0.5) / 16.0);
            if (num16 < computerPos.X - Player.tileRangeX || num16 > computerPos.X + Player.tileRangeX + 1 || num17 < computerPos.Y - Player.tileRangeY || num17 > computerPos.Y + Player.tileRangeY + 1)
            {
                inComputer = false;
            }
            if (Main.playerInventory == true || Player.sign != -1 || Player.talkNPC != -1)
            {
                inComputer = false;
            }
            switch (computerTextNo)
            {
                case 0:
                    //no drive
                    computerText = ComputerText;
                    break;
                case 1:
                    //wasteland
                    computerText = ComputerText1;
                    break;
                case 2:
                    //infernace
                    computerText = ComputerText2;
                    break;
                case 3:
                    //scourge fighter
                    computerText = ComputerText3;
                    break;
                case 4:
                    //regaroth
                    computerText = ComputerText4;
                    break;
                case 5:
                    //the celestial
                    computerText = ComputerText5;
                    break;
                case 6:
                    //obsidious
                    computerText = ComputerText6;
                    break;
                case 7:
                    //permafrost
                    computerText = ComputerText7;
                    break;
                case 8:
                    //aqueous
                    computerText = ComputerText8;
                    break;
                case 9:
                    //the guardian
                    if (guardianEntryNo == 0)
                    {
                        computerText = ComputerText9;
                    }
                    else
                    {
                        computerText = ComputerText10;
                    }
                    break;
                case 10:
                    //volcanox
                    computerText = ComputerText11;
                    break;
                case 11:
                    //void leviathan
                    computerText = ComputerText12;
                    break;
                case 12:
                    //azana
                    if (azanaEntryNo == 0)
                    {
                        computerText = ComputerText13;
                    }
                    else
                    {
                        computerText = ComputerText14;
                    }
                    break;
                case 13:
                    //ancients
                    if (ancientsEntryNo == 0)
                    {
                        computerText = ComputerText15;
                    }
                    else
                    {
                        computerText = ComputerText16;
                    }
                    break;
                default:
                    computerText = ComputerText17;
                    return;
            }
        }
        public override void FrameEffects()
        {
            if (Player.mount.Active && Player.mount.Type == MountType<ElementalDragonBunny>() && Math.Abs(Player.velocity.X) > Player.mount.DashSpeed - Player.mount.RunSpeed / 3f)
            {
                Player.armorEffectDrawShadow = true;
            }
            if (eaDashDelay > 0)
            {
                Player.armorEffectDrawShadow = true;
            }
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (acidBurn)
            {
                r *= 0.69f;
                g *= 1f;
                b *= 0.48f;
            }
            if (toySlimeClawSliding)
            {
                Player.bodyFrame.Y = Player.bodyFrame.Height * 3;
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (elementalArmor && !elementalArmorCooldown)
            {
                SoundEngine.PlaySound(SoundID.Item113, Player.position);
                Projectile.NewProjectile(Main.LocalPlayer.GetSource_FromThis(), Player.position.X, Player.position.Y, 0f, 0f, ProjectileType<AsteroxShieldBase>(), 0, 10f, Player.whoAmI, 0.0f, 0.0f);
                int life = Player.statLifeMax / 2;
                Player.statLife += life;
                Player.HealEffect(life);
                Player.AddBuff(BuffType<ElementalArmorCooldown>(), 2700);
                return false;
            }
            if (noRespawnTime)
            {
                Player.statLife += Player.statLifeMax;
                return false;
            }
            if (Player.HasItem(ItemType<DeathMirror>()))
            {
                hellsReflectionTimer = 1800;
            }
            return true;
        }
        public override void PreUpdate()
        {
            if (forceWisp)
            {
                Player.mount.SetMount(MountType<WispForm>(), Player, false);
                Player.releaseMount = false;
            }
            if (glassHeart)
            {
                Player.immune = false;
                Player.immuneTime = 0;
                Player.shadowDodgeTimer = 0;
                Player.shadowDodge = false;
                Player.shadowDodgeCount = 0;
            }
            if (oiniteDoubledBuff.Length != Player.MaxBuffs)
            {
                Array.Resize(ref oiniteDoubledBuff, Player.MaxBuffs);
            }
            if (discordPotBuff.Length != Player.MaxBuffs)
            {
                Array.Resize(ref discordPotBuff, Player.MaxBuffs);
            }
        }
        public override void UpdateBadLifeRegen()
        {
            neovirtuoTimer--;

            if (boostDriveTimer > 0 && boostDrive == 2)
            {
                if (Math.Abs(Player.velocity.X) > 25)
                {
                    Player.lifeRegen -= (int)MathHelper.Lerp(0, 30, (Math.Abs(Player.velocity.X) - 25) / 5);
                }
            }
            if (acidBurn) Player.lifeRegen -= 16;
            if (toySlimed > 0) Player.lifeRegen -= 18;
            if (dragonfire || starstruck) Player.lifeRegen -= 20;
            if (extinctionCurse || handsOfDespair)  Player.lifeRegen -= 30;
            if (chaosBurn || discordDebuff)Player.lifeRegen -= 40;
            if (behemothGazeTimer > 600)
            {
                int amount = (int)MathHelper.Lerp(0, 80, (float)(leviathanDist - 3000) / 9000f);
                Player.lifeRegen -= amount;
            }
            if (endlessTears)  Player.velocity *= 0.8f;
            if (iceBound)
            {
                Player.velocity.Y = 0f;
                Player.velocity.X = 0f;
            }
            if (cantFly)
            {
                if (Player.wingTimeMax <= 0)
                {
                    Player.wingTimeMax = 0;
                }
                Player.wingTimeMax = 0;
                Player.wingTime = 0;
            }
            if (Player.lifeRegen < 0 && theAntidote)
            {
                Player.lifeRegen /= 2;
            }
        }
        public override void UpdateLifeRegen()
        {
            if (MyWorld.credits) Player.lifeRegen = 0;      
            if (archaicProtectionTimer > 0) Player.lifeRegen = 0;
            if (voidBlood && Player.lifeRegen > 0) Player.lifeRegen = 0;
        }
        public override void NaturalLifeRegen(ref float regen)
        {
            if (voidBlood && regen > 0) regen = 0;
        }
        //public override void UpdateBiomeVisuals()
        //{
        //    bool useLeviathan = NPC.AnyNPCs(mod.NPCType("VoidLeviathanHead"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:VoidLeviathanHead", useLeviathan);

        //    bool useInfernace = NPC.AnyNPCs(mod.NPCType("Infernace"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Infernace", useInfernace);

        //    bool usePermafrost = NPC.AnyNPCs(mod.NPCType("Permafrost"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Permafrost", usePermafrost);

        //    bool useGuardian = NPC.AnyNPCs(mod.NPCType("TheGuardianFly"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:TheGuardianFly", useGuardian);

        //    bool useVolcanox = NPC.AnyNPCs(mod.NPCType("Volcanox"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Volcanox", useVolcanox);

        //    bool useAzana = NPC.AnyNPCs(mod.NPCType("Azana"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Azana", useAzana);

        //    bool useAncients = NPC.AnyNPCs( ModContent.NPCType<Izaris>()) || NPC.AnyNPCs(mod.NPCType("Kirvein")) || NPC.AnyNPCs(ModContent.NPCType<Krecheus>()) || NPC.AnyNPCs(ModContent.NPCType<Xernon>()) || NPC.AnyNPCs(mod.NPCType("AncientAmalgam"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Ancients", useAncients);

        //    bool useVoidEvent = MyWorld.voidInvasionUp && Main.time <= 16220 && !Main.dayTime;
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:VoidEvent", useVoidEvent);
        //    bool useVoidEventDark = MyWorld.voidInvasionUp && Main.time > 16220 && !Main.dayTime;
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:VoidEventDark", useVoidEventDark);

        //    bool useRadRain = MyWorld.radiantRain && player.position.Y / 16 < Main.worldSurface;
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:RadiantRain", useRadRain);

        //    int useRegaroth = 0;
        //    for (int i = 0; i < Main.npc.Length; ++i)
        //    {
        //        if (Main.npc[i].active && Main.npc[i].type == mod.NPCType("RegarothHead"))
        //        {
        //            if (Main.npc[i].life > Main.npc[i].lifeMax / 2)
        //            {
        //                useRegaroth = 1;
        //                if (Main.npc[i].localAI[1] == 1)
        //                {
        //                    useRegaroth = 3;
        //                }
        //            }
        //            else
        //            {
        //                useRegaroth = 2;
        //                if (Main.npc[i].localAI[1] == 1)
        //                {
        //                    useRegaroth = 4;
        //                }
        //            }

        //        }
        //    }

        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Regaroth", useRegaroth == 1);
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Regaroth2", useRegaroth == 2);

        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:RegarothIntense", useRegaroth == 3);
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Regaroth2Intense", useRegaroth == 4);

        //    bool useEncounter1 = ElementsAwoken.encounter == 1;
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter1", useEncounter1);
        //    bool useEncounter2 = ElementsAwoken.encounter == 2;
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter2", useEncounter2);
        //    bool useEncounter3 = ElementsAwoken.encounter == 3;
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Encounter3", useEncounter3);

        //    bool useDespair = voidEnergyTimer > 0 || voidWalkerAura > 0;
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Despair", useDespair);



        //    //Point point = player.Center.ToTileCoordinates();
        //    bool useblizzard = MyWorld.hailStormTime > 0 && player.ZoneOverworldHeight && !player.ZoneDesert && !ActiveBoss() && !GetInstance<Config>().lowDust;
        //    player.ManageSpecialBiomeVisuals("Blizzard", useblizzard, default(Vector2));

        //        bool useInfWrath = MyWorld.firePrompt > ElementsAwoken.bossPromptDelay && !ActiveBoss() && !GetInstance<Config>().promptsDisabled;
        //        player.ManageSpecialBiomeVisuals("ElementsAwoken:AshShader", useInfWrath);
        //        player.ManageSpecialBiomeVisuals("ElementsAwoken:AshBlizzardEffect", useInfWrath && player.position.Y / 16 < Main.worldSurface);

        //    if (useInfWrath)
        //    {
        //        SkyManager.Instance.Activate("ElementsAwoken:InfernacesWrath", player.Center);
        //        if (!GetInstance<Config>().lowDust) Overlays.Scene.Activate("ElementsAwoken:AshParticles", player.Center);
        //    }
        //    else
        //    {
        //        SkyManager.Instance.Deactivate("ElementsAwoken:InfernacesWrath");
        //        Overlays.Scene.Deactivate("ElementsAwoken:AshParticles");
        //    }
        //    if (GetInstance<Config>().lowDust) Overlays.Scene.Deactivate("ElementsAwoken:AshParticles");
        //    NPC aqueous = null;
        //    for (int i = 0; i < Main.npc.Length; ++i)
        //    {
        //        NPC npc = Main.npc[i];
        //        if (npc.active && npc.type == mod.NPCType("Aqueous"))
        //        {
        //            aqueous = npc;
        //            break;
        //        }
        //    }

        //    bool useAqueous = NPC.AnyNPCs(mod.NPCType("Aqueous"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:Aqueous", useAqueous);
        //    if (aqueous != null)
        //    {
        //        bool useAqueousSky = aqueous.life <= aqueous.lifeMax * 0.65f;
        //        if (useAqueousSky)
        //        {
        //            SkyManager.Instance.Activate("ElementsAwoken:AqueousSky", player.Center);
        //        }
        //        else
        //        {
        //            SkyManager.Instance.Deactivate("ElementsAwoken:AqueousSky");
        //        }
        //    }
        //    else
        //    {
        //        SkyManager.Instance.Deactivate("ElementsAwoken:AqueousSky");
        //    }

        //    /*bool useCelestial = NPC.AnyNPCs(mod.NPCType("TheCelestial"));
        //    player.ManageSpecialBiomeVisuals("ElementsAwoken:TheCelestial", useCelestial);*/
        //}
        private bool ActiveBoss()
        {
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].boss)
                {
                    return true;
                }
            }
            return false;
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            float damage = modifiers.SourceDamage.Base;

            if (target.GetGlobalNPC<NPCsGLOBAL>().impishCurse)
            {
                damage = (int)(damage * 1.75f);
            }
            if (fadedCloth)
            {
                float scale = 1.5f;
                if (Main.hardMode) scale = 1.75f;
                if (NPC.downedPlantBoss) scale = 2f;
                if (NPC.downedMoonlord) scale = 4f;
                damage = (int)(damage * scale);
            }
        }
        //public override void ModifyDraswLayers(List<PlayerLayer> layers)
        //{
        //    MiscEffects.visible = true;
        //    layers.Add(MiscEffects);
        //    if ((MyWorld.credits && MyWorld.creditsCounter > screenTransDuration / 2) || wispForm)
        //    {
        //        foreach (PlayerLayer layer in layers)
        //        {
        //            layer.visible = false;
        //        }
        //    }
        //}
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            PlayerUtils playerUtils = Player.GetModPlayer<PlayerUtils>();
            var source = Main.LocalPlayer.GetSource_FromThis();

            if (ancientDecayWeapon)
            {
                target.AddBuff(BuffType<AncientDecay>(), 360, false);
            }
            if (eaMagmaStone)
            {
                if (Main.rand.Next(4) == 0)
                {
                    target.AddBuff(BuffID.OnFire, 360);
                }
                else if (Main.rand.Next(2) == 0)
                {
                    target.AddBuff(BuffID.OnFire, 240);
                }
                else
                {
                    target.AddBuff(BuffID.OnFire, 120);
                }
            }
            if (venomSample || vilePower)
            {
                target.AddBuff(BuffID.Venom, 120);
                target.AddBuff(BuffID.Poisoned, 120);
            }
            if (dragonmailGreathelm)
            {
                target.AddBuff(BuffType<Dragonfire>(), 300, false);
            }
            if (sufferWithMe)
            {
                target.AddBuff(BuffType<ChaosBurn>(), 300, false);
            }
            if (voidWalkerArmor == 1)
            {
                target.AddBuff(BuffType<ExtinctionCurse>(), 300, false);
            }
            if (extinctionCurseImbue)
            {
                target.AddBuff(BuffType<ExtinctionCurse>(), 360, false);
            }
            if (frozenGauntlet)
            {
                target.AddBuff(BuffID.Chilled, 300);
                target.AddBuff(BuffID.Frostburn, 300);
            }
            if (replenishRing)
            {
                if (target.life <= 0)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        int randLife = Main.rand.Next(1, 4);
                        Player.statLife += randLife;
                        Player.HealEffect(randLife);
                    }
                }
            }
            if (neovirtuoBonus && Main.rand.Next(9) == 0)
            {
                if (neovirtuoTimer <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item84, Player.position);
                    int neoDamage = 200;
                    int speed = 8;
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, speed, speed, ProjectileType<NeovirtuoHoming>(), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, speed, -speed, ProjectileType<NeovirtuoHoming>(), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, -speed, speed, ProjectileType<NeovirtuoHoming>(), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, -speed, -speed, ProjectileType<NeovirtuoHoming>(), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                    neovirtuoTimer = 15;
                }
            }
            if (immortalResolve)
            {
                if (crit && immortalResolveCooldown <= 0)
                {
                    int randLife = 0;
                    if (Player.GetCritChance(DamageClass.Magic) < 10 && Player.GetCritChance(DamageClass.Melee) < 10 && Player.GetCritChance(DamageClass.Ranged) < 10 && Player.GetCritChance(DamageClass.Throwing) < 10)
                    {
                        randLife = Main.rand.Next(1, 18);
                    }
                    if (Player.GetCritChance(DamageClass.Magic) >= 10 && Player.GetCritChance(DamageClass.Melee) >= 10 && Player.GetCritChance(DamageClass.Ranged) >= 10 && Player.GetCritChance(DamageClass.Throwing) >= 10 && Player.GetCritChance(DamageClass.Magic) < 25 && Player.GetCritChance(DamageClass.Melee) < 25 && Player.GetCritChance(DamageClass.Ranged) < 25 && Player.GetCritChance(DamageClass.Throwing) < 25)
                    {
                        randLife = Main.rand.Next(1, 15);
                    }
                    if (Player.GetCritChance(DamageClass.Magic) >= 25 && Player.GetCritChance(DamageClass.Melee) >= 25 && Player.GetCritChance(DamageClass.Ranged) >= 25 && Player.GetCritChance(DamageClass.Throwing) >= 25 && Player.GetCritChance(DamageClass.Magic) < 75 && Player.GetCritChance(DamageClass.Melee) < 75 && Player.GetCritChance(DamageClass.Ranged) < 75 && Player.GetCritChance(DamageClass.Throwing) < 75)
                    {
                        randLife = Main.rand.Next(1, 10);
                    }
                    if (Player.GetCritChance(DamageClass.Magic) >= 50 && Player.GetCritChance(DamageClass.Melee) >= 50 && Player.GetCritChance(DamageClass.Ranged) >= 50 && Player.GetCritChance(DamageClass.Throwing) >= 50)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            randLife = Main.rand.Next(1, 10);
                        }
                    }
                    Player.statLife += randLife;
                    Player.HealEffect(randLife);
                    immortalResolveCooldown = 10;
                }
            }
            if (crowsArmor && crowsArmorCooldown <= 0)
            {
                float lightningSpeed = 8f;
                Vector2 spawnpoint = new Vector2(target.Center.X, target.Center.Y - 100);
                float rotation = -(float)Math.Atan2(spawnpoint.X - target.Center.Y, spawnpoint.X - target.Center.X);
                Vector2 speed = new Vector2((float)((Math.Cos(rotation) * lightningSpeed) * -1), (float)((Math.Sin(rotation) * lightningSpeed) * -1));

                Vector2 vector94 = new Vector2(speed.X, speed.Y);
                float ai = (float)Main.rand.Next(100);
                Vector2 vector95 = Vector2.Normalize(vector94) * 2f;
                Projectile.NewProjectile(source, spawnpoint.X, spawnpoint.Y, vector95.X, vector95.Y, ProjectileType<CrowLightning>(), 100, 0f, Main.myPlayer, vector94.ToRotation(), ai);
                Projectile.NewProjectile(source, spawnpoint.X, spawnpoint.Y, 0f, 0f, ProjectileType<CrowStorm>(), 0, 0f, Main.myPlayer);
                crowsArmorCooldown = 30;
            }
            if (cosmicGlass && crit && cosmicGlassCD <= 0)
            {
                if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(Player.Center.Y - target.Center.Y, Player.Center.X - target.Center.X);

                    Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    SoundEngine.PlaySound(SoundID.Item12, Player.position);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, speed.X, speed.Y, ProjectileType<ChargeRifleHalf>(), 30, 3f, Player.whoAmI, 0f);
                    cosmicGlassCD = 3;
                }
            }
            int strikeChance = 10;
            if (NPC.downedBoss3) strikeChance = 7;
            if (Main.hardMode) strikeChance = 5;
            if (NPC.downedPlantBoss) strikeChance = 4;
            if (NPC.downedMoonlord) strikeChance = 2;
            if (strangeUkulele && Main.rand.Next(strikeChance) == 0)
            {
                List<int> availableNPCs = new List<int>();
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (other.active && !other.friendly && other.damage > 0 && !other.dontTakeDamage && Vector2.Distance(other.Center, Player.Center) < 300)
                    {
                        availableNPCs.Add(other.whoAmI);
                    }
                }
                if (availableNPCs.Count > 0)
                {
                    NPC arcTarget = Main.npc[availableNPCs[Main.rand.Next(availableNPCs.Count)]];
                    if (arcTarget.active && !arcTarget.friendly && arcTarget.damage > 0 && !arcTarget.dontTakeDamage)
                    {
                        SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ElectricArcing"), Player.position);

                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(Player.Center.Y - target.Center.Y, Player.Center.X - target.Center.X);
                        rotation += MathHelper.ToRadians(Main.rand.Next(-60, 60));
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, speed.X, speed.Y, ProjectileType<UkuleleArc>(), (int)(item.damage * 0.5f), 3f, Player.whoAmI, arcTarget.whoAmI);
                    }
                }
            }
            if (noDamageCounter > 0)
            {
                noDamageCounter = 0;
            }
            if (bleedingHeart)
            {
                if (target.life <= 0 && playerUtils.enemiesKilledLast10Secs >= 4 && !target.SpawnedFromStatue)
                {
                    Player.AddBuff(BuffType<Bloodbath>(), 600, false);
                }
            }
        }
        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (ancientDecayWeapon && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int num280 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustType<AncientDust>(), Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
                    Main.dust[num280].scale = 1f;
                    Main.dust[num280].noGravity = true;
                }
            }
            if (extinctionCurseImbue && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int num1 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, PinkFlame, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
                    Main.dust[num1].scale = 2f;
                    Main.dust[num1].noGravity = true;
                }
            }
            if (frozenGauntlet && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int num1 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 135, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
                    Main.dust[num1].scale = 2f;
                    Main.dust[num1].noGravity = true;
                }
            }
            if (eaMagmaStone && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic)
            {
                bool makeDust = Main.rand.Next(3) == 0;
                if (GetInstance<Config>().lowDust) makeDust = makeDust = Main.rand.Next(8) == 0;
                if (makeDust)
                {
                    int num311 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 6, Player.velocity.X * 0.2f + (float)(Player.direction * 3), Player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
                    Main.dust[num311].noGravity = true;
                    Dust expr_F239_cp_0_cp_0 = Main.dust[num311];
                    expr_F239_cp_0_cp_0.velocity.X = expr_F239_cp_0_cp_0.velocity.X * 2f;
                    Dust expr_F258_cp_0_cp_0 = Main.dust[num311];
                    expr_F258_cp_0_cp_0.velocity.Y = expr_F258_cp_0_cp_0.velocity.Y * 2f;
                }
            }
        }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            float damage = modifiers.SourceDamage.Base;
            if (greatLensTimer > 0)
            {
                Player.ApplyDamageToNPC(npc, (int)(damage * 0.2f), 2f, Math.Sign(Player.Center.X - npc.Center.X), false);
                damage = (int)(damage * 0.8f);
            }
            if (honeyCocooned > 0)
            {
                npc.velocity.X = Math.Sign(npc.Center.X - Player.Center.X) * 4 * npc.knockBackResist;
                npc.velocity.Y = Math.Sign(npc.Center.Y - (Player.Center.Y + 16)) * 4 * npc.knockBackResist;
                Main.NewText(Math.Sign(npc.Center.Y - Player.Center.Y));
            }
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            float damage = modifiers.SourceDamage.Base;
            if (greatLensTimer > 0)
            {
                damage = (int)(damage * 0.8f);
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            var source = Main.LocalPlayer.GetSource_FromThis();
            float damage = modifiers.SourceDamage.Base;
            if (wispForm)
            {
                modifiers.DisableSound();
                SoundEngine.PlaySound(SoundID.NPCHit5,Player.position);
                for (int i = 0; i < 16; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, wispDust)];
                    dust.noGravity = true;
                    dust.velocity.X = (modifiers.HitDirection * 12) * Main.rand.NextFloat(0.8f,1.2f);
                    dust.velocity.Y = -6f * Main.rand.NextFloat(0.8f, 1.2f);
                }
            }

            damage = (int)(damage * damageTaken);

            if (honeyCocooned> 0)
            {
                if (!Player.immune)
                {
                    honeyCocoonDamage += (int)damage;
                    CombatText.NewText(Player.getRect(), Color.Orange, honeyCocoonDamage);
                    if (honeyCocoonDamage >= 100)
                    {
                        honeyCocooned = 0;
                        SoundEngine.PlaySound(SoundID.NPCDeath1, Player.position);

                        Vector2 pos = Player.Center;
                        int numDusts = 36;
                        for (int i = 0; i < numDusts; i++)
                        {
                            Vector2 position = Vector2.One.RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + pos;
                            Vector2 velocity = position - pos;
                            Vector2 spawnPos = position + velocity;
                            Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, 153, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1f)];
                            dust.noGravity = true;
                            dust.noLight = true;
                            dust.velocity = Vector2.Normalize(velocity) * 6f * Main.rand.NextFloat(0.8f, 1.2f);
                        }
                    }
                    SoundEngine.PlaySound(SoundID.NPCHit1.WithPitchOffset(-0.2f), Player.position);
                }
                Player.immuneTime = 30;
                Player.immune = true;
                return;
            }
            if (scourgeDrive)
            {
                if (scourgeSpeed)
                {
                    explosionEffect(Player, ProjectileType<Explosion>(), 100, 0f, PinkFlame);
                }
            }
            if (mechArmor && mechArmorCD <= 0)
            {
                int numLightning = 0;
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    NPC other = Main.npc[k];
                    if (other.CanBeChasedBy(this) && Vector2.Distance(other.Center, Player.Center) < 500)
                    {
                        float Speed = 6f;
                        float rotation = (float)Math.Atan2(Player.Center.Y - other.Center.Y, Player.Center.X - other.Center.X);

                        Vector2 projVel = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, projVel.X, projVel.Y, ProjectileType<MechLightning>(), 200, 0f, Main.myPlayer, 0f, 0f);
                        numLightning++;
                        if (numLightning > 3) break;
                    }
                }
                mechArmorCD = 20;
            }
            if (Player.FindBuffIndex(BuffType<ChaosShield>()) != -1 && !Player.dead)
            {
                chaosBoost += damage;
                damage /= 5;
            }
            if (oceanicArmor)
            {
                if (damage > 25)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        float speed = Main.rand.Next(-6, 6);
                        Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, speed, speed, ProjectileType<PoisonWater>(), 60, 1.25f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            if (viridiumDash)
            {
                explosionEffect(Player, ProjectileType<Explosion>(), 1000, 10f, 21);
                return;
            }
            if (aegisDashTimer > 0)
            {
                damage = (int)(damage * 0.1f);
            }
            if (voidEnergyTimer > 0)
            {
                damage = (int)(damage * 0.5f);
            }
            if (flingToShackle)
            {
                return;
            }
            if (icyHeart)
            {
                damage = (int)(damage * icyHeartDR);
                icyHeartTimer = -600;
                if (damage == 0) return;
            }
            if (starstruck)
            {
                damage = (int)(damage * MathHelper.Lerp(1, 4, (float)starstruckCounter / 20));
            }
            if (hellFury) damage *= 2;
            if (glassHeart)
            {
                modifiers.DisableSound();
                SoundEngine.PlaySound(SoundID.Shatter, Player.position);
                NetworkText DeathReason = NetworkText.FromLiteral(Player.name + " " + this.GetLocalization("PlayerDeath.Death").Value);
                Player.KillMe(PlayerDeathReason.ByCustomReason(DeathReason), 1, 1);
                return;

            }
            return;
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            var source = Main.LocalPlayer.GetSource_FromThis();
            float damage = info.Damage;

            if (puffFall)
            {
                if (info.DamageSource.SourceOtherIndex == 0)
                {
                    damage /= 2f;
                    info.Damage = (int)damage; 
                }
            }
            if (lightningCloud && lightningCloudCharge > 60)
            {
                if (damage > 0 && info.DamageSource.SourceOtherIndex != 0)
                {
                    SoundEngine.PlaySound(SoundID.Item93, Player.position);

                    int lightningDamage = 10;
                    float speed = 6;
                    if (Main.hardMode)
                    {
                        lightningDamage = 40;
                        speed = 12;
                    }
                    if (NPC.downedMoonlord)
                    {
                        lightningDamage = 100;
                        speed = 18;
                    }
                    float lightningMultiplier = lightningCloudCharge / 300f;

                    speed = speed * lightningMultiplier;
                    lightningDamage = (int)(lightningDamage * lightningMultiplier);
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        float numberProjectiles = 8;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                            Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<LightningExplosion>(), lightningDamage, 2f, 0);
                        }
                    }
                    lightningCloudCharge = 0;
                }
            }
            if (puffFall)
            {
                if (info.DamageSource.SourceOtherIndex == 0)
                {
                    damage /= 2;
                }
            }
            if (arid && info.DamageSource.SourceOtherIndex == 0)
            {
                damage = (int)(damage * (MathHelper.Clamp(aridFalling, 0, Player.maxFallSpeed) / Player.maxFallSpeed));
            }
            if (gelticConqueror)
            {
                if (info.DamageSource.SourceOtherIndex == 0 && !Player.controlDown)
                {
                    damage = (int)(damage * 0.25);
                }
            }
            if (spikeBoots && info.DamageSource.SourceOtherIndex == 3 && damage <= 46) return;
            if (templeSpikeBoots && info.DamageSource.SourceOtherIndex == 3) return;
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (Main.rand.Next(101) < saveAmmo) return false;
            return base.CanConsumeAmmo(weapon, ammo);
        }
        public override void PostHurt(Player.HurtInfo info)
        {
            var source = Main.LocalPlayer.GetSource_FromThis();
            float damage = info.Damage;
            if (vleviAegis)
            {
                vleviAegisDamage += (int)damage;
            }
            if (abyssalMatter && abyssalRage <= 0)
            {
                if (damage > Player.statLifeMax2 * 0.4f)
                {
                    abyssalRage = 600;
                }
            }
            if (toyArmor && toyArmorCooldown <= 0)
            {
                for (int i = 0; i < Main.rand.Next(1, 4); i++)
                {
                    Projectile brick = Main.projectile[Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-3, -1), ProjectileType<LegoBrickFriendly>(), 25, 0, Player.whoAmI)];
                }
                toyArmorCooldown = 60;
            }
            if (voidWalkerChest && damage > Player.statLifeMax2 / 2)
            {
                voidWalkerRegen = 180;
            }
            if (voidBlood)
            {
                int bloodDamage = 10;
                if (NPC.downedBoss1) bloodDamage = 15;
                if (NPC.downedBoss3) bloodDamage = 20;
                if (Main.hardMode) bloodDamage = 25;
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) bloodDamage = 30;
                if (NPC.downedPlantBoss) bloodDamage = 35;
                if (NPC.downedAncientCultist) bloodDamage = 40;
                if (NPC.downedMoonlord) bloodDamage = 60; for (int i = 0; i < 3; i++)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), ProjectileType<VoidBlood>(), bloodDamage, 0f, Main.myPlayer, 0f, 0f)];
                }
            }
            if (slimeBooster)
            {
                for (int i = 0; i < 7; i++)
                {
                    Vector2 vector2 = new Vector2((float)(i - 2), -4f);
                    vector2.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                    vector2.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                    vector2.Normalize();
                    vector2 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                    Projectile proj = Main.projectile[Projectile.NewProjectile(source, Player.Top.X, Player.Top.Y, vector2.X, vector2.Y, ProjectileID.SpikedSlimeSpike, 30, 0f, Main.myPlayer, 0f, 0f)];
                    proj.friendly = true;
                    proj.hostile = false;
                }
            }
            if (awokenWood)
            {
                Player.AddBuff(BuffType<AwokenHealing>(), 180);
            }
            if (starstruck && starstruckCounter < 20)
            {
                starstruckCounter++;
                CombatText.NewText(Player.getRect(), Color.HotPink, starstruckCounter, true, false);
            }
        }
        public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
        {
            if (voidBlood)
            {
                price *= 3;
            }
        }
        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            if (voidBlood)
            {
                if (AnyBoss())
                {
                    chatText = this.GetLocalization("NurseHeal.Heal").Value;
                    return false;
                }
                if (nurse.life < nurse.lifeMax)
                {
                    chatText = this.GetLocalization("NurseHeal.Heal1").Value;
                    return false;
                }
            }
            return base.ModifyNurseHeal(nurse, ref health, ref removeDebuffs, ref chatText);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            var source = Main.LocalPlayer.GetSource_FromThis();
            PlayerEnergy energyPlayer = Player.GetModPlayer<PlayerEnergy>();
            if (ElementsAwoken.specialAbility.JustPressed)
            {
                if (chaosRing && Player.FindBuffIndex(BuffType<ChaosShieldCooldown>()) == -1 && !Player.dead)
                {
                        Player.AddBuff(BuffType<ChaosShield>(), 900);
                        Player.AddBuff(BuffType<ChaosShieldCooldown>(), 3600);
                }
                if (honeyCocoon)
                {
                    if (honeyCocooned <= 0)
                    {
                        if (Player.FindBuffIndex(BuffType<HoneyCocoonCD>()) == -1 && !Player.dead)
                        {
                            int duration = GetInstance<Config>().debugMode ? 300 : 3600;
                            Player.AddBuff(BuffType<HoneyCocoonCD>(), duration);
                            honeyCocooned = 900;
                        }
                    }
                    else if (honeyCocooned > 0) honeyCocooned = 0;
                }
            }
            if (neovirtuoBonus)
            {
                if (ElementsAwoken.neovirtuo.JustPressed)
                {
                    if (Player.FindBuffIndex(BuffType<NeovirtuoCooldown>()) == -1 && !Player.dead)
                    {
                        Vector2 vector2 = Player.RotatedRelativePoint(Player.MountedCenter, true);
                        vector2.X = (float)Main.mouseX + Main.screenPosition.X;
                        vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
                        Projectile.NewProjectile(source, vector2.X, vector2.Y, 0f, 0f, ProjectileType<NeovirtuoPortal>(), 0, 0, Player.whoAmI, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item113, Player.position);
                        Player.AddBuff(BuffType<NeovirtuoCooldown>(), 1800);
                    }
                }
            }
            if (boostDrive != 0 && ElementsAwoken.specialAbility.JustPressed && Player.FindBuffIndex(BuffType<BoostDriveCD>()) == -1)
            {
                string Energy = this.GetLocalization("Other.Other1").Value;
                bool hasEnergy = false;
                int dustID = 226;
                if (boostDrive == 1 && energyPlayer.energy >= 50)
                {
                    energyPlayer.energy -= 50;
                    hasEnergy = true;
                }
                else if (boostDrive == 2 && energyPlayer.energy >= 150)
                {
                    energyPlayer.energy -= 150;
                    hasEnergy = true;
                    dustID = 205;
                }
                if (hasEnergy)
                {
                    int duration = GetInstance<Config>().debugMode ? 420 : 2700;
                    Player.AddBuff(BuffType<BoostDriveCD>(), duration);
                    boostDriveTimer = 300;

                    int numDusts = 30;
                    for (int p = 0; p < numDusts; p++)
                    {
                        Vector2 position = (Vector2.One * new Vector2((float)Player.width / 2f, (float)Player.height) * 0.3f * 0.5f).RotatedBy((double)((float)(p - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Player.Center;
                        Vector2 velocity = position - Player.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, dustID, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 2f;
                    }
                }
                else CombatText.NewText(Player.getRect(), Color.Red, Energy, true, false);
            }
            if (flare)
            {
                if (ElementsAwoken.specialAbility.JustPressed)
                {
                    string dead = this.GetLocalization("Other.Other2").Value;
                    if (!Player.dead)
                    {
                        if (flareShieldCD <= 0)
                        {
                            Player.AddBuff(BuffType<Content.Buffs.Other.FlareShield>(), 900);
                            flareShieldCD = 3600;
                        }
                        else
                        {
                            Main.NewText(flareShieldCD / 60 + " " + dead);
                        }
                    }
                }
            }
            if (voidEnergyCharge > 600)
            {
                if (ElementsAwoken.specialAbility.JustPressed)
                {
                    voidEnergyTimer = voidEnergyCharge / 4;
                    voidEnergyCharge = 0;
                    SoundEngine.PlaySound(SoundID.Zombie96, Player.position);
                }
            }
            if (vleviAegis)
            {
                if (ElementsAwoken.dash2.JustPressed && aegisDashCooldown <= 0)
                {
                    aegisDashTimer = 45;
                    aegisDashCooldown = 300;
                    aegisDashDir = Player.direction;

                    // make dust in an expanding circle
                    int numDusts = 56;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.Normalize(Player.velocity) * new Vector2((float)Player.width / 2f, (float)Player.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Player.Center;
                        Vector2 velocity = position - Player.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 9f;
                    }
                }
            }
            if (Player.controlDown && Player.releaseDown)
            {
                if (doubleDownWindow > 0)
                {
                    doubleTappedDown = true;
                    doubleDownWindow = 0;
                }
                else
                {
                    doubleDownWindow = 15;
                }
            }

            if (doubleTappedDown && forgedArmor)
            {
                forgedShackled = 600;
            }
            if (doubleTappedDown && awokenWood)
            {
                Projectile.NewProjectile(source, Player.Center, Vector2.Zero, ProjectileType<LifesAura>(), 0, 0, Player.whoAmI);
            }
            if (doubleTappedDown && fireAccCD < 0 && fireAcc)
            {
                var mod = ModLoader.GetMod("ElementsAwoken");
                Projectile exp = Main.projectile[Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ProjectileType<Explosion>(), 40, 12, Player.whoAmI, 0f, 0f)];
                exp.width = 100;
                exp.height = 20;
                exp.Center = Player.Bottom + new Vector2(0, -10);
                SoundEngine.PlaySound(SoundID.Item14, Player.position);
                int num = GetInstance<Config>().lowDust ? 10 : 20;
                for (int i = 0; i < num; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                    if (dust.position.X < Player.Center.X) dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * -3f;
                    else dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * 3f;
                    dust.velocity.Y = -2f;
                }
                int num2 = GetInstance<Config>().lowDust ? 5 : 10;
                for (int i = 0; i < num2; i++)
                {
                    int dustID = 6;
                    Dust dust = Main.dust[Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, dustID, 0f, 0f, 100, default(Color), 2.5f)];
                    dust.noGravity = true;
                    dust.velocity *= 5f;
                    if (dust.position.X < Player.Center.X) dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * -3f;
                    else dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * 3f;
                    dust.velocity.Y = -2f; int dustID2 = 6;
                    dust = Main.dust[Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, dustID2, 0f, 0f, 100, default(Color), 1.5f)];
                    dust.velocity *= 3f;
                    if (dust.position.X < Player.Center.X) dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * -3f;
                    else dust.velocity.X = Main.rand.NextFloat(0.8f, 1.2f) * 3f;
                    dust.velocity.Y = -2f;
                }
                int num373 = Gore.NewGore(source, new Vector2(Player.position.X, Player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore85 = Main.gore[num373];
                gore85.velocity.X = gore85.velocity.X + 1f;
                Gore gore86 = Main.gore[num373];
                gore86.velocity.Y = gore86.velocity.Y + 1f;
                num373 = Gore.NewGore(source, new Vector2(Player.position.X, Player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore87 = Main.gore[num373];
                gore87.velocity.X = gore87.velocity.X - 1f;
                Gore gore88 = Main.gore[num373];
                gore88.velocity.Y = gore88.velocity.Y + 1f;
                num373 = Gore.NewGore(source, new Vector2(Player.position.X, Player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore89 = Main.gore[num373];
                gore89.velocity.X = gore89.velocity.X + 1f;
                Gore gore90 = Main.gore[num373];
                gore90.velocity.Y = gore90.velocity.Y - 1f;
                num373 = Gore.NewGore(source, new Vector2(Player.position.X, Player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore91 = Main.gore[num373];
                gore91.velocity.X = gore91.velocity.X - 1f;
                Gore gore92 = Main.gore[num373];
                gore92.velocity.Y = gore92.velocity.Y - 1f;

                Player.velocity.Y -= 8;
                fireAccCD = 60;
            }
            if (doubleTappedDown && radiantCrown)
            {
                Vector2 toPos = Main.MouseWorld;
                Tile tileTest = Framing.GetTileSafely((int)(toPos.X / 16), (int)(toPos.Y / 16));
                if (!Collision.SolidCollision(toPos, Player.width, Player.height) && toPos.X > 50f && toPos.X < (float)(Main.maxTilesX * 16 - 50) && toPos.Y > 50f && toPos.Y < (float)(Main.maxTilesY * 16 - 50))
                {
                    if (Player.chaosState)
                    {
                        NetworkText DeathReason1 = NetworkText.FromLiteral(Player.name + " " + this.GetLocalization("PlayerDeath.Death1").Value);
                        Player.statLife -= Player.statLifeMax2 / 7;
                        if (Player.statLife < 0) Player.KillMe(PlayerDeathReason.ByCustomReason(DeathReason1), 1, 1);
                    }
                    int numProj = 5;
                    Vector2 distance = (toPos - Player.Center) / numProj;
                    for (int k = 0; k < numProj; k++)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(source, Player.Center + distance - new Vector2(0, 23), Vector2.Zero, ProjectileType<RadiantPTeleport>(), 0, 0f, Main.myPlayer)];
                        proj.spriteDirection = Player.direction;
                        distance += (toPos - Player.Center) / numProj;
                    }
                    float num2 = Vector2.Distance(Player.position, toPos);
                    Player.Center = toPos;
                    NetMessage.SendData(65, -1, -1, null, 0, (float)Player.whoAmI, toPos.X, toPos.Y, 1, 0, 0);

                    // screen stuff
                    Player.fallStart = (int)(Player.position.Y / 16f);
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        bool flag = false;
                        if (num2 < new Vector2((float)Main.screenWidth, (float)Main.screenHeight).Length() / 2f + 100f)
                        {
                            int time = 10;
                            Main.SetCameraLerp(0.1f, time);
                            flag = true;
                        }
                        else
                        {
                            Main.BlackFadeIn = 255;
                            Lighting.Clear();
                            Main.screenLastPosition = Main.screenPosition;
                            Main.screenPosition.X = Player.position.X + (float)(Player.width / 2) - (float)(Main.screenWidth / 2);
                            Main.screenPosition.Y = Player.position.Y + (float)(Player.height / 2) - (float)(Main.screenHeight / 2);
                            Main.instantBGTransitionCounter = 10;
                        }
                            if (Main.mapTime < 5)
                            {
                                Main.mapTime = 5;
                            }
                            Main.maxQ = true;
                            Main.renderNow = true;
                    }

                    Player.AddBuff(BuffID.ChaosState, 300);
                }
            }
            if (Player.controlDown && Player.releaseDown && forgedShackled > 0)
            {
                flingToShackle = true;
            }

            if (crystallineLocket && ElementsAwoken.specialAbility.JustPressed && Player.FindBuffIndex(BuffType<CrystallineLocketCD>()) == -1)
            {
                crystallineLocketCrit = 600;
                if (!GetInstance<Config>().debugMode) Player.AddBuff(BuffType<CrystallineLocketCD>(), 3600);
                else Player.AddBuff(BuffType<CrystallineLocketCD>(), 60);

            }
            if (toySlimeClaw && toySlimeClawCD <= 0&& ElementsAwoken.specialAbility.JustPressed)
            {
                SoundEngine.PlaySound(SoundID.Item95, Player.Center);

                float speed = 5f;
                float rotation = (float)Math.Atan2(Player.Center.Y - Main.MouseWorld.Y, Player.Center.X - Main.MouseWorld.X);
                Vector2 projVel = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
                ElementsAwoken.DebugModeText(rotation);
                if (rotation < -(Math.PI / 2 - Math.PI / 8) && rotation > -(Math.PI / 2 + Math.PI / 8))
                {
                    float jumpSpd = 16f;
                    if (Player.slowFall) jumpSpd *= 0.5f;
                    if (Player.HeldItem.type == 946) jumpSpd *= 0.5f;
                    Player.velocity.Y -= jumpSpd;
                    int numberProjectiles = Main.rand.Next(3, 7);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = projVel.RotatedByRandom(MathHelper.ToRadians(35));
                        Projectile.NewProjectile(source, Player.Center,perturbedSpeed, ProjectileType<SlimeClawBall>(), 20, 3f, Player.whoAmI);
                    }
                }
                Projectile.NewProjectile(source, Player.Center, projVel, ProjectileType<SlimeClawBall>(), 20, 3f, Player.whoAmI);
                toySlimeClawCD = 60;
            }
            if (greatLens && Player.FindBuffIndex(BuffType<GreatLensCD>()) == -1 && ElementsAwoken.specialAbility.JustPressed)
            {
                greatLensTimer = 600;
                if (!GetInstance<Config>().debugMode) Player.AddBuff(BuffType<GreatLensCD>(), 3600);
                else Player.AddBuff(BuffType<GreatLensCD>(), 60);
            }
            if (ElementsAwoken.armorAbility.JustPressed && voidWalkerCooldown <= 0 && voidWalkerArmor > 0)
            {
                voidWalkerAura = 300;
            }
            doubleTappedDown = false;
        }
        public static void explosionEffect(Player player, int type, int damage, float knockback, int dust)
        {
            var source = Main.LocalPlayer.GetSource_FromThis();
            Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, type, damage, knockback, Main.myPlayer, 0f, 0f);
            SoundEngine.PlaySound(SoundID.Item14, player.position);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dust, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.4f;
            }
            for (int num371 = 0; num371 < 10; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dust, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 5f;
                num372 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, dust, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num372].velocity *= 3f;
            }
            int num373 = Gore.NewGore(source, new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(source, new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(source, new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(source, new Vector2(player.position.X, player.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
        private static void EyeDust(Player player, int dust)
        {
            int num = 0;
            num += player.bodyFrame.Y / 56;
            if (num >= Main.OffsetsPlayerHeadgear.Length)
            {
                num = 0;
            }
            Vector2 vector = new Vector2((float)(3 * player.direction - ((player.direction == 1) ? 1 : 0)), -11.5f * player.gravDir) + Vector2.UnitY * player.gfxOffY + player.Size / 2f + Main.OffsetsPlayerHeadgear[num];
            Vector2 vector2 = new Vector2((float)(3 * player.shadowDirection[1] - ((player.direction == 1) ? 1 : 0)), -11.5f * player.gravDir) + player.Size / 2f + Main.OffsetsPlayerHeadgear[num];
            Vector2 vector3 = Vector2.Zero;
            if (player.mount.Active && player.mount.Cart)
            {
                int num2 = Math.Sign(player.velocity.X);
                if (num2 == 0)
                {
                    num2 = player.direction;
                }
                vector3 = new Vector2(MathHelper.Lerp(0f, -8f, player.fullRotation / 0.7853982f), MathHelper.Lerp(0f, 2f, Math.Abs(player.fullRotation / 0.7853982f))).RotatedBy((double)player.fullRotation, default(Vector2));
                if (num2 == Math.Sign(player.fullRotation))
                {
                    vector3 *= MathHelper.Lerp(1f, 0.6f, Math.Abs(player.fullRotation / 0.7853982f));
                }
            }
            if (player.fullRotation != 0f)
            {
                vector = vector.RotatedBy((double)player.fullRotation, player.fullRotationOrigin);
                vector2 = vector2.RotatedBy((double)player.fullRotation, player.fullRotationOrigin);
            }
            float num3 = 0f;
            if (player.mount.Active)
            {
                num3 = (float)player.mount.PlayerOffset;
            }
            Vector2 vector4 = player.position + vector + vector3;
            Vector2 vector5 = player.oldPosition + vector2 + vector3;
            vector5.Y -= num3 / 2f;
            vector4.Y -= num3 / 2f;

            int num5 = (int)Vector2.Distance(vector4, vector5) / 3 + 1;
            if (Vector2.Distance(vector4, vector5) % 3f != 0f)
            {
                num5++;
            }
            float num4 = 1f;
            for (float num6 = 1f; num6 <= (float)num5; num6 += 1f)
            {
                Dust expr_3D9 = Main.dust[Dust.NewDust(player.Center, 0, 0, dust, 0f, 0f, 0, default(Color), 1f)];
                expr_3D9.position = Vector2.Lerp(vector5, vector4, num6 / (float)num5);
                expr_3D9.noGravity = true;
                expr_3D9.velocity = Vector2.Zero;
                expr_3D9.customData = player;
                expr_3D9.scale = num4;
                expr_3D9.shader = GameShaders.Armor.GetSecondaryShader(player.cYorai, player);
            }
        }
        private int CollideWithNPCs(Rectangle myRect, float Damage, float Knockback, int NPCImmuneTime, int PlayerImmuneTime)
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.dontTakeDamage && !nPC.friendly && nPC.immune[Player.whoAmI] == 0)
                {
                    Rectangle rect = nPC.getRect();
                    if (myRect.Intersects(rect) && (nPC.noTileCollide || Collision.CanHit(Player.position, Player.width, Player.height, nPC.position, nPC.width, nPC.height)))
                    {
                        int direction = Player.direction;
                        if (Player.velocity.X < 0f)
                        {
                            direction = -1;
                        }
                        if (Player.velocity.X > 0f)
                        {
                            direction = 1;
                        }
                        if (Player.whoAmI == Main.myPlayer)
                        {
                            Player.ApplyDamageToNPC(nPC, (int)Damage, Knockback, direction, false);
                        }
                        nPC.immune[Player.whoAmI] = NPCImmuneTime;
                        Player.immune = true;
                        Player.immuneNoBlink = true;
                        Player.immuneTime = PlayerImmuneTime;
                        num++;
                        break;
                    }
                }
            }
            return num;
        }
        private void EAWallSlide()
        {
            Player.sliding = false;
            toySlimeClawSliding = false;
            if (Player.slideDir != 0 && Player.spikedBoots == 0  && toySlimeClaw && !Player.mount.Active && ((Player.controlLeft && Player.slideDir == -1) || (Player.controlRight && Player.slideDir == 1)))
            {
                bool flag = false;
                float num = Player.position.X;
                if (Player.slideDir == 1)
                {
                    num += (float)Player.width;
                }
                num += (float)Player.slideDir;
                float num2 = Player.position.Y + (float)Player.height + 1f;
                if (Player.gravDir < 0f)
                {
                    num2 = Player.position.Y - 1f;
                }
                num /= 16f;
                num2 /= 16f;
                if (WorldGen.SolidTile((int)num, (int)num2) && WorldGen.SolidTile((int)num, (int)num2 - 1))
                {
                    flag = true;
                }
                if ((flag && (double)Player.velocity.Y > 0.5 && Player.gravDir == 1f) || ((double)Player.velocity.Y < -0.5 && Player.gravDir == -1f))
                {
                    Player.fallStart = (int)(Player.position.Y / 16f);
                    if (Player.controlDown)
                    {
                        Player.velocity.Y = 4f * Player.gravDir;
                    }
                    else
                    {
                        Player.velocity.Y = 0.5f * Player.gravDir;
                    }
                    Player.sliding = true;
                    toySlimeClawSliding = true;
                    int num5 = Dust.NewDust(new Vector2(Player.position.X + (float)(Player.width / 2) + (float)((Player.width / 2 - 4) * Player.slideDir), Player.position.Y + (float)(Player.height / 2) + (float)(Player.height / 2 - 4) * Player.gravDir), 8, 8, 4, 0f, 0f, 150, new Color(0, 220, 40, 100), 1f);
                    if (Player.slideDir < 0)
                    {
                        Dust expr_48D_cp_0_cp_0 = Main.dust[num5];
                        expr_48D_cp_0_cp_0.position.X = expr_48D_cp_0_cp_0.position.X - 10f;
                    }
                    if (Player.gravDir < 0f)
                    {
                        Dust expr_4B5_cp_0_cp_0 = Main.dust[num5];
                        expr_4B5_cp_0_cp_0.position.Y = expr_4B5_cp_0_cp_0.position.Y - 12f;
                    }
                    Main.dust[num5].velocity *= 0.1f;
                    Main.dust[num5].scale *= 1.2f;
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                }
            }
        }
        public void ApplyDamageToNPC(NPC npc, int damage, float knockback, int direction, bool crit)
        {
            npc.StrikeNPC(new NPC.HitInfo() { Damage = damage, Knockback = knockback, HitDirection = direction, Crit = Main.rand.Next(2) == 0 });
            if (Main.netMode != 0)
            {
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, (float)damage, knockback, (float)direction, crit.ToInt(), 0, 0);
            }
            int num = Item.NPCtoBanner(npc.BannerID());
            if (num >= 0)
            {
                Player.lastCreatureHit = num;
            }
        }
        private void CantMove()
        {
            Player.controlUp = false;
            Player.controlLeft = false;
            Player.releaseLeft = false;
            Player.controlRight = false;
            Player.releaseRight = false;
            Player.controlDown = false;
            Player.controlJump = false;

            eaDashDelay = 0;
            eaDashTime = 0;
        }
        private bool AnyBoss()
        {
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC nPC = Main.npc[i];
                if (nPC.boss && nPC.active)
                {
                    return true;
                }
            }
            return false;
        }
        void HitEffect(NPC.HitInfo hit)
        {
            crit = hit.Crit;
        }
    }
}