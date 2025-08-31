using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.EASystem.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.StatIncreases
{
    public class EmptyVessel : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 10;
            Item.consumable = true;
            Item.width = 18;
            Item.height = 18;
            Item.useStyle = 4;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item4;
            Item.useAnimation = 30;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        //public override bool CanUseItem(Player player)
        //{
        //    //bool calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
        //    return player.statLifeMax == 500 && player.GetModPlayer<MyPlayer>().voidHeartsUsed < 10;
        //}
        public override void UpdateInventory(Player player)
        {
            HeartsPlayers HeartsPlayers = player.GetModPlayer<HeartsPlayers>();
            if (Item.favorited)
            {
                HeartsPlayers.CompressorVisual = false;
                HeartsPlayers.chaosHeartLife = 0;
                HeartsPlayers.emptyVesselHeartLife = 0;
                HeartsPlayers.EmptyVesselVisual = false;
                HeartsPlayers.ChaosHeartVisual = true;
                HeartsPlayers.HPTier_0 = false;
                if (HeartsPlayers.CountUsechaosHear == 10)
                {
                    HeartsPlayers.chaosHeartLife = 0;
                }
            }
            else
            {
                HeartsPlayers.HPTier_0 = false;
                HeartsPlayers.ChaosHeartVisual = false;
                if (HeartsPlayers.CountUsechaosHear == 10)
                {
                    HeartsPlayers.chaosHeartLife = 0;
                }
            }
            if (HeartsPlayers.CountUsechaosHear > 0 && HeartsPlayers.EmptyVesselVisual == false)
            {
                HeartsPlayers.ChaosHeartVisual = true;
                HeartsPlayers.HPTier_0 = false;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoidAshes>(), 12);
            recipe.AddIngredient(ModContent.ItemType<Placeable.VoidStone.VoidStone>(), 25);
            recipe.AddIngredient(ModContent.ItemType<Essence.VoidEssence>(), 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            //if (ModLoader.GetMod("CalamityMod") == null) recipe.AddRecipe();
        }
    }
}
