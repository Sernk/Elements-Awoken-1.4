using ElementsAwoken.Content.Projectiles.GemLasers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers
{
    public abstract class GemLasersTier2Class : ModItem
    {
        public abstract int Damage { get; }
        public abstract int Marerial { get; }
        public abstract int AI { get; }
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 28;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.damage = Damage;
            Item.knockBack = 4;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<GemLaser>();
            Item.shootSpeed = 24f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override void SetStaticDefaults() { }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int ai = 0;
            if (type == ProjectileID.Bullet)
            {
                type = ModContent.ProjectileType<GemLaser>();
                SoundEngine.PlaySound(SoundID.Item12, player.position);
                ai = AI;
            }
            else SoundEngine.PlaySound(SoundID.Item11, player.position);
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, ai);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(Marerial);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}