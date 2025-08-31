using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Eoite
{
    public class EoitesWrath : ModItem
    {
        public int weaponMode = 0;

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.damage = 235;
            Item.knockBack = 4;
            Item.mana = 10;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item12;
            Item.shoot = ModContent.ProjectileType<EoiteBeam>();
            Item.shootSpeed = 18f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                weaponMode++;
                if (weaponMode >= 2)
                {
                    weaponMode = 0;
                }
                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                string text = "";
                switch (weaponMode)
                {
                    case 0:
                        text = ModContent.GetInstance<EALocalization>().EoitesWrath;

                        Item.useTime = 13;
                        Item.useAnimation = 13;
                        break;
                    case 1:
                        text = ModContent.GetInstance<EALocalization>().EoitesWrath1;

                        Item.useTime = 13;
                        Item.useAnimation = 13;
                        break;
                    default:
                        return base.CanUseItem(player);
                }
                CombatText.NewText(player.getRect(), Color.DarkMagenta, text, false, false);
            }
            else
            {
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                if (weaponMode == 0)
                {
                    int numberProjectiles = 4;
                    for (int index = 0; index < numberProjectiles; ++index)
                    {
                        Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                        vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                        vector2_1.Y -= (float)(100 * index);
                        float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
                        float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                        if ((double)num13 < 0.0) num13 *= -1f;
                        if ((double)num13 < 20.0) num13 = 20f;
                        float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                        float num15 = Item.shootSpeed / num14;
                        float num16 = num12 * num15;
                        float num17 = num13 * num15;
                        float SpeedX = num16 + (float)Main.rand.Next(-40, 1) * 0.02f;
                        float SpeedY = num17 + (float)Main.rand.Next(-40, 1) * 0.02f;
                        Projectile.NewProjectile(source, vector2_1.X, vector2_1.Y, SpeedX, SpeedY, ModContent.ProjectileType<EoiteBeam2>(), damage, knockback, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
                    }
                }
                if (weaponMode == 1)
                {
                    int numberProjectiles = 3;
                    for (int i = 0; i < numberProjectiles; ++i)
                    {
                        Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                        vector2.X = (float)Main.mouseX + Main.screenPosition.X;
                        vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
                        Projectile.NewProjectile(source, vector2.X - 20 + i * 20, vector2.Y - 1600, 0, 10, ModContent.ProjectileType<EoiteBeam>(), damage, 3f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(source, vector2.X - 20 + i * 20, vector2.Y - 900, 0, 26, ModContent.ProjectileType<EoiteBlast>(), damage, 3f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 6);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.Amethyst, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}