using ElementsAwoken.Content.Items.Elements.Desert;
using ElementsAwoken.Content.Items.Elements.Fire;
using ElementsAwoken.Content.Items.Elements.Frost;
using ElementsAwoken.Content.Items.Elements.Void;
using ElementsAwoken.Content.Items.Elements.Water;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Wings)]
    public class SkylineWhirlwind : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(flyTime: 160, 9.8f, 1f);
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
            if (player.controlJump) player.GetModPlayer<MyPlayer>().skylineFlying = true;
            player.accRunSpeed = 9.8f;
            player.lavaMax += 420;
            player.wingTimeMax = 160;
            player.noFallDmg = true;
            player.fireWalk = true;
        }
        public override void UpdateVanity(Player player)
        {
            if (player.controlJump) player.GetModPlayer<MyPlayer>().skylineFlying = true;
            base.UpdateVanity(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.WingGroup);
            recipe.AddIngredient(ItemID.LuckyHorseshoe, 1);
            recipe.AddIngredient(ItemType<FireTreads>(), 1);
            recipe.AddIngredient(ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup(EARecipeGroups.WingGroup);
            recipe1.AddIngredient(ItemID.ObsidianHorseshoe, 1);
            recipe1.AddIngredient(ItemType<FireTreads>(), 1);
            recipe1.AddIngredient(ItemType<SkyEssence>(), 6);
            recipe1.AddIngredient(ItemID.Cloud, 25);
            recipe1.AddIngredient(ItemID.HallowedBar, 5);
            recipe1.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe1.Register();
        }
    }
}
