using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.EAClouds
{
    public abstract class EACloud : ModCloud
    {
        bool IsRareCloud = true;
        public override bool RareCloud => IsRareCloud;
        public override float SpawnChance()
        {
            if (!Main.gameMenu && MyWorld.firePrompt > ElementsAwoken.bossPromptDelay) { IsRareCloud = false; return 15f; }
            else { return -1f; }
        }
        public override void OnSpawn(Cloud cloud)
        {
            cloud.spriteDir = SpriteEffects.None;
        }
        public override bool Draw(SpriteBatch spriteBatch, Cloud cloud, int cloudIndex, ref DrawData drawData)
        {
            if (!Main.gameMenu && MyWorld.firePrompt > ElementsAwoken.bossPromptDelay)
            {
                var drawDataCopy = drawData;
                drawDataCopy.color *= 0.5f;
                drawDataCopy.position += Utils.NextVector2Circular(Main.rand, 5, 5);
                drawDataCopy.Draw(spriteBatch);
            }
            return true;
        }
    }
    public class EACloud0 : EACloud { }
    public class EACloud1 : EACloud { }
    public class EACloud2 : EACloud { }
    public class EACloud3 : EACloud { }
    public class EACloud4 : EACloud { }
    public class EACloud5 : EACloud { }
    public class EACloud6 : EACloud { }
    public class EACloud7 : EACloud { }
    public class EACloud8 : EACloud { }
    public class EACloud9 : EACloud { }
    public class EACloud10 : EACloud { }
    public class EACloud11 : EACloud { }
    public class EACloud12 : EACloud { }
    public class EACloud13 : EACloud { }
    public class EACloud14 : EACloud { }
    public class EACloud15 : EACloud { }
    public class EACloud16 : EACloud { }
    public class EACloud17 : EACloud { }
    public class EACloud18 : EACloud { }
    public class EACloud19 : EACloud { }
    public class EACloud20 : EACloud { }
    public class EACloud21 : EACloud { }
}