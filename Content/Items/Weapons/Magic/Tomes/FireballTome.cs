using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Tomes
{
    public class FireballTome : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = 5;
            Item.damage = 18;
            Item.mana = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = false;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item42;
            Item.shoot = ProjectileType<FireballP>();
            Item.shootSpeed = 9f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Obsidian, 2);
            recipe.AddIngredient(ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}