using System;

namespace AllTheSame.WebAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    ///     Use this attribute to change the name of the <see cref="ModelDescription" /> generated for a type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
    public sealed class ModelNameAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ModelNameAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ModelNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; private set; }
    }
}