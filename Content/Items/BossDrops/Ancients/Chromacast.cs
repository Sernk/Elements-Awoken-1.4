using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Ancients
{
    public class Chromacast : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 550;
            Item.knockBack = 2;
            Item.mana = 9;
            Item.useStyle = 5;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 75, 0, 0);
            Item.rare = ModContent.RarityType<Rarity14>();
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<ChromacastBall>();
            Item.shootSpeed = 22f;
        }
    }
}