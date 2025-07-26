using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EARecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                recipe.AddOnCraftCallback(EAOnCraft.SanityUPOnCraft);
            }
        }
    }
}
