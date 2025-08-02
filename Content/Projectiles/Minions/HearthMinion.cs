using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.Content.Projectiles.Minions.MinionProj;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.Minions
{   
    public class HearthMinion : MinionINFO
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 34;
            Projectile.minionSlots = 2;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            inertia = 30f;
            shoot = ProjectileType<HearthBeam>();
            shootSpeed = 14f;
            shootCool = 300;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(BuffType<HearthMinionBuff>(), 3600);
            if (player.dead)  modPlayer.hearthMinion = false;
            if (modPlayer.hearthMinion)  Projectile.timeLeft = 2;
        }
        public override void CreateDust()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }
        public override void ShootExtraAction()
        {
            SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        }
        public override void SelectFrame()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 3;
            }
        }
    }
}