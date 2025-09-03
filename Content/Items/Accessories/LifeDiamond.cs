using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class LifeDiamond : ModItem
    {
        public int drainLifeTimer = 20;
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && (player.armor[i].type == ItemType<VoidDiamond>() || player.armor[i].type == ItemType<BloodDiamond>()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            float maxDistance = 500f;
            float targetDist = maxDistance;
            NPC drainTarget = null;
            if (player.statLife < player.statLifeMax2 && player.ownedProjectileCounts[ProjectileType<HealProjLife>()] < 15)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    float distance = Vector2.Distance(nPC.Center, player.Center);
                    if (distance < targetDist && nPC.CanBeChasedBy(this) && nPC.life <= 3000 && !nPC.SpawnedFromStatue)
                    {
                        targetDist = distance;
                        drainTarget = nPC;
                    }
                }
                if (drainTarget != null)
                {
                    drainTarget.AddBuff(BuffType<VariableLifeRegen>(), 20);
                    drainTarget.GetGlobalNPC<NPCsGLOBAL>().lifeDrainAmount = 20;
                    drainLifeTimer--;
                    if (drainLifeTimer <= 0)
                    {
                        drainTarget.GetGlobalNPC<NPCsGLOBAL>().lifeDrainAmount = 50; if (Main.myPlayer == player.whoAmI)
                        {
                            float healAmount = Main.rand.Next(2, 4);
                            Projectile.NewProjectile(player.GetSource_FromThis(), drainTarget.Center.X, drainTarget.Center.Y, 0f, 0f, ProjectileType<HealProjLife>(), 0, 0f, Main.myPlayer, Main.myPlayer, healAmount); // ai 1 is how much it heals
                            drainLifeTimer = 30;
                        }
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BloodDiamond>(), 1);
            recipe.AddIngredient(ItemType<MysticLeaf>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}