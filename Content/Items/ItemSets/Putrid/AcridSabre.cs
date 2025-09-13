using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    public class AcridSabre : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;          
            Item.damage = 68;
            Item.useTime = 21;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileType<PutridSkull>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float rotation = MathHelper.TwoPi;
            float numProj = Main.rand.Next(3, 7);
            float speed = 4.5f;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = (rotation / numProj * i).ToRotationVector2() * speed;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return true;
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