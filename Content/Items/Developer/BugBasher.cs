using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Developer
{
    public class BugBasher : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 210;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.useTime = 18;
            Item.knockBack = 10f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Throwing;
            Item.height = 68;
            Item.width = 56;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.shoot = ModContent.ProjectileType<BugBasherP>();
            Item.shootSpeed = 10f;
            Item.GetGlobalItem<EATooltip>().developer = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.ChlorophyteWarhammer, 1);
            recipe.AddIngredient(ItemID.Buggy, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
