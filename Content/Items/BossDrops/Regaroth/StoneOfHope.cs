using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    public class StoneOfHope : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.accessory = true;
            Item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //TODO: Bonuses in another method
            if (player.statLife <= (player.statLifeMax2 * 0.9f) && player.statLife > (player.statLifeMax2 * 0.8f))
            {
                Stat(player, bonus: 1.01f, 2);
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.7f))
            {
                Stat(player, bonus: 1.02f, 4);
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.7f) && player.statLife > (player.statLifeMax2 * 0.6f))
            {
                Stat(player, bonus: 1.04f, 6);
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.5f))
            {
                Stat(player, bonus: 1.06f, 8);
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.5f) && player.statLife > (player.statLifeMax2 * 0.4f))
            {
                Stat(player, bonus: 1.09f, 10);
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.3f))
            {
                Stat(player, bonus: 1.12f, 12);
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.3f) && player.statLife > (player.statLifeMax2 * 0.2f))
            {
                Stat(player, bonus: 1.16f, 15);
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.2f) && player.statLife > (player.statLifeMax2 * 0.1f))
            {
                Stat(player, bonus: 1.2f, 17);
            }
            else if (player.statLife <= (player.statLifeMax2 * 0.1f))
            {
                Stat(player, bonus: 1.25f, 20);
            }
        }
        void Stat(Player player, float bonus, int bonus2)
        {
            player.GetDamage(DamageClass.Melee) *= bonus;
            player.GetDamage(DamageClass.Throwing) *= bonus;
            player.GetDamage(DamageClass.Ranged) *= bonus;
            player.GetDamage(DamageClass.Magic) *= bonus;
            player.GetDamage(DamageClass.Summon) *= bonus;
            player.statDefense += bonus2;
        }
    }
}