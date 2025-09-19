using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.StrangePlant
{
    public class StrangeWand3 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.mana = 5;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<StrangePlantBall3>();
            Item.shootSpeed = 6f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StrangePlant3, 1);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}