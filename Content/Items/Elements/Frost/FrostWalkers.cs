using ElementsAwoken.Content.Items.Elements.Desert;
using ElementsAwoken.Content.Items.Elements.Fire;
using ElementsAwoken.Content.Items.Elements.Sky;
using ElementsAwoken.Content.Items.Elements.Void;
using ElementsAwoken.Content.Items.Elements.Water;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Frost
{
    [AutoloadEquip(EquipType.Wings)]
    public class FrostWalkers : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.rare = 7;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(flyTime: 180, 10.75f, 1f);
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
            player.accRunSpeed = 10.75f;
            player.iceSkate = true;
            player.fireWalk = true;
            player.noFallDmg = true;
            player.lavaMax += 420;
            player.wingTimeMax = 180;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<FrostEssence>(), 7);
            recipe.AddRecipeGroup(EARecipeGroups.IceGroup, 5);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ItemID.IceSkates);
            recipe.AddIngredient(ItemType<SkylineWhirlwind>(), 1);
            recipe.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}