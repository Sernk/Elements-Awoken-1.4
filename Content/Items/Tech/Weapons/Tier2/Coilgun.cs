using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier2
{
    public class Coilgun : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 65;
            Item.knockBack = 3.5f;
            Item.GetGlobalItem<ItemEnergy>().energy = 8;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item40;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 8, 0, 0);
            Item.rare = 3;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<CoilRound>();
            Item.useAmmo = 97;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<CoilRound>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.EvilBar, 8);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 2);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}