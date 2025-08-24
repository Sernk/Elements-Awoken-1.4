using ElementsAwoken.Content.Buffs.Cooldowns;
using ElementsAwoken.Content.Items.BossDrops.zVanilla;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Developer
{
    public class ViridiumGreatsword : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;         
            Item.damage = 300;
            Item.knockBack = 6;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<ViridiumLightning>();
            Item.shootSpeed = 32f;
            Item.GetGlobalItem<EATooltip>().developer = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ElectricArcing"), Item.position);

                int numberProjectiles = Main.rand.Next(2, 3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    int velocity = (int)player.velocity.X;
                    if (velocity < 0)
                    {
                        velocity *= -1;
                    }

                    Vector2 vector94 = new Vector2(speed.X, speed.Y);
                    float ai = (float)Main.rand.Next(100);
                    Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * 4f;
    
                    Projectile.NewProjectile(source, position.X, position.Y, vector95.X, vector95.Y, ModContent.ProjectileType<ViridiumLightning>(), damage, 0f, Main.myPlayer, vector94.ToRotation(), ai);
                }
            }
            return false;
        }
        public override bool AltFunctionUse(Player player)
        {
            if (player.FindBuffIndex(ModContent.BuffType<DashCooldown>()) == -1)
            {
                return true;
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            if (Main.rand.Next(100) == 0)
            {
                SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ElectricArcing"), Item.position);

                Vector2 vector94 = new Vector2(2, 2);
                float ai = (float)Main.rand.Next(100);
                Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(360)) * 2f;
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, vector95.X, vector95.Y, ModContent.ProjectileType<ViridiumLightningPassive>(), 300, 0f, Main.myPlayer, (vector94.RotatedByRandom(360)).ToRotation(), ai);
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.direction == 1 )
                {
                    Item.noMelee = true;
                    Item.noUseGraphic = true;
                    player.velocity.X += 25f;
                    Item.useStyle = 5;
                    player.AddBuff(ModContent.BuffType<DashCooldown>(), 300);
                }
                if (player.direction == -1)
                {
                    Item.noMelee = true;
                    Item.noUseGraphic = true;
                    player.velocity.X -= 25f;
                    Item.useStyle = 5;
                    player.AddBuff(ModContent.BuffType<DashCooldown>(), 300);
                }
                player.GetModPlayer<MyPlayer>().viridiumDash = true;
                player.GetModPlayer<MyPlayer>().dashDustTimer = 60;
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<ViridiumExplosion>(), 400, 10f, player.whoAmI, 0.0f, 0.0f);

                for (int l = 0; l < 100; l++)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 222, 0f, 0f, 100, default(Color), 2f);
                    Dust expr_A4_cp_0 = Main.dust[num];
                    expr_A4_cp_0.position.X = expr_A4_cp_0.position.X + (float)Main.rand.Next(-20, 21);
                    Dust expr_CB_cp_0 = Main.dust[num];
                    expr_CB_cp_0.position.Y = expr_CB_cp_0.position.Y + (float)Main.rand.Next(-20, 21);
                    Main.dust[num].velocity *= 0.4f;
                    Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(player.cWaist, player);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                        Main.dust[num].noGravity = true;
                    }
                }
            }
            else
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
                Item.useStyle = 1;
            }
            return base.CanUseItem(player);
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += Math.Abs(player.velocity.X) * 0.28f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<NinjaKatana>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}