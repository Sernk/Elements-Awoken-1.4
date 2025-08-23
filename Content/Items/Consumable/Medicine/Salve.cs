using ElementsAwoken.Content.Buffs.PotionBuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Medicine
{
    public class Salve : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = 2;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 0, 50, 0);
            Item.rare = 0;
            return;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(ModContent.BuffType<MedicineCooldown>()) == -1)
            {
                return true;
            }
            return false;
        }
        public override bool? UseItem(Player player)
        {
            if (player.FindBuffIndex(ModContent.BuffType<MedicineCooldown>()) == -1)
            {
                player.AddBuff(ModContent.BuffType<MedicineCooldown>(), 1500);
                player.AddBuff(ModContent.BuffType<SalveBuff>(), 600);
                return true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GrassSeeds, 1);
            recipe.AddIngredient(ItemID.VineRope, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
