using ElementsAwoken.Content.Items.Artifacts.Materials;
using ElementsAwoken.Content.Items.Elements.Desert;
using ElementsAwoken.Content.Items.Elements.Fire;
using ElementsAwoken.Content.Items.Elements.Frost;
using ElementsAwoken.Content.Items.Elements.Sky;
using ElementsAwoken.Content.Items.Elements.Void;
using ElementsAwoken.Content.Items.Elements.Water;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Artifacts
{
    public class Nanocore : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 9;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().artifact = true;
        }
        public override void SetStaticDefaults()
        {         
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 6));
            Const.SetSoul(Type);
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<ElementalArcanum>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) *= 1.20f;
            player.GetDamage(DamageClass.Ranged) *= 1.20f;
            player.GetDamage(DamageClass.Magic) *= 1.20f;
            player.GetDamage(DamageClass.Summon) *= 1.20f;
            player.pStone = true;
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
            player.lifeRegen += 1;
            if (player.statLife <= player.statLifeMax2 * 0.5f)
            {
                player.endurance += 0.15f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SoulOfPlight>(), 1);
            recipe.AddIngredient(ItemID.CelestialShell, 1);
            recipe.AddIngredient(ItemID.CharmofMyths, 1);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}