﻿using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Materials
{
    public class MagmaCrystal : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 3;
        }
    }
}