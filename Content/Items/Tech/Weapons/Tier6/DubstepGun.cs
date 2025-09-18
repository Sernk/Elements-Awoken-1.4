using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Items.Tech.Weapons.Tier2;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier6
{
    public class DubstepGun : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 34;
            Item.damage = 110;
            Item.knockBack = 3.75f;
            Item.GetGlobalItem<ItemEnergy>().energy = 3;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item15;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 8;
            Item.shoot = ProjectileType<DubstepGunHeld>();
            Item.shootSpeed = 20f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BassBooster>(), 1);
            recipe.AddIngredient(ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ItemType<CopperWire>(), 15);
            recipe.AddIngredient(ItemType<SiliconBoard>(), 1);
            recipe.AddIngredient(ItemType<Microcontroller>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}