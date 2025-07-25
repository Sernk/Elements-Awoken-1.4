using ElementsAwoken.Content.NPCs;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class HandsOfDespair : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hands of Despair");
            // Description.SetDefault("They grasp at your limbs, pulling you down");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().handsOfDespair = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().handsOfDespair = true;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Other.HandsOfDespair>()] == 0)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.Other.HandsOfDespair>(), 0, 0f, player.whoAmI, 0f, player.whoAmI);
            }
        }
    }
}