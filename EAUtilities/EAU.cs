using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Tiles.Crafting;
using ElementsAwoken.EASystem.UI;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken
{
    /// <summary>
    /// Elements Awoken Utilities
    /// </summary>
    public class EAU
    {
        public const string ProjTexture = "ElementsAwoken/Content/Projectiles/Blank";
        public static string ModifyProjTexture(string name) => $"ElementsAwoken/Content/Projectiles/{name}";
        public static string SoundPath(string name) => $"ElementsAwoken/Sounds/Item/{name}";
        public static int FireEssence => ModContent.ItemType<FireEssence>();
        public static int VoidEssence => ModContent.ItemType<VoidEssence>();
        public static int ElementalForge => ModContent.TileType<ElementalForge>();
        public static int ChaoticCrucible => ModContent.TileType<ChaoticCrucible>();
        public static int PinkFlame => DustID.Firework_Pink;
        public static int HandsOfDespair => ModContent.BuffType<HandsOfDespair>();
        public static int Dragonfire => ModContent.BuffType<Dragonfire>();
        /// <param name="seconds"></param>  <param name="minutes"> 1 minutes = 3600 2 minutes = 7200 3 minutes = 11800 </param> <param name="hours"></param>
        public static int BuffsTime(int seconds = 0, int minutes = 0, int hours = 0) { int result = 0; result += seconds * 60; result += minutes * 60 * 60; result += hours * 60 * 60 * 60; return result; }
        public static SpriteBatch Sb => Main.spriteBatch;
        public static void SetSoul(int type) => ItemID.Sets.AnimatesAsSoul[type] = true;
        public static void Longer(int type) => BuffID.Sets.LongerExpertDebuff[type] = true;
        public static void CanBeCleared(int type) => BuffID.Sets.NurseCannotRemoveDebuff[type] = true;
        public static void DSCursor(int type) => TileID.Sets.DisableSmartCursor[type] = true;
        public static IEntitySource Proj(Projectile projectile) => projectile.GetSource_FromThis();
        public static IEntitySource NPCs(NPC npc) => npc.GetSource_FromThis();
        public static IEntitySource Play(Player player) => player.GetSource_FromThis();
        public static float BalanceHP(float Base, float Scale, float ScaleMaster, int AwakenedHP, float FactorScale = 1.25f, float FactorMaster = 1.50f, float FactorForAwakened = 0.15f, int roundTo = 10000)
        {
            float value = Base * Scale * FactorScale;

            if (Main.masterMode)value *= ScaleMaster * FactorMaster;       
            else if (Main.expertMode) value *= ScaleMaster;
            if (MyWorld.awakenedMode) value += AwakenedHP * FactorForAwakened;

            return (float)(Math.Round(value / roundTo) * roundTo);
        }
        public static int BalanceHP2(int Base, int Expert, int Master, int Awakened, int AwakenedMaster)
        {
            if (Main.masterMode)
            {
                if (MyWorld.awakenedMode) return AwakenedMaster;
                else return Master;
            }
            if (Main.expertMode)
            {
                if (MyWorld.awakenedMode) return Awakened;
                else return Expert;
            }
            else return Base;
        }
        public static float BalanceDamage(int Base, float Scale, float ScaleMaster, int AwakenedDamage, float FactorScale = 1.25f, float FactorMaster = 1.25f)
        {
            float value = Base * Scale * FactorScale * ScaleMaster * FactorMaster;
            if (MyWorld.awakenedMode) return AwakenedDamage;
            else return value;
        }
        public static int BalanceDefense(int Base, int AwakenedDefense)
        {
            if (MyWorld.awakenedMode) return AwakenedDefense;
            else return Base;
        }
        public static int GetDustID()
        {
            return Main.rand.Next(4) switch
            {
                0 => ModContent.DustType<AncientRed>(), 1 => ModContent.DustType<AncientGreen>(),
                2 => ModContent.DustType<AncientBlue>(), 3 => ModContent.DustType<AncientPink>(),
                _ => ModContent.DustType<AncientRed>(),
            };
        }
        public static int GetDustIDForAI(Projectile projectile)
        {
            int index = (int)projectile.ai[0];

            if (index >= 0 && index < EAList.DustIDs.Count) return EAList.DustIDs[index];

            return EAList.DustIDs[0];
        }
        public static List<int> VanillaOreIDs()
        {
            List<int> idList =
            [
                ItemID.CopperOre,
                ItemID.TinOre,
                ItemID.IronOre,
                ItemID.LeadOre,
                ItemID.SilverOre,
                ItemID.TungstenOre,
                ItemID.GoldOre,
                ItemID.PlatinumOre,
                ItemID.Meteorite,
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre,
                ItemID.Hellstone,
                ItemID.CobaltOre,
                ItemID.PalladiumOre,
                ItemID.MythrilOre,
                ItemID.OrichalcumOre,
                ItemID.AdamantiteOre,
                ItemID.TitaniumOre,
                ItemID.ChlorophyteOre,
                ItemID.LunarOre,
            ];
            return idList;
        }
    }
}