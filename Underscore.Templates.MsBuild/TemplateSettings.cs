using System.Runtime.InteropServices;

namespace Underscore.Templates.MsBuild
{
    /// <summary>
    /// Used to pass settings to _.templateSettings
    /// </summary>
    [ComVisible(true)]
    public sealed class TemplateSettings
    {
        public string interpolate { get; set; }
        public string evaluate { get; set; }
        public string escape { get; set; }
    }
}
