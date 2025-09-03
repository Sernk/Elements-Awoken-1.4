using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions.PhantomBane
{
    public class PhantomHook : ModProjectile
    {
        public bool hasGivenBuff = false;
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 0.5f;
            Projectile.light = 2f;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 388;
            Projectile.aiStyle = 66;
            Projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Phantom Hook");
            //Main.projFrames[projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 3;
        }

        private static int[] minionToType = new int[6];
        private int minion;

        public PhantomHook() : this(1) { }

        public PhantomHook(int minion)
        {
            this.minion = minion;
        }

        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }
        //public override bool IsLoadingEnabled(Mod mod)
        //{
        //    if (Mod.ContentAutoloadingEnabled) 
        //    {
        //        for (int k = 0; k <= 3; k++)
        //        {
        //            ModProjectile next = new PhantomHook(k);
        //            mod.AddContent(next);
        //            minionToType[k] = next.Projectile.type;
        //        }
        //    }
        //    return false;
        //}
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            bool flag64 = Projectile.type == ModContent.ProjectileType<PhantomHook>();

            if (!hasGivenBuff)
            {
                player.AddBuff(ModContent.BuffType<PhantomHookBuff>(), 3600);

                hasGivenBuff = true;
            }
            if (player.dead)
            {
                modPlayer.phantomHook = false;
            }
            if (modPlayer.phantomHook)
            {
                Projectile.timeLeft = 2;
            }
            Projectile.rotation += Projectile.velocity.X * 0.04f;

            if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner && minion == 0)
            {
                for (int k = 1; k <= 5; k++)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center, Projectile.velocity, minionToType[k], Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
                Projectile.localAI[0] = 1f;
            }
            Projectile.frame = minion;

            ProjectileUtils.PushOtherEntities(Projectile);

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}