using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Projectiles.Amulet;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class AmuletOfDestruction : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.accessory = true;
            Item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife <= (player.statLifeMax2 * 0.2f))
            {
                player.lifeRegen += 10;
            }
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.voidEnergyCharge < 3600 && modPlayer.voidEnergyTimer == 0)
            {
                modPlayer.voidEnergyCharge++;
            }
            player.buffImmune[ModContent.BuffType<ExtinctionCurse>()] = true;
            player.buffImmune[ModContent.BuffType<HandsOfDespair>()] = true;

            if (!hideVisual)
            {
                if (Main.rand.Next(12) == 0)
                {
                    float x = player.position.X + (float)Main.rand.Next(-400, 400);
                    float y = player.position.Y - (float)Main.rand.Next(500, 800);
                    Vector2 vector = new Vector2(x, y);
                    float num12 = player.Center.X - vector.X;
                    float num13 = player.Center.Y - vector.Y;
                    num12 += (float)Main.rand.Next(-100, 101);
                    float num = 23;
                    float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
                    num14 = num / num14;
                    num12 *= num14;
                    num13 *= num14;
                    int num15 = Projectile.NewProjectile(player.GetSource_Accessory(Item), x, y, num12, num13, ModContent.ProjectileType<AmuletProj>(), 200, 5f, player.whoAmI, 0f, 0f);
                    Main.projectile[num15].ai[1] = player.position.Y;
                }
            }
        }
    }
}