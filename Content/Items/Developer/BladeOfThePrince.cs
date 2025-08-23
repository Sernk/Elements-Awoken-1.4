using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Weapons.Melee;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Developer
{
    public class BladeOfThePrince : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 250;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 12;
            Item.useTurn = true;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.knockBack = 6.5f;
            Item.value = Item.buyPrice(1, 50, 0, 0);
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<PrinceStrike>();
            Item.shootSpeed = 18f;
            Item.GetGlobalItem<EATooltip>().developer = true;
        }
        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<RavenPrince>()] < 3)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<RavenPrince>(), Item.damage, Item.knockBack, player.whoAmI);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 25);
            recipe.AddIngredient(ModContent.ItemType<Armageddon>(), 1);
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 10);
            recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
