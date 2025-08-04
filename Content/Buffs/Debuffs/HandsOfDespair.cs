using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class HandsOfDespair : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            EAU.Longer(Type);
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
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.Other.HandsOfDespair>(), 0, 0f, player.whoAmI, 0f, player.whoAmI);
            }
        }
    }
}