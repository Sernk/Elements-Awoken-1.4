using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Wings)]

    public class BubblePack : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(162, 12f, 3f);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 162;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)          
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 12f;
            acceleration *= 3f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int projectile1 = Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileID.FlaironBubble, 20, 5f, player.whoAmI, 0f, 0f);
                    Main.projectile[projectile1].timeLeft = 40;
                }
            }
            base.WingUpdate(player, inUse);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}