using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Developer
{
    public class PurpleTearstoneRing : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 4;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().developer = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife < player.statLifeMax2 * 0.15f)
            {
                player.GetDamage(DamageClass.Throwing) *= 1.2f;
                player.GetDamage(DamageClass.Melee) *= 1.2f;
                player.GetDamage(DamageClass.Magic) *= 1.2f;
                player.GetDamage(DamageClass.Ranged) *= 1.2f;
                player.GetDamage(DamageClass.Summon) *= 1.2f;

                player.statDefense += (int)(player.statDefense * 1.5f);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 8);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}