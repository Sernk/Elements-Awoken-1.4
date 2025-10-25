using ElementsAwoken.Content.Items.Ancient;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Global
{
    public class QoLItem : GlobalItem
    {
        public override void UpdateInventory(Item item, Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (item.type == ItemID.CellPhone)
            {
                EnableAllEAInfo(modPlayer);
            }
            if (item.type == ItemID.Shellphone)
            {
                EnableAllEAInfo(modPlayer);
            }
        }
        public static void EnableAllEAInfo(MyPlayer modPlayer)
        {
            modPlayer.alchemistTimer = true;
            modPlayer.dryadsRadar = true;
            modPlayer.rainMeter = true;
        }
        public class RodOfDiscordSystem : ModSystem
        {
            public override void Load()
            {
                On_ShimmerTransforms.IsItemTransformLocked += ТewКequirements;
            }
            static bool ТewКequirements(On_ShimmerTransforms.orig_IsItemTransformLocked orig, int type)
            {
                if (type == ModContent.ItemType<MysticGemstone>()) return NPC.downedBoss3;
                if (type == ItemID.RodofDiscord) return !MyWorld.downedAzana;
                else return orig(type);
            }
        }
    }
}