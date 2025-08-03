using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    [AutoloadEquip(EquipType.Body)]
    public class ObsidiousRobes : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 36;
            Item.rare = 1;
            Item.vanity = true;
        }
    }
}