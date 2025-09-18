using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier5
{
    public class PrototypeX51 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 34;
            Item.knockBack = 2;
            Item.GetGlobalItem<ItemEnergy>().energy = 15;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 42;
            Item.useAnimation = 42;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item15;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 6;
            Item.mana = 9;
            Item.shoot = ProjectileID.ElectrosphereMissile;
            Item.shootSpeed = 8f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            damage = 19;
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MythrilBar, 8);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Transistor>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.OrichalcumBar, 8);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Transistor>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
