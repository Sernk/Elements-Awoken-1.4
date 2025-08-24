using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Elemental
{
    [AutoloadEquip(EquipType.Shield)]

    public class Asterox : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.statDefense += 12;
            player.GetArmorPenetration(DamageClass.Generic) += 5;
            player.lifeRegen += 3;
            player.moveSpeed *= 1.2f;
            player.panic = true; 
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<AsteroxShieldBase>()] < 1)
                {
                    Projectile.NewProjectile(EAU.Play(player), player.position.X, player.position.Y, 0f, 0f, ModContent.ProjectileType<AsteroxShieldBase>(), 50, 10f, player.whoAmI, 0.0f, 0.0f);
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElementalEssence>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}