using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Content.Buffs.Prompts
{
    public class Psychosis : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Psychosis");
            // Description.SetDefault("The force of the void drags you down\n10 decreased defence and 5% damage reduction\nCauses hallucinations\nDefeat the Void Leviathan to stop this effect\nDisable this effect in the config");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 10;
            player.GetDamage(DamageClass.Melee) *= 0.95f;
            player.GetDamage(DamageClass.Summon) *= 0.95f;
            player.GetDamage(DamageClass.Ranged) *= 0.95f;
            player.GetDamage(DamageClass.Magic) *= 0.95f;
            player.GetDamage(DamageClass.Throwing) *= 0.95f;
        }
    }
}