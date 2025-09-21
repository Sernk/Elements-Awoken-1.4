using ElementsAwoken.Content.Items.Weapons.Thrown;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class ThrowableBookP : ModProjectile
    {    	
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 600;
            AIType = 48;
        }
        public override void OnKill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
                int item = Item.NewItem(EAU.Proj(Projectile), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<ThrowableBook>());
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}