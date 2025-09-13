using ElementsAwoken.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Tomes
{
    public class FrostMine : ModItem
    {
        readonly int FrostMineP = ModContent.ProjectileType<Projectiles.FrostMine>();
        public override void SetDefaults()
        {
            Item.height = 30;
            Item.width = 28;
            Item.damage = 58;
            Item.knockBack = 3.5f;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.useTime = 8;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item20;
            Item.shoot = FrostMineP;
            Item.shootSpeed = 1f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(source, player.Center.X + Main.rand.Next(-360, 360), player.Center.X + Main.rand.Next(-360, 360), 0, 0, FrostMineP, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FrostCore, 2);
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}