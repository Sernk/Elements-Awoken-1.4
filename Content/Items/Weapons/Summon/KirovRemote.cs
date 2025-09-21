using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Summon
{  
    public class KirovRemote : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 34;
            Item.knockBack = 3;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item44;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 5;
            Item.shoot = ModContent.ProjectileType<KirovAirship>();
            Item.shootSpeed = 7f;
            Item.rare = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddRecipeGroup("IronBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
