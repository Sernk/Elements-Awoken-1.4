using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class SkyFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 45;
            Item.knockBack = 6;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<CloudBlast>();
            Item.shootSpeed = 5f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(7);
            position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 10f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                int num1 = damage / 2;
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, num1, knockback, player.whoAmI);
            }

            int numberProjectiles2 = 4;
            for (int index = 0; index < numberProjectiles2; ++index)
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
                float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f;
                float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, vector2_1.X, vector2_1.Y, SpeedX, SpeedY, ModContent.ProjectileType<SkyFuryRain>(), damage, knockback, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
            }
            return false;
        }
    }
}