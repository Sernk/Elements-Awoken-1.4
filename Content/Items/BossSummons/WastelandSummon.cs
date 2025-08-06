using ElementsAwoken.Content.NPCs.Bosses.Wasteland;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class WastelandSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 20;
            Item.rare = 2;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mutated Scorpion");
            // Tooltip.SetDefault("It writhes in your hand\nSummons Wasteland on use");
        }
        public override bool CanUseItem(Player player)
        {
            return 
            player.ZoneDesert && !NPC.AnyNPCs(ModContent.NPCType<Wasteland>());
            
        }
        public override bool? UseItem(Player player)
        {
            int npcIndex = NPC.NewNPC(EAU.Play(player), (int)player.Center.X, (int)player.Center.Y + 600, ModContent.NPCType<Wasteland>(), 0, 0f, 0f, 0f, 0f, 255);
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex, 0f, 0f, 0f, 0, 0, 0);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
}