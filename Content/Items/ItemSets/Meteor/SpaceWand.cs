using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Meteor
{
    public class SpaceWand : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 26;
            Item.knockBack = 3.75f;
            Item.mana = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item15;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Held.SpaceWand>();
            Item.shootSpeed = 6f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            if (player.armor[0].type == ItemID.MeteorHelmet && player.armor[1].type == ItemID.MeteorSuit && player.armor[2].type == ItemID.MeteorLeggings) mult = 0;
        }
    }
}