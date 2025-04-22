using ApplicationModes;
using DocumentationGenerator.Modes.Build;
using DocumentationGenerator.Modes.Host;
using DocumentationGenerator.Utilities;

namespace DocumentationGenerator.Modes;

/// <summary>
/// Registry for mode enums to mode objects.
/// </summary>
public static class ModeRegistry
{
    private static readonly Dictionary<ApplicationMode, IApplicationMode> _modes = new()
    {
        { ApplicationMode.Host, new HostMode() },
        { ApplicationMode.Build, new BuildMode() }
    };

    public static IApplicationMode GetMode(ApplicationMode type)
    {
        if (!_modes.TryGetValue(type, out var mode))
            throw new ArgumentOutOfRangeException(nameof(type));

        return mode;
    }
}

