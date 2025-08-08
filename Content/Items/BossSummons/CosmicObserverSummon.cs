using ElementsAwoken.Content.NPCs.Bosses.CosmicObserver;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
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
    public class CosmicObserverSummon : ModItem
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
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
            EABossSummon.TimeToSummon = true;
        }
        public override bool CanUseItem(Player player)
        {
            return!NPC.AnyNPCs(ModContent.NPCType<CosmicObserver>()) && player.ZoneSkyHeight && EABossSummon.TimeToSummonNextUses <= 0;        
        }
        public override bool? UseItem(Player player)
        {
            if (MyWorld.awakenedMode)
            {
                EABossSummon.TimeToSummonNextUses = 600;
            }
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.observerChanceTimer = 3600;
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            if (EABossSummon.TimeToSummonNextUses > 0)
            {
                EABossSummon.TimeToSummonNextUses--;
            }
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (EABossSummon.TimeToSummonNextUses > 0)
            {
                for (var j = 0; j < 10; ++j)
                {
                    string text = "" + EABossSummon.TimeToSummonNextUses / 60;
                    Vector2 textScale = new Vector2(Main.hotbarScale[j], Main.hotbarScale[j]);
                    Item otherItem = Main.player[Main.myPlayer].inventory[j];
                    if (otherItem == Item)
                    {
                        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position + new Vector2(23f, 20f) * Main.inventoryScale, Color.Red, 0f, Vector2.Zero, new Vector2(Main.inventoryScale), -1f, Main.inventoryScale);
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddRecipeGroup(EARecipeGroups.IronBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}