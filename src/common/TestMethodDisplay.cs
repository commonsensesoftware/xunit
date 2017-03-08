using System;

#if XUNIT_FRAMEWORK
namespace Xunit.Sdk
#else
namespace Xunit
#endif
{
    /// <summary>
    /// Indicates the default display name format for test methods.
    /// </summary>
    [Flags]
    public enum TestMethodDisplay
    {
        /// <summary>
        /// Use a fully qualified name (namespace + class + method)
        /// </summary>
        ClassAndMethod = 1,

        /// <summary>
        /// Use just the method name (without class)
        /// </summary>
        Method = 2,

        /// <summary>
        /// Replace underscores in display names with a space.
        /// </summary>
        ReplaceUnderscoreWithSpace = 4,

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
        UseOperatorMonikers = 8,

        /// <summary>
        /// Replace supported escape sequences with their equivalent character.
        /// <list type="table">
        /// <listheader>
        ///  <term>Encoding</term>
        ///  <description>Format</description>
        /// </listheader>
        /// <item><term>ASCII</term><description>X hex-digit hex-digit (ex: X2C)</description></item>
        /// <item><term>Unicode</term><description>U hex-digit hex-digit hex-digit hex-digit (ex: U00A9)</description></item>
        /// </list>
        /// </summary>
        UseEscapeSequences = 16,

        /// <summary>
        /// Replaces the period delimiter used in namespace and type references with a comma.
        /// </summary>
        ReplacePeriodWithComma = 32
    }
}
