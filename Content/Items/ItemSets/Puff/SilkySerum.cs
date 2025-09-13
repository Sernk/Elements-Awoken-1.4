using ElementsAwoken.Content.Buffs.PotionBuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Puff
{
    public class SilkySerum : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = 2;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 28;
            Item.value = 2500;
            Item.rare = 1;
            Item.buffType = ModContent.BuffType<SilkySerumBuff>();
            Item.buffTime = EAU.BaffsTime(minutes: 4);
            //item.potion = true;
            return;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ModContent.ItemType<Puffball>(), 2);
            recipe.AddIngredient(ItemID.Mushroom, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}