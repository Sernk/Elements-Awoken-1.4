using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    public class Firelash : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Code2);
            Item.useStyle = 5;
            Item.damage = 30;
            Item.width = 16;
            Item.height = 16;
            Item.rare = 4;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = 541;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<FirelashP>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}