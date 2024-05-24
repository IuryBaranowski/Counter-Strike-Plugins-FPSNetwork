namespace Util;

using Microsoft.Extensions.Configuration;

public static class Util
{
    private static readonly IConfigurationRoot configuration;

    static Util()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        configuration = builder.Build();
    }

    /// <summary>
    /// Get the banner's image.
    /// </summary>
    /// <returns></returns>
    public static string GetBannerImage()
    {
        string image = configuration["banner:image"];
        return image ?? string.Empty;
    }
}