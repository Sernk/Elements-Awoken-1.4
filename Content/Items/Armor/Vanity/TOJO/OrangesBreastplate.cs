using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Armor.Vanity.TOJO
{
    [AutoloadEquip(EquipType.Body)]
    public class OrangesBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 36;
            Item.rare = 9;
            Item.vanity = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ThatOneJuicyOrange's Breastplate");
        }
    }
}
