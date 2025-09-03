using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.EASystem.EAPlayer;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class Demon : ModProjectile
    {
        public int shootTimer = 30;
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.minion = true;
            Projectile.aiStyle = 62;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }
        public override bool PreDraw(ref Color lightColor)
        { 
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 12)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 1)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.04f;
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (player.dead)
            {
                Projectile.active = false;
            }
            if (modPlayer.superbaseballDemon)
            {
                Projectile.timeLeft = 2;
            }
            else
            {
                Projectile.active = false;
            }
            shootTimer--;
            float maxDist = 400f;
            float shootSpeed = 15f;
            Vector2 targetPos = Projectile.position;
            float targetDist = maxDist;
            bool target = false;
            Projectile.tileCollide = true;
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                    }
                }
            }
            if (target && shootTimer <= 0)
            {
                if ((targetPos - Projectile.Center).X > 0f)
                {
                    Projectile.spriteDirection = (Projectile.direction = -1);
                }
                else if ((targetPos - Projectile.Center).X < 0f)
                {
                    Projectile.spriteDirection = (Projectile.direction = 1);
                }
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 shootVel = targetPos - Projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= shootSpeed;
                    SoundEngine.PlaySound(SoundID.Item8, player.position);
                    int proj = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ProjectileID.DemonScythe, 20, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].timeLeft = 300;
                    Main.projectile[proj].netUpdate = true;
                    Projectile.netUpdate = true;
                }
                shootTimer = 40;
            }
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