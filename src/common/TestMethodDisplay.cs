#if XUNIT_FRAMEWORK
namespace Xunit.Sdk
#else
namespace Xunit
#endif
{
    using System;

    /// <summary>
    /// Indicates the default display name format for test methods.
    /// </summary>
    [Flags]
    public enum TestMethodDisplay
    {
        /// <summary>
        /// Use a fully qualified name (namespace + class + method)
        /// </summary>
        ClassAndMethod = 0x01,

        /// <summary>
        /// Use just the method name (without class)
        /// </summary>
        Method = 0x02,

        /// <summary>
        /// Replace underscores in display names with a space.
        /// </summary>
        ReplaceUnderscoreWithSpace = 0x04,

        /// <summary>
        /// Replace well-known monikers with their equivalent operator.
        /// </summary>
        /// <list type="bullet">
        /// <item><description>lt : &lt;</description></item>
        /// <item><description>le : &lt;=</description></item>
        /// <item><description>eq : =</description></item>
        /// <item><description>ne : !=</description></item>
        /// <item><description>gt : &gt;</description></item>
        /// <item><description>ge : &gt;=</description></item>
        /// </list>
        AllowOperatorMonikers = 0x08,

        /// <summary>
        /// Replace supported escape sequences with their equivalent character.
        /// </summary>
        AllowEscapeSequences = 0x10,

        /// <summary>
        /// Use all formatting extensions.
        /// </summary>
        AllFormatExtensions = ReplaceUnderscoreWithSpace | AllowOperatorMonikers | AllowEscapeSequences
    }
}
