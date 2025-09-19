using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Projectiles.Yoyos;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Melee.SpaceYoyos
{
    public class Sol : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.useStyle = 5;
            Item.damage = 390;
            Item.knockBack = 8f;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity13>();
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<SolP>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Jupiter>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 15);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}