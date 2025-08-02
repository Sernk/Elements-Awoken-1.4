using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.CosmicObserver
{
    [AutoloadEquip(EquipType.Legs)]
    public class CosmicalusLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.defense = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.15f;

            if (player.velocity.Y == 0f && player.velocity.X != 0)
            {
                int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, 220, 0f, 0f, 100, default(Color), 1.4f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CosmicShard>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}