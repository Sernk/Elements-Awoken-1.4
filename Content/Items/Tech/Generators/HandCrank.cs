using ElementsAwoken.EASystem;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class HandCrank : ModItem
    {
        public int crankCooldown = 0;
        protected override bool CloneNewInstances { get { return true; } }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = 0;
            Item.maxStack = 1;
        }
        public override void UpdateInventory(Player player) => crankCooldown--;
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (crankCooldown <= 0 && modPlayer.energy < modPlayer.maxEnergy)
            {
                SoundEngine.PlaySound(new SoundStyle("ElementsAwokenSounds/Item/HandCrank"), Item.position);
                modPlayer.energy += 1;
                crankCooldown = 20;
            }
            Item.stack++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 16);
            recipe.AddIngredient(ItemID.StoneBlock, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}