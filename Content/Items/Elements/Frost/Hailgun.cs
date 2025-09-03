using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    public class Hailgun : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Gatligator);
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 58;
            Item.height = 32;
            Item.useTime = 4;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.25f;
            Item.UseSound = SoundID.Item11;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 7;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.SnowBallFriendly;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Snowball;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int num6 = Main.rand.Next(2, 3);
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = speed.X + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speed.Y + (float)Main.rand.Next(-25, 26) * 0.05f;
                switch (Main.rand.Next(2))
                {
                    case 0: type = ProjectileID.SnowBallFriendly; break;
                    default: break;
                }
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .9f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.SnowballCannon);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}