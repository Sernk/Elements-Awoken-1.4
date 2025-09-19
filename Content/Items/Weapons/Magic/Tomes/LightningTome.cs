using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Tomes
{
    public class LightningTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32; 
            Item.damage = 22;
            Item.knockBack = 2;
            Item.mana = 12;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = false;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ProjectileType<Lightning>();
            Item.shootSpeed = 14f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Firefly, 8);
            recipe.AddIngredient(ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}