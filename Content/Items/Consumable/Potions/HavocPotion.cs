using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    public class HavocPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;       
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0,0,4,0);
            Item.rare = 3;
            Item.buffType = ModContent.BuffType<HavocPotionBuff>();
            Item.buffTime = 10800;
            return;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BattlePotion, 1);
            recipe.AddIngredient(ModContent.ItemType<ImpEar>(), 1);
            recipe.AddIngredient(ItemID.Hellstone, 1);
            recipe.AddIngredient(ItemID.ViciousPowder, 3);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BattlePotion, 1);
            recipe.AddIngredient(ModContent.ItemType<ImpEar>(), 1);
            recipe.AddIngredient(ItemID.Hellstone, 1);
            recipe.AddIngredient(ItemID.VilePowder, 3);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
