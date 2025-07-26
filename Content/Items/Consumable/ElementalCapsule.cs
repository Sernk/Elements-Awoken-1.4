using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
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

        //public override void SetStaticDefaults()
        //{
        //    DisplayName.SetDefault("Elemental Capsule");
        //    Tooltip.SetDefault($"Forces bend around this capsule...\nCan only be used in Expert Mode\nOnly use if you are up for a challenge\nActivates [c/f442aa:Awakened Mode] and sanity [i:{ItemType<SanityChanger>()}]\n  -75% more enemy life, defence and damage\n  -New harder boss AI's and stats");
        //}

        public override bool CanUseItem(Player player)
        {
            if (NPCsGLOBAL.AnyBoss())
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " couldn't withstand the power."), 3000, 1);
            }
            if (Main.expertMode)
            {
                if (MyWorld.awakenedMode)
                {
                    Main.NewText("The forces of the world settle.", Color.DeepPink);
                    MyWorld.awakenedMode = false;
                }
                else
                {
                    Main.NewText("The forces of the world get twisted beyond imagination...", Color.DeepPink);
                    MyWorld.awakenedMode = true;
                }
                if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                return true;
            }
            return false;
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemType<Stardust>(), 2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
