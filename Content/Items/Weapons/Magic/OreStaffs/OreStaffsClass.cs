using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.OreStaffs
{
    public abstract class OreStaffsClass : ModItem
    {
        public abstract int Damage { get; }
        public abstract int ProjType { get; }
        public abstract int Materials { get; }
        public virtual float ShotSpeed => 13f;
        public override void SetDefaults()
        {
            Item.damage = Damage;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 4;
            Item.mana = 10;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ProjType;
            Item.shootSpeed = ShotSpeed;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Materials, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
    public abstract class OreStaffsProClass : ModItem
    {
        public abstract int Damage { get; }
        public abstract int ProjType { get; }
        public abstract int Materials { get; }
        public virtual float ShotSpeed => 13f;
        public virtual int ToRadiansQuantity => 16;
        public override void SetDefaults()
        {
            Item.damage = Damage;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 4;
            Item.mana = 10;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ProjType;
            Item.shootSpeed = ShotSpeed;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(ToRadiansQuantity));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Materials, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}