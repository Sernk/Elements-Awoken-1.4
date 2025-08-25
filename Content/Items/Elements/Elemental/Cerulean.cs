using ElementsAwoken.Content.Items.Accessories;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Elemental
{
    public class Cerulean : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Magic) += 5;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetCritChance(DamageClass.Throwing) += 5;
            if (player.statLife <= (player.statLifeMax2 * 0.5f))
            {
                player.manaCost *= 0.5f;
                player.endurance += 0.2f;
                player.GetDamage(DamageClass.Throwing) *= 1.1f;
                player.GetDamage(DamageClass.Ranged) *= 1.1f;
                player.GetDamage(DamageClass.Magic) *= 1.1f;
                player.GetDamage(DamageClass.Summon) *= 1.1f;
            }
            if (player.statLife <= (player.statLifeMax2 * 0.25f))
            {
                player.endurance += 0.2f;
            }
            player.statManaMax2 += 100;
            player.GetDamage(DamageClass.Magic) *= 1.15f;
            player.moveSpeed *= 1.16f;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[46] = true;
            player.buffImmune[44] = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElementalEssence>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<Unity>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}