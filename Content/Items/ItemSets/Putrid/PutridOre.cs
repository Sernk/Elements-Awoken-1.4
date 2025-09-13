using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    public class PutridOre : ModItem
    {
        public int soundCD = 0;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = 7;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            soundCD--;
            if (Item.lavaWet)
            {
                Item conPyroplasm = null;
                for (int k = 0; k < Main.maxItems; k++)
                {
                    Item other = Main.item[k];
                    if (Vector2.Distance(Item.Center, other.Center) <= 50 && other.active)
                    {
                        if (other.type == ModContent.ItemType<ConcentratedPyroplasm>())
                        {
                            conPyroplasm = other;
                        }
                    }
                }
                if (conPyroplasm != null && conPyroplasm.active)
                {
                    if (conPyroplasm.stack > 0 && Item.stack > 0)
                    {
                        if (conPyroplasm.stack > 1) conPyroplasm.stack--;
                        else conPyroplasm.active = false;
                        if (Item.stack > 1) Item.stack--;
                        else Item.active = false;
                        Projectile.NewProjectile(Item.GetSource_FromThis(), Item.Center.X, Item.Center.Y, Main.rand.NextFloat(-2f, 2f), -4f, ModContent.ProjectileType<BlightfireSpawner>(), 0, 0, Main.myPlayer, 0f, 0f);
                        if (soundCD <= 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item103, Item.Center);
                            soundCD = 20;
                        }
                    }
                }
            }
        }
    }
}
