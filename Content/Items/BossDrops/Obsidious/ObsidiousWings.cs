using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    [AutoloadEquip(EquipType.Wings)]

    public class ObsidiousWings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(150, 10f, 2);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 150;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)          
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2f;
            constantAscend = 0.135f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 10f;
            acceleration *= 3f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                for (int l = 0; l < 5; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(player.Center, 2, 2, 62)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= player.velocity / 6f * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
            base.WingUpdate(player, inUse);
            return false;
        }
    }
}