using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    [AutoloadEquip(EquipType.Neck)]
    public class FrostNeck : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 7;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pendant of Frost");
            // Tooltip.SetDefault("Harness the power of frost\nImmunity to chilled and frozen\nUpon entering snow:\n5% increased damage\n5% increased melee speed and throwing velocity");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Chilled] = true;
            if (Main.player[Main.myPlayer].ZoneSnow)
            {
                player.GetDamage(DamageClass.Melee) += 0.05f;
                player.GetDamage(DamageClass.Throwing) += 0.05f;
                player.GetDamage(DamageClass.Ranged) += 0.05f;
                player.GetDamage(DamageClass.Magic) += 0.05f;
                player.GetDamage(DamageClass.Summon) += 0.05f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
                player.ThrownVelocity += 0.1f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}
