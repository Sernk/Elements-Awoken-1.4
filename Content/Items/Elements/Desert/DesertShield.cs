using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    [AutoloadEquip(EquipType.Shield)]

    public class DesertShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 36;
            Item.width = 24;
            Item.height = 28;
            Item.rare = 3;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.accessory = true;
            Item.defense = 4;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.dashType = 2;

            if (player.eocDash == 14 && player.ownedProjectileCounts[ModContent.ProjectileType<Shieldnado>()] <= 0)
            {
                int speed = 7;
                if (player.direction == -1)
                {
                    speed = -7;
                }
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, speed, Main.rand.NextFloat(-1.5f, 1.5f), ModContent.ProjectileType<Shieldnado>(), 10, 5f, player.whoAmI);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertEssence>(), 4);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 25);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 10);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}