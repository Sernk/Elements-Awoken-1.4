using ElementsAwoken.Content.Items.Accessories;
using ElementsAwoken.Content.Items.Artifacts.Materials;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Artifacts
{
    public class GreatThunderTotem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 5;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().artifact = true;
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<ElementalArcanum>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.lightningCloud = true;
            if (hideVisual)
            {
                modPlayer.lightningCloudHidden = true;
            }
            player.GetArmorPenetration(DamageClass.Generic) += 5;
            player.moveSpeed += 0.50f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<StrangeTotem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<LightningCloud>(), 1);
            recipe.AddIngredient(ItemID.SharkToothNecklace, 1);
            recipe.AddIngredient(ItemID.AnkletoftheWind, 1);
            recipe.AddIngredient(ItemID.Aglet, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}