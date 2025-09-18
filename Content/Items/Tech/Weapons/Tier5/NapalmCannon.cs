using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier5
{
    public class NapalmCannon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 32;
            Item.knockBack = 1f;
            Item.GetGlobalItem<ItemEnergy>().energy = 15;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 2, 50, 0);
            Item.rare = 6;
            Item.shootSpeed = 12f;
            Item.shoot = ProjectileType<NapalmCannonP>();
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ProjectileType<NapalmCannonP>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-7, 0);
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.AdamantiteBar, 12);
            recipe.AddIngredient(ItemType<Capacitor>(), 1);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ItemType<Transistor>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
        }
    }
}