using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Held
{
    public class SpaceWand : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Items/ItemSets/Meteor/SpaceWand"; } }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            EAU.Sb.Draw(tex, drawPos, null, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            if (Projectile.localAI[0] == 1)
            {
                Texture2D crossTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/MeteorCross").Value;
                Vector2 crossOrigin = new Vector2(crossTex.Width * 0.5f, crossTex.Height * 0.5f);

                EAU.Sb.Draw(crossTex, new Vector2(Main.mouseX, Main.mouseY), null, Color.White, 0f, crossOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.ai[1] += 1f;
            if (Main.myPlayer == Projectile.owner)
            {
                if ((player.channel && (player.HeldItem.mana > 0 && player.CheckMana(player.inventory[player.selectedItem].mana, false, false))) && !player.noItems && !player.CCed)
                {
                    if (Projectile.ai[1] >= 40 && Projectile.ai[0] == 0)
                    {
                        if (Collision.CanHitLine(Projectile.position + Projectile.velocity * 2, 2, 2, Main.MouseWorld, 2, 2))
                        {
                            CreateProj();
                            Projectile.ai[0]++;
                            Projectile.localAI[0] = 0;
                        }
                        else
                        {
                            Projectile.localAI[0] = 1;
                        }
                    }
                }
                else
                {
                    for (int p = 0; p < Main.maxProjectiles; p++)
                    {
                        if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].type == Mod.Find<ModProjectile>("MeteoricFireball").Type)
                        {
                            Main.projectile[p].Kill();
                            break;
                        }
                    }
                    Projectile.Kill();
                }
                int soundDelay = (int)MathHelper.Lerp(12, 0, Projectile.ai[1] / 40);

                if (Projectile.soundDelay <= 0)
                {
                    Projectile.soundDelay = 2 + soundDelay;
                    Projectile.soundDelay *= 2;
                    SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
                }

                ProjectileUtils.HeldWandPos(Projectile, player);
            }
            if (Projectile.ai[1] < 40)
            {
                Vector2 add = Projectile.Center + Projectile.velocity * 4f;
                int width = 16;
                Dust dust = Main.dust[Dust.NewDust(add - Vector2.One * (width / 2), width, width, 6)];
                dust.velocity = Main.rand.NextVector2Square(-3f, 3f);
                dust.noGravity = true;
                dust.customData = player;
            }
        }
        private void CreateProj()
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ModContent.ProjectileType<MeteoricFireball>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);

            for (int p = 0; p < 10; p++)
            {
                int width = 16;
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y) - Vector2.One * (width / 2), 16, 16, 6)];
                dust.velocity = Main.rand.NextVector2Square(-3f, 3f);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1f, 2f);
            }
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
        }
    }
}