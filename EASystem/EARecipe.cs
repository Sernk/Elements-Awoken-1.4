using ElementsAwoken.Content.Items.Accessories.Emblems;
using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EARecipe : ModSystem
    {
        public override void PostAddRecipes() //TODO: ВРОДЕ КАК НЕ ДАЕТ
        {
            foreach (Recipe recipe in Main.recipe)
            {
                recipe.AddOnCraftCallback(EAOnCraft.SanityUPOnCraft);
            }
        }
        public override void AddRecipes()
        {
            Recipe fallenstar = Recipe.Create(ItemID.FallenStar);
            fallenstar.AddIngredient(ModContent.ItemType<Stardust>());
            fallenstar.Register();

            Recipe avengeremblem = Recipe.Create(ItemID.AvengerEmblem);
            avengeremblem.AddIngredient(ModContent.ItemType<ThrowerEmblem>());
            avengeremblem.AddIngredient(ItemID.SoulofMight, 5);
            avengeremblem.AddIngredient(ItemID.SoulofSight, 5);
            avengeremblem.AddIngredient(ItemID.SoulofFright, 5);
            avengeremblem.AddTile(TileID.TinkerersWorkbench);
            avengeremblem.Register();

            Recipe lihzahrdbrick = Recipe.Create(ItemID.LihzahrdBrick, 100);
            lihzahrdbrick.AddIngredient(ModContent.ItemType<SunFragment>(), 5);
            lihzahrdbrick.AddIngredient(ItemID.StoneBlock, 100);
            lihzahrdbrick.AddTile(TileID.WorkBenches);
            lihzahrdbrick.Register();

            Recipe magicmirror = Recipe.Create(ItemID.MagicMirror);
            magicmirror.AddIngredient(ModContent.ItemType<Stardust>(), 13);
            magicmirror.AddIngredient(ItemID.Glass, 12);
            magicmirror.AddIngredient(ItemID.RecallPotion, 6);
            magicmirror.AddTile(TileID.Anvils);
            magicmirror.Register();

            Recipe rodofdiscord = Recipe.Create(ItemID.RodofDiscord);
            rodofdiscord.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            rodofdiscord.AddIngredient(ModContent.ItemType<Pyroplasm>(), 30);
            rodofdiscord.AddIngredient(ItemID.LunarBar, 18);
            rodofdiscord.AddIngredient(ItemID.CrystalShard, 10);
            rodofdiscord.AddIngredient(ItemID.PixieDust, 6);
            rodofdiscord.AddIngredient(ItemID.UnicornHorn, 2);
            rodofdiscord.AddTile(TileID.LunarCraftingStation);
            rodofdiscord.Register();

            Recipe watercandle = Recipe.Create(ItemID.WaterCandle);
            watercandle.AddIngredient(ModContent.ItemType<DeathwishFlame>(), 10);
            watercandle.AddIngredient(ItemID.Torch, 1);
            watercandle.AddTile(TileID.Anvils);
            watercandle.Register();

            Recipe throwingknife = Recipe.Create(ItemID.ThrowingKnife, 25);
            throwingknife.AddRecipeGroup("Wood", 2);
            throwingknife.AddIngredient(ItemID.StoneBlock, 5);
            throwingknife.AddTile(TileID.WorkBenches);
            throwingknife.Register();

            // accessories
            Recipe waterwalkingboots = Recipe.Create(ItemID.WaterWalkingBoots);
            waterwalkingboots.AddRecipeGroup(EARecipeGroups.IronBar, 12);
            waterwalkingboots.AddIngredient(ItemID.Bone, 16);
            waterwalkingboots.AddTile(TileID.Anvils);
            waterwalkingboots.Register();

            Recipe luckyhorseshoe = Recipe.Create(ItemID.LuckyHorseshoe);
            luckyhorseshoe.AddRecipeGroup(EARecipeGroups.GoldBar, 12);
            luckyhorseshoe.AddIngredient(ItemID.SoulofFlight, 3);
            luckyhorseshoe.AddTile(TileID.MythrilAnvil);
            luckyhorseshoe.Register();

            Recipe cobaltshield = Recipe.Create(ItemID.CobaltShield);
            cobaltshield.AddRecipeGroup(EARecipeGroups.CobaltBar, 12);
            cobaltshield.AddIngredient(ItemID.Bone, 30);
            cobaltshield.AddTile(TileID.MythrilAnvil);
            cobaltshield.Register();

            Recipe bandofregeneration = Recipe.Create(ItemID.BandofRegeneration);
            bandofregeneration.AddIngredient(ItemID.LifeCrystal, 1);
            bandofregeneration.AddRecipeGroup(EARecipeGroups.IronBar, 12);
            bandofregeneration.AddIngredient(ItemID.Ruby, 1);
            bandofregeneration.AddTile(TileID.Anvils);
            bandofregeneration.Register();

            Recipe bandofstarpower = Recipe.Create(ItemID.BandofStarpower);
            bandofstarpower.AddIngredient(ItemID.ManaCrystal, 1);
            bandofstarpower.AddRecipeGroup(EARecipeGroups.IronBar, 12);
            bandofstarpower.AddIngredient(ItemID.Sapphire, 1);
            bandofstarpower.AddTile(TileID.Anvils);
            bandofstarpower.Register();

            Recipe lavacharm = Recipe.Create(ItemID.LavaCharm);
            lavacharm.AddIngredient(ItemID.HellstoneBar, 12);
            lavacharm.AddIngredient(ItemID.SoulofLight, 4);
            lavacharm.AddIngredient(ModContent.ItemType<MagmaCrystal>(), 5);
            lavacharm.AddTile(TileID.Anvils);
            lavacharm.Register();

            Recipe aglet = Recipe.Create(ItemID.Aglet);
            aglet.AddRecipeGroup(EARecipeGroups.IronBar, 8);
            aglet.AddIngredient(ItemID.SwiftnessPotion, 1);
            aglet.AddIngredient(ModContent.ItemType<LensFragment>(), 8);
            aglet.AddTile(TileID.Anvils);
            aglet.Register();

            Recipe naturesgift = Recipe.Create(ItemID.NaturesGift);
            naturesgift.AddIngredient(ItemID.JungleRose);
            naturesgift.AddIngredient(ItemID.ManaCrystal, 5);
            naturesgift.AddTile(TileID.WorkBenches);
            naturesgift.Register();

            Recipe ankletofthewind = Recipe.Create(ItemID.AnkletoftheWind);
            ankletofthewind.AddIngredient(ItemID.Vine, 3);
            ankletofthewind.AddIngredient(ItemID.SwiftnessPotion, 3);
            ankletofthewind.AddIngredient(ItemID.JungleSpores, 15);
            ankletofthewind.AddIngredient(ItemID.Stinger, 5);
            ankletofthewind.AddTile(TileID.Anvils);
            ankletofthewind.Register();

            Recipe hermesboots = Recipe.Create(ItemID.HermesBoots);
            hermesboots.AddIngredient(ItemID.Bone, 30);
            hermesboots.AddIngredient(ItemID.Feather, 9);
            hermesboots.AddIngredient(ModContent.ItemType<Stardust>(), 12);
            hermesboots.AddTile(TileID.Anvils);
            hermesboots.Register();

            Recipe celestialmagnet = Recipe.Create(ItemID.CelestialMagnet);
            celestialmagnet.AddIngredient(ItemID.ManaCrystal, 5);
            celestialmagnet.AddIngredient(ItemID.HellstoneBar, 8);
            celestialmagnet.AddIngredient(ItemID.NaturesGift, 1);
            celestialmagnet.AddTile(TileID.Anvils);
            celestialmagnet.Register();

            Recipe recipe = Recipe.Create(ItemID.IceSkates);
            recipe.AddIngredient(ItemID.Silk, 18);
            recipe.AddRecipeGroup(EARecipeGroups.IronBar, 8);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}