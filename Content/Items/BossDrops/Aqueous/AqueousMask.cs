using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.Content.Projectiles.Minions.AqueousMinion;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Aqueous
{
    public class AqueousMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 11;
            Item.accessory = true;
            Item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var p = EAU.Play(player);
            if (((double)player.velocity.X > 0 || (double)player.velocity.Y > 0 || (double)player.velocity.X < -0.1 || (double)player.velocity.Y < -0.1))
            {
                if (Main.rand.Next(3) == 0)
                {
                    int projectile1 = Projectile.NewProjectile(p, player.Center.X, player.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.FlaironBubble, 30, 5f, player.whoAmI, 0f, 0f);
                    Main.projectile[projectile1].timeLeft = 40;
                }
            }
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.aqueousMinions = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<AqueousMinions>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<AqueousMinions>(), 2, true);
                }
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AqueousMinionFriendly1>()] < 1)
            {
                Projectile.NewProjectile(p, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<AqueousMinionFriendly1>(), 60, 2f, Main.myPlayer, 0f, 0f);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AqueousMinionFriendly2>()] < 1)
            {
                Projectile.NewProjectile(p, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<AqueousMinionFriendly2>(), 60, 2f, Main.myPlayer, 0f, 0f);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AqueousMinionFriendly3>()] < 1)
            {
                Projectile.NewProjectile(p, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<AqueousMinionFriendly3>(), 60, 2f, Main.myPlayer, 0f, 0f);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AqueousMinionFriendly4>()] < 1)
            {
                Projectile.NewProjectile(p, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<AqueousMinionFriendly4>(), 60, 2f, Main.myPlayer, 0f, 0f);
            }
        }
    }
}