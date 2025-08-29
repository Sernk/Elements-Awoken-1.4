using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Minions.HappyCloud;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{  
    public class HappyLittleCloud : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 30;
            Item.mana = 10;
            Item.knockBack = 3;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item44;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<HappyCloud>();
            Item.shootSpeed = 7f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}