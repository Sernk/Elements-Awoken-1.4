using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.CosmicObserver
{
    [AutoloadEquip(EquipType.Head)]
    public class CosmicalusVisor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.defense = 11;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void Load()
        {
            _ = this.GetLocalization("SetBonus").Value;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 5;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetCritChance(DamageClass.Throwing) += 5;

            player.maxMinions += 1;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CosmicalusBreastplate>() && legs.type == ModContent.ItemType<CosmicalusLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = this.GetLocalization("SetBonus").Value;
            player.GetModPlayer<MyPlayer>().cosmicalusArmor = true;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<CosmicalusRing>()] <= 0)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<CosmicalusRing>(), 0, 0f, player.whoAmI);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CosmicShard>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
