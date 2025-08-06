using ElementsAwoken.Content.Items.Artifacts.Materials;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Artifacts
{
    public class FrozenGauntlet : ModItem
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
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.frozenGauntlet = true;
            player.longInvince = true;
            player.starCloakItem = Item;
            player.GetDamage(DamageClass.Melee) *= 1.15f;
            player.GetDamage(DamageClass.Ranged) *= 1.15f;
            player.GetDamage(DamageClass.Magic) *= 1.15f;
            player.GetDamage(DamageClass.Summon) *= 1.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GlowingSlush>(), 1);
            recipe.AddIngredient(ItemID.FireGauntlet, 1);
            recipe.AddIngredient(ItemID.StarVeil, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}