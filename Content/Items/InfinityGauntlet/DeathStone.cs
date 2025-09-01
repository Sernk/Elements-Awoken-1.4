using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Storyteller;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.InfinityGauntlet
{
    public class DeathStone : ModItem
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
                if (player.HasItem(ModContent.ItemType<MoonStone>()) || player.HasItem(ModContent.ItemType<PyroStone>()) || player.HasItem(ModContent.ItemType<AquaticStone>()) || player.HasItem(ModContent.ItemType<FrigidStone>()) || player.HasItem(ModContent.ItemType<AridStone>()))
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

            player.statLifeMax2 += 50;

            if (Main.rand.Next(1200) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    bool immune = false;
                    foreach (int i in ElementsAwoken.instakillImmune)
                    {
                        if (nPC.type == i)
                        {
                            immune = true;
                        }
                    }
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600 && nPC.lifeMax < 30000 && !immune)
                    {
                        nPC.SimpleStrikeNPC(nPC.life, 0, false, 0f, DamageClass.Default, false, 0, false);
                        for (int d = 0; d < 100; d++)
                        {
                            int dust = Dust.NewDust(nPC.position, nPC.width, nPC.height, 219);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale = 1f;
                            Main.dust[dust].velocity *= 2f;
                        }
                        return; // to only kill 1 
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(EAU.VoidEssence, 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}