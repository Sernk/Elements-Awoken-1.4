using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable
{
    public class ElementalCapsule : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.rare = 11;
            Item.expert = true;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item119;
        }
        public override bool CanUseItem(Player player)
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            if (NPCsGLOBAL.AnyBoss())
            {
                NetworkText DeathReason = NetworkText.FromLiteral(player.name + " " + EALocalization.ElementalCapsule);
                player.KillMe(PlayerDeathReason.ByCustomReason(DeathReason), 3000, 1);
            }
            if (Main.expertMode)
            {
                if (MyWorld.awakenedMode)
                {
                    Main.NewText(EALocalization.ElementalCapsule1, Color.DeepPink);
                    MyWorld.awakenedMode = false;
                    MyWorld.awakenedModeNoActive = true;
                }
                else
                {
                    Main.NewText(EALocalization.ElementalCapsule2, Color.DeepPink);
                    MyWorld.awakenedMode = true;
                    MyWorld.awakenedModeNoActive = false;
                }
                if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
                return true;
            }
            return false;
        }
        //public bool Enabled
        //{
        //    get => MyWorld.AwakenedModeEnabled;
        //    set
        //    {
        //        MyWorld.AwakenedModeEnabled = value;
        //    }
        //}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.IronBar, 8);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
