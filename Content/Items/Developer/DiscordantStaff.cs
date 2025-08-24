using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Developer
{
    public class DiscordantStaff : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 300;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 18;
            Item.width = 54;
            Item.height = 52;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(1, 50, 0, 0);
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<DiscordantBolt>();
            Item.shootSpeed = 18f;
            Item.GetGlobalItem<EATooltip>().developer = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 1 + Main.rand.Next(3); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(7));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
