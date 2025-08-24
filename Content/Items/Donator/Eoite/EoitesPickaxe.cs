using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Eoite
{
    public class EoitesPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 60;
            Item.damage = 85;
            Item.knockBack = 4.5f;
            Item.useTime = 5;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.pick = 240;
            Item.tileBoost += 5;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item1;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 6);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.Amethyst, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 100);
        }
    }
}