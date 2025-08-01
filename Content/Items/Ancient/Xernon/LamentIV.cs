using ElementsAwoken.Content.Items.BossDrops.Ancients;
using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.Ancient.Xernon
{
    public class LamentIV : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 240;
            Item.mana = 12;
            Item.knockBack = 9;
            Item.crit = 20;
            Item.useStyle = 5;
            Item.useTime = 3;
            Item.useAnimation = 15;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ModContent.RarityType<Rarity14>();
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<LamentBallExplosive>();
            Item.shootSpeed = 15f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(7));
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            if (Main.rand.Next(3) == 0)type = ModContent.ProjectileType<LamentSuperPierce>();
            if (Main.rand.Next(5) == 0)
            {
                SoundEngine.PlaySound(SoundID.Item88, player.position);
                Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<LamentWave>(), (int)(Item.damage * 2f), knockback, player.whoAmI, 0f, 0f);

                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LamentIII>(), 1);
            recipe.AddIngredient(ModContent.ItemType<BossDrops.Ancients.AncientShard>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 4);
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 20);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ChaoticCrucible>());
            recipe.Register();
        }
    }
}