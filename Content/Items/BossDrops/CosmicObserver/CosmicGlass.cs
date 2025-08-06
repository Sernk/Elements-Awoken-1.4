using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.CosmicObserver
{
    public class CosmicGlass : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ModContent.RarityType<Awakened>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.GetCritChance(DamageClass.Magic) += 15;
            player.GetCritChance(DamageClass.Melee) += 15;
            player.GetCritChance(DamageClass.Ranged) += 15;
            player.GetCritChance(DamageClass.Throwing) += 15;
            modPlayer.cosmicGlass = true;
        }
    }
}
