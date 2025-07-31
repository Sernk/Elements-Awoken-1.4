using ElementsAwoken.Content.Projectiles.Minions.BreathOfDarkness;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class BreathOfDarkness : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 120;
            Item.knockBack = 3;
            Item.mana = 10;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.shoot = ModContent.ProjectileType<VleviHead>();
            Item.shootSpeed = 7f; 
        }
        public override bool CanUseItem(Player player)
        {
            float minions = 0;

            for (int j = 0; j < Main.maxProjectiles; j++)
            {
                if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && Main.projectile[j].minion)
                {
                    minions += Main.projectile[j].minionSlots;
                }
            }
            if (minions >= player.maxMinions)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float velocityX = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float velocityY = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;

            int head = -1;
            int tail = -1;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer)
                {
                    if (head == -1 && Main.projectile[i].type == ModContent.ProjectileType<VleviHead>())
                    {
                        head = i;
                    }
                    if (tail == -1 && Main.projectile[i].type == ModContent.ProjectileType<VleviTail>())
                    {
                        tail = i;
                    }
                    if (head != -1 && tail != -1)
                    {
                        break;
                    }
                }
            }
            if (head == -1 && tail == -1)
            {
                velocityX = 0f;
                velocityY = 0f;
                vector2.X = (float)Main.mouseX + Main.screenPosition.X;
                vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;

                int current = Projectile.NewProjectile(source, vector2.X, vector2.Y, velocityX, velocityX, ModContent.ProjectileType<VleviHead>(), damage, knockBack, Main.myPlayer);

                int previous = current;
                current = Projectile.NewProjectile(source, vector2.X, vector2.Y, velocityX, velocityX, ModContent.ProjectileType<VleviBody>(), damage, knockBack, Main.myPlayer, (float)previous);

                previous = current;
                current = Projectile.NewProjectile(source, vector2.X, vector2.Y, velocityX, velocityX, ModContent.ProjectileType<VleviTail>(), damage, knockBack, Main.myPlayer, (float)previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;
            }
            else if (head != -1 && tail != -1)
            {
                int body = Projectile.NewProjectile(source, vector2.X, vector2.Y, velocityX, velocityY, ModContent.ProjectileType<VleviBody>(), damage, knockBack, Main.myPlayer, Main.projectile[tail].ai[0]);

                Main.projectile[body].localAI[1] = (float)tail;
                Main.projectile[body].ai[1] = 1f;
                Main.projectile[body].netUpdate = true;
                Main.projectile[tail].ai[0] = (float)body;
                Main.projectile[tail].netUpdate = true;
                Main.projectile[tail].ai[1] = 1f;
            }
            return false;
        }
    }
}