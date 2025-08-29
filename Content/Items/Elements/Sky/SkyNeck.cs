using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    public class SkyNeck : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 6;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax += 50;
            player.moveSpeed += 3f;
            if (Main.player[Main.myPlayer].ZoneSkyHeight)
            {
                player.GetDamage(DamageClass.Melee) += 0.05f;
                player.GetDamage(DamageClass.Throwing) += 0.05f;
                player.GetDamage(DamageClass.Ranged) += 0.05f;
                player.GetDamage(DamageClass.Magic) += 0.05f;
                player.GetDamage(DamageClass.Summon) += 0.05f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
                player.ThrownVelocity += 0.2f;
                player.statDefense += 8;
                player.moveSpeed += 0.5f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}