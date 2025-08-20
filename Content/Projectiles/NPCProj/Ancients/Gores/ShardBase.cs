using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Gores
{
    public class ShardBase : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.timeLeft = 100000;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Main.netMode == 0)
            {
                if (!player.active || player.dead) Projectile.Kill();
            }
            else
            {
                if (Projectile.ai[0] == -1) Projectile.Kill();
                else
                {
                    player = Main.player[(int)Projectile.ai[0]];
                }
                if (!player.active || player.dead)
                {
                    Projectile.ai[0] = FindActivePlayer();
                }
            }
            Projectile.Center = player.Center + new Vector2(0, -300);
            if (!AnyAncients())
            {
                Projectile.ai[1]++;
                if (Projectile.ai[1] == 180)
                {
                    SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/NPC/AncientMergeRise"), Projectile.position);
                }
                if (Projectile.ai[1] == 300)
                {
                    if (Main.autoPause == true)
                    {
                        Main.NewText(ModContent.GetInstance<EALocalization>().ShardBase, Color.LightCyan.R, Color.LightCyan.G, Color.LightCyan.B);
                    }
                    NPC aa = Main.npc[NPC.NewNPC(EAU.Proj(Projectile), (int)Projectile.Center.X, (int)Projectile.Center.Y, ModContent.NPCType<AncientAmalgam>())];
                    aa.netUpdate = true;
                }
            }
        }
        private int FindActivePlayer()
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player p = Main.player[i];
                if (p.active)
                {
                    return i;
                }
            }
            return -1;
        }
        private bool AnyAncients()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Izaris>())) return true;
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Ancients.Kirvein>())) return true;
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Ancients.Krecheus>())) return true;
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Ancients.Xernon>())) return true;
            return false;
        }
    }
}