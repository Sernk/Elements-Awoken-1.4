using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    [AutoloadEquip(EquipType.Shield)]

    public class VoidShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            Item.accessory = true;
            Item.defense = 4;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            if ((player.statDefense >= 40) && (player.statDefense <= 79))  
            {
                Buff(0.5f, 100, player);
            }
            if ((player.statDefense >= 80) && (player.statDefense <= 119)) 
            {                                                                              
                Buff(0.1f, 150, player);
            }
            if (player.statDefense >= 120)
            { 
                Buff(0.15f, 200, player);
            }
        }
        public static void Buff(float BonusDamage, int HP, Player player)
        {
            player.statLifeMax2 += HP;
            player.GetDamage(DamageClass.Generic) += BonusDamage;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.LifeCrystal, 3);
            recipe.AddIngredient(ItemID.LifeFruit, 3);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}