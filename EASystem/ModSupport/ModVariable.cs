using ElementsAwoken.EASystem.UI;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.ModSupport;

/// <summary>
/// Mod variables, such as paths to assets in the mod
/// </summary>
public class ModVariable : ModSystem
{
    public static string Heart2 = "ElementsAwoken/Extra/HPHeart2Heart";
    public static string Heart3 = "ElementsAwoken/Extra/Heart3HeartHeart";
    public static string Heart4 = "ElementsAwoken/Extra/Heart4";

    public static string CHeart = "ElementsAwoken/Extra/HPHeart3AltHeart";

    public static string Mana2 = "ElementsAwoken/Extra/Mana2";

    /// <summary>
    /// If you want to add something, look there
    /// <para/> Changes will take effect only after the mod is compiled
    /// </summary>
    public static string HeartAndManaToolTips => "EALocalization.Resurs";

    /// <summary>
    /// Disables all EA Heart and Mana assets for the specified player.
    /// </summary>
    /// <remarks>This method resets all related visual and gameplay effects associated with EA Heart and Mana
    /// assets  for the specified player. It ensures that the player's state is cleared of any modifications  introduced
    /// by these assets.</remarks>
    /// Use this method if yo want to add your own EA Heart and Mana assets
    /// <param name="player">The player whose EA Heart and Mana assets will be disabled.</param>
    public static bool DisableEAHeartAndManaAssets(Player player)
    {
        HeartsPlayers modPlayer = player.GetModPlayer<HeartsPlayers>();
        modPlayer.emptyVesselHeartLife = 0;
        modPlayer.EmptyVesselVisual = false;
        modPlayer.chaosHeartLife = 0;
        modPlayer.ChaosHeartVisual = false;
        modPlayer.CompressorVisual = false;
        modPlayer.HPTier_0 = false;
        modPlayer.ManaBonus = false;
        return true;
    }
    /// <summary>
    /// Enables one or more specific visual or functional assets related to hearts and mana for the specified player, based on the given priorities.
    /// </summary>
    /// <remarks>
    /// This method first resets all EA Heart and Mana assets by calling <c>DisableEAHeartAndManaAssets</c>, then enables the requested ones. If any invalid priority is provided, it is ignored.
    /// </remarks>
    /// <param name="player"> The player for whom the assets are being enabled. </param>
    /// <param name="priorities"> An array of integers representing the priorities of the assets to enable.Valid values are:
    /// <list type="bullet">
    /// <item><description>0: Enables Tier 0 heart visuals.</description></item>
    /// <item><description>1: Enables Empty Vessel visuals.</description></item>
    /// <item><description>2: Enables Chaos Heart visuals.</description></item>
    /// <item><description>3: Enables Compressor visuals.</description></item>
    /// <item><description>4: Enables Mana Bonus functionality.</description></item>
    /// <item><description>5: Sets Chaos Heart life to 1.</description></item>
    /// <item><description>6: Sets Empty Vessel heart life to 1.</description></item>
    /// </list>
    /// Any other values are ignored.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if at least one valid asset was enabled; otherwise, 
    /// <see langword="false"/>.
    /// </returns>
    public static bool EnabledEAHeartAndManaAssets(Player player, params int[] priorities)
    {
        HeartsPlayers modPlayer = player.GetModPlayer<HeartsPlayers>();
        DisableEAHeartAndManaAssets(player);

        bool enabledAny = false;

        foreach (int priority in priorities)
        {
            switch (priority)
            {
                case 0: modPlayer.HPTier_0 = true; enabledAny = true; break;
                case 1: modPlayer.EmptyVesselVisual = true; enabledAny = true; break;
                case 2: modPlayer.ChaosHeartVisual = true; enabledAny = true; break;
                case 3: modPlayer.CompressorVisual = true; enabledAny = true; break;
                case 4: modPlayer.ManaBonus = true; enabledAny = true; break;
                case 5: modPlayer.chaosHeartLife = 1; enabledAny = true; break;
                case 6: modPlayer.emptyVesselHeartLife = 1; enabledAny = true; break;
            }
        }
        // EnabledEAHeartAndManaAssets(player, 2, 5, 6); // ChaosHeartVisual + chaosHeartLife + emptyVesselHeartLife
        return enabledAny;
    }
}