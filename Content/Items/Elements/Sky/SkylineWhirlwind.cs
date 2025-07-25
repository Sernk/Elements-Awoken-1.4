using ElementsAwoken.EASystem;
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
            // DisplayName.SetDefault("Skyline Whirlwind");
            // Tooltip.SetDefault("Crazy speed!\nGrants immunity to fire blocks\nTemporary immunity to lava\nAllows flight and slow fall");
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(flyTime: 160, 9.8f, 1f);
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i &&
                        (player.armor[i].type == ItemID.HermesBoots ||
                        player.armor[i].type == ItemID.SpectreBoots ||
                        player.armor[i].type == ItemID.LightningBoots ||
                        player.armor[i].type == ItemID.FrostsparkBoots))
                        //player.armor[i].type == ItemType<DesertTrailers>() ||
                        //player.armor[i].type == ItemType<FireTreads>() ||
                        //player.armor[i].type == ItemType<FrostWalkers>() ||
                        //player.armor[i].type == ItemType<AqueousWaders>() ||
                        //player.armor[i].type == ItemType<VoidBoots>() ||
                        //player.armor[i].type == ItemType<NyanBoots>()))
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
        //public override void AddRecipes()
        //{
        //    ModRecipe recipe = new ModRecipe(mod);
        //    recipe.AddRecipeGroup("ElementsAwoken:WingGroup");
        //    recipe.AddIngredient(ItemID.LuckyHorseshoe, 1);
        //    recipe.AddIngredient(null, "FireTreads", 1);
        //    recipe.AddIngredient(null, "SkyEssence", 6);
        //    recipe.AddIngredient(ItemID.Cloud, 25);
        //    recipe.AddIngredient(ItemID.HallowedBar, 5);
        //    recipe.AddTile(null, "ElementalForge");
        //    recipe.SetResult(this);
        //    recipe.AddRecipe();
        //    recipe = new ModRecipe(mod);
        //    recipe.AddRecipeGroup("ElementsAwoken:WingGroup");
        //    recipe.AddIngredient(ItemID.ObsidianHorseshoe, 1);
        //    recipe.AddIngredient(null, "FireTreads", 1);
        //    recipe.AddIngredient(null, "SkyEssence", 6);
        //    recipe.AddIngredient(ItemID.Cloud, 25);
        //    recipe.AddIngredient(ItemID.HallowedBar, 5);
        //    recipe.AddTile(null, "ElementalForge");
        //    recipe.SetResult(this);
        //    recipe.AddRecipe();
        //}
    }
}
