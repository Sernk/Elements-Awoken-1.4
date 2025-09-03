using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class ToySlimeSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = 1;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummonToolTips>().AwakenedSummonItem = true;
            EABossSummonToolTips.TimeToSummon = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (EABossSummonToolTips.TimeToSummonNextUses <= 0)
            {
                return true;
            }
            return false;
        }
        public override bool? UseItem(Player player)
        {
            if (MyWorld.awakenedMode)
            {
                EABossSummonToolTips.TimeToSummonNextUses = 6000;
            }
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.toySlimeChanceTimer = 3600;
            return true;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (EABossSummonToolTips.TimeToSummonNextUses > 0)
            {
                for (var j = 0; j < 10; ++j)
                {
                    string text = "" + EABossSummonToolTips.TimeToSummonNextUses / 60;
                    Vector2 textScale = new Vector2(Main.hotbarScale[j], Main.hotbarScale[j]);
                    Item otherItem = Main.player[Main.myPlayer].inventory[j];
                    if (otherItem == Item)
                    {
                        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position + new Vector2(23f, 20f) * Main.inventoryScale, Color.Red, 0f, Vector2.Zero, new Vector2(Main.inventoryScale), -1f, Main.inventoryScale);
                    }
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            if (EABossSummonToolTips.TimeToSummonNextUses > 0)
            {
                EABossSummonToolTips.TimeToSummonNextUses--;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 30);
            recipe.AddIngredient(ItemID.Bone, 3);
            recipe.AddRecipeGroup(EARecipeGroups.CopperBar, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}