using System.Collections.ObjectModel;

namespace AllTheSame.WebAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    ///     ComplexTypeModelDescription
    /// </summary>
    public class ComplexTypeModelDescription : ModelDescription
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ComplexTypeModelDescription" /> class.
        /// </summary>
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
        }

        /// <summary>
        ///     Gets the properties.
        /// </summary>
        /// <value>
        ///     The properties.
        /// </value>
        public Collection<ParameterDescription> Properties { get; private set; }
    }
}