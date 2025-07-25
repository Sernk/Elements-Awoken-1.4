using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class PlayerUtils : ModPlayer
    {
        public int enemiesKilledLastMin = 0;
        public int enemiesKilledLast10Secs = 0;
    }
}
