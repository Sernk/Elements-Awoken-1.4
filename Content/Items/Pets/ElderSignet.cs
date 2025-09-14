using ElementsAwoken.Content.Buffs.PetBuffs;
using ElementsAwoken.Content.Projectiles.Pets;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Pets
{
    public class ElderSignet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.useStyle = 1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item2;
            Item.rare = RarityType<EARarity.Rarity14>();
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.noMelee = true;
            Item.shoot = ProjectileType<AncientStellate>();
            Item.buffType = BuffType<AncientStellateBuff>();
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