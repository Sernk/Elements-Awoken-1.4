using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Water
{
    public class WaterSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 72;
            Item.damage = 70;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.useTime = 5;
            Item.knockBack = 6.5f;
            Item.autoReuse = false;
            Item.height = 78;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8;
            Item.shoot = ModContent.ProjectileType<WaterSwordP>();
            Item.shootSpeed = 10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}