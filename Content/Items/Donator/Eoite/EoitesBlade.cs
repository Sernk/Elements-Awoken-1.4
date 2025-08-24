using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Eoite
{
    public class EoitesBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.damage = 190;
            Item.knockBack = 6;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;      
            Item.useTime = 13;   
            Item.useAnimation = 13;     
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;  
            Item.shoot = ModContent.ProjectileType<EoiteWave>();
            Item.shootSpeed = 6f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(2);
            position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                int num1 = Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 6);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemID.Amethyst, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}