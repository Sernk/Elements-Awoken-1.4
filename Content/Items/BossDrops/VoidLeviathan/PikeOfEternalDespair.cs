using ElementsAwoken.Content.Projectiles.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    class PikeOfEternalDespair : ModItem
    {
        public override void SetDefaults()
        {       
            Item.width = 66;
            Item.height = 66;
            Item.damage = 193;
            Item.knockBack = 4f;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 19;
            Item.useStyle = 5;
            Item.useTime = 19;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.shoot = ModContent.ProjectileType<PikeOfEternalDespairP>();
            Item.shootSpeed = 11f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}