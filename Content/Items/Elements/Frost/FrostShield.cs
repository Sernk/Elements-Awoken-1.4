using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    [AutoloadEquip(EquipType.Shield)]
    public class FrostShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 7;
            Item.accessory = true;
            Item.defense = 4;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.statLifeMax2 += 30;
            player.statDefense += 5;
            player.statLifeMax2 += 30;
            if (Main.player[Main.myPlayer].ZoneSnow)
            {
                player.statDefense += 5;
                player.statLifeMax2 += 30;
                player.GetDamage(DamageClass.Melee) += 0.1f;
                player.GetDamage(DamageClass.Throwing) += 0.1f;
                player.GetDamage(DamageClass.Ranged) += 0.1f;
                player.GetDamage(DamageClass.Magic) += 0.1f;
                player.GetDamage(DamageClass.Summon) += 0.1f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.LifeCrystal, 3);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}