using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles.Thrown;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Elemental
{
    public class Cosmicus : ModItem
    {
        public int charge = 0;
        public override void SetDefaults()
        {
            Item.width = 38;  
            Item.height = 38;
            Item.damage = 210;
            Item.useAnimation = 7;
            Item.useStyle = 1;
            Item.useTime = 7;
            Item.knockBack = 7.5f;
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Throwing;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.shoot = ModContent.ProjectileType<CosmicusP>();
            Item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElementalEssence>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}