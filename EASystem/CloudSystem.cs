using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class CloudSystem : ModSystem
    {
        private static Asset<Texture2D>[] vanillaClouds;

        //public override void Load()
        //{
        //    vanillaClouds = new Asset<Texture2D>[22];
        //    for (int i = 0; i < 22; i++)
        //    {
        //        vanillaClouds[i] = TextureAssets.Cloud[i];
        //    }
        //}
        //public override void Unload()
        //{
        //    vanillaClouds = null;
        //}
        //public static void SetCustomClouds()
        //{
        //    for (int i = 0; i < 22; i++)
        //    {
        //        TextureAssets.Cloud[i] = ModContent.Request<Texture2D>(
        //            $"Terraria/Images/Cloud_{i}"
        //        );
        //    }
        //}
        //public static void ResetCloudTexture()
        //{
        //    for (int i = 0; i < 22; i++)
        //    {
        //        TextureAssets.Cloud[i] = vanillaClouds[i];
        //    }
        //}
    }
}