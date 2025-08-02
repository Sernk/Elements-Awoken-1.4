using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.CosmicObserver
{
    [AutoloadEquip(EquipType.Body)]
    public class CosmicalusBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.defense = 10;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) *= 1.1f;
            player.GetDamage(DamageClass.Melee) *= 1.1f;
            player.GetDamage(DamageClass.Summon) *= 1.1f;
            player.GetDamage(DamageClass.Ranged) *= 1.1f;
            player.GetDamage(DamageClass.Throwing) *= 1.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CosmicShard>(), 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}