using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Summon
{
    public class Deathwish : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 105;
            Item.knockBack = 3;
            Item.mana = 10;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.DamageType = DamageClass.Summon;
            Item.noMelee = true;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<Deathwatcher>();
            Item.shootSpeed = 7f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deathwish");
            // Tooltip.SetDefault("Summons a Deathwatcher to annihilate your enemies");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
