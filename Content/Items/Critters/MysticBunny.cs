using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Critters
{
    public class MysticBunny : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Bunny);
            Item.makeNPC = ModContent.NPCType<NPCs.Critters.MysticBunny>();
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 7));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }
    }
}