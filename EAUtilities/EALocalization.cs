using ElementsAwoken.Content.NPCs.Bosses.Aqueous;
using ElementsAwoken.Content.NPCs.Bosses.Azana;
using ElementsAwoken.Content.NPCs.Bosses.Infernace;
using ElementsAwoken.Content.NPCs.Bosses.Obsidious;
using ElementsAwoken.Content.NPCs.Bosses.Permafrost;
using ElementsAwoken.Content.NPCs.Bosses.Regaroth;
using ElementsAwoken.Content.NPCs.Bosses.ScourgeFighter;
using ElementsAwoken.Content.NPCs.Bosses.TheGuardian;
using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.Content.NPCs.Bosses.Volcanox;
using ElementsAwoken.Content.NPCs.Bosses.Wasteland;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.EAUtilities
{
    public class EALocalization : ModSystem
    {
        #region Bestiary
        public string WastelandBoss => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.WastelandBoss");
        public string ToySlime => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.ToySlime");
        public string InfernaceBestiary => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.InfernaceBestiary");
        public string CosmicObserverBestiary => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.CosmicObserverBestiary");
        public string ScourgeFighterBoss => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.ScourgeFighterBoss");
        public string Regaroth => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.Regaroth");
        public string AqueousBoss => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.AqueousBoss");
        public string ObsidiousBoss => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.ObsidiousBoss");
        public string TheEye => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.TheEye");
        public string AncientWyrm => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.AncientWyrm");
        public string TheGuardianFly => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.TheGuardianFly");
        public string ShadeWyrm => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.ShadeWyrm");
        public string VoidLeviathan => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.VoidLeviathan");
        public string ElderShadeWyrm => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.ElderShadeWyrm");
        public string RadiantMaster => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.RadiantMaster");
        public string AzanaEyeBestiary => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.AzanaEyeBestiary");
        public string AzanaBestiary => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.AzanaBestiary");
        public string VolcanoxBoss => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.VolcanoxBoss");
        public string IzarisBoss => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.IzarisBoss");
        public string Kirvein => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.Kirvein");
        public string Krecheus => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.Krecheus");
        public string Xernon => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.Xernon");
        public string AncientAmalgam => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Bosses.AncientAmalgam");
        public string Scorpion => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Enemies.Scorpion");
        public string MiniToySlime => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Enemies.MiniToySlime");
        public string Furosia => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Enemies.Furosia");
        public string RegarothMinion => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Enemies.RegarothMinion");
        public string VoidLeviathanOrb => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Enemies.VoidLeviathanOrb");
        public string Firefly => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Enemies.Firefly");
        public string MysticBunny => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.Critters.MysticBunny");
        public string Storyteller => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.TownNPCs.Storyteller");
        public string Storyteller2 => Language.GetTextValue("Mods.ElementsAwoken.Bestiary.TownNPCs.Storyteller2");
        #endregion
        #region Ore
        public string Voidite => Language.GetTextValue("Mods.ElementsAwoken.Ore.Voidite");
        public string Luminite => Language.GetTextValue("Mods.ElementsAwoken.Ore.Luminite");
        #endregion
        #region Nurse
        public string Nurse => Language.GetTextValue("Mods.ElementsAwoken.Nurse.Said");
        public string Nurse1 => Language.GetTextValue("Mods.ElementsAwoken.Nurse.Said1");
        #endregion
        #region Resurs
        public string Sanity => Language.GetTextValue("Mods.ElementsAwoken.Resurs.Sanity");
        public string Energy => Language.GetTextValue("Mods.ElementsAwoken.Resurs.Energy");
        public static string LifeFruit => Language.GetTextValue("Mods.ElementsAwoken.Resurs.LifeFruit");
        public static string EmptyHeart => Language.GetTextValue("Mods.ElementsAwoken.Resurs.EmptyHeart");
        public static string ChaosHeart => Language.GetTextValue("Mods.ElementsAwoken.Resurs.ChaosHeart");
        public static string CompressorHeart => Language.GetTextValue("Mods.ElementsAwoken.Resurs.CompressorHeart");
        public static string LunarStarMana => Language.GetTextValue("Mods.ElementsAwoken.Resurs.LunarStarMana");

        #endregion
        #region Tooltips
        #region ItemEnergy
        public string ItemEnergy => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ItemEnergy");
        public string ItemEnergy1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ItemEnergy1");
        #endregion
        #region ItemsTooltips
        public string Awakened => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Awakened");
        public string Donator => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Donator");
        public string Artifact => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Artifact");
        public string Developer => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Developer");
        public string Youtuber => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Youtuber");
        public string Betatest => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Betatest");
        #endregion
        #region ElementalCapsule
        public string ElementalCapsule => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ElementalCapsule");
        public string ElementalCapsule1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ElementalCapsule1");
        public string ElementalCapsule2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ElementalCapsule2");
        #endregion
        #region VoidbloodHeart
        public string VoidbloodHeart => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidbloodHeart");
        public string VoidbloodHeart1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidbloodHeart1");
        #endregion
        #region MysteriousPotion
        public string MysteriousPotion => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion");
        public string MysteriousPotion1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion1");
        public string MysteriousPotion2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion2");
        public string MysteriousPotion3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion3");
        public string MysteriousPotion4 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion4");
        public string MysteriousPotion5 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion5");
        public string MysteriousPotion6 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion6");
        public string MysteriousPotion7 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion7");
        public string MysteriousPotion8 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion8");
        public string MysteriousPotion9 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion9");
        public string MysteriousPotion10 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion10");
        public string MysteriousPotion11 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion11");
        public string MysteriousPotion12 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion12");
        public string MysteriousPotion13 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion13");
        public string MysteriousPotion14 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion14");
        public string MysteriousPotion15 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion15");
        public string MysteriousPotion16 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion16");
        public string MysteriousPotion17 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion17");
        public string MysteriousPotion18 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion18");
        public string MysteriousPotion19 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion19");
        public string MysteriousPotion20 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion20");
        public string MysteriousPotion21 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion21");
        public string MysteriousPotion22 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.MysteriousPotion22");
        #endregion
        #region HoneyCocoon
        public string HoneyCocoon => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.HoneyCocoon");
        public string HoneyCocoon1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.HoneyCocoon1");
        public string HoneyCocoon2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.HoneyCocoon2");
        #endregion
        #region DeathMirror
        public string DeathMirror => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.DeathMirror");
        public string DeathMirror1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.DeathMirror1");
        #endregion
        #region Railgun
        public string Railgun => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Railgun");
        #endregion
        #region Desolation
        public string Desolation => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Desolation");
        public string Desolation1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Desolation1");
        public string Desolation2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Desolation2");
        public string Desolation3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Desolation3");
        #region VoidLeviathansAegis
        public string VoidLeviathansAegis => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidLeviathansAegis");
        public string VoidLeviathansAegis1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidLeviathansAegis1");
        public string VoidLeviathansAegis2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidLeviathansAegis2");
        public string VoidLeviathansAegis3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidLeviathansAegis3");
        public string VoidLeviathansAegis4 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidLeviathansAegis4");
        public string VoidLeviathansAegis5 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidLeviathansAegis5");
        public string VoidLeviathansAegis6 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidLeviathansAegis6");
        public string VoidLeviathansAegis7 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidLeviathansAegis7");
        #endregion
        #endregion
        #region GiftOfTheArchaic
        public string GiftOfTheArchaic => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GiftOfTheArchaic");
        public string GiftOfTheArchaic1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GiftOfTheArchaic1");
        #endregion
        #region GreatLens
        public string GreatLens => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GreatLens");
        public string GreatLens1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GreatLens1");
        public string GreatLens2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GreatLens2");
        public string GreatLens3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GreatLens3");
        #endregion
        #region AqueousSummon
        public string AqueousSummon => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AqueousSummon");
        #endregion
        #region InfernaceSummon
        public string InfernaceSummon => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfernaceSummon");
        #endregion
        #region RadiantRainSummon
        public string RadiantRainSummon => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.RadiantRainSummon");
        #endregion
        #region SlimeRainSummon
        public string SlimeRainSummon => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.SlimeRainSummon");
        #endregion
        #region ToySlimeClaw
        public string ToySlimeClaw => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ToySlimeClaw");
        public string ToySlimeClaw1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ToySlimeClaw1");
        public string ToySlimeClaw2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ToySlimeClaw2");
        public string ToySlimeClaw3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ToySlimeClaw3");
        public string ToySlimeClaw4 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.ToySlimeClaw4");
        #endregion
        #region VoidEventSummon
        public string VoidEventSummon => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidEventSummon");
        public string VoidEventSummon1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidEventSummon1");
        public string VoidEventSummon2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidEventSummon2");
        public string VoidEventSummon3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidEventSummon3");
        public string VoidEventSummon4 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidEventSummon4");
        public string VoidEventSummon5 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidEventSummon5");
        public string VoidEventSummon6 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.VoidEventSummon6");
        #endregion
        #region AncientsSummon
        public string AncientsSummon => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon");
        public string AncientsSummon1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon1");
        public string AncientsSummon2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon2");
        public string AncientsSummon3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon3");
        public string AncientsSummon4 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon4");
        public string AncientsSummon5 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon5");
        public string AncientsSummon6 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon6");
        public string AncientsSummon7 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon7");
        public string AncientsSummon8 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon8");
        public string AncientsSummon9 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon9");
        public string AncientsSummon10 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.AncientsSummon10");
        #endregion
        #region EoitesWrath
        public string EoitesWrath => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.EoitesWrath");
        public string EoitesWrath1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.EoitesWrath1");
        #endregion
        #region SandstormStone
        public string SandstormStone => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.SandstormStone");
        public string SandstormStone1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.SandstormStone1");
        #endregion
        #region RainSigil
        public string RainSigil => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.RainSigil");
        public string RainSigil1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.RainSigil1");
        public string RainSigil2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.RainSigil2");
        #endregion
        #region InfinityStone
        public string InfinityStone => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityStone");
        #endregion
        #region InfinityGauntlet
        public string InfinityGauntlet => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet");
        public string InfinityGauntlet1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet1");
        public string InfinityGauntlet2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet2");
        public string InfinityGauntlet3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet3");
        public string InfinityGauntlet4 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet4");
        public string InfinityGauntlet5 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet5");
        public string InfinityGauntlet6 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet6");
        public string InfinityGauntlet7 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet7");
        public string InfinityGauntlet8 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet8");
        public string InfinityGauntlet9 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet9");
        public string InfinityGauntlet10 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet10");
        public string InfinityGauntlet11 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet11");
        public string InfinityGauntlet12 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.InfinityGauntlet12");
        #endregion
        #region CreditsSetup
        public string CreditsSetup => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.CreditsSetup");
        public string CreditsSetup1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.CreditsSetup1");
        #endregion
        #region Statues
        public string Statues => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Statues");
        #endregion
        #region BoostDrive
        public string BoostDrive => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.BoostDrive");
        #endregion
        #region GravityCore
        public string GravityCore => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GravityCore");
        public string GravityCore1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GravityCore1");
        public string GravityCore2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GravityCore2");
        public string GravityCore3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.GravityCore3");
        #endregion
        #region HyperDrive
        public string HyperDrive => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.HyperDrive");
        #endregion
        #region Generator
        public string Generator => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Generator");
        public string Generator1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.Generator1");
        public string BiofuelBurner => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.BiofuelBurner");
        public string BiofuelBurner1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.BiofuelBurner1");
        public string BiofuelBurner2 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.BiofuelBurner2");
        public string BiofuelBurner3 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.BiofuelBurner3");
        public string LifeforceBurner => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.LifeforceBurner");
        public string LifeforceBurner1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.LifeforceBurner1");
        public string WindGenerator => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.WindGenerator");
        public string WindGenerator1 => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.WindGenerator1");
        #endregion
        public string UndownerTerraria => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.UndownerTerraria");
        #endregion
        #region TimeToken
        public string TimeToken => Language.GetTextValue("Mods.ElementsAwoken.Tooltips.TimeToken");
        #endregion
        #region AccInfo
        public string NEB => Language.GetTextValue("Mods.ElementsAwoken.AccInfo.NEB");
        public string Nearby => Language.GetTextValue("Mods.ElementsAwoken.AccInfo.Nearby");
        public string BDPS => Language.GetTextValue("Mods.ElementsAwoken.AccInfo.BDPS");
        public string BDPS1 => Language.GetTextValue("Mods.ElementsAwoken.AccInfo.BDPS1");
        public string RainTime => Language.GetTextValue("Mods.ElementsAwoken.AccInfo.RainTime");
        public string SR => Language.GetTextValue("Mods.ElementsAwoken.AccInfo.SR");
        public string Clear => Language.GetTextValue("Mods.ElementsAwoken.AccInfo.Clear");
        #endregion
        #region Said
        public string Wasteland => Language.GetTextValue("Mods.ElementsAwoken.Said.Wasteland");
        public string WastelandDryad => Language.GetTextValue("Mods.ElementsAwoken.Said.WastelandDryad");
        public string WastelandDryad1 => Language.GetTextValue("Mods.ElementsAwoken.Said.WastelandDryad1");
        public string Infernace => Language.GetTextValue("Mods.ElementsAwoken.Said.Infernace");
        public string Infernace1 => Language.GetTextValue("Mods.ElementsAwoken.Said.Infernace1");
        public string Infernace2 => Language.GetTextValue("Mods.ElementsAwoken.Said.Infernace2");
        public string InfernaceWifeSoul => Language.GetTextValue("Mods.ElementsAwoken.Said.InfernaceWifeSoul");
        public string InfernaceWifeSoul1 => Language.GetTextValue("Mods.ElementsAwoken.Said.InfernaceWifeSoul1");
        public string InfernaceWifeSoul2 => Language.GetTextValue("Mods.ElementsAwoken.Said.InfernaceWifeSoul2");
        public string InfernaceWifeSoul3 => Language.GetTextValue("Mods.ElementsAwoken.Said.InfernaceWifeSoul3");
        public string ScourgeFighter => Language.GetTextValue("Mods.ElementsAwoken.Said.ScourgeFighter");
        public string ScourgeFighter1 => Language.GetTextValue("Mods.ElementsAwoken.Said.ScourgeFighter1");
        public string ScourgeFighter2 => Language.GetTextValue("Mods.ElementsAwoken.Said.ScourgeFighter2");
        public string Aqueous => Language.GetTextValue("Mods.ElementsAwoken.Said.Aqueous");
        public string Aqueous1 => Language.GetTextValue("Mods.ElementsAwoken.Said.Aqueous1");
        public string TheGuardian => Language.GetTextValue("Mods.ElementsAwoken.Said.TheGuardian");
        public string VoidLeviathanHead => Language.GetTextValue("Mods.ElementsAwoken.Said.VoidLeviathanHead");
        public string AzanaEye => Language.GetTextValue("Mods.ElementsAwoken.Said.AzanaEye");
        public string AzanaEye1 => Language.GetTextValue("Mods.ElementsAwoken.Said.AzanaEye1");
        public string Azana => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana");
        public string Azana1 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana1");
        public string Azana2 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana2");
        public string Azana3 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana3");
        public string Azana4 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana4");
        public string Azana5 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana5");
        public string Azana6 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana6");
        public string Azana7 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana7");
        public string Azana8 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana8");
        public string Azana9 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana9");
        public string Azana10 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana10");
        public string Azana11 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana11");
        public string Azana12 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana12");
        public string Azana13 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana13");
        public string Azana14 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana14");
        public string Azana15 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana15");
        public string Azana16 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana16");
        public string Azana17 => Language.GetTextValue("Mods.ElementsAwoken.Said.Azana17");
        public string Obsidious => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious");
        public string Obsidious1 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious1");
        public string Obsidious2 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious2");
        public string Obsidious3 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious3");
        public string Obsidious4 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious4");
        public string Obsidious5 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious5");
        public string Obsidious6 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious6");
        public string Obsidious7 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious7");
        public string Obsidious8 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious8");
        public string Obsidious9 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious9");
        public string Obsidious10 => Language.GetTextValue("Mods.ElementsAwoken.Said.Obsidious10");
        public string Volcanox => Language.GetTextValue("Mods.ElementsAwoken.Said.Volcanox");
        public string Izaris => Language.GetTextValue("Mods.ElementsAwoken.Said.Izaris");
        public string Izaris1 => Language.GetTextValue("Mods.ElementsAwoken.Said.Izaris1");
        public string ShardBase => Language.GetTextValue("Mods.ElementsAwoken.Said.ShardBase");
        #endregion
        #region SetBonus
        public string SetBonusToolTips => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.SetBonusToolTips");
        public string CosmicalusVisor => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.CosmicalusVisor");
        public string VoidWalkersVisage => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.VoidWalkersVisage");
        public string VoidWalkersHood => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.VoidWalkersHood");
        public string VoidWalkersHelm => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.VoidWalkersHelm");
        public string VoidWalkersGreatmask => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.VoidWalkersGreatmask");
        public string EnergyWeaversHelm => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.EnergyWeaversHelm");
        public string MechSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.MechSetBonus");
        public string ForgedSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.ForgedSetBonus");
        public string CrowsSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.CrowsSetBonus");
        public string EoitesSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.EoitesSetBonus");
        public string AwokenWoodSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.AwokenWoodSetBonus");
        public string FireDemonsSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.FireDemonsSetBonus");
        public string GelticConquerorSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.GelticConquerorSetBonus");
        public string AridSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.AridSetBonus");
        public string ElementalSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.ElementalSetBonus");
        public static string EmpyreanSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.EmpyreanSetBonus");
        public static string VoidSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.VoidSetBonus");
        public string OceanicSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.OceanicSetBonus");
        public string DragonmailGreathelmSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.DragonmailGreathelmSetBonus");
        public string DragonmailHoodSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.DragonmailHoodSetBonus");
        public string DragonmailMaskSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.DragonmailMaskSetBonus");
        public string DragonmailVisageSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.DragonmailVisageSetBonus");
        public string ComfySetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.ComfySetBonus");
        public string PutridSetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.PutridSetBonus");
        public string ToySetBonus => Language.GetTextValue("Mods.ElementsAwoken.SetBonus.ToySetBonus");
        #endregion
        #region LooTCondition 
        public string AwakenedModeActive => Language.GetTextValue("Mods.ElementsAwoken.LooTCondition.AwakenedModeActive");
        public string ScourgeLootCondition => Language.GetTextValue("Mods.ElementsAwoken.LooTCondition.ScourgeLootCondition");
        public string AncientWyrmHeadDeath => Language.GetTextValue("Mods.ElementsAwoken.LooTCondition.AncientWyrmHeadDeath");
        public string TheEyeCondition => Language.GetTextValue("Mods.ElementsAwoken.LooTCondition.TheEye");
        public string BIDRC => Language.GetTextValue("Mods.ElementsAwoken.LooTCondition.BIDRC");
        public string AllMechs => Language.GetTextValue("Mods.ElementsAwoken.LooTCondition.AllMechs");
        public string BiomeConditions => Language.GetTextValue("Mods.ElementsAwoken.LooTCondition.BiomeConditions");
        #endregion
        #region ShopCondition
        public string BossString => Language.GetTextValue("Mods.ElementsAwoken.ShopCondition.BossString");
        public string BossString1 => Language.GetTextValue("Mods.ElementsAwoken.ShopCondition.BossString1");
        #endregion
        #region Credits
        public string Credits => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits");
        public string Credits1 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits1");
        public string Credits2 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits2");
        public string Credits3 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits3");
        public string Credits4 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits4");
        public string Credits5 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits5");
        public string Credits6 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits6");
        public string Credits7 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits7");
        public string CreditsPort => Language.GetTextValue("Mods.ElementsAwoken.Credits.CreditsPort");
        public string CreditsPort1 => Language.GetTextValue("Mods.ElementsAwoken.Credits.CreditsPort1");
        public string CreditsPort2 => Language.GetTextValue("Mods.ElementsAwoken.Credits.CreditsPort2");
        public string Credits8 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits8");
        public string Credits9 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits9");
        public string Credits10 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits10");
        public string Credits11 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits11");
        public string Credits12 => Language.GetTextValue("Mods.ElementsAwoken.Credits.Credits12");

        #endregion
        #region SanityBook
        public string SanityBookOnCraft => Language.GetTextValue("Mods.ElementsAwoken.SanityBook.SanityBookOnCraft");
        public string SanityRegens => Language.GetTextValue("Mods.ElementsAwoken.SanityBook.SanityRegens");
        public string SanityDrains => Language.GetTextValue("Mods.ElementsAwoken.SanityBook.SanityDrains");
        #endregion
        #region Other
        public string Prompt => Language.GetTextValue("Mods.ElementsAwoken.Prompt");
        public string AwakenedSummonItem => Language.GetTextValue("Mods.ElementsAwoken.AwakenedSummonItem");
        public string IsSummon => Language.GetTextValue("Mods.ElementsAwoken.IsSummon");
        public string AwakenedMode => Language.GetTextValue("Mods.ElementsAwoken.AwakenedMode");
        public string AwakenedModeActive2 => Language.GetTextValue("Mods.ElementsAwoken.AwakenedModeActive2");
        public string AwakenedModeNonActive => Language.GetTextValue("Mods.ElementsAwoken.AwakenedModeNonActive");
        public string Unbound => Language.GetTextValue("Mods.ElementsAwoken.Unbound");
        #endregion
        #region BiomeName
        public string Desert => Language.GetTextValue("Mods.ElementsAwoken.BiomeName.Desert");
        public string Sky => Language.GetTextValue("Mods.ElementsAwoken.BiomeName.Sky");
        public string Hell => Language.GetTextValue("Mods.ElementsAwoken.BiomeName.Hell");
        public string Frost => Language.GetTextValue("Mods.ElementsAwoken.BiomeName.Frost");
        public string Beach => Language.GetTextValue("Mods.ElementsAwoken.BiomeName.Beach");
        #endregion
        #region BossNAME
        public static string BossName(int BossProgres)
        {
            var e = ModContent.GetInstance<EALocalization>();
            return BossProgres switch
            {
                1 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.EyeofCthulhu)}",
                2 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.EaterofWorldsHead) + " " + e.BossString1 + " " + Lang.GetNPCNameValue(NPCID.BrainofCthulhu)}",
                3 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.SkeletronHead)}",
                4 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.WallofFlesh)}",
                5 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.TheDestroyer)}",
                6 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.Spazmatism) + " " + e.BossString1 + " " + Lang.GetNPCNameValue(NPCID.Retinazer)}",
                7 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.SkeletronPrime)}",
                8 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.Plantera)}",
                9 =>  $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.Golem)}",
                10 => $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.DukeFishron)}",
                11 => $"{e.BossString + " " + Lang.GetNPCNameValue(NPCID.MoonLordHead)}",
                _ => "",
            };
        }
        public static string MBossName(int MBossProgres)
        {
            var e = ModContent.GetInstance<EALocalization>();
            return MBossProgres switch
            {
                1 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<Wasteland>())}",
                2 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<Infernace>())}",
                3 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<ScourgeFighter>())}",
                4 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<RegarothHead>())}",
                5 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<Obsidious>())}",
                6 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<Permafrost>())}",
                7 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<Aqueous>())}",
                8 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<TheGuardian>())}",
                9 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<Volcanox>())}",
                10 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<VoidLeviathanHead>())}",
                11 => $"{e.BossString + " " + Lang.GetNPCNameValue(ModContent.NPCType<Azana>())}",
                _ => ""
            };
        }
        #endregion
    }
}