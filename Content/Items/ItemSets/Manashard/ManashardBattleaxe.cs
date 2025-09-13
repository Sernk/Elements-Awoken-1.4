using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class ManashardBattleaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.width = 70;
            Item.height = 70;
            Item.useTime = 28;
            Item.useTurn = true;
            Item.useAnimation = 28;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(3) == 0)
            {
                SoundEngine.PlaySound(SoundID.Item27, player.position);
                int numberProjectiles = 4;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 value15 = new Vector2((float)Main.rand.Next(-6, 6), (float)Main.rand.Next(-6, 6));
                    Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, value15.X, value15.Y, ModContent.ProjectileType<Manashatter>(), Item.damage / 2, 2f, player.whoAmI, 0f, 0f);
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Manashard>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}