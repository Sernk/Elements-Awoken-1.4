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

        public int Mana = 0;

        public int CountUseEmptyVessel = 0;
        public int CountUsechaosHear = 0;
        public int CountUseCompressor = 0;

        public bool ChaosHeartVisual = false;
        public bool CompressorVisual = false;
        public bool EmptyVesselVisual = false;
        public bool CombilityEfects = false;

        public override void SaveData(TagCompound tag)
        {
            tag["emptyVesselHeartLife"] = emptyVesselHeartLife;
            tag["chaosHeartLife"] = chaosHeartLife;
            tag["voidHeartLife"] = voidHeartLife;
            tag["shieldLife"] = shieldLife;

            tag["Mana"] = Mana;

            tag["CountUseEmptyVessel"] = CountUseEmptyVessel;
            tag["CountUsechaosHear"] = CountUsechaosHear;
            tag["CountUseCompressor"] = CountUseCompressor;

            tag["ChaosHeartVisual"] = ChaosHeartVisual;
            tag["VoidHeartVisual"] = CompressorVisual;
            tag["EmptyVesselVisual"] = EmptyVesselVisual;
            tag["CombilityEfects"] = CombilityEfects;
        }
        public override void LoadData(TagCompound tag)
        {
            emptyVesselHeartLife = tag.GetInt("emptyVesselHeartLife");
            chaosHeartLife = tag.GetInt("chaosHeartLife");
            voidHeartLife = tag.GetInt("voidHeartLife");
            shieldLife = tag.GetInt("shieldLife");

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
            int totalBonusHP = (CountUsechaosHear * 10);
            int totalBonusHP2 = (CountUseEmptyVessel * 10);
            int totalBonusMana = (Mana * 10);
            if (CountUsechaosHear == 10)
            {
                health = new StatModifier(1f, 1f, totalBonusHP + totalBonusHP2, 0);
            }
            else
            {
                health = new StatModifier(1f, 1f, totalBonusHP, 0);
            }
            mana = new StatModifier(1f, 1f, Mana, 0);
            if (CountUsechaosHear > 0)
            {
                chaosHeartLife = 1;
            }
            if (CountUseEmptyVessel > 0)
            {
                emptyVesselHeartLife = 1;
            }
        }
    }
}