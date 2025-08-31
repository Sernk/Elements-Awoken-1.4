using ElementsAwoken.Content.Items.BossDrops.Ancients;
using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.Chaos;
using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class AncientsSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.rare = ModContent.RarityType<EARarity.Rarity14>();
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.maxStack = 9999;
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override bool? UseItem(Player player)
        {
            var e = ModContent.GetInstance<EALocalization>(); 
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Izaris>());
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Kirvein>());
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Krecheus>());
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Xernon>());
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<ShardBase>());
            }
            else 
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<Izaris>(), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<Kirvein>(), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<Krecheus>(), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<Xernon>(), 0f, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<ShardBase>(), 0f, 0f, 0, 0, 0);
            }
            if (!MyWorld.downedAncients)
            {
                if (MyWorld.ancientSummons == 0)
                {
                    Main.NewText(e.AncientsSummon, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 1)
                {
                    Main.NewText(e.AncientsSummon1, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 2)
                {
                    Main.NewText(e.AncientsSummon2, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 3)
                {
                    Main.NewText(e.AncientsSummon3, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 4)
                {
                    Main.NewText(e.AncientsSummon4, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 5)
                {
                    Main.NewText(e.AncientsSummon5, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons > 5 && MyWorld.ancientSummons < 10)
                {
                    Main.NewText(e.AncientsSummon6, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 10)
                {
                    Main.NewText(e.AncientsSummon7, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 20)
                {
                    Main.NewText(e.AncientsSummon8, new Color(3, 188, 127));
                }
                if (MyWorld.ancientSummons == 100)
                {
                    Main.NewText(e.AncientsSummon9, new Color(3, 188, 127));
                }
            }
            else
            {
                Main.NewText(e.AncientsSummon10, new Color(3, 188, 127));
            }
            // Projectile.NewProjectile(player.Center.X, player.Center.Y - 300, 0f, 0f, mod.ProjectileType("ShardBase"), 0, 0f, player.whoAmI);

            MyWorld.ancientSummons++;
            
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChaoticFlare>(), 2);
            recipe.AddIngredient(ModContent.ItemType<CrystalAmalgamate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordantBar>(), 4);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ChaoticCrucible>());
            recipe.Register();
        }
    }
}
