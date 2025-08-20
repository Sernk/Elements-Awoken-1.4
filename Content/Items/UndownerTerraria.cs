using ElementsAwoken.EASystem.UI.Tooltips;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ElementsAwoken.Utilities;

namespace ElementsAwoken.Content.Items
{
    public class UndownerTerraria : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = ModContent.RarityType<EARarity.BETATEST>();
            Item.GetGlobalItem<EARaritySettings>().betatest = true;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
        }
        public override bool CanUseItem(Player player)
        {
            if (MyWorld.downedPermafrost)
            {
                Main.NewText($"{ModContent.GetInstance<EALocalization>().UndownerTerraria} {Convert.ToString(MyWorld.downedPermafrost)}", Color.Green);
                return MyWorld.downedPermafrost = false;
            }
            else
            {
                Main.NewText($"{ModContent.GetInstance<EALocalization>().UndownerTerraria} {Convert.ToString(MyWorld.downedPermafrost)}", Color.Red);
            }
            return true;
        }
    }
}