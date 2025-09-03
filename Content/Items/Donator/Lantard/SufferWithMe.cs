using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.Accessories;
using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Lantard
{
    public class SufferWithMe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = ModContent.RarityType<EARarity.Rarity13>();
            Item.value = Item.sellPrice(0, 45, 0, 0);
            Item.accessory = true;
            Item.defense = 5;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.sufferWithMe = true;
            player.buffImmune[ModContent.BuffType<ChaosBurn>()] = true;
            player.GetArmorPenetration(DamageClass.Generic) += 10;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SharktoothShackle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EntropicCoating>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ElementalEssence>(), 5);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ChaoticCrucible>());
            recipe.Register();
        }
    }
}
