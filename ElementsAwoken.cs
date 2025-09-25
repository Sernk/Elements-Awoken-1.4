using ElementsAwoken.Content.Effects;
using ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase2.ShadeWyrm;
using ElementsAwoken.Content.NPCs.Bosses.Infernace;
using ElementsAwoken.Content.NPCs.Bosses.Obsidious;
using ElementsAwoken.Content.NPCs.Bosses.Regaroth;
using ElementsAwoken.Content.NPCs.Bosses.TheTempleKeepers;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan.Minions;
using ElementsAwoken.Content.NPCs.Bosses.Volcanox;
using ElementsAwoken.Content.NPCs.ItemSets.ToySlime;
using ElementsAwoken.Content.NPCs.Projectiles;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Biome;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace ElementsAwoken
{
    public class ElementsAwoken : Mod
    {
        public const string Ancients = "ElementsAwoken:Ancients";
        public const string Azana = "ElementsAwoken:Azana";
        public const string VoidLeviathanHead = "ElementsAwoken:VoidLeviathanHead";
        public const string Volcanox = "ElementsAwoken:Volcanox";
        public const string TheGuardianFly = "ElementsAwoken:TheGuardianFly";
        public const string Aqueous = "ElementsAwoken:Aqueous";
        public const string Permafrost = "ElementsAwoken:Permafrost";
        public const string Infernace = "ElementsAwoken:Infernace";

        public const string VoidEvent = "ElementsAwoken:VoidEvent";
        public const string VoidEventDark = "ElementsAwoken:VoidEventDark";
        public const string RadiantRain = "ElementsAwoken:RadiantRain";

        public const string Regaroth = "ElementsAwoken:Regaroth";
        public const string Regaroth2 = "ElementsAwoken:Regaroth2";
        public const string RegarothIntense = "ElementsAwoken:RegarothIntense";
        public const string Regaroth2Intense = "ElementsAwoken:Regaroth2Intense";

        public const string Encounter1 = "ElementsAwoken:Encounter1";
        public const string Encounter2 = "ElementsAwoken:Encounter2";
        public const string Encounter3 = "ElementsAwoken:Encounter3";

        public const string Despair = "ElementsAwoken:Despair";
        public const string Blizzard = "ElementsAwoken:Blizzard";
        public const string InfernacesWrath = "ElementsAwoken:InfernacesWrath";
        public const string AshParticles = "ElementsAwoken:AshParticles";

        public static string a = "";
        public static ItemJson itemList;

        public static DynamicSpriteFont encounterFont;

        internal UserInterface AlchemistUserInterface;
        internal UserInterface VoidTimerChangerUI;
        internal PromptInfoUI PromptUI;
        public UserInterface PromptInfoUserInterface;

        public static ModKeybind neovirtuo;
        public static ModKeybind specialAbility;
        public static ModKeybind armorAbility;
        public static ModKeybind dash2;
        public static ModKeybind ASBT;

        public static ElementsAwoken instance;

        public static bool calamityEnabled;
        public static bool recipebrowser;
        public static bool bossChecklistEnabled;
        public static bool ancientsAwakenedEnabled;
        public static bool eaMusicEnabled;
        public static bool eaRetroMusicEnabled;
        public static bool WikithisEnabled;

        public static int[] screenTextTimer = new int[10];
        public static int[] screenTextDuration = new int[10];
        public static float[] screenTextAlpha = new float[10];
        public static float[] screenTextScale = new float[10];
        public static string[] screenText = new string[10];
        public static Vector2[] screenTextPos = new Vector2[10];

        //public static Dictionary<int, Color> RarityColors = new Dictionary<int, Color>();

        public static Texture2D AADeathBall;
        public static Texture2D insanityTex;
        public static Texture2D heartGlowTex;

        public static List<int> instakillImmune = new List<int>();

        public static bool aprilFools = false;

        public static int encounter = 0;
        public static bool encounterSetup = false;
        public static int encounterTimer = 0;
        public static int encounterShakeTimer = 0;

        public const int bossPromptDelay = 108000;

        int b = 0;
        public ElementsAwoken()
        {
            ContentAutoloadingEnabled = true;
            GoreAutoloadingEnabled = true;
            MusicAutoloadingEnabled = true;
        }
        public override void PostSetupContent()
        {
            using var stream = ModContent.GetInstance<ElementsAwoken>().GetFileStream("ItemList.json");
            using var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            itemList = JsonConvert.DeserializeObject<ItemJson>(json);
        }
        public static void PremultiplyTexture(Texture2D texture)
        {
            //Color[] buffer = new Color[texture.Width * texture.Height];
            //if (texture != null)
            //{
            //    texture.GetData(buffer);
            //    for (int i = 0; i < buffer.Length; i++)
            //    {
            //        buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            //    }
            //    texture.SetData(buffer);
            //}
        }
        public override void Load()
        {
            Terraria.IL_Player.Update += RemoveManaCap;

            DateTime now = DateTime.Today;
            if (now.Day == 1 && now.Month == 4) aprilFools = true;

            calamityEnabled = ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod);
            recipebrowser = ModLoader.TryGetMod("RecipeBrowser", out Mod RecipeBrowser);
            eaRetroMusicEnabled = ModLoader.TryGetMod("EARetroMusic", out Mod EARetroMusic);
            eaMusicEnabled = ModLoader.TryGetMod("EAMusic", out Mod EAMusic);
            WikithisEnabled = ModLoader.TryGetMod("Wikithis", out Mod wikithis);
            if (wikithis != null && !Main.dedServ)
            {
                wikithis.Call(0, this, "https://elementsawoken.fandom.com/{}");
                wikithis.Call("AddWikiTexture", this, ModContent.Request<Texture2D>("ElementsAwoken/icon_small"));
            }

            instance = this;

            //HOTKEYS
            neovirtuo = KeybindLoader.RegisterKeybind(this, "Neovirtuo", "C");
            specialAbility = KeybindLoader.RegisterKeybind(this, "Special Ability", "Z");
            armorAbility = KeybindLoader.RegisterKeybind(this, "Armor Ability", "X");
            dash2 = KeybindLoader.RegisterKeybind(this, "Secondary Dash", "F");
            ASBT = KeybindLoader.RegisterKeybind(this, "Armor Set Bonus ToolTips", "Q");
            MyWorld.awakenedModeNoActive = true;
            if (!Main.dedServ)
            {
                Filters.Scene[Ancients] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0.8f, 0.4f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                SkyManager.Instance[Ancients] = new EABiomeSky();
                Filters.Scene[Azana] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 0.2f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance[Azana] = new EABiomeSky();
                Filters.Scene[VoidLeviathanHead] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1.0f, 0.2f, 0.55f).UseOpacity(0.4f), EffectPriority.VeryHigh);
                SkyManager.Instance[VoidLeviathanHead] = new EABiomeSky();
                Filters.Scene[Volcanox] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1.0f, 0.2f, 0.55f).UseOpacity(0.4f), EffectPriority.VeryHigh);
                SkyManager.Instance[Volcanox] = new EABiomeSky();
                Filters.Scene[TheGuardianFly] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance[TheGuardianFly] = new EABiomeSky();
                Filters.Scene[Aqueous] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.7f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance[Aqueous] = new EABiomeSky();
                Filters.Scene[Permafrost] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.6f, 0.7f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance[Permafrost] = new EABiomeSky();
                Filters.Scene[Infernace] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance[Infernace] = new EABiomeSky();
                Filters.Scene[VoidEvent] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.8f, 0.2f, 0.0f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                SkyManager.Instance[VoidEvent] = new EABiomeSky();
                Filters.Scene[VoidEventDark] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.0f, 0.0f, 0.2f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance[VoidEventDark] = new EABiomeSky();
                Filters.Scene[RadiantRain] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.3f, 0.7f).UseOpacity(0.4f), EffectPriority.VeryHigh);
                SkyManager.Instance[RadiantRain] = new EABiomeSky();
                Filters.Scene[Regaroth] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.4f, 0.7f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                SkyManager.Instance[Regaroth] = new EABiomeSky();
                Filters.Scene[Regaroth2] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.3f, 0.7f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                SkyManager.Instance[Regaroth2] = new EABiomeSky();
                Filters.Scene[RegarothIntense] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.4f, 0.7f).UseOpacity(0.75f), EffectPriority.VeryHigh);
                SkyManager.Instance[RegarothIntense] = new EABiomeSky();
                Filters.Scene[Regaroth2Intense] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.3f, 0.7f).UseOpacity(0.75f), EffectPriority.VeryHigh);
                SkyManager.Instance[Regaroth2Intense] = new EABiomeSky();
                Filters.Scene[Encounter1] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 0.2f, 0.3f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance[Encounter1] = new EABiomeSky();
                Filters.Scene[Encounter2] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.1f, 0.1f, 0.3f).UseOpacity(0.2f), EffectPriority.VeryHigh);
                SkyManager.Instance[Encounter2] = new EABiomeSky();
                Filters.Scene[Encounter3] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.0f, 0.0f, 0.2f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance[Encounter3] = new EABiomeSky();
                Filters.Scene[Despair] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 1f, 1f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.VeryHigh);
                SkyManager.Instance[Despair] = new EABiomeSky();
                Filters.Scene[Blizzard] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.0f, 0.3f).UseOpacity(0.4f), EffectPriority.VeryHigh);
                Overlays.Scene["Blizzard"] = new SimpleOverlay("Images/Misc/noise", new ScreenShaderData("FilterBlizzardBackground").UseColor(1f, 1f, 1f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High, RenderLayers.Landscape);
                SkyManager.Instance["Blizzard"] = new EABiomeSky();
                Filters.Scene[InfernacesWrath] = new Filter( new ScreenShaderData("FilterBloodMoon") .UseColor(1f, 0.3f, 0.3f).UseIntensity(0.7f), EffectPriority.VeryHigh);
                SkyManager.Instance[InfernacesWrath] = new EABiomeSky();
                Filters.Scene["ElementsAwoken:HeatDistortion"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, null).UseIntensity(4f), EffectPriority.Low);
                Ref<Effect> screenRef = new(Assets.Request<Effect>("Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value);
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();

                Effect effect = Assets.Request<Effect>("Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value;

                Main.QueueMainThreadAction(() => { AADeathBall = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/Ancients/Gores/LightBall", AssetRequestMode.ImmediateLoad).Value; PremultiplyTexture(AADeathBall); });

                insanityTex = ModContent.Request<Texture2D>("ElementsAwoken/Effects/Insanity").Value;
                PremultiplyTexture(insanityTex);
                heartGlowTex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/HeartGlow").Value;
                PremultiplyTexture(heartGlowTex);

                AlchemistUserInterface = new UserInterface();
                VoidTimerChangerUI = new UserInterface();
                PromptUI = new PromptInfoUI();
                PromptUI.Activate();
                PromptInfoUserInterface = new UserInterface();
                PromptInfoUserInterface.SetState(PromptUI);

                TileID.Sets.Platforms = TileID.Sets.Factory.CreateBoolSet([19, 427, 435, 436, 437, 438, 439, TileID.PlanterBox]);
            }
            for (int i = 0; i < screenText.Length; i++)
            {
                if (screenText[i] == null)
                {
                    screenText[i] = "";
                }
            }
            #region instakill immunities
            instakillImmune.Add(NPCID.EyeofCthulhu);
            instakillImmune.Add(NPCID.EaterofWorldsHead);
            instakillImmune.Add(NPCID.EaterofWorldsBody);
            instakillImmune.Add(NPCID.EaterofWorldsTail);
            instakillImmune.Add(NPCID.BrainofCthulhu);
            instakillImmune.Add(NPCID.Creeper);
            instakillImmune.Add(NPCID.SkeletronHead);
            instakillImmune.Add(NPCID.SkeletronHand);
            instakillImmune.Add(NPCID.QueenBee);
            instakillImmune.Add(NPCID.KingSlime);
            instakillImmune.Add(NPCID.WallofFlesh);
            instakillImmune.Add(NPCID.WallofFleshEye);
            instakillImmune.Add(NPCID.TheDestroyer);
            instakillImmune.Add(NPCID.TheDestroyerBody);
            instakillImmune.Add(NPCID.TheDestroyerTail);
            instakillImmune.Add(NPCID.Retinazer);
            instakillImmune.Add(NPCID.Spazmatism);
            instakillImmune.Add(NPCID.SkeletronPrime);
            instakillImmune.Add(NPCID.PrimeCannon);
            instakillImmune.Add(NPCID.PrimeSaw);
            instakillImmune.Add(NPCID.PrimeVice);
            instakillImmune.Add(NPCID.PrimeLaser);
            instakillImmune.Add(NPCID.Plantera);
            instakillImmune.Add(NPCID.PlanterasTentacle);
            instakillImmune.Add(NPCID.Golem);
            instakillImmune.Add(NPCID.GolemHead);
            instakillImmune.Add(NPCID.GolemFistLeft);
            instakillImmune.Add(NPCID.GolemFistRight);
            instakillImmune.Add(NPCID.GolemHeadFree);
            instakillImmune.Add(NPCID.DukeFishron);
            instakillImmune.Add(NPCID.CultistBoss);
            instakillImmune.Add(NPCID.MoonLordHead);
            instakillImmune.Add(NPCID.MoonLordHand);
            instakillImmune.Add(NPCID.MoonLordCore);
            instakillImmune.Add(NPCID.MoonLordFreeEye);
            instakillImmune.Add(NPCID.DungeonGuardian);
            instakillImmune.Add(NPCID.IceGolem);
            instakillImmune.Add(NPCID.WyvernHead);
            instakillImmune.Add(NPCID.WyvernLegs);
            instakillImmune.Add(NPCID.WyvernTail);
            instakillImmune.Add(NPCID.WyvernBody);
            instakillImmune.Add(NPCID.WyvernBody2);
            instakillImmune.Add(NPCID.WyvernBody3);
            instakillImmune.Add(NPCID.Mothron);
            instakillImmune.Add(NPCID.PlanterasHook);
            instakillImmune.Add(NPCID.PlanterasTentacle);
            instakillImmune.Add(NPCID.Paladin);
            instakillImmune.Add(NPCID.HeadlessHorseman);
            instakillImmune.Add(NPCID.MourningWood);
            instakillImmune.Add(NPCID.Pumpking);
            instakillImmune.Add(NPCID.PumpkingBlade);
            instakillImmune.Add(NPCID.Yeti);
            instakillImmune.Add(NPCID.Everscream);
            instakillImmune.Add(NPCID.IceQueen);
            instakillImmune.Add(NPCID.Krampus);
            instakillImmune.Add(NPCID.MartianSaucer);
            instakillImmune.Add(NPCID.MartianSaucerCannon);
            instakillImmune.Add(NPCID.MartianSaucerCore);
            instakillImmune.Add(NPCID.MartianSaucerTurret);
            instakillImmune.Add(NPCID.MoonLordCore);
            instakillImmune.Add(NPCID.LunarTowerVortex);
            instakillImmune.Add(NPCID.LunarTowerStardust);
            instakillImmune.Add(NPCID.LunarTowerSolar);
            instakillImmune.Add(NPCID.LunarTowerNebula);
            instakillImmune.Add(NPCID.CultistArcherBlue);
            instakillImmune.Add(NPCID.CultistArcherWhite);
            instakillImmune.Add(NPCID.CultistDevote);
            instakillImmune.Add(NPCID.CultistDragonBody1);
            instakillImmune.Add(NPCID.CultistDragonBody2);
            instakillImmune.Add(NPCID.CultistDragonBody3);
            instakillImmune.Add(NPCID.CultistDragonBody4);
            instakillImmune.Add(NPCID.CultistDragonHead);
            instakillImmune.Add(NPCID.CultistDragonTail);
            instakillImmune.Add(NPCID.CultistTablet);
            instakillImmune.Add(NPCID.GoblinSummoner);
            instakillImmune.Add(NPCID.BigMimicJungle);
            instakillImmune.Add(NPCID.BigMimicCorruption);
            instakillImmune.Add(NPCID.BigMimicHallow);
            instakillImmune.Add(NPCID.BigMimicCrimson);
            instakillImmune.Add(NPCID.PirateShip);
            instakillImmune.Add(NPCID.PirateShipCannon);
            instakillImmune.Add(NPCID.SandElemental);
            instakillImmune.Add(NPCID.DD2Betsy);
            instakillImmune.Add(NPCID.DD2DarkMageT1);
            instakillImmune.Add(NPCID.DD2DarkMageT3);
            instakillImmune.Add(NPCID.SolarCrawltipedeBody);
            instakillImmune.Add(NPCID.SolarCrawltipedeHead);
            instakillImmune.Add(NPCID.DD2OgreT2);
            instakillImmune.Add(NPCID.DD2OgreT3);
            instakillImmune.Add(NPCID.RainbowSlime);
            instakillImmune.Add(NPCID.PirateCaptain);
            instakillImmune.Add(NPCID.TargetDummy);
            instakillImmune.Add(ModContent.NPCType<Content.NPCs.Bosses.CosmicObserver.CosmicObserver>());
            instakillImmune.Add(ModContent.NPCType<ToySlime>());
            instakillImmune.Add(ModContent.NPCType<AncientWyrmArms>());
            instakillImmune.Add(ModContent.NPCType<AncientWyrmBody>());
            instakillImmune.Add(ModContent.NPCType<AncientWyrmHead>());
            instakillImmune.Add(ModContent.NPCType<AncientWyrmTail>());
            instakillImmune.Add(ModContent.NPCType<BarrenSoul>());
            instakillImmune.Add(ModContent.NPCType<Furosia>());
            instakillImmune.Add(ModContent.NPCType<ObsidiousHand>());
            instakillImmune.Add(ModContent.NPCType<RegarothHead>());
            instakillImmune.Add(ModContent.NPCType<RegarothBody>());
            instakillImmune.Add(ModContent.NPCType<RegarothTail>());
            instakillImmune.Add(ModContent.NPCType<ShadeWyrmHead>());
            instakillImmune.Add(ModContent.NPCType<ShadeWyrmBody>());
            instakillImmune.Add(ModContent.NPCType<ShadeWyrmTail>());
            instakillImmune.Add(ModContent.NPCType<SolarFragment>());
            instakillImmune.Add(ModContent.NPCType<SoulOfInfernace>());
            instakillImmune.Add(ModContent.NPCType<VoidLeviathanHead>());
            instakillImmune.Add(ModContent.NPCType<VoidLeviathanBody>());
            instakillImmune.Add(ModContent.NPCType<VoidLeviathanTail>());
            instakillImmune.Add(ModContent.NPCType<VolcanoxHook>());
            instakillImmune.Add(ModContent.NPCType<VolcanoxTentacle>());
            #endregion
        }
        public override void Unload()
        {
            MyWorld.awakenedModeNoActive = false;
        }
        public static void DebugModeText(object text, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (ModContent.GetInstance<Config>().debugMode)
            {
                Main.NewText(text, color);
            }
        }
        public static void DebugModeNetworkText(string text, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (ModContent.GetInstance<Config>().debugMode)
            {
                NetworkText f = NetworkText.FromLiteral(text);
                ChatHelper.BroadcastChatMessage(f, color);
            }
        }
        public static void DebugModeCombatText(string text, Rectangle rect, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (ModContent.GetInstance<Config>().debugMode)
            {
                CombatText.NewText(rect, color, text, true, false);
            }
        }    
        #region draw methods
        public static void DrawVoidBloodGlow() { }
        public static void DrawInsanityUI() { }
        public static void DrawEnergyUI() { }
        public static void DrawInsanityOverlay()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>();

            if (modPlayer.sanity > modPlayer.sanityMax * 0.50f) return;

            Color color = new Color(255, InsanityOverlay.gbValues, InsanityOverlay.gbValues) * InsanityOverlay.transparency;
            int width = insanityTex.Width;
            int num = 10;
            Rectangle rect = Main.player[Main.myPlayer].getRect();
            rect.Inflate((width - rect.Width) / 2, (width - rect.Height) / 2 + num / 2);
            rect.Offset(-(int)Main.screenPosition.X, -(int)Main.screenPosition.Y + (int)Main.player[Main.myPlayer].gfxOffY - num);
            Rectangle destinationRectangle1 = Rectangle.Union(new Rectangle(0, 0, 1, 1), new Rectangle(rect.Right - 1, rect.Top - 1, 1, 1));
            Rectangle destinationRectangle2 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, 0, 1, 1), new Rectangle(rect.Right, rect.Bottom - 1, 1, 1));
            Rectangle destinationRectangle3 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left, rect.Bottom, 1, 1));
            Rectangle destinationRectangle4 = Rectangle.Union(new Rectangle(0, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left - 1, rect.Top, 1, 1));
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, destinationRectangle1, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, destinationRectangle2, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, destinationRectangle3, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, destinationRectangle4, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(insanityTex, rect, color);
        }
        public static void BlackScreenTrans()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            Color color = Color.Black * modPlayer.screenTransAlpha;
            Rectangle rect = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, rect, color);
        }
        public void DrawComputer(SpriteBatch spriteBatch)
        {
            #region Computer
            var background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText").Value;
            var mod = ModLoader.GetMod("ElementsAwoken");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            string text = player.computerText;
            if (Language.ActiveCulture.Name == "ru-RU") // TODO: Maybe the problem is with other languages too
            {
                if (player.computerTextNo == 0)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 1)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText1Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 401, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 2)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 3)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 4)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 5)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 6)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 7)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerTextRuText9").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 385, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 8)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText1Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 401, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 9)
                {
                    if (player.guardianEntryNo == 0)
                    {
                        background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                        spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                    }
                    else
                    {
                        background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                        spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                    }
                }
                if (player.computerTextNo == 10)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 11)
                {
                    background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText1Ru").Value;
                    spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                    Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 401, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                }
                if (player.computerTextNo == 12)
                {
                    if (player.azanaEntryNo == 0)
                    {
                        background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                        spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                    }
                    else
                    {
                        background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText9Ru").Value;
                        spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 395, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                    }
                }
                if (player.computerTextNo == 13)
                {
                    if (player.ancientsEntryNo == 0)
                    {
                        background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText13Ru").Value;
                        spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 425, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                    }
                    else
                    {
                        background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText13Ru").Value;
                        spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                        Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 425, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
                    }
                }
            }
            else
            {
                background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ComputerText").Value;
                spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
                Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth / 2 - 230, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
            #endregion
        }
        public void DrawSanityBook()
        {
            Player player = Main.player[Main.myPlayer];
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>();

            var background = ModContent.Request<Texture2D>("ElementsAwoken/Extra/InsanityBookUI").Value;
            Main.spriteBatch.Draw(background, new Rectangle(Main.screenWidth - 350, Main.screenHeight - 250, background.Width, background.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            // draw the positive
            Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, ModContent.GetInstance<EALocalization>().SanityRegens, Main.screenWidth - 330, Main.screenHeight - 220, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            for (int i = 0; i < awakenedPlayer.sanityRegens.Count; i++)
            {
                string text = awakenedPlayer.sanityRegensName[i] + ": " + awakenedPlayer.sanityRegens[i];
                int yPos = Main.screenHeight - 200 + 25 * i;
                Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, text, Main.screenWidth - 330, yPos, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
            // draw the negative
            Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, ModContent.GetInstance<EALocalization>().SanityDrains, Main.screenWidth - 150, Main.screenHeight - 220, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            for (int i = 0; i < awakenedPlayer.sanityDrains.Count; i++)
            {
                string text = awakenedPlayer.sanityDrainsName[i] + ": " + awakenedPlayer.sanityDrains[i];
                int textLength = (int)FontAssets.MouseText.Value.MeasureString(text).X;
                int xPos = Main.screenWidth - 150;
                if (Main.screenWidth - 150 + textLength > Main.screenWidth) xPos = Main.screenWidth - textLength - 35;
                int yPos = Main.screenHeight - 200 + 25 * i;
                Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, text, xPos, yPos, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
        }
        public void DrawInfoAccs()
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            var EALocalization = ModContent.GetInstance<EALocalization>();
            int amountOfInfoActive = CountAvailableInfo() - 1; // - 1 so it starts at 0 when 1 is equipped
            int amountOfInfoEquipped = CountEquippedInfo() - 1;

            float num4 = 215f;
            int whichInfoDrawing = -1;
            string text = "";

            for (int infoNum = 0; infoNum < 3; infoNum++)
            {
                string text2 = "";
                string hoverText = "";

                if (infoNum == 0 && modPlayer.alchemistTimer)
                {
                    if ((!modPlayer.hideEAInfo[0] || Main.playerInventory))
                    {
                        hoverText = EALocalization.BDPS;
                        whichInfoDrawing = infoNum;

                        text2 = modPlayer.buffDPS + " " + EALocalization.BDPS1;
                        if (modPlayer.buffDPS <= 0)
                        {
                            text2 = Language.GetTextValue("GameUI.NoDPS");
                        }
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[0])
                    {
                        amountOfInfoActive++;
                    }
                }
                else if (infoNum == 1 && modPlayer.dryadsRadar)
                {
                    if ((!modPlayer.hideEAInfo[1] || Main.playerInventory))
                    {
                        hoverText = EALocalization.NEB;
                        whichInfoDrawing = infoNum;

                        text2 = modPlayer.nearbyEvil + " " + EALocalization.Nearby;
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[1])
                    {
                        amountOfInfoActive++;
                    }
                }
                else if (infoNum == 2 && modPlayer.rainMeter)
                {
                    if ((!modPlayer.hideEAInfo[2] || Main.playerInventory))
                    {
                        hoverText = EALocalization.RainTime;
                        whichInfoDrawing = infoNum;

                        text2 = (int)(Main.rainTime / 60) + " " + EALocalization.SR;
                        if (Main.rainTime == 0) text2 = EALocalization.Clear;
                    }
                    amountOfInfoEquipped++;
                    if (!modPlayer.hideEAInfo[2])
                    {
                        amountOfInfoActive++;
                    }
                }
                if (text2 != "")
                {
                    int mH = (int)((typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)).GetValue(null));
                    if ((Main.npcChatText == null || Main.npcChatText == "") && player.sign < 0)
                    {
                        int distBetweenInfo = 22;
                        if (Main.screenHeight < 650)
                        {
                            distBetweenInfo = 20;
                        }


                        int iconPosX;
                        int iconPosY;
                        if (!Main.playerInventory)
                        {
                            iconPosX = Main.screenWidth - 280;
                            iconPosY = -32;
                            if (Main.mapStyle == 1 && Main.mapEnabled)
                            {
                                iconPosY += 254;
                            }
                        }
                        else if (Main.ShouldDrawInfoIconsHorizontally)
                        {
                            iconPosX = Main.screenWidth - 280 + 20 * amountOfInfoEquipped - 10;
                            iconPosY = 94;
                            if (Main.mapStyle == 1 && Main.mapEnabled)
                            {
                                iconPosY += 254;
                            }
                            if (amountOfInfoEquipped + 1 > 12)
                            {
                                iconPosX -= 20 * 12;
                                iconPosY += 26;
                            }
                        }
                        else
                        {
                            int num28 = (int)(52f * Main.inventoryScale);
                            iconPosX = 697 - num28 * 4 + Main.screenWidth - 800 + 20 * (amountOfInfoEquipped % 2);
                            iconPosY = 114 + mH + num28 * 7 + num28 / 2 + 20 * (amountOfInfoEquipped / 2) + 8 * (amountOfInfoEquipped / 4) - 20;
                            if (Main.EquipPage == 2)
                            {
                                iconPosX += num28 + num28 / 2;
                                iconPosY -= num28;
                            }
                        }
                        if (whichInfoDrawing >= 0)
                        {                   
                            //иконки
                            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/EAInfo" + whichInfoDrawing).Value;
                            int a = 0; int b;
                            if ((Language.ActiveCulture.Name == "ru-RU")) { a = 50; b = 78; }
                            else { a += 0; b = 78; }
                            Vector2 vector = new((float)iconPosX - a, (float)(iconPosY + b + distBetweenInfo * amountOfInfoActive + 52));

                            Color white = Color.White;
                            bool flag14 = false;
                            if (Main.playerInventory)
                            {
                                vector = new Vector2((float)iconPosX, (float)iconPosY);
                                if ((float)Main.mouseX >= vector.X && (float)Main.mouseY >= vector.Y && (float)Main.mouseX <= vector.X + (float)tex.Width && (float)Main.mouseY <= vector.Y + (float)tex.Height && !PlayerInput.IgnoreMouseInterface)
                                {
                                    flag14 = true;
                                    player.mouseInterface = true;
                                    if (Main.mouseLeft && Main.mouseLeftRelease)
                                    {
                                        SoundEngine.PlaySound(SoundID.MenuTick);
                                        Main.mouseLeftRelease = false;
                                        modPlayer.hideEAInfo[whichInfoDrawing] = !modPlayer.hideEAInfo[whichInfoDrawing];
                                    }
                                    if (!Main.mouseText)
                                    {
                                        text = hoverText;
                                        Main.mouseText = true;
                                    }
                                }
                                if (modPlayer.hideEAInfo[whichInfoDrawing])
                                {
                                    white = new Color(80, 80, 80, 70);
                                }

                            }
                            else if ((float)Main.mouseX >= vector.X && (float)Main.mouseY >= vector.Y && (float)Main.mouseX <= vector.X + (float)tex.Width && (float)Main.mouseY <= vector.Y + (float)tex.Height && !Main.mouseText)
                            {
                                Main.mouseText = true;
                                text = hoverText;
                            }
                            //UILinkPointNavigator.SetPosition(1558 + amountOfInfoEquipped - 1, vector + tex.Size() * 0.75f);
                            if (!Main.playerInventory && modPlayer.hideEAInfo[whichInfoDrawing])
                            {
                                white = Color.Transparent;
                            }
                            Main.spriteBatch.Draw(tex, vector, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), white, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                            if (flag14)
                            {
                                Texture2D outline = ModContent.Request<Texture2D>("Terraria/Images" + Path.DirectorySeparatorChar.ToString() + "UI" + Path.DirectorySeparatorChar.ToString() + "InfoIcon_13").Value;
                                Main.spriteBatch.Draw(outline, vector - Vector2.One * 2f, null, Main.OurFavoriteColor, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, hoverText, new Vector2(Main.mouseX, Main.mouseX), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                            }
                            iconPosX += 20;
                        }
                        if (!Main.playerInventory)
                        {
                            Vector2 vector2 = new Vector2(1f);

                            Vector2 vector3 = FontAssets.MouseText.Value.MeasureString(text2);
                            if (vector3.X > num4)
                            {
                                vector2.X = num4 / vector3.X;
                            }
                            if (vector2.X < 0.58f)
                            {
                                vector2.Y = 1f - vector2.X / 3f;
                            }
                            for (int num31 = 0; num31 < 5; num31++)
                            {
                                int num32 = 0;
                                int num33 = 0;
                                Color black = Color.Black;
                                if (num31 == 0)
                                {
                                    num32 = -2;
                                }
                                if (num31 == 1)
                                {
                                    num32 = 2;
                                }
                                if (num31 == 2)
                                {
                                    num33 = -2;
                                }
                                if (num31 == 3)
                                {
                                    num33 = 2;
                                }
                                if (num31 == 4)
                                {
                                    black = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                                }
                                int offsetX = 0;
                                int offsetY = 0;
                                if ((Language.ActiveCulture.Name == "ru-RU")) { offsetX = -47; offsetY = 6; }
                                else { offsetX += 0; offsetY = 6; }
                                // текст
                                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, text2, new Vector2(iconPosX + num32 + offsetX, iconPosY + 74 + distBetweenInfo * amountOfInfoActive + num33 + 48 + offsetY), black, 0f, default(Vector2), vector2, SpriteEffects.None, 0f);
                            }
                        }
                        if (!string.IsNullOrEmpty(text))
                        {
                            if (Main.playerInventory)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                            }
                            Vector2 drawTextPos = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(16.0f);
                            if (drawTextPos.X + FontAssets.MouseText.Value.MeasureString(text).X > Main.screenWidth)
                            {
                                drawTextPos.X -= FontAssets.MouseText.Value.MeasureString(text).X; // to stop it drawing off the side
                            }
                            int offsetX = -0;
                            int offsetY = -0;
                            // при наводке
                            Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, text, drawTextPos.X + offsetX, drawTextPos.Y + offsetY, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), Color.Black, new Vector2());
                        }
                    }
                }
            }
        }
        private int CountAvailableInfo()
        {
            Player player = Main.player[Main.myPlayer];
            int num = 0;
            if (!player.hideInfo[0] && player.accWatch > 0) num++;
            if (!player.hideInfo[1] && player.accWeatherRadio) num++;
            if (!player.hideInfo[2] && player.accFishFinder)  num++;
            if (!player.hideInfo[3] && player.accCompass > 0) num++;
            if (!player.hideInfo[4] && player.accDepthMeter > 0)num++;
            if (!player.hideInfo[5] && player.accThirdEye) num++;
            if (!player.hideInfo[6] && player.accJarOfSouls) num++;
            if (!player.hideInfo[7] && player.accCalendar) num++;
            if (!player.hideInfo[9] && player.accStopwatch) num++;
            if (!player.hideInfo[10] && player.accOreFinder)num++;
            if (!player.hideInfo[11] && player.accCritterGuide) num++;
            if (!player.hideInfo[12] && player.accDreamCatcher) num++;
            return num;
        }
        private int CountEquippedInfo()
        {
            Player player = Main.player[Main.myPlayer];
            int num = 0;
            if (player.accWatch > 0) num++;
            if (player.accWeatherRadio) num++;
            if (player.accFishFinder) num++;
            if (player.accCompass > 0) num++;
            if (player.accDepthMeter > 0) num++;
            if (player.accThirdEye) num++;
            if (player.accJarOfSouls) num++;
            if (player.accCalendar) num++;
            if (player.accStopwatch) num++;
            if (player.accOreFinder) num++;
            if (player.accCritterGuide) num++;
            if (player.accDreamCatcher) num++;
            return num;
        }
        internal static void DrawStringOutlined(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale)
        {
            // outlines
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X - 1, position.Y), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X + 1, position.Y), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X, position.Y - 1), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X, position.Y + 1), Color.Black * (color.A / 255f), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            // actual text
            spriteBatch.DrawString(FontAssets.DeathText.Value, text, new Vector2(position.X, position.Y), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
        public void DrawEncounterText(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            string text = player.encounterText;
            if (player.encounterTextTimer > 0)
            {
                Vector2 textSize = FontAssets.DeathText.Value.MeasureString(text);
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;

                Vector2 pos = new Vector2(textPositionLeft, Main.screenHeight / 2 - 200);
                float rand = player.finalText ? 3.5f : 2f;
                pos.X += Main.rand.NextFloat(-rand, rand);
                pos.Y += Main.rand.NextFloat(-rand, rand);
                Color color = player.finalText ? new Color(player.encounterTextAlpha, 0, 0, player.encounterTextAlpha) : new Color(player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha);
                DrawStringOutlined(spriteBatch, text, pos, color, 1f);
            }
        }
        #endregion
        public void Credits()
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            Vector2 monitorScale = new Vector2((float)Main.screenWidth / 1920f, (float)Main.screenHeight / 1080f);
            var mod = ModLoader.GetMod("ElementsAwoken");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>();
            b = 1;
            if(b == 1)DrawStringOutlined(Main.spriteBatch, EALocalization.Credits, new Vector2(Main.screenWidth - 220 * monitorScale.X, Main.screenHeight - 35 * monitorScale.Y), Color.White * (player.escHeldTimer > 0 ? 1 : 0.4f), 0.5f * monitorScale.Y);
            if (MyWorld.creditsCounter > 180 && MyWorld.creditsCounter < 480)
            {
                var logo = ModContent.Request<Texture2D>("ElementsAwoken/Extra/ElementsAwoken").Value;
                float scale = 1.4f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - 180, 300); // old: (float)Math.Sin(MathHelper.Lerp(0, (float)Math.PI, ((float)MyWorld.creditsCounter - 300f) / 180f))
                Main.spriteBatch.Draw(logo, new Vector2(Main.screenWidth / 2 - ((logo.Width * scale) / 2), Main.screenHeight / 2 - 200 - ((logo.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            #region slide 1
            if (MyWorld.creditsCounter == player.screenDuration + 60)
            {
                string text = EALocalization.Credits1 + " " + "ThatOneJuicyOrange_";
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 300 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            int statueDuration = player.screenDuration - 60 * 3;
            int statueOffset = (player.screenDuration - statueDuration) / 2;
            if (MyWorld.creditsCounter > player.screenDuration + statueOffset && MyWorld.creditsCounter < player.screenDuration * 2 - statueOffset)
            {
                var statue = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/ThatOneJuicyOrangeStatue").Value;
                float scale = 1.5f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration - statueOffset, statueDuration);
                Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            #endregion
            #region slide 2
            if (MyWorld.creditsCounter == player.screenDuration * 2 + 60)
            {
                string text = EALocalization.Credits2;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 2 + 90)
            {
                string text = "Ranipla";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 2 + 120)
            {
                string text = "GENIH WAT";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 160 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 90, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 2 + 150)
            {
                string text = "Universe";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 100 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 120, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 2 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 3 - statueOffset)
            {
                var statue = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/RaniplaStatue").Value;
                var statue2 = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/GenihWatStatue").Value;
                float scale = 1.5f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 2 - statueOffset, statueDuration);
                Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 2) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(statue2, new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 2) / 2 - ((statue2.Width * scale) / 2), Main.screenHeight / 2 - ((statue2.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
            }
            #endregion
            #region slide 3
            if (MyWorld.creditsCounter == player.screenDuration * 3 + 60)
            {
                string text = EALocalization.Credits3;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 3 + 90)
            {
                string text = "Burst";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 3 + 120)
            {
                string text = "Amadis";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 160 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 90, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 3 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 4 - statueOffset)
            {
                var statue = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/BurstStatue").Value;
                var statue2 = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/AmadisStatue").Value;
                float scale = 1.5f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 3 - statueOffset, statueDuration);
                Main.spriteBatch.Draw(statue, new Vector2(Main.screenWidth - 200 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 3) / 2 - ((statue.Width * scale) / 2), Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(statue2, new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 3) / 2 - ((statue2.Width * scale) / 2), Main.screenHeight / 2 - ((statue2.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
            }
            #endregion
            #region slide 4
            if (MyWorld.creditsCounter == player.screenDuration * 4 + 60)
            {
                string text = EALocalization.Credits4;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 4 + 90)
            {
                string text = "Silvestre";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 4 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 5 - statueOffset)
            {
                var statue = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/Azana").Value;
                float scale = 1.2f * monitorScale.Y;
                Color color = Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 4 - statueOffset, statueDuration);
                Main.spriteBatch.Draw(statue, new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 4) / 2, Main.screenHeight / 2 - ((statue.Height * scale) / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0f);
            }
            #endregion
            #region slide 5
            if (MyWorld.creditsCounter == player.screenDuration * 5 + 60)
            {
                string text = EALocalization.Credits4;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 5 + 90)
            {
                string text = "Aloe";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 5 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 6 - statueOffset)
            {
                float scale = 1.5f * monitorScale.Y;
                int offset = 30;
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/Shimmerspark").Value, new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 5 - statueOffset, statueDuration), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/SolarGeneratorIV").Value, new Vector2(Main.screenWidth - 500 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2 - 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset - player.screenDuration * 5 - statueOffset, statueDuration - offset), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/DesertGun").Value, new Vector2(500 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 2 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/AncientsSword").Value, new Vector2(Main.screenWidth - 400 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 3 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 3), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/BurnerGenerator").Value, new Vector2(900 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 5) / 3, Main.screenHeight / 2 + 300 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 4 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 4), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/DesertTrailers").Value, new Vector2(150 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 5) / 2, 100 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 5 - player.screenDuration * 5 - statueOffset, statueDuration - offset * 5), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            #endregion
            #region slide 6
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 60)
            {
                string text = EALocalization.Credits4;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 90)
            {
                string text = "NnickykunN";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 105)
            {
                string text = "Darkpuppey";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 160 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 75, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 120)
            {
                string text = "Skeletony";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 100 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 90, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 135)
            {
                string text = "Mayhemm";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 40 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 105, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 6 + 150)
            {
                string text = "daimgamer";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 + 20 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 120, scale, pos);
            }
            if (MyWorld.creditsCounter > player.screenDuration * 6 + statueOffset && MyWorld.creditsCounter < player.screenDuration * 7 - statueOffset)
            {
                float scale = 1.1f * monitorScale.Y;
                int offset = 45;
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/AncientAmalgam").Value, new Vector2(200 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 6) / 3, Main.screenHeight / 2 + 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - player.screenDuration * 6 - statueOffset, statueDuration), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/ScourgeFighter").Value, new Vector2(Main.screenWidth - 500 * monitorScale.X - (MyWorld.creditsCounter - player.screenDuration * 6) / 3, Main.screenHeight / 2 - 200 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset - player.screenDuration * 6 - statueOffset, statueDuration - offset), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Extra/Credits/Permafrost").Value, new Vector2(300 * monitorScale.X + (MyWorld.creditsCounter - player.screenDuration * 6) / 3, Main.screenHeight / 2 - 250 * monitorScale.Y), null, Color.White * GetFadeAlpha(MyWorld.creditsCounter - offset * 2 - player.screenDuration * 6 - statueOffset, statueDuration - offset * 2), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            #endregion
            #region slide 7
            if (MyWorld.creditsCounter == player.screenDuration * 7 + 60)
            {
                string text = EALocalization.Credits5;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 7 + 90)
            {
                string text = "Eoite\n" +
                    "ChamCham\n" +
                    "Aegida\n" +
                    "Buildmonger\n" +
                    "YukkiKun\n" +
                    "Superbaseball101\n" +
                    "Crow\n" +
                    "Lantard\n" +
                    "Megaswave\n" +
                    "Keydrian\n" +
                    "InstaFiz\n" +
                    "kREEpDABoom\n" +
                    "Big Spoon";
                float scale = 0.7f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale) + 45, Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            #endregion
            #region slide 8
            if (MyWorld.creditsCounter == player.screenDuration * 8 + 60)
            {
                string text = EALocalization.Credits6;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 8 + 90)
            {
                string text = "Dradonhunter11\n" +
                    "jopojelly\n" +
                    "Misaro\n" +
                    "Alpaca121\n" +
                    "ReedemtheD3ad!\n" +
                    "Oinite12\n" +
                    "And many more";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            #endregion
            #region 1.4
            if (MyWorld.creditsCounter == player.screenDuration * 8 + 450)
            {
                string textHeder = EALocalization.CreditsPort;
                float scaleHeder = 1.3f * monitorScale.Y;
                Vector2 posHeder = new Vector2(FindTextCenterX(textHeder, scaleHeder), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(textHeder, 7 * 60, scaleHeder, posHeder);
                string text = $"{EALocalization.CreditsPort1}  \n{EALocalization.CreditsPort2}";
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            #endregion
            #region slide 9
            if (MyWorld.creditsCounter == player.screenDuration * 9 + 230)
            {
                string text = EALocalization.Credits7;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 330 * monitorScale.Y);
                DrawScreenText(text, 7 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 9 + 270)
            {
                string text = EALocalization.Credits8;
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale) + 60, Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 7 * 60 - 60, scale, pos);
            }
            #endregion
            #region slide 10
            if (MyWorld.creditsCounter == player.screenDuration * 10 + 220)
            {
                string text = EALocalization.Credits9;
                float scale = 1.3f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 360 * monitorScale.Y);
                DrawScreenText(text, 12 * 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 10 + 260)
            {
                string text = EALocalization.Credits10;
                float scale = 1.4f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 290 * monitorScale.Y);
                DrawScreenText(text, 12 * 60 - 60, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 10 + 290)
            {
                string text = EALocalization.Credits11;
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale), Main.screenHeight / 2 - 220 * monitorScale.Y);
                DrawScreenText(text, 12 * 60 - 90, scale, pos);
            }
            if (MyWorld.creditsCounter == player.screenDuration * 10 + 310)
            {
                string text = EALocalization.Credits12;
                float scale = 1f * monitorScale.Y;
                Vector2 pos = new Vector2(FindTextCenterX(text, scale) + 300, Main.screenHeight / 2 + 40 * monitorScale.Y);
                DrawScreenText(text, 10 * 60, scale, pos);
                b = 0;
            }
            #endregion
        }

        private float FindTextCenterX(string text, float scale)
        {
            Vector2 textSize = FontAssets.DeathText.Value.MeasureString(text) * scale;
            float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;
            return textPositionLeft;
        }
        public void DrawScreenText(string text, int duration, float scale, Vector2 pos)
        {
            for (int i = 0; i < screenText.Length - 1; i++)
            {
                if (screenText[i] == "")
                {
                    screenText[i] = text;
                    screenTextAlpha[i] = 0;
                    screenTextTimer[i] = 0;
                    screenTextDuration[i] = duration;
                    screenTextScale[i] = scale;
                    screenTextPos[i] = pos;
                    break;
                }
            }
        }
        public float GetFadeAlpha(float timer, float duration)
        {
            if (timer < duration / 8) return timer / (duration / 8f);
            else if (timer > duration - (duration / 8)) return 1 - (timer - (duration - duration / 8f)) / (duration / 8f); // probably a better way to do this
            else return 1f;
        }
        //    Mod fargos = ModLoader.GetMod("Fargowiltas");
        //    if (fargos != null)
        //    {
        //        // AddSummon, order or value in terms of vanilla bosses, your mod internal name, summon item internal name, inline method for retrieving downed value, price to sell for in copper
        //        fargos.Call("AddSummon", wasteland, "ElementsAwoken", "WastelandSummon", (Func<bool>)(() => MyWorld.downedWasteland), Item.buyPrice(0, 10, 0, 0));
        //        fargos.Call("AddSummon", toySlime, "ElementsAwoken", "ToySlimeSummon", (Func<bool>)(() => MyWorld.downedToySlime), Item.buyPrice(0, 2, 50, 0));
        //        fargos.Call("AddSummon", infernace, "ElementsAwoken", "InfernaceSummon", (Func<bool>)(() => MyWorld.downedInfernace), Item.buyPrice(0, 17, 50, 0));
        //        fargos.Call("AddSummon", observer, "ElementsAwoken", "CosmicObserverSummon", (Func<bool>)(() => MyWorld.downedCosmicObserver), Item.buyPrice(0, 20, 0, 0));
        //        fargos.Call("AddSummon", scourge, "ElementsAwoken", "ScourgeFighterSummon", (Func<bool>)(() => MyWorld.downedScourgeFighter), Item.buyPrice(0, 45, 0, 0));
        //        fargos.Call("AddSummon", regaroth, "ElementsAwoken", "RegarothSummon", (Func<bool>)(() => MyWorld.downedRegaroth), Item.buyPrice(0, 50, 0, 0));
        //        //fargos.Call("AddSummon", celestials, "ElementsAwoken", "CelestialSummon", (Func<bool>)(() => MyWorld.downedCelestial), Item.buyPrice(0, 55, 0, 0));
        //        fargos.Call("AddSummon", permafrost, "ElementsAwoken", "PermafrostSummon", (Func<bool>)(() => MyWorld.downedPermafrost), Item.buyPrice(0, 60, 0, 0));
        //        fargos.Call("AddSummon", obsidious, "ElementsAwoken", "ObsidiousSummon", (Func<bool>)(() => MyWorld.downedObsidious), Item.buyPrice(0, 60, 0, 0));
        //        fargos.Call("AddSummon", aqueous, "ElementsAwoken", "AqueousSummon", (Func<bool>)(() => MyWorld.downedAqueous), Item.buyPrice(0, 67, 50, 0));
        //        fargos.Call("AddSummon", keepers, "ElementsAwoken", "AncientDragonSummon", (Func<bool>)(() => (MyWorld.downedAncientWyrm && MyWorld.downedEye)), Item.buyPrice(0, 80, 0, 0));
        //        //fargos.Call("AddSummon", guardian, "ElementsAwoken", "GuardianSummon", (Func<bool>)(() => MyWorld.downedToySlime), Item.buyPrice(0, 2, 50, 0));
        //        //fargos.Call("AddSummon", dotv, "ElementsAwoken", "VoidEventSummon", (Func<bool>)(() => MyWorld.downedToySlime), Item.buyPrice(0, 2, 50, 0));
        //        fargos.Call("AddSummon", volcanox, "ElementsAwoken", "VolcanoxSummon", (Func<bool>)(() => MyWorld.downedVolcanox), Item.buyPrice(1, 0, 50, 0));
        //        //fargos.Call("AddSummon", vlevi, "ElementsAwoken", "Summon", (Func<bool>)(() => MyWorld.downedToySlime), Item.buyPrice(0, 2, 50, 0));
        //        //fargos.Call("AddSummon", azana, "ElementsAwoken", "Summon", (Func<bool>)(() => (MyWorld.downedAzana || MyWorld.sparedAzana)), Item.buyPrice(0, 2, 50, 0));
        //        fargos.Call("AddSummon", ancients, "ElementsAwoken", "AncientsSummon", (Func<bool>)(() => MyWorld.downedAncients), Item.buyPrice(1, 25, 0, 0));

        //    }
        //    Mod mystaria = ModLoader.GetMod("Mystaria");
        //    if (mystaria != null)
        //    {
        //        mystaria.Call("AddSpellCombo", ItemType("FrostMine"), 1, 1, 2, 2, 3, 1, 0, 0, 4, 2);
        //    }
        //    Mod censusMod = ModLoader.GetMod("Census");
        //    if (censusMod != null)
        //    {
        //        //censusMod.Call("TownNPCCondition", NPCType("Example Person"), $"Have [i:{ItemType<Items.ExampleItem>()}] or [i:{ItemType<Items.Placeable.ExampleBlock>()}] in inventory and build a house out of [i:{ItemType<Items.Placeable.ExampleBlock>()}] and [i:{ItemType<Items.Placeable.ExampleWall>()}]");
        //        censusMod.Call("TownNPCCondition", NPCType("Alchemist"), "Defeat the Brain of Cthulhu or Eater of Worlds");
        //        censusMod.Call("TownNPCCondition", NPCType("Storyteller"), "Always available");
        //    }

        
        public static void ApplyScreenShakeToAll(float amount)
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (player.active)
                {
                    modPlayer.screenshakeAmount = amount;
                }
            }
        }
        private void RemoveManaCap(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);

            if (!cursor.TryGotoNext(MoveType.Before,
                                    i => i.MatchLdfld("Terraria.Player", "statManaMax2"),
                                    i => i.MatchLdcI4(400)))
            {
                Logger.Fatal("Could not find instruction to patch");
                return;
            }

            cursor.Next.Next.Operand = int.MaxValue;
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ElementsAwokenMessageType msgType = (ElementsAwokenMessageType)reader.ReadByte();
            switch (msgType)
            {
                case ElementsAwokenMessageType.StarHeartSync:
                    byte playernumber = reader.ReadByte();
                    MyPlayer starHeartPlayer = Main.player[playernumber].GetModPlayer<MyPlayer>();
                    int voidHeartsUsed = reader.ReadInt32();
                    int chaosHeartsUsed = reader.ReadInt32();
                    int lunarStarsUsed = reader.ReadInt32();
                    starHeartPlayer.voidHeartsUsed = voidHeartsUsed;
                    starHeartPlayer.chaosHeartsUsed = chaosHeartsUsed;
                    starHeartPlayer.lunarStarsUsed = lunarStarsUsed;
                    break;
                case ElementsAwokenMessageType.AwakenedSync:
                    playernumber = reader.ReadByte();
                    AwakenedPlayer awakenedPlayer = Main.player[playernumber].GetModPlayer<AwakenedPlayer>();
                    int sanity = reader.ReadInt32();
                    awakenedPlayer.sanity = sanity;
                    break;
                case ElementsAwokenMessageType.EnergySync:
                    playernumber = reader.ReadByte();
                    PlayerEnergy energyPlayer = Main.player[playernumber].GetModPlayer<PlayerEnergy>();
                    int energy = reader.ReadInt32();
                    energyPlayer.energy = energy;
                    break;
                default:
                    Logger.WarnFormat("Elements Awoken: Unknown Message type: {0}", msgType);
                    break;
            }
        }
        public enum ElementsAwokenMessageType : byte
        {
            StarHeartSync,
            AwakenedSync,
            EnergySync,
            Storyteller,
        }
    }
}