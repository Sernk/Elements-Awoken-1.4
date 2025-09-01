using ElementsAwoken.Content.Items.Elements.Elemental;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.EASystem.UI
{
    public class HeartsPlayers : ModPlayer
    {
        public int emptyVesselHeartLife;
        public int chaosHeartLife;
        public int voidHeartLife;
        public int shieldLife;

        public int HPBonus;

        public bool HPTier_0 = false;
        public bool ManaBonus = false;

        public int Mana = 0;

        public int CountUseEmptyVessel = 0;
        public int CountUsechaosHear = 0;
        public int CountUseCompressor = 0;

        public bool ChaosHeartVisual = false;
        public bool CompressorVisual = false;
        public bool EmptyVesselVisual = false;
        public bool CombilityEfects = false;
        public override void ResetEffects()
        {
            HPBonus = 0; 
        }
        public override void PreSavePlayer()
        {
            if (HPTier_0) HPTier_0 = true;
            if (ManaBonus) ManaBonus = true;
            if (ChaosHeartVisual) ChaosHeartVisual = true;
            if (EmptyVesselVisual) EmptyVesselVisual = true;
        }
        public override void SaveData(TagCompound tag)
        {
            tag["emptyVesselHeartLife"] = emptyVesselHeartLife;
            tag["chaosHeartLife"] = chaosHeartLife;
            tag["voidHeartLife"] = voidHeartLife;
            tag["shieldLife"] = shieldLife;

            tag["HPBonus"] = HPBonus;

            if (HPTier_0) tag["HPTier_0"] = true;
            if (ManaBonus) tag["ManaBonus"] = true;
            if (ChaosHeartVisual) tag["ChaosHeartVisual"] = true;
            if (EmptyVesselVisual) tag["EmptyVesselVisual"] = true;

            tag["Mana"] = Mana;

            tag["CountUseEmptyVessel"] = CountUseEmptyVessel;
            tag["CountUsechaosHear"] = CountUsechaosHear;
            tag["CountUseCompressor"] = CountUseCompressor;

            tag["VoidHeartVisual"] = CompressorVisual;
            tag["CombilityEfects"] = CombilityEfects;
        }
        public override void LoadData(TagCompound tag)
        {
            emptyVesselHeartLife = tag.GetInt("emptyVesselHeartLife");
            chaosHeartLife = tag.GetInt("chaosHeartLife");
            voidHeartLife = tag.GetInt("voidHeartLife");
            shieldLife = tag.GetInt("shieldLife");

            HPBonus = tag.GetInt("HPBonus");

            HPTier_0 = tag.GetBool("HPTier_0");
            ManaBonus = tag.GetBool("ManaBonus");

            Mana = tag.GetInt("Mana");

            CountUseEmptyVessel = tag.GetInt("CountUseEmptyVessel");
            CountUsechaosHear = tag.GetInt("CountUsechaosHear");
            CountUseCompressor = tag.GetInt("CountUseCompressor");

            ChaosHeartVisual = tag.GetBool("ChaosHeartVisual");
            CompressorVisual = tag.GetBool("VoidHeartVisual");
            EmptyVesselVisual = tag.GetBool("EmptyVesselVisual");
            CombilityEfects = tag.GetBool("CombilityEfects");
        }
        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            int hpBonus = 0;

            hpBonus += CountUsechaosHear * 10;
            if (CountUsechaosHear == 10) hpBonus += CountUseEmptyVessel * 10;
            if (Mana > 0) ManaBonus = true;
            if (Player.armor[1].type == ModContent.ItemType<ElementalBreastplate>()) hpBonus += 100;

            HPBonus = hpBonus;

            health = new StatModifier(1f, 1f, hpBonus, 0);
            mana = new StatModifier(1f, 1f, Mana, 0);
        }
    }
}