using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class EnergyGeode : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = 5;    
            Item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 60;
            player.GetDamage(DamageClass.Magic) *= 1.1f;
            player.moveSpeed *= 1.08f;
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
            recipe.AddIngredient(ModContent.ItemType<IllusiveCharm>(), 1);
            recipe.AddIngredient(ItemID.CrystalShard, 14);
            recipe.AddIngredient(ItemID.AnkhShield, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}