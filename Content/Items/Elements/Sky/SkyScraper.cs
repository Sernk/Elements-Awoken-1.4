using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class SkyScraper : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 60;
            Item.width = 60;       
            Item.damage = 38;
            Item.knockBack = 4.75f;
            Item.crit = 12;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<SkyScraperP>();
            Item.shootSpeed = 12f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
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