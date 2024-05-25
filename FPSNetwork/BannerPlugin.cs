namespace BannerPlugin;

using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Cvars;


public class BannerPlugin : BasePlugin
{
    public FakeConVar<bool> bannerEnabled = new("banner_enable", "If banner is enabled or not. Default: true", true);

    public override string ModuleName => "Banner Plugin";

    public override string ModuleVersion => "1.0.0";

    public override string ModuleAuthor => $"Iury - https://github.com/IuryBaranowski";

    public override string ModuleDescription => "Banner on the screen";

    public override void Load(bool hotReload)
    {
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
            if (player != null && bannerEnabled.Value)
            {
                if (player?.LifeState == 0)
                    player?.PrintToCenterHtml("<img src='https://i.ibb.co/zsSgqB7/FPSNetwork3.jpg'</img>"); //Change the photo URL here.
            }
        }
    }

    [ConsoleCommand("css_b", "alias for !b")]
    [ConsoleCommand("css_banner", "alias for !banner")]
    [ConsoleCommand("sv_showbanner", "Shows a banner in players hud")]
    public void OnBanner(CCSPlayerController? player, CommandInfo command)
    {

        if (player != null && bannerEnabled.Value)
        {
            ShowBanner(player, "FPSNetwork Server!");
            return;
        }

        Console.WriteLine("Banner command called.");
    }

    private void OnTick()
    {
        if (!bannerEnabled.Value)
            return;

        ShowHtml();
    }

    private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
    {
        bannerEnabled.Value = true;
        return HookResult.Continue;
    }

    private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        bannerEnabled.Value = true;
        return HookResult.Continue;
    }

    public void RegisterListeners()
    {
        RegisterListener<Listeners.OnTick>(OnTick);

        RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        RegisterEventHandler<EventRoundStart>(OnRoundStart);

        return;
    }
}
