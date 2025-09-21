using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class PrismaticFlamebuster : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 89;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 16;
            Item.useTime = 3;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.25f;
            Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<PrismaticFire>();
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Gel;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int num6 = Main.rand.Next(2 , 3);
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = speed.X + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speed.Y + (float)Main.rand.Next(-25, 26) * 0.05f;
                switch (Main.rand.Next(3))
                {
                    case 0: type = ModContent.ProjectileType<PrismaticFire>(); break;
                    default: break;
                }
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .8f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 25);
            recipe.AddIngredient(ItemID.Flamethrower);
            recipe.AddIngredient(ItemID.RainbowBrick, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
