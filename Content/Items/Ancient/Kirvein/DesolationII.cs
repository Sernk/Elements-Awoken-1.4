using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Arrows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ElementsAwoken.Content.Items.Ancient.Kirvein
{
    public class DesolationII : ModItem
    {
        public int discCooldown = 0;
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 44;
            Item.damage = 38;
            Item.knockBack = 2f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 5;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 14f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (discCooldown > 0)
            {
                for (var j = 0; j < 10; ++j)
                {
                    string text = "" + discCooldown / 60;
                    Vector2 textScale = new Vector2(Main.hotbarScale[j], Main.hotbarScale[j]);
                    Item otherItem = Main.player[Main.myPlayer].inventory[j];
                    if (otherItem == Item)
                    {
                        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position + new Vector2(23f, 20f) * Main.inventoryScale, Color.Red, 0f, Vector2.Zero, new Vector2(Main.inventoryScale), -1f, Main.inventoryScale);
                    }
                }
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            if (discCooldown <= 0)
            {
                return true;
            }
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            discCooldown--;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .75f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ModContent.ProjectileType<DesolationArrow>();
                }
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                float pi = 0.314159274f;
                int numProjectiles = 3;
                Vector2 vector14 = new Vector2(speed.X, speed.Y);
                vector14.Normalize();
                vector14 *= 40f;
                bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
                for (int num123 = 0; num123 < numProjectiles; num123++)
                {
                    float num124 = (float)num123 - ((float)numProjectiles - 1f) / 2f;
                    Vector2 vector15 = vector14.RotatedBy((double)(pi * num124), default(Vector2));
                    if (!flag11)
                    {
                        vector15 -= vector14;
                    }
                    int num125 = Projectile.NewProjectile(source, vector2.X + vector15.X, vector2.Y + vector15.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                    Main.projectile[num125].noDropItem = true;
                }
            }
            else
            {
                Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<DesolationDisc>(), damage, knockback, player.whoAmI, 0.1f, Main.rand.Next(-1, 2));
                discCooldown = 600;
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesolationI>(), 1);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}