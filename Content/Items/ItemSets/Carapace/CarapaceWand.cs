using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Carapace
{
    public class CarapaceWand : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 12;
            Item.knockBack = 5;
            Item.mana = 12;
            Item.UseSound = SoundID.Item8;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;     
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Magic;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.rare = 0;
            Item.shoot = ProjectileType<CarapaceShard>();
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<CarapaceItem>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
