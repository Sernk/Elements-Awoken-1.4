using ElementsAwoken.Content.Items.Elements.Desert;
using ElementsAwoken.Content.Items.Elements.Fire;
using ElementsAwoken.Content.Items.Elements.Frost;
using ElementsAwoken.Content.Items.Elements.Sky;
using ElementsAwoken.Content.Items.Elements.Void;
using ElementsAwoken.Content.Items.Elements.Water;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class NyanBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = RarityType<EARarity.Rarity12>();
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        { 
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(flyTime: 300, 13.75f, 4f);
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
            if (hideVisual)
            {
                if (player.ownedProjectileCounts[ProjectileType<NyanBootsTrail>()] < 1 && player.GetModPlayer<MyPlayer>().nyanBoots)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, ProjectileType<NyanBootsTrail>(), 0, 0, player.whoAmI);
                }
            }

            modPlayer.eaDash = 2;
            player.accRunSpeed = 13.75f;
            player.rocketBoots = 3;
            player.spikedBoots = 2;
            player.wingTimeMax = 300;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            player.blackBelt = true;
        }
        public override void UpdateVanity(Player player)
        {
            player.GetModPlayer<MyPlayer>().nyanBoots = true;
            if (player.ownedProjectileCounts[ProjectileType<NyanBootsTrail>()] < 1 && player.GetModPlayer<MyPlayer>().nyanBoots)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, ProjectileType<NyanBootsTrail>(), 0, 0, player.whoAmI);
            }
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)           
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 13f;
            acceleration *= 4f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            base.WingUpdate(player, inUse);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ItemType<ElementalEssence>(), 5);
            recipe.AddIngredient(ItemID.RainbowDye, 1);
            recipe.AddIngredient(ItemID.RainbowBrick, 10);
            recipe.AddIngredient(ItemType<VoidBoots>());
            recipe.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}