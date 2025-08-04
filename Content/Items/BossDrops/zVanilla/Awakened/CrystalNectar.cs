using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla.Awakened
{
    public class CrystalNectar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.accessory = true;
            Item.rare = ModContent.RarityType<EARarity.Awakened>();
            Item.color = Color.Yellow;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.honeyWet)
            {
                player.ignoreWater = true;
                player.runAcceleration *= 2;
                player.moveSpeed *= 1.2f;
                player.accRunSpeed *= 1.2f;
                player.jumpSpeedBoost += 2f;
                player.AddBuff(BuffID.Honey, 3600);
            }
            player.npcTypeNoAggro[NPCID.Bee] = true;
            player.npcTypeNoAggro[NPCID.BeeSmall] = true;
            player.strongBees = true;
        }
    }
}