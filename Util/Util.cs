namespace Util;

using Microsoft.Extensions.Configuration;

public static class Util
{
    private static IConfigurationBuilder? build = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    /// <summary>
    /// Get the banner's image.
    /// </summary>
    /// <returns></returns>
    public static string GetBannerImage()
    {
        IConfiguration configuration = build.Build();

        string imagem = configuration["banner:image"];

        return imagem;
    }
}
