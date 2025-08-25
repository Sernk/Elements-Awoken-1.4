using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Elemental
{
    public class Andromeda : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.damage = 190;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<AndromedaBlast>();
            Item.shootSpeed = 10f;
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
