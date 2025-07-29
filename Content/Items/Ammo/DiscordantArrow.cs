using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.Ammo
{
    public class DiscordantArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.damage = 28;
            Item.knockBack = 1.5f;
            Item.consumable = true;
            Item.DamageType = DamageClass.Ranged;
            Item.maxStack = 999;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.shoot = ModContent.ProjectileType<Projectiles.Arrows.DiscordantArrow>();
            Item.shootSpeed = 34f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.WoodenArrow, 50);
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 1);
            recipe.AddTile(ModContent.TileType<ChaoticCrucible>());
            recipe.Register();
        }
    }
}