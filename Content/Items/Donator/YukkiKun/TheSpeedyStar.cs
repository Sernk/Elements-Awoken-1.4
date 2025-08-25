using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.YukkiKun
{
    public class TheSpeedyStar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 22;
            Item.mana = 50;
            Item.reuseDelay = 16;
            Item.useAnimation = 25;
            Item.useTime = 5;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.autoReuse = true;
            Item.knockBack = 2.25f;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item29;
            Item.shoot = ModContent.ProjectileType<Speedstar>();
            Item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ModContent.ItemType<DemonicFleshClump>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}