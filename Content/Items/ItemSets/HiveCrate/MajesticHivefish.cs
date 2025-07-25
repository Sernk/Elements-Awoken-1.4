using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.HiveCrate
{
    public class MajesticHivefish : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 30;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.potion = true;
            Item.useTurn = true;
            Item.healMana = 120;
            Item.value = 10000;
            Item.rare = 1;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Majestic Hivefish");
        }
    }
}