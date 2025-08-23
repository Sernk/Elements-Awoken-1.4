using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Medicine
{
    public class NanowrapBandage : ModItem
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
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 7;
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
                player.AddBuff(ModContent.BuffType<NanowrapBuff>(), 600);
                return true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Bandage>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MagicalHerbs>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
