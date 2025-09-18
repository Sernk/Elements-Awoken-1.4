using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier4
{
    public class ArcBlade : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.damage = 55;
            Item.knockBack = 5;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;        
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
            if (modPlayer.energy > 8)
            {
                modPlayer.energy -= 8;
                target.AddBuff(ModContent.BuffType<ElectrifiedNPC>(), 120);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.AdamantiteBar, 8);
            recipe.AddIngredient(ModContent.ItemType<Capacitor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GoldWire>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SiliconBoard>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}