using ElementsAwoken.Content.Items.Other;
using ElementsAwoken.Content.Items.Pets;
using ElementsAwoken.Content.NPCs.Bosses.Azana;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class TomatoP : ModProjectile
    {  	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 600;
            AIType = 48;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type == ModContent.NPCType<Azana>())
            {
                int item = Item.NewItem(EAU.Proj(Projectile), (int)target.position.X, (int)target.position.Y, target.width, target.height, ModContent.ItemType<AzanaChibi>());
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                Main.NewText("Glegrep?", new Color(235, 70, 106));
            }
            else
            {
                int item = Item.NewItem(EAU.Proj(Projectile), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<Tomato>());
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int item = Item.NewItem(EAU.Proj(Projectile), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<Tomato>());
            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            return true;
        }
    }
}