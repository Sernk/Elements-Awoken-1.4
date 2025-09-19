using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.Chaos;
using ElementsAwoken.Content.Projectiles.Flails;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class DyingAzure : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 28;
            Item.damage = 340;
            Item.knockBack = 9;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ModContent.RarityType<EARarity.Rarity13>();
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<DyingAzureP>();
            Item.shootSpeed = 18f;
        }
        public override bool CanUseItem(Player player)
        {
            int maxThrown = 4;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DyingAzureP>()] >= maxThrown) return false;
            else return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<ChaoticFlare>(), 8);
            recipe.AddIngredient(ModContent.ItemType<KindleCrusher>(), 1);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}