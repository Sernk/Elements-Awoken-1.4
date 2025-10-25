using ElementsAwoken.Content.Items.Elements.Desert;
using ElementsAwoken.Content.Items.Elements.Frost;
using ElementsAwoken.Content.Items.Elements.Sky;
using ElementsAwoken.Content.Items.Elements.Void;
using ElementsAwoken.Content.Items.Elements.Water;
using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    public class FireTreads : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 4;
            Item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && (player.armor[i].type == ItemID.HermesBoots || player.armor[i].type == ItemID.SpectreBoots || player.armor[i].type == ItemID.LightningBoots || player.armor[i].type == ItemID.FrostsparkBoots || player.armor[i].type == ItemID.SandBoots || player.armor[i].type == ItemID.TerrasparkBoots || player.armor[i].type == ItemType<DesertTrailers>() || player.armor[i].type == ItemType<FireTreads>() || player.armor[i].type == ItemType<SkylineWhirlwind>() || player.armor[i].type == ItemType<FrostWalkers>() || player.armor[i].type == ItemType<AqueousWaders>() || player.armor[i].type == ItemType<VoidBoots>()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lavaRose = true;
            player.accRunSpeed = 8.8f;
            player.rocketBoots = 1;
            player.fireWalk = true;
            player.lavaMax += 420;
            player.shimmerImmune = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<FireEssence>(), 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.TerrasparkBoots);
            recipe.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}