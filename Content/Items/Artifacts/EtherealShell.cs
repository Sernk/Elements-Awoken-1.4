using ElementsAwoken.Content.Items.Accessories;
using ElementsAwoken.Content.Items.Artifacts.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Artifacts
{
    public class EtherealShell : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 7;
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
            player.breathEffectiveness = StatModifier.Default + 5f;
            player.magicCuffs = true;
            player.manaMagnet = true;
            player.manaFlower = true;
            player.arcticDivingGear = true;
            player.statManaMax2 += 100;
            player.GetDamage(DamageClass.Magic) *= 1.2f;
            if (player.wet)
            {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.9f, 0.2f, 0.6f);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ManaShield>(), 1);
            recipe.AddIngredient(ModContent.ItemType<OddWater>(), 1);
            recipe.AddIngredient(ModContent.ItemType<IllusiveCharm>(), 1);
            recipe.AddIngredient(ItemID.SorcererEmblem, 1);
            recipe.AddIngredient(ItemID.JellyfishDivingGear, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}