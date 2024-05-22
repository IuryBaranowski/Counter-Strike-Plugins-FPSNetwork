namespace FPSNetwork;

using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API;
using Util;

public class Banner : BasePlugin
{
    public FakeConVar<bool> BannerEnabled = new("banner_enable", "Whether banner is enabled or not. Default: false", false);
    public override string ModuleName => "Banner Plugin";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "https://github.com/iurybaranowski";
    public override string ModuleDescription => "Plugin for showing banner on the screen";

    private string Image { get; set; }

    public override void Load(bool hotReload)
    {
        Console.WriteLine("Banner Plugin Loaded");
        ConfigurePlugin();
        RegisterListeners();
    }

    public void ShowBanner(CCSPlayerController player, string text)
    {
        player.PrintToCenterHtml(text);

        return;
    }

    public void ShowHtml()
    {
        foreach (var player in Utilities.GetPlayers())
        {
            if (player != null && BannerEnabled.Value)
            {
                if (player?.LifeState == 0)
                    player?.PrintToCenterHtml($"<img src='{Image}'</img>");
            }
        }
    }

    [ConsoleCommand("css_b", "alias for !b")]
    [ConsoleCommand("css_banner", "alias for !banner")]
    [ConsoleCommand("sv_showbanner", "Shows a banner in players hud")]
    public void OnBanner(CCSPlayerController? player, CommandInfo command)
    {

        if (player != null && BannerEnabled.Value)
        {
            ShowBanner(player, "Welcome to FPSNetwork Server");
            return;
        }

        Console.WriteLine("Banner command called.");
    }

    public void RegisterListeners()
    {
        RegisterListener<Listeners.OnTick>(OnTick);

        RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        RegisterEventHandler<EventRoundStart>(OnRoundStart);

        return;
    }

    #region Private Methods

    /// <summary>
    /// Anything you need to configure you can set here to load.
    /// </summary>
    private void ConfigurePlugin()
    {
        Image = Util.GetBannerImage();
    }

    private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
    {
        BannerEnabled.Value = true;
        return HookResult.Continue;
    }

    private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        BannerEnabled.Value = false;
        return HookResult.Continue;
    }

    private void OnTick()
    {
        if (!BannerEnabled.Value)
            return;

        ShowHtml();
    }

    #endregion
}