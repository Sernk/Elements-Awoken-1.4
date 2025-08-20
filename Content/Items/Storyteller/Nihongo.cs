using ElementsAwoken.Content.Projectiles.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Storyteller
{
    public class Nihongo : ModItem
    {
        public override void SetDefaults()
        {       
            Item.damage = 42;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.useTime = 18;
            Item.knockBack = 8.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 60;
            Item.width = 60;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.shoot = ModContent.ProjectileType<NihongoP>();
            Item.shootSpeed = 9f;
            Item.rare = 4;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}