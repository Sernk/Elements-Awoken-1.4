using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Summon
{
    public class EnchantedStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62;
            Item.damage = 36;
            Item.knockBack = 2f;
            Item.mana = 10;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 5;
            Item.shoot = ModContent.ProjectileType<Projectiles.Minions.EnchantedTrio0>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int choice = Main.rand.Next(3);

            int projType = choice switch
            {
                0 => ModContent.ProjectileType<Projectiles.Minions.EnchantedTrio0>(),
                1 => ModContent.ProjectileType<Projectiles.Minions.EnchantedTrio1>(),
                _ => ModContent.ProjectileType<Projectiles.Minions.EnchantedTrio2>(),
            };

            Projectile.NewProjectile(source, position, velocity, projType, damage, knockback, player.whoAmI);

            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 16);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
