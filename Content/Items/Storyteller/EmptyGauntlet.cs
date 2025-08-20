using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Storyteller
{
    public class EmptyGauntlet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 8;
        }
    }
}