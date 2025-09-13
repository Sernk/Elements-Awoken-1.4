using ElementsAwoken.Content.Projectiles.Flails;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class ManashardWhip : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 36;
            Item.height = 28;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ManashardWhipP>();
            Item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            int maxThrown = 3;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ManashardWhipP>()] >= maxThrown)
            {
                return false;
            }
            else return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Manashard>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}