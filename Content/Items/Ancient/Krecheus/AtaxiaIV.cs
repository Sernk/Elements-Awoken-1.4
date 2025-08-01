using ElementsAwoken.Content.Items.BossDrops.Ancients;
using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.Ancient.Krecheus
{
    public class AtaxiaIV : ModItem
    {
        private int attackNum = 0;
        private float altCounter = 0;
        private int altResetTimer = 0;
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.damage = 580;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ModContent.RarityType<Rarity14>();
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<AtaxiaWave>();
            Item.shootSpeed = 22f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void HoldItem(Player player)
        {
            altResetTimer--;
            if (player.altFunctionUse == 2)
            {
                if (altCounter < 300) altCounter++;
                altResetTimer = 20;
                if (altCounter > 0)
                {
                    Item.useTime = (int)MathHelper.Lerp(22, 4, altCounter / 300f);
                    Item.useAnimation = (int)MathHelper.Lerp(22, 4, altCounter / 300f);
                }
            }
            else
            {
                if (altResetTimer <= 0) 
                {
                    altCounter = 0;
                    Item.useTime = 12;
                    Item.useAnimation = 12;
                }
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                altResetTimer = 0;
                int numberProjectiles = Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(12));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                }
                attackNum++;
                if (attackNum >= 8)
                {
                    for (int l = 0; l < 8; l++)
                    {
                        int distance = Main.rand.Next(360);
                        Projectile orbital = Main.projectile[Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<AtaxiaCrystal>(), damage, 0f, 0, l * distance, l >= 3 ? Main.rand.Next(3, 5) : Main.rand.Next(3))];
                        orbital.localAI[0] = 50;
                        if (l > 2 && l <= 5) orbital.localAI[0] = 75;
                        else if (l > 5) orbital.localAI[0] = 100;
                    }
                    attackNum = 0;
                }
                if (Main.rand.Next(3) == 0)
                {
                    int numberProjectiles2 = Main.rand.Next(1, 4);
                    for (int i = 0; i < numberProjectiles2; i++)
                    {
                        Vector2 velocity = new Vector2(speed.X, speed.Y) * 0.75f;
                        Vector2 perturbedSpeed = speed.RotatedByRandom(MathHelper.ToRadians(15));
                        perturbedSpeed *= Main.rand.NextFloat(1f, 1.3f);
                        Projectile proj = Main.projectile[Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AtaxiaBlade>(), damage, knockback, player.whoAmI)];
                        proj.scale *= 1.4f;
                    }
                }
            }
            else
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(7));
                speed.X = perturbedSpeed.X;
                speed.Y = perturbedSpeed.Y;
                Projectile proj = Main.projectile[Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<AtaxiaBolt>(), (int)MathHelper.Lerp(damage * 0.3f, damage * 1.7f, altCounter / 300), knockback, player.whoAmI, altCounter)];
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AtaxiaIII>(), 1);
            recipe.AddIngredient(ModContent.ItemType<BossDrops.Ancients.AncientShard>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 4);
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 20);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ChaoticCrucible>());
            recipe.Register();
        }
    }
}