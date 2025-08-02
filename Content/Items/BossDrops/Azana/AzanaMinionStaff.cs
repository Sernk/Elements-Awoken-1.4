using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class AzanaMinionStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62;
            Item.damage = 400;
            Item.knockBack = 2f;
            Item.mana = 10;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.DamageType = DamageClass.Summon;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<InfectionMouthMinion>();
            Item.shootSpeed = 10f;
        }
    }
}