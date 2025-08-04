using ElementsAwoken.Content.Items.Artifacts.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Artifacts
{
    public class DiscordantSkull : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 4;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().artifact = true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 4));
            EAU.SetSoul(Type);
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<ElementalArcanum>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pickSpeed -= 0.3f;
            player.GetDamage(DamageClass.Ranged) *= 1.04f;
            player.GetDamage(DamageClass.Magic) *= 1.04f;
            player.GetDamage(DamageClass.Summon) *= 1.04f;
            player.endurance += 0.05f;
            player.noKnockback = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DiscordantAmber>(), 1);
            recipe.AddIngredient(ItemID.SiltBlock, 100);
            recipe.AddIngredient(ItemID.CobaltShield, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}