using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    public class FireNeck : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 4;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.fireAccCD--;
            modPlayer.fireAcc = true;
            player.magmaStone = true;
            player.GetDamage(DamageClass.Generic) *= 1.02f;
            if (player.velocity.Y != 0)
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 6)];
                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 0.2f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}