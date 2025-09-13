using ElementsAwoken.Content.Projectiles.Thrown;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    public class FetidBlades : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.damage = 46;
            Item.knockBack = 3f;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;           
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.useTime = 18;
            Item.UseSound = SoundID.Item39;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.shoot = ProjectileType<FetidBladeP>();
            Item.shootSpeed = 12f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.type == Item.shoot && proj.alpha < 100 && proj.ai[1] != 0)
                    {
                        proj.Kill();
                        ProjectileUtils.Explosion(proj, new int[] { 46 }, proj.damage, "thrown",0.5f,0.75f);
                    }
                }
                return false;
            }
            else
            {
                float numberProjectiles = 5;
                float rotation = MathHelper.ToRadians(5);
                position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 5f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<PutridBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}