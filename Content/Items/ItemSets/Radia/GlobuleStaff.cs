using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Radia
{
    public class GlobuleStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 320;
            Item.mana = 10;
            Item.knockBack = 5;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.UseSound = SoundID.Item44;
            Item.shoot = ProjectileType<GlobuleMinion>();
            Item.shootSpeed = 7f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Radia>(), 16);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}