using ElementsAwoken.Content.Items.BossDrops.TheGuardian;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.StatIncreases
{
    public class VoidCompressor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.useStyle = 4;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item4;
            Item.rare = ModContent.RarityType<EARarity.Rarity13>();
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override void UpdateInventory(Player player)
        {
            HeartsPlayers HeartsPlayers = player.GetModPlayer<HeartsPlayers>();
            if (Item.favorited)
            {
                HeartsPlayers.CompressorVisual = true;
                HeartsPlayers.chaosHeartLife = 0;
                HeartsPlayers.emptyVesselHeartLife = 0;
                HeartsPlayers.EmptyVesselVisual = false;
                HeartsPlayers.ChaosHeartVisual = false;
                HeartsPlayers.HPTier_0 = false;
            }
            else
            {
                HeartsPlayers.CompressorVisual = false;
                HeartsPlayers.HPTier_0 = true;
            }
        }
        public override bool? UseItem(Player player)
        {
            MyPlayer mPlayer = player.GetModPlayer<MyPlayer>();
            if (!mPlayer.voidCompressor)
            {
                mPlayer.voidCompressor = true;
            }
            else
            {
                mPlayer.voidCompressor = false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Placeable.Darkstone.Darkstone>(), 25);
            recipe.AddIngredient(ModContent.ItemType<Essence.VoidEssence>(), 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            //if (ModLoader.GetMod("CalamityMod") == null) recipe.AddRecipe();
        }
    }
}
