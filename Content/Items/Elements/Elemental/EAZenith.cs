using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Elemental
{
    public class EAZenith : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.damage = 150;
            Item.knockBack = 5;
            Item.mana = 18;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item20;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.shoot = ModContent.ProjectileType<ZenithOrb1>();
            Item.shootSpeed = 24f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 1 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                switch (Main.rand.Next(4))
                {
                    case 0: type = ModContent.ProjectileType<ZenithOrb1>(); break;
                    case 1: type = ModContent.ProjectileType<ZenithOrb2>(); break;
                    case 2: type = ModContent.ProjectileType<ZenithOrb3>(); break;
                    case 3: type = ModContent.ProjectileType<ZenithOrb4>(); break;
                    default: break;
                }
                float rand = Main.rand.Next(2, 4);
    

                rand = rand * 4;
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X / rand, perturbedSpeed.Y / rand, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElementalEssence>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.MagnetSphere, 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}