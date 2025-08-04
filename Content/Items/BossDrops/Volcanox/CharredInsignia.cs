using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Volcanox
{
    public class CharredInsignia : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.expert = true;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.GetDamage(DamageClass.Ranged) += 0.1f;
            player.GetDamage(DamageClass.Magic) += 0.1f;
            player.GetDamage(DamageClass.Summon) += 0.1f;

            player.GetCritChance(DamageClass.Magic) += 6;
            player.GetCritChance(DamageClass.Melee) += 6;
            player.GetCritChance(DamageClass.Ranged) += 6;
            player.GetCritChance(DamageClass.Throwing) += 6;

            player.GetArmorPenetration(DamageClass.Generic) += 20;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.05f, 1f, 0.1f);
            Dust.NewDust(player.position, player.width, player.height, 6, 0, 0, 0, default(Color));
            float maxDist = 500f;
            int random = Main.rand.Next(10);
            if (player.whoAmI == Main.myPlayer)
            {
                if (random == 0)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= maxDist)
                        {
                            nPC.AddBuff(BuffID.OnFire, 180, false);
                        }
                    }
                }
            }
        }
    }
}