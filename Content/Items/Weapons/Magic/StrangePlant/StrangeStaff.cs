using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.StrangePlant
{
    public class StrangeStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.mana = 5;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<StrangePlantBall6>();
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<StrangeWand1>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StrangeWand2>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StrangeWand3>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StrangeWand4>(), 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddRecipeGroup(EARecipeGroups.EvilBar, 10);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}