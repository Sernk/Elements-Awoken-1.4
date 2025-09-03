using ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined;
using ElementsAwoken.Content.Items.ItemSets.Drakonite.Regular;
using ElementsAwoken.Content.Items.ItemSets.Mortemite;
using ElementsAwoken.Content.Items.ItemSets.Radia;
using ElementsAwoken.Content.Items.ItemSets.Stellarium;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Items.Tech.Weapons.Tier6;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Developer
{
    public class AeroflakGun : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26; 
            Item.damage = 900;
            Item.knockBack = 3.5f;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.GetGlobalItem<EATooltip>().developer = true;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = RarityType<EARarity.Rarity12>();
            Item.shootSpeed = 12f;
            Item.shoot = ProjectileType<AeroflakP>();
            Item.GetGlobalItem<ItemEnergy>().energy = 10;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item91, player.position); 
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 120f;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            float damageScale = MathHelper.Lerp(1, 2, MathHelper.Clamp((float)modPlayer.aeroflakHits / 10,0,1));
        
            Projectile.NewProjectile(source, position.X, position.Y , speed.X, speed.Y, type, (int)(damage * damageScale), knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-26, -2);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Railgun>(), 1);
            recipe.AddIngredient(ItemType<LRM>(), 1);
            recipe.AddIngredient(ItemType<Drakonite>(), 8);
            recipe.AddIngredient(ItemType<RefinedDrakonite>(), 8);
            recipe.AddIngredient(ItemType<MortemiteDust>(), 8);
            recipe.AddIngredient(ItemType<StellariumBar>(), 8);
            recipe.AddIngredient(ItemType<Radia>(), 8);
            recipe.AddTile(TileType<Tiles.Crafting.ChaoticCrucible>());
            recipe.Register();
        }
    }
}
