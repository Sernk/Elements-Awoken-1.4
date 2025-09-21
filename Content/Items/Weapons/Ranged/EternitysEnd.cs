using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class EternitysEnd : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 64;
            Item.damage = 70;
            Item.knockBack = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item5;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<EternityArrow>();
            Item.shootSpeed = 26f;
            Item.useAmmo = 40;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .60f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int addPosition = Main.rand.Next(-20, 8);
            if (type == 1) type = ModContent.ProjectileType<EternityArrow>();
            Projectile.NewProjectile(source, position.X + addPosition, position.Y + addPosition, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            if (Main.rand.Next(10) == 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<EternityBeam>(), (int)(damage * 1.5f), knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Phantasm, 1);
            recipe.AddIngredient(ItemID.CrystalShard, 12);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
