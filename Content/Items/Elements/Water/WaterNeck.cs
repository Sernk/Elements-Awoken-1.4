using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Water
{
    public class WaterNeck : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
            player.accMerman = true;
            if (hideVisual)
            {
                player.hideMerman = true;
            }
            bool wet = player.wet;
            if (wet)
            {
                player.GetDamage(DamageClass.Melee) += 0.2f;
                player.GetDamage(DamageClass.Throwing) += 0.2f;
                player.GetDamage(DamageClass.Ranged) += 0.2f;
                player.GetDamage(DamageClass.Magic) += 0.2f;
                player.GetDamage(DamageClass.Summon) += 0.2f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
                player.ThrownVelocity += 0.2f;
                player.magicCuffs = true;
                player.statDefense += 10;
                player.moveSpeed += 1f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}