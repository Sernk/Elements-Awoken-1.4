using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.ScourgeFighter
{
    public class ScourgeDrive : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.expert = true;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 4));
            EAU.SetSoul(Type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, EAU.PinkFlame, 0, 0, 0, default(Color))];
                dust.noGravity = true;
            }
            player.accRunSpeed *= 1.5f;
            player.GetModPlayer<MyPlayer>().scourgeDrive = true;
        }
    }
}