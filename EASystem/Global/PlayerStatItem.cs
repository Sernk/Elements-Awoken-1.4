using ElementsAwoken.Content.Items.Consumable.StatIncreases;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.EAPlayer
{
    public class PlayerStatItem : GlobalItem
    {
        public override bool? UseItem(Item item, Player player)
        {
            HeartsPlayers HeartsPlayers = player.GetModPlayer<HeartsPlayers>();
            if (item.type == ModContent.ItemType<EmptyVessel>())
            {
                if (HeartsPlayers.CountUsechaosHear >= 10)
                {
                    return false;
                }

                player.itemTime = (int)((float)item.useTime / player.GetAttackSpeed(item.DamageType));
                HeartsPlayers.CountUsechaosHear++;
                player.HealEffect(10, true);
                AchievementsHelper.HandleSpecialEvent(player, 2);
                HeartsPlayers.HPTier_0 = false;
                return HeartsPlayers.ChaosHeartVisual = true;
            }
            if (item.type == ModContent.ItemType<VoidCompressor>())
            {
                if (HeartsPlayers.CountUseCompressor >= 1)
                {
                    return false;
                }
                HeartsPlayers.CountUseCompressor++;
            }
            if (item.type == ModContent.ItemType<ChaosHeart>())
            {
                if (HeartsPlayers.CountUseEmptyVessel >= 10)
                {
                    return false;
                }
                player.itemTime = (int)((float)item.useTime / player.GetAttackSpeed(item.DamageType));
                player.HealEffect(10, true);
                AchievementsHelper.HandleSpecialEvent(player, 2);
                HeartsPlayers.CountUseEmptyVessel++;
                HeartsPlayers.HPTier_0 = false;
                HeartsPlayers.ChaosHeartVisual = false;
                return HeartsPlayers.EmptyVesselVisual = true;
            }
            if (item.type == ModContent.ItemType<LunarStar>())
            {
                HeartsPlayers.Mana = 100;
                if (Main.myPlayer == player.whoAmI) { player.ManaEffect(100); }
                player.itemTime = (int)((float)item.useTime / player.GetAttackSpeed(item.DamageType));
                player.GetModPlayer<MyPlayer>().lunarStarsUsed += 1;
                HeartsPlayers.ManaBonus = true;
            }
            if (item.type == ItemID.LifeFruit)
            {
                player.itemTime = (int)((float)item.useTime / player.GetAttackSpeed(item.DamageType));
                return HeartsPlayers.HPTier_0 = true;
            }
            return base.UseItem(item, player);
        }
        public override bool CanUseItem(Item item, Player player)
        {
            HeartsPlayers HeartsPlayers = player.GetModPlayer<HeartsPlayers>();
            if (item.type == ModContent.ItemType<EmptyVessel>())
            {
                item.consumable = true;
                if (player.statLifeMax >= 500 && HeartsPlayers.CountUsechaosHear < 10) return true;
                else return false;
            }
            if (item.type == ModContent.ItemType<ChaosHeart>())
            {
                item.consumable = true;
                if (player.statLifeMax >= 600 && HeartsPlayers.CountUseEmptyVessel < 10) return true;
                else return false;
            }
            if (item.type == ModContent.ItemType<LunarStar>())
            {
                if (player.statManaMax == 200 && player.GetModPlayer<MyPlayer>().lunarStarsUsed < 1) return true;
                else return false;
            }
            return base.CanUseItem(item, player);
        }
    }
}