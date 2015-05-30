namespace AllTheSame.WebAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// </summary>
    public class KeyValuePairModelDescription : ModelDescription
    {
        /// <summary>
        ///     Gets or sets the key model description.
        /// </summary>
        /// <value>
        ///     The key model description.
        /// </value>
        public ModelDescription KeyModelDescription { get; set; }

        /// <summary>
        ///     Gets or sets the value model description.
        /// </summary>
        /// <value>
        ///     The value model description.
        /// </value>
        public ModelDescription ValueModelDescription { get; set; }
    }
}