using ElementsAwoken.Content.Items.Elements.Desert;
using ElementsAwoken.Content.Items.Elements.Fire;
using ElementsAwoken.Content.Items.Elements.Frost;
using ElementsAwoken.Content.Items.Elements.Sky;
using ElementsAwoken.Content.Items.Elements.Water;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    [AutoloadEquip(EquipType.Wings)]
    public class VoidBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = 11;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {           
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(flyTime: 220, 12.75f, 3f);
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
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.eaDash = 1;
            player.accRunSpeed = 12.75f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.wingTimeMax = 220;
            modPlayer.voidBoots = !hideVisual;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)        
        {
            ascentWhenFalling = 0.65f;
            ascentWhenRising = 0.10f;
            maxAscentMultiplier = 2f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 12f;
            acceleration *= 3f;
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (inUse && modPlayer.voidBoots)
            {
                int dust = Dust.NewDust(player.position, player.width, player.height, 127, 0, 0, 0, default(Color));
                Main.dust[dust].scale *= 1.5f;
            }
            base.WingUpdate(player, inUse);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<VoidEssence>(), 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemType<AqueousWaders>());
            recipe.AddRecipeGroup(EARecipeGroups.LunarWings);
            recipe.AddIngredient(ItemID.MasterNinjaGear, 1);
            recipe.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}