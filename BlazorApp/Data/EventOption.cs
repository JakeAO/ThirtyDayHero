using System;
using System.Collections.Generic;

using Action = System.Action;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public interface IEventOption
    {
        bool Disabled { get; }
        string Text { get; }
        string Tooltip { get; }
        uint Priority { get; }
    }

    public class SingleOption : IEventOption
    {
        public bool Disabled { get; set; } = false;
        public string Text { get; set; } = string.Empty;
        public string Tooltip { get; set; } = string.Empty;
        public uint Priority { get; set; } = 0;

        public Action Select { get; set; } = null;
    }

    public class MultipleOption : IEventOption
    {
        public bool Disabled { get; set;  } = false;
        public string Text { get; set; } = string.Empty;
        public string Tooltip { get; set; } = string.Empty;
        public uint Priority { get; set; } = 0;
        public string DefaultValue { get; set; } = string.Empty;

        public IReadOnlyList<(string text, string tooltip, string value)> AlternateOptions { get; set; } = null;
        public Action<string> Select { get; set; } = null;
    }
}