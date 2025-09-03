using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Storyteller;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.InfinityGauntlet
{
    public class MoonStone : ModItem
    {
        public int pushTimer = 0;
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 10;
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (!player.HasItem(ModContent.ItemType<EmptyGauntlet>()))
            {
                if (player.HasItem(ModContent.ItemType<FrigidStone>()) || player.HasItem(ModContent.ItemType<PyroStone>()) || player.HasItem(ModContent.ItemType<AquaticStone>()) || player.HasItem(ModContent.ItemType<DeathStone>()) || player.HasItem(ModContent.ItemType<AridStone>()))
                {
                    if (modPlayer.overInfinityCharged == 0)
                    {
                        Main.NewText(ModContent.GetInstance<EALocalization>().InfinityStone);
                    }
                    modPlayer.overInfinityCharged++;
                }
                else
                {
                    modPlayer.overInfinityCharged = 0;
                }
            }
            else
            {
                modPlayer.overInfinityCharged = 0;
            }

            player.wingTimeMax = (int)(player.wingTimeMax * 1.2f);

            pushTimer--;
            if (pushTimer <= 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (!nPC.friendly && nPC.active && nPC.damage > 0 && !nPC.boss && Vector2.Distance(nPC.Center, player.Center) < 300)
                    {
                        Vector2 toTarget = new Vector2(player.Center.X - nPC.Center.X, player.Center.Y - nPC.Center.Y);
                        toTarget.Normalize();
                        nPC.velocity -= toTarget * 8f;
                        if (!nPC.noGravity)
                        {
                            nPC.velocity.Y -= 7.5f;
                        }
                    }
                }
                pushTimer = 300;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}