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
    public class FrigidStone : ModItem
    {
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
                if (player.HasItem(ModContent.ItemType<MoonStone>()) || player.HasItem(ModContent.ItemType<PyroStone>()) || player.HasItem(ModContent.ItemType<AquaticStone>()) || player.HasItem(ModContent.ItemType<DeathStone>()) || player.HasItem(ModContent.ItemType<AridStone>()))
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

            player.GetCritChance(DamageClass.Magic) += 5;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetCritChance(DamageClass.Throwing) += 5;

            if (Main.rand.Next(60) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600)
                    {
                        nPC.AddBuff(BuffID.Frostburn, 180, false);
                        return;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}