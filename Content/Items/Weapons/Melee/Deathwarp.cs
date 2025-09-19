using ElementsAwoken.Content.Buffs.Cooldowns;
using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Spears;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Weapons.Melee
{
    public class Deathwarp : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 60;
            Item.width = 60;
            Item.damage = 326;
            Item.knockBack = 4.75f;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.maxStack = 1;
            Item.shoot = ProjectileType<DeathwarpP>();
            Item.shootSpeed = 14f;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) return player.FindBuffIndex(BuffType<DeathwarpCooldown>()) == -1;
            else return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                int swirlCount = 2;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 180;

                   Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, ProjectileType<DeathwarpSpinner>(), damage, knockback, player.whoAmI, l * distance);
                }
                player.AddBuff(BuffType<DeathwarpCooldown>(), 1200);
            }
            else
            {
                int numberProjectiles = Main.rand.Next(2,5);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(2));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<DeathwarpLaser>(), damage, knockback, player.whoAmI);
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<DiscordantBar>(), 15);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}
