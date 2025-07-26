using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.EASystem
{
    public class PlayerEnergy : ModPlayer
    {

        public int energy = 0;
        public int batteryEnergy = 0; // updated by the battieres
        public int maxEnergy = 0; // updated by the battieres

        public bool soulConverter;
        public bool kineticConverter;
        public override void ResetEffects()
        {
            batteryEnergy = 0;

            soulConverter = false;
            kineticConverter = false;
        }

        public override void PostUpdateMiscEffects()
        {
            maxEnergy = batteryEnergy;
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
            if (energy < 0)
            {
                energy = 0;
            }
        }
        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            tag["energy"] = energy;
        }
        public override void LoadData(TagCompound tag)
        {
            energy = tag.GetInt("energy");
        }
        //public override void CopyClientState(ModPlayer clientClone)/* tModPorter Suggestion: Replace Item.Clone usages with Item.CopyNetStateTo */
        //{
        //    MyPlayer clone = clientClone as MyPlayer;
        //}
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)ElementsAwoken.ElementsAwokenMessageType.EnergySync);
            packet.Write((byte)Player.whoAmI);
            packet.Write(energy);
            packet.Send(toWho, fromWho);
        }
        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (kineticConverter && energy < maxEnergy)
            {
                int energyAmount = 0;
                if (proj.hostile)
                {
                    energyAmount += (int)(proj.damage * 0.15f);
                }
                int projSpeed = (int)((Math.Abs(proj.velocity.X) + Math.Abs(proj.velocity.Y)) * 0.5f);
                energyAmount += projSpeed;
                energy += energyAmount;
                CombatText.NewText(Player.getRect(), Color.LightBlue, energyAmount, false, false);
            }
        }
    }
}