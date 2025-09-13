using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    [AutoloadEquip(EquipType.Legs)]
    public class PutridLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.defense = 13;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) *= 1.1f;
            player.maxMinions++;
            if (player.velocity.Y == 0 && (player.velocity.X < -1 || player.velocity.X > 1) && player.GetModPlayer<MyPlayer>().generalTimer % 3 == 0)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Bottom.X, player.Bottom.Y - 6, 0, -0.9f, ProjectileType<PutridTrail>(), 60, 0, player.whoAmI, 0.0f, 0.0f);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<PutridBar>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}