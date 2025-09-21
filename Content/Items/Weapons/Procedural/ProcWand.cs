using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Held.Procedural;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.Content.Items.Weapons.Procedural
{
    public class ProcWand : ModItem
    {
        public override string Texture { get { return "ElementsAwoken/Content/Items/Weapons/Procedural/Fire1"; } }

        public int variant = 0;
        public string element = "Fire";

        public int spell = 0; 
        protected override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(variant);
        }
        public override void NetReceive(BinaryReader reader)
        {
            variant = reader.ReadInt32();
        }
        public override void SaveData(TagCompound tag)
        {
            tag ["variant"] = variant;
        }
        public override void LoadData(TagCompound tag)
        {
            variant = tag.GetInt("variant");
        }
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 28;
            Item.mana = 9;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item42;
            Item.shoot = ModContent.ProjectileType<CursedBall>();
            Item.shootSpeed = 13f;
        }
        public override bool OnPickup(Player player)
        {
            if (ModContent.GetInstance<Config>().debugMode) variant = Main.rand.Next(2);
            return base.OnPickup(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            string name = element;

            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name.Contains("ItemName"))
                {
                    line2.Text = name + " Wand";
                    if (ModContent.GetInstance<Config>().debugMode) line2.Text += " " + variant;
                }
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int input = 0;
            if (element == "Desert") input = 1;
            if (element == "Fire") input = 2;
            if (element == "Sky") input = 3;
            if (element == "Frost") input = 4;
            if (element == "Water") input = 5;
            if (element == "Void") input = 6;
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<WandHeld>(), 0, 0, player.whoAmI, input, variant);
            return false;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Items/Weapons/Procedural/" + element + variant).Value;
            Texture2D startTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Items/Weapons/Procedural/Fire1").Value;
            float scaleExtra = (float)startTex.Width / (float)tex.Width;
            scaleExtra *= 0.99f;
            spriteBatch.Draw(tex, position, frame, drawColor, 0f, origin, scale * scaleExtra, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Items/Weapons/Procedural/" + element + variant).Value;
            spriteBatch.Draw(tex, Item.position, null, lightColor, 0f, Vector2.Zero, scale , SpriteEffects.None, 0f);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return false;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            return false;
        }
    }
}