using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Wasteland
{
    [AutoloadEquip(EquipType.Head)]
    public class WastelandMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.rare = 1;
            Item.vanity = true;
        }
    }
}