using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Regular
{
    public class DrakoniteBlade : ModItem
    {
        public int CD = 0;
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 52;          
            Item.damage = 13;
            Item.knockBack = 6;
            Item.useTime = 30;   
            Item.useAnimation = 30;     
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Drakonite", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void UpdateInventory(Player player)
        {
            CD--;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 120);
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DragonBladeBlast>()] < 1 && CD <= 0)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y + 4, 0f, 0f, ModContent.ProjectileType<DragonBladeBlast>(), 0, 0, player.whoAmI, 0.0f, 0.0f);
                SoundEngine.PlaySound(SoundID.Item62, player.Center);
                CD = 30;
            }
        }
    }
}
