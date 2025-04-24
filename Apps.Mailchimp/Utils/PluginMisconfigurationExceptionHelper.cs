using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.Mailchimp.Utils;

public static class PluginMisconfigurationExceptionHelper
{
    public static void ThrowIfNullOrEmpty(string value, string parameterName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new PluginMisconfigurationException($"The '{parameterName}' parameter is null or empty. Please provide a valid value.");
        }
    }
}