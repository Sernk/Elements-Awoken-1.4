using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EASystem.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.StatIncreases
{
    public class ChaosHeart : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 10;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.useStyle = 4;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item4;
            Item.rare = ModContent.RarityType<EARarity.Rarity13>();
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaosporidic Heart");
            // Tooltip.SetDefault("Fighters of The Calamity need not apply\nPermanently increases maximum life by 10");
        }
        //public override bool CanUseItem(Player player)
        //{
        //    bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
        //    return !calamityEnabled && player.GetModPlayer<MyPlayer>().voidHeartsUsed == 10 && player.GetModPlayer<MyPlayer>().chaosHeartsUsed < 10;
        //}

        //public override bool UseItem(Player player)
        //{
        //    player.statLifeMax2 += 10;
        //    player.statLife += 10;
        //    if (Main.myPlayer == player.whoAmI)
        //    {
        //        player.HealEffect(10, true);
        //    }
        //    player.GetModPlayer<MyPlayer>().chaosHeartsUsed += 1;
        //    return true;
        //}
        public override void UpdateInventory(Player player)
        {
            HeartsPlayers HeartsPlayers = player.GetModPlayer<HeartsPlayers>();
            if (Item.favorited)
            {
                HeartsPlayers.EmptyVesselVisual = true;
                if (HeartsPlayers.CountUseEmptyVessel == 10)
                {
                    HeartsPlayers.emptyVesselHeartLife = 0;
                }
                HeartsPlayers.ChaosHeartVisual = false;
                HeartsPlayers.CompressorVisual = true;
                HeartsPlayers.chaosHeartLife = 0;
                HeartsPlayers.emptyVesselHeartLife = 0;
                HeartsPlayers.CompressorVisual = false;
                HeartsPlayers.ChaosHeartVisual = false;
            }
            else
            {
                if (HeartsPlayers.CountUseEmptyVessel == 10)
                {
                    HeartsPlayers.emptyVesselHeartLife = 0;
                }
                HeartsPlayers.EmptyVesselVisual = false;
            }
            if (HeartsPlayers.CountUseEmptyVessel > 1 && HeartsPlayers.CountUsechaosHear == 10)
            {
                HeartsPlayers.EmptyVesselVisual = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoidAshes>(), 8);
            recipe.AddIngredient(ModContent.ItemType<Essence.VoidEssence>(), 15);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ChaoticCrucible>());
            recipe.Register();
            //if (ModLoader.GetMod("CalamityMod") == null) recipe.AddRecipe();
        }
    }
}
