using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    public class SandstormTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;          
            Item.damage = 18;
            Item.mana = 13;
            Item.knockBack = 2.25f;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = 5;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item66;
            Item.shoot = ModContent.ProjectileType<Skysand>();
            Item.shootSpeed = 8f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertEssence>(), 4);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 25);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 10);
            recipe.AddIngredient(ItemID.FallenStar, 4);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(2,6);
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
                float SpeedX = num16 + (float)Main.rand.Next(-50, 51) * 0.02f;
                float SpeedY = num17 + (float)Main.rand.Next(-50, 51) * 0.02f;
                Projectile.NewProjectile(source, vector2_1.X, vector2_1.Y, SpeedX, SpeedY, type, damage, knockback, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
            }
            return false;
        }
    }
}
