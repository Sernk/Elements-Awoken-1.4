using ElementsAwoken.Content.Buffs.PetBuffs;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Pets.Wyvern;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Chamcham
{
    public class PinkShoe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.useStyle = 1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item2;
            Item.rare = 4;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<AnarchyWave>();
            Item.buffType = ModContent.BuffType<WyvernPetBuff>();
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<WyvernHead>()] <= 0)
            {
                int current = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<WyvernHead>(), 0, 0f, Main.myPlayer);

                int previous = current;
                for (int k = 0; k < 5; k++)
                {
                    previous = current;
                    current = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<WyvernBody>(), 0, 0f, Main.myPlayer, previous);
                }
                previous = current;
                current = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<WyvernTail>(), 0, 0f, Main.myPlayer, previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(ItemID.Silk, 1);
            recipe.AddIngredient(ItemID.PinkGel, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}