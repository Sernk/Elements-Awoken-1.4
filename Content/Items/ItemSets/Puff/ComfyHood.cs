using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using ElementsAwoken.Content.NPCs.ItemSets.Puff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Puff
{
    [AutoloadEquip(EquipType.Head)]
    public class ComfyHood : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 0, 2, 0);
            Item.rare = 1;
            Item.defense = 2;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player) => player.maxMinions++;
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ComfyShirt>() && legs.type == ModContent.ItemType<ComfyPants>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ModContent.GetInstance<EALocalization>().ComfySetBonus;
            player.npcTypeNoAggro[ModContent.NPCType<NPCs.ItemSets.Puff.Puff>()] = true;
            player.npcTypeNoAggro[ModContent.NPCType<SpikedPuff>()] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Puffball>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}