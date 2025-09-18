using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier6
{
    public class ParticleAccelerator : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;            
            Item.damage = 70;
            Item.knockBack = 3.5f;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item96;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 8;
            Item.shootSpeed = 10f;
            Item.shoot = ProjectileType<Particles>();
            Item.GetGlobalItem<ItemEnergy>().energy = 8;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) => true;
        public override Vector2? HoldoutOffset() => new Vector2(-26, -2);
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(); 
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ItemType<Microcontroller>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}