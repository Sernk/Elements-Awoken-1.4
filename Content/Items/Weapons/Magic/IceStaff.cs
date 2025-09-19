using ElementsAwoken.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic
{
    public class IceStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.mana = 5;
            Item.UseSound = SoundID.Item30;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.IceBolt;
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IceBlock, 10);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}