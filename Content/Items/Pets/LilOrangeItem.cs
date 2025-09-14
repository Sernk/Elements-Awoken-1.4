using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Pets
{
    public class LilOrangeItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.damage = 0;
            Item.useStyle = 1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item2;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Pets.LilOrange>();
            Item.buffType = ModContent.BuffType<Buffs.PetBuffs.LilOrange>();
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }
}