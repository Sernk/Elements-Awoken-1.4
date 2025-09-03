using CalamityMod.Items.Weapons.Ranged;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier2
{
    public class Taser : ModItem
    {
        public float charge = 0;
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 7;
            Item.knockBack = 1.5f;
            Item.GetGlobalItem<ItemEnergy>().energy = 3;
            Item.useAnimation = 16;
            Item.useTime = 8;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 2;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<TaserLightning>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Taser");
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(new SoundStyle("Antiaris/Sounds/Item/ElectricArcing"), player.position);

            int velocity = (int)player.velocity.X;
            if (velocity < 0)
            {
                velocity *= -1;
            }

            Vector2 vector94 = new Vector2(speed.X, speed.Y);
            float ai = (float)Main.rand.Next(100);
            Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * 4f;
            Projectile.NewProjectile(source, position.X, position.Y, vector95.X, vector95.Y, ModContent.ProjectileType<TaserLightning>(), damage, 0f, Main.myPlayer, vector94.ToRotation(), ai);

            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.EvilBar, 8);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
