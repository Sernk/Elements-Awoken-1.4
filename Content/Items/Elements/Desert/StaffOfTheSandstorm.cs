using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Desert
{  
    public class StaffOfTheSandstorm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;           
            Item.damage = 14;
            Item.knockBack = 3;
            Item.mana = 10;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<MiniatureSandstorm>();
            Item.shootSpeed = 7f;
            Item.rare = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertEssence>(), 4);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 25);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 10);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}