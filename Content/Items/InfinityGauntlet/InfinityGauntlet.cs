using ElementsAwoken.Content.Buffs.Cooldowns;
using ElementsAwoken.Content.Items.Storyteller;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.InfinityGauntlet;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.InfinityGauntlet
{
    public class InfinityGauntlet : ModItem
    {
        public int gauntletMode = 0;
        public int pushTimer = 0;

        protected override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void SetDefaults()
        {
            Item.damage = 105;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 8;
            Item.useAnimation = 16;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 2;
            Item.value = Item.buyPrice(2, 0, 0, 0);
            Item.rare = 10;
            Item.mana = 18;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<AncientStar>();
            Item.shootSpeed = 18f;
            Item.useTurn = true;
            Item.channel = false;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            player.buffImmune[BuffID.WindPushed] = true;
            player.moveSpeed *= 1.1f;

            player.GetDamage(DamageClass.Melee) *= 1.05f;
            player.GetDamage(DamageClass.Magic) *= 1.05f;
            player.GetDamage(DamageClass.Ranged) *= 1.05f;
            player.GetDamage(DamageClass.Summon) *= 1.05f;
            player.GetDamage(DamageClass.Throwing) *= 1.05f;
            if (Main.rand.Next(90) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600)
                    {
                        nPC.AddBuff(BuffID.OnFire, 180, false);
                        return;
                    }
                }
            }

            player.wingTimeMax = (int)(player.wingTimeMax * 1.2f);

            pushTimer--;
            if (pushTimer <= 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (!nPC.friendly && nPC.active && nPC.damage > 0 && !nPC.boss && Vector2.Distance(nPC.Center, player.Center) < 300)
                    {
                        Vector2 toTarget = new Vector2(player.Center.X - nPC.Center.X, player.Center.Y - nPC.Center.Y);
                        toTarget.Normalize();
                        nPC.velocity -= toTarget * 8f;
                        if (!nPC.noGravity)
                        {
                            nPC.velocity.Y -= 7.5f;
                        }
                    }
                }
                pushTimer = 300;
            }

            player.GetCritChance(DamageClass.Magic) += 5;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetCritChance(DamageClass.Throwing) += 5;
            if (Main.rand.Next(90) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600)
                    {
                        nPC.AddBuff(BuffID.Frostburn, 180, false);
                        return;
                    }
                }
            }

            player.ignoreWater = true;
            //player.statManaMax2 += 50; // makes it beep

            if (Main.rand.Next(1600) == 0)
            {
                for (int l = 0; l < Main.npc.Length; l++)
                {
                    NPC nPC = Main.npc[l];
                    bool immune = false;
                    foreach (int i in ElementsAwoken.instakillImmune)
                    {
                        if (nPC.type == i)
                        {
                            immune = true;
                        }
                    }
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= 600 && nPC.lifeMax < 30000 && !immune)
                    {
                        nPC.SimpleStrikeNPC(nPC.life, 0, false, 0f, DamageClass.Default, false, 0, false);
                        for (int d = 0; d < 100; d++)
                        {
                            int dust = Dust.NewDust(nPC.position, nPC.width, nPC.height, 219);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].scale = 1f;
                            Main.dust[dust].velocity *= 2f;
                        }
                        return; // to only kill 1 
                    }
                }
            }
            if (Main.mapFullscreen && player.FindBuffIndex(ModContent.BuffType<InfinityPortalCooldown>()) == -1 && gauntletMode == 2 && Main.mouseRight && Main.mouseRightRelease)
            {
                // thx heros mod for help with this code
                Vector2 worldMapSize = new Vector2(Main.maxTilesX * 16, Main.maxTilesY * 16);
                Vector2 cursorPosition = new Vector2((Main.mouseX - Main.screenWidth / 2) / 16, (Main.mouseY - Main.screenHeight / 2) / 16) * 16 / Main.mapFullscreenScale;
                Vector2 targetPos = (Main.mapFullscreenPos + cursorPosition) * 16;

                // to stop the player teleporting out of the map
                if (targetPos.X < 0) targetPos.X = 0;
                else if (targetPos.X + player.width > worldMapSize.X) targetPos.X = worldMapSize.X - player.width;
                if (targetPos.Y < 0) targetPos.Y = 0;
                else if (targetPos.Y + player.height > worldMapSize.Y) targetPos.Y = worldMapSize.Y - player.height;
                
                Point tilePos = (targetPos / 16).ToPoint();
                Tile tile = Framing.GetTileSafely(tilePos.X, tilePos.Y);
                MapTile mapTile = Main.Map[tilePos.X, tilePos.Y];
                if (ValidTile(tile) && DiscoveredArea(mapTile))
                {
                    player.position = targetPos;
                    player.velocity = Vector2.Zero;
                    player.fallStart = (int)targetPos.Y/16; // to stop fall damage
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, targetPos.X, targetPos.Y, 1, 0, 0);
                    }

                    if (!ModContent.GetInstance<Config>().debugMode)
                    {
                        player.AddBuff(ModContent.BuffType<InfinityPortalCooldown>(), 1800);
                    }
                    else
                    {
                        player.AddBuff(ModContent.BuffType<InfinityPortalCooldown>(), 30);
                    }
                }
            }
        }
        private bool ValidTile(Tile tile)
        {
            if (Main.tileSolid[tile.TileType] && tile.HasTile)
            {
                return false;
            }
            return true;
        }
        private bool DiscoveredArea(MapTile mapTile)
        {
            if (mapTile.Type == 0)
            {
                return false;
            }
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.mana = 0;
                gauntletMode++;
                if (gauntletMode >= 6)
                {
                    gauntletMode = 0;
                }
                SoundEngine.PlaySound(SoundID.MenuTick, player.position);
                string text = "";
                switch (gauntletMode)
                {
                    case 0:
                        text = ModContent.GetInstance<EALocalization>().InfinityGauntlet;
                        Item.useTime = 8;
                        Item.useAnimation = 16;
                        break;
                    case 1:
                        text = ModContent.GetInstance<EALocalization>().InfinityGauntlet1;
                        Item.useTime = 8;
                        Item.useAnimation = 16;
                        break;
                    case 2:
                        text = ModContent.GetInstance<EALocalization>().InfinityGauntlet2;
                        Item.useTime = 20;
                        Item.useAnimation = 20;
                        break;
                    case 3:
                        text = ModContent.GetInstance<EALocalization>().InfinityGauntlet3;
                        Item.useTime = 8;
                        Item.useAnimation = 16;
                        break;
                    case 4:
                        text = ModContent.GetInstance<EALocalization>().InfinityGauntlet4;
                        Item.useTime = 2;
                        Item.useAnimation = 16;
                        break;
                    case 5:
                        text = ModContent.GetInstance<EALocalization>().InfinityGauntlet5;
                        Item.useTime = 8;
                        Item.useAnimation = 16;
                        break;
                    default:
                        return base.CanUseItem(player);
                }
                Main.NewText(text, Color.White.R, Color.White.G, Color.White.B);
            }
            else
            {
                Item.mana = 18;
                if (gauntletMode == 4)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<InfinityBubbleCooldown>()) == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (gauntletMode == 5)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<InfinityVoidCooldown>()) == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return base.CanUseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string baseTooltip = ModContent.GetInstance<EALocalization>().InfinityGauntlet6;

            string text = "";
            switch (gauntletMode)
            {
                case 0:
                    text = $"\n{ModContent.GetInstance<EALocalization>().InfinityGauntlet7}";
                    break;
                case 1:
                    text = $"\n{ModContent.GetInstance<EALocalization>().InfinityGauntlet8}";
                    break;
                case 2:
                    text = $"\n{ModContent.GetInstance<EALocalization>().InfinityGauntlet9}";
                    break;
                case 3:
                    text = $"\n{ModContent.GetInstance<EALocalization>().InfinityGauntlet10}";
                    break;
                case 4:
                    text = $"\n{ModContent.GetInstance<EALocalization>().InfinityGauntlet11}";
                    break;
                case 5:
                    text = $"\n{ModContent.GetInstance<EALocalization>().InfinityGauntlet12}";
                    break;
            }
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.Text = baseTooltip + text;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                if (gauntletMode == 0)
                {
                    int numberProjectiles = 6 + Main.rand.Next(2);
                    for (int index = 0; index < numberProjectiles; ++index)
                    {
                        Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                        vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                        vector2_1.Y -= (float)(100 * index);
                        float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
                        float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                        if ((double)num13 < 0.0) num13 *= -1f;
                        if ((double)num13 < 20.0) num13 = 20f;
                        float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                        float num15 = Item.shootSpeed / num14;
                        float num16 = num12 * num15;
                        float num17 = num13 * num15;
                        float SpeedX = num16 + (float)Main.rand.Next(-80, 81) * 0.02f;  //this defines the projectile X position speed and randomnes
                        float SpeedY = num17 + (float)Main.rand.Next(-80, 81) * 0.02f;  //this defines the projectile Y position speed and randomnes
                        Projectile.NewProjectile(source, vector2_1.X, vector2_1.Y, SpeedX, SpeedY, ModContent.ProjectileType<DesertCrystal>(), damage / 2, knockback, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
                    }
                }
                else if (gauntletMode == 1)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(10));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<InfinityFireball>(), damage, knockback, player.whoAmI);
                }
                else if (gauntletMode == 2)
                {
                    Main.mapFullscreen = true;
                    Main.playerInventory = false;
                }
                else if (gauntletMode == 3)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<Buffs.FrostShield>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<Buffs.FrostShield>(), 1200);
                    }
                }
                else if (gauntletMode == 4)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<InfinityBubbleCooldown>()) == -1)
                    {
                        for (int l = 0; l < 20; l++)
                        {
                            for (int k = 0; k < Main.maxProjectiles; k++)
                            {
                                Projectile other = Main.projectile[k];
                                if (other.active && other.type != ModContent.ProjectileType<InfinityBubble>() && other.hostile && !other.friendly)
                                {
                                    other.Kill();
                                    for (int i = 0; i < Main.rand.Next(4); k++)
                                    {
                                        Projectile.NewProjectile(source, other.position.X, other.position.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ModContent.ProjectileType<InfinityBubble>(), 0, 0, player.whoAmI);
                                    }
                                }
                            }
                        }
                        if (!ModContent.GetInstance<Config>().debugMode)
                        {
                            player.AddBuff(ModContent.BuffType<InfinityBubbleCooldown>(), 1200);
                        }
                        else
                        {
                            player.AddBuff(ModContent.BuffType<InfinityBubbleCooldown>(), 30);
                        }
                    }
                }
                else if (gauntletMode == 5)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<InfinityVoidCooldown>()) == -1)
                    {
                        bool kill = false;
                        bool immune = false;
                        for (int k = 0; k < Main.npc.Length; k++)
                        {
                            NPC other = Main.npc[k];
                            if (other.active && !other.friendly && !other.boss && other.damage > 0 && other.lifeMax < 10000)
                            {
                                foreach (int p in ElementsAwoken.instakillImmune)
                                {
                                    if (other.type == p)
                                    {
                                        immune = true;
                                    }
                                }
                                if (!immune)
                                {
                                    kill = !kill;
                                    if (kill)
                                    {
                                        if (Main.netMode == 0)
                                        {
                                            other.active = false;
                                        }
                                        else
                                        {
                                            player.ApplyDamageToNPC(other, other.lifeMax, knockback: 0f, direction: 0, crit: true);
                                        }
                                        for (int d = 0; d < 100; d++)
                                        {
                                            int dust = Dust.NewDust(other.position, other.width, other.height, 219);
                                            Main.dust[dust].noGravity = true;
                                            Main.dust[dust].scale = 1f;
                                            Main.dust[dust].velocity *= 2f;
                                        }
                                    }
                                }
                            }
                        }
                        if (!ModContent.GetInstance<Config>().debugMode)
                        {
                            player.AddBuff(ModContent.BuffType<InfinityVoidCooldown>(), 2700);//2700
                        }
                        else
                        {
                            player.AddBuff(ModContent.BuffType<InfinityVoidCooldown>(), 30);
                        }
                    }
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EmptyGauntlet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AridStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<PyroStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MoonStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FrigidStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AquaticStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DeathStone>(), 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
