using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.BossDrops.RadiantMaster
{
    [AutoloadEquip(EquipType.Face)]
    public class RadiantCrown : ModItem
    {
        private int cooldown = 0;
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.accessory = true;
            Item.rare = RarityType<Awakened>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.radiantCrown = true;

            float range = 500f;
            cooldown--;
            if (cooldown <= 0)
            {
                for (int l = 0; l < Main.maxNPCs; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (!nPC.CanBeChasedBy(this) || Vector2.Distance(player.Center, nPC.Center) > range) continue;
                    if (player.whoAmI == Main.myPlayer)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.Play(player), nPC.Center, Vector2.Zero, ProjectileType<RadiantStorm>(), 200, 5f, player.whoAmI, 0f, 0f)];
                        proj.Bottom = nPC.Bottom;
                        break;
                    }
                }
                cooldown = 120;
            }
        }
    }
}
