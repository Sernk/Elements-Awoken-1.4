using ElementsAwoken.Content.Buffs.Cooldowns;
using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.ToySlime
{
    public class ToyBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;      
            Item.damage = 27;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useTime = 16;   
            Item.useAnimation = 16;     
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 3;      
            Item.UseSound = SoundID.Item1;  
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(BuffType<BrokenToyBlade>()) != -1) return false;
            return base.CanUseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(12))
            {
                SoundEngine.PlaySound(SoundID.Item37, player.Center);
                for (int i = 0; i < 2; i++)
                {
                    Projectile brick = Main.projectile[Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-2, 2), ProjectileType<LegoBrickFriendly>(), Item.damage, 0, player.whoAmI)];
                    brick.penetrate = -1;
                    brick.timeLeft = 450;
                }
                player.itemAnimation = 0;
                player.AddBuff(BuffType<BrokenToyBlade>(), 60);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}