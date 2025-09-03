using ElementsAwoken.Content.NPCs.Bosses.ScourgeFighter;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class ScourgeFighterSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = 6;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            //item.shoot = mod.ProjectileType("ScourgeFighterSpawn");
            Item.GetGlobalItem<EABossSummonToolTips>().AwakenedSummonItem = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !Main.dayTime;
        }
        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<ScourgeFighter>());
            else NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<ScourgeFighter>(), 0f, 0f, 0, 0, 0); 
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Main.NewText(player.position);
            return true;        
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofSight, 3);
            recipe.AddIngredient(ItemID.SoulofMight, 3);
            recipe.AddIngredient(ItemID.SoulofFright, 3);
            recipe.AddRecipeGroup(EARecipeGroups.IronBar, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}