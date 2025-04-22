using ApplicationModes;
using DocumentationGenerator.Modes.Build;
using DocumentationGenerator.Modes.Host;
using DocumentationGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationGenerator.Modes;

public static class ModeRegistry
{
    private static readonly Dictionary<ApplicationMode, IApplicationMode> _modes = new()
    {
        { ApplicationMode.Host, new HostMode() },
        { ApplicationMode.Build, new BuildMode() }
    };

    public static IApplicationMode GetMode(ApplicationMode mode)
    {
        if (!_modes.TryGetValue(mode, out var handler))
            throw new ArgumentOutOfRangeException(nameof(mode));

        return handler;
    }
}

