using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class Chlorobyte : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = -1;
            Item.maxStack = 9999;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 13));
            EAU.SetSoul(Type);
        }
        public override void UpdateInventory(Player player)
        {
            if (Item.stack >= 5)
            {
                int chance = 500 - Item.stack * 3;
                if (chance < 60)
                {
                    chance = 60;
                }
                if (Main.rand.Next(chance) == 0)
                {
                    Item.stack--;
                    Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), ModContent.ProjectileType<EscapedChlorobyte>(), 0, 0f, player.whoAmI, player.whoAmI, 0f);
                }
            }
        }
    }
}