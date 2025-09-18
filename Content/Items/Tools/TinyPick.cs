using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tools
{
    public class TinyPick : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 3;
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 60;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useTurn = true;
            Item.pick = 20;
            Item.useStyle = 1;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 0, 40, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
    }
}