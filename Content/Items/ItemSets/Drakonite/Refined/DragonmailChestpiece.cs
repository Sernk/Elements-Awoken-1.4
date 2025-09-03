using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Body)]
    public class DragonmailChestpiece : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 22;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Throwing) *= 1.03f;
            player.GetDamage(DamageClass.Melee) *= 1.03f;
            player.GetDamage(DamageClass.Magic) *= 1.03f;
            player.GetDamage(DamageClass.Ranged) *= 1.03f;
            player.GetDamage(DamageClass.Summon) *= 1.03f;
            player.pickSpeed *= 0.95f;
            player.buffImmune[BuffID.OnFire] = true;
            Lighting.AddLight(player.Center, 1f, 0.2f, 0.2f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RefinedDrakonite>(), 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}