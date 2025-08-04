using ElementsAwoken.Content.Projectiles.Other;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class Starstruck : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            EAU.Longer(Type);
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().starstruck = true;
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.Firework_Pink)];
                dust.velocity.Y = Main.rand.NextFloat(-0.5f, -5f);
                dust.scale = Main.rand.NextFloat(0.4f, 1.2f);
                dust.noGravity = false;
                dust.fadeIn = 0.2f;
            }
            if (Main.rand.Next(10) == 0)
            {
                Projectile.NewProjectile(EAU.Play(player), player.position.X + Main.rand.Next(player.width), player.position.Y + Main.rand.Next(player.height), Main.rand.NextFloat(-1,1), Main.rand.NextFloat(-1, 1), ProjectileType<StarstruckP>(), 0, 0, Main.myPlayer, Main.rand.Next(0, 5));
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().starstruck = true;
        }
    }
}