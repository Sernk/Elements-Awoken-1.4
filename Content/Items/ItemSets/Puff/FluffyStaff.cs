using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Puff
{
    public class FluffyStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.mana = 10;
            Item.width = 26;
            Item.height = 28;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(0, 0, 2, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<BabyPuff>();
            Item.shootSpeed = 7f;
            Item.DamageType = DamageClass.Summon;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddIngredient(ModContent.ItemType<Puffball>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
