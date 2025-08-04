using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheGuardian
{
    public class FieryCore : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.accessory = true;
            Item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.player[Main.myPlayer].ZoneUnderworldHeight)
            {
                player.GetDamage(DamageClass.Melee) *= 1.10f;
                player.GetDamage(DamageClass.Throwing) *= 1.10f;
                player.GetDamage(DamageClass.Ranged) *= 1.10f;
                player.GetDamage(DamageClass.Magic) *= 1.10f;
                player.GetDamage(DamageClass.Summon) *= 1.10f;
            }
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Burning] = true;
            float num2 = 500f;
            int random = Main.rand.Next(10);
            if (player.whoAmI == Main.myPlayer)
            {
                if (random == 0)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && !nPC.boss && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= num2 && nPC.life <= 30)
                        {
                            Projectile.NewProjectile(EAU.Play(player), nPC.Center.X, nPC.Center.Y, 0f, 0f, 612, 50, 4, Main.myPlayer);
                        }
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= num2)
                        {
                            nPC.AddBuff(BuffID.OnFire, 180, false);
                        }
                    }
                }
            }
        }
    }
}