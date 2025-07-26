using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable.Drives
{
    public class GuardianDrive : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 0;

            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<Tiles.Lab.Drives.GuardianDrive>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Guardian Drive");
        }
        public override void UpdateInventory(Player player)
        {
            MyWorld.guardianDrive = true;
        }
    }
}
