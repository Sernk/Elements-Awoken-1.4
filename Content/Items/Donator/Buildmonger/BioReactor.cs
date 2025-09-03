using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Buildmonger
{
    public class BioReactor : ModItem
    {
        public float charge = 0;
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 19;
            Item.knockBack = 1.5f;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item34;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 2;
            Item.shootSpeed = 20f;
            Item.useAmmo = ItemID.JungleSpores;
            Item.shoot = ModContent.ProjectileType<BioLightning>();
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ElectricArcing"), Item.position);

            int velocity = (int)player.velocity.X;
            if (velocity < 0)
            {
                velocity *= -1;
            }

            Vector2 vector94 = new Vector2(speed.X, speed.Y);
            float ai = (float)Main.rand.Next(100);
            Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(8)) * 4f;
    

            Projectile.NewProjectile(source, position.X, position.Y, vector95.X, vector95.Y, ModContent.ProjectileType<BioLightning>(), damage, 0f, Main.myPlayer, vector94.ToRotation(), ai);

            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.EvilBar, 8);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(ItemID.Stinger, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}