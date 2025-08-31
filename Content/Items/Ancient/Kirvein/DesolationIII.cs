using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Arrows;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ElementsAwoken.Content.Items.Ancient.Kirvein
{
    public class DesolationIII : ModItem
    {
        public int discCooldown = 0;
        public int mode = 0;
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 44;
            Item.damage = 120;
            Item.knockBack = 2f;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item5;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 10;
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
                return true;
        }
        public override void UpdateInventory(Player player)
        {
            discCooldown--;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .50f;
        }
        public override bool CanUseItem(Player player)
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            if (player.altFunctionUse == 2)
            {
                mode++;
                if (mode > 2)
                {
                    mode = 0;
                }
                string text = "";
                if (mode == 0) text = EALocalization.Desolation;
                else if (mode == 1) text = EALocalization.Desolation1;
                else if (mode == 2) text = EALocalization.Desolation2;

                CombatText.NewText(player.getRect(), Color.DarkGreen, text, false, false);
                
                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                switch (mode)
                {
                    case 0:
                        Item.channel = false;
                        Item.noUseGraphic = false;
                        Item.autoReuse = true;
                        break;
                    case 1:
                        Item.channel = true;
                        Item.noUseGraphic = true;
                        Item.autoReuse = false;
                        break;
                    case 2:
                        Item.channel = false;
                        Item.noUseGraphic = false;
                        Item.autoReuse = true;
                        break;
                    default:
                        return base.CanUseItem(player);
                }
            }
            else
            {
                if (mode == 2)
                {
                    if (discCooldown <= 0)
                    {
                        return base.CanUseItem(player);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)

        {
            if (player.altFunctionUse != 2)
            {
                if (mode == 0)
                {
                    if (type == ProjectileID.WoodenArrowFriendly)
                    {
                        type = ModContent.ProjectileType<DesolationArrow>();
                    }
                    Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                    float pi = 0.314159274f;
                    int numProjectiles = 5;
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
                else if (mode == 1)
                {
                    Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<DesolationIIIHeld>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
                }
                else
                {
                    Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<DesolationDisc>(), damage, knockback, player.whoAmI, 0.1f, Main.rand.Next(-1, 2));
                    discCooldown = 540;
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesolationII>(), 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}