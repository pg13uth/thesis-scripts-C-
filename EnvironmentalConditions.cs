namespace Apex.Examples.Misc
{
    using System;
    using Apex.Common;

    /// <summary>
    /// Modified from Apex Path Examples:
    /// An example of creating the custom attributes to be associated with <see cref="Apex.Common.AttributedComponent"/>s and other attribute using components.
    /// </summary>
    [Flags, EntityAttributesEnum]
    public enum EnvironmentalCondition
    {
        /// <summary>
        /// Mild Temperature Attribute; Low Cost Multiplier
        /// </summary>
        MildTemp = 1,

        /// <summary>
        /// Hot/Humid Temperature Attribute; High Cost Multiplier
        /// </summary>
        HotHumidTemp = 1000,
        
    }
}
