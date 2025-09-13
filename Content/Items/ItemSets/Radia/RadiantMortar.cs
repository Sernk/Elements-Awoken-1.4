using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Radia
{
    public class RadiantMortar : ModItem
    {
        public int shootTimer = 120;
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            shootTimer--;
            if (player.whoAmI == Main.myPlayer && !hideVisual && shootTimer <= 0)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Center, new Vector2(Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-14, -10)), ProjectileType<RadiantStarMortar>(), 750, 0f, player.whoAmI);
                shootTimer = Main.rand.Next(10, 120);
            }
            player.accRunSpeed *= 1.02f;
            player.moveSpeed *= 1.02f;
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