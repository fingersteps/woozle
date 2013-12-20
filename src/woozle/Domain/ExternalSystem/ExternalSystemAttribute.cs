using System;
using System.ComponentModel.Composition;

namespace Prosa.ExternalSystem
{
    /// <summary>
    /// Attribute for tagging an external system with metadata.
    /// <remarks>
    /// With this attribute an external system could be named and versioned.
    /// </remarks>
    /// </summary>
    [MetadataAttribute]
    public class ExternalSystemAttribute : Attribute
    {
        /// <summary>
        /// The name of the external system
        /// </summary>
        public string Name { get; set; }
    }
}
