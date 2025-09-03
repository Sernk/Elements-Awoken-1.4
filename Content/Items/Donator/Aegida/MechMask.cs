using ElementsAwoken.Content.Items.BossDrops.Volcanox;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Donator.Aegida
{
    [AutoloadEquip(EquipType.Head)]
    public class MechMask : ModItem
    {
        private float pulsate = 0;
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 60, 0);
            Item.rare = 11;
            Item.defense = 18;
            Item.GetGlobalItem<EATooltip>().donator = true;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) *= 1.20f;
            player.GetCritChance(DamageClass.Ranged) += 10;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<MechBreastplate>() && legs.type == ItemType<MechLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            pulsate++;
            float pulsateValue = 0.6f + (float)Math.Sin(pulsate / 30f) / 2;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.2f * pulsateValue, 0.2f * pulsateValue, 0.8f * pulsateValue);
            player.setBonus = ModContent.GetInstance<EALocalization>().MechSetBonus;
            player.GetDamage(DamageClass.Generic) *= 1.1f;
            player.moveSpeed *= 1.35f;
            player.GetModPlayer<MyPlayer>().saveAmmo += 17;
            player.GetModPlayer<MyPlayer>().mechArmor = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VortexHelmet);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 20);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 3);
            recipe.AddIngredient(ItemType<VolcanicStone>(), 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}