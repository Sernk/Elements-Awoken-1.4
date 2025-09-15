using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class EnergyPickup : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 0;
            Item.maxStack = 1;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            float num = (float)Main.rand.Next(90, 111) * 0.01f;
            num *= Main.essScale;
            Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.1f * num, 0.5f * num, 0.2f * num);
        }
        public override bool OnPickup(Player player)
        {
            int amount = 5;
            int downedBosses = 0;
            if (NPC.downedSlimeKing) downedBosses++;
            if (NPC.downedBoss1) downedBosses++;
            if (NPC.downedBoss2) downedBosses++;
            if (NPC.downedQueenBee) downedBosses++;
            if (NPC.downedBoss3) downedBosses++;
            if (Main.hardMode) downedBosses++;
            if (NPC.downedMechBossAny) downedBosses++;
            if (NPC.downedPlantBoss) downedBosses++;
            if (NPC.downedMoonlord) downedBosses++;
            if (MyWorld.downedVoidLeviathan) downedBosses++;
            amount *= downedBosses;
            CombatText.NewText(player.getRect(), Color.LightBlue, amount, false, false);
            PlayerEnergy energyPlayer = player.GetModPlayer<PlayerEnergy>();
            energyPlayer.energy += amount;
            return false;
        }
    }
}
