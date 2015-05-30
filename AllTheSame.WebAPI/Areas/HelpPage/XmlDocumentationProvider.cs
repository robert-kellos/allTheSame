using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Xml.XPath;
using AllTheSame.WebAPI.Areas.HelpPage.ModelDescriptions;

namespace AllTheSame.WebAPI.Areas.HelpPage
{
    /// <summary>
    ///     A custom <see cref="IDocumentationProvider" /> that reads the API documentation from an XML documentation file.
    /// </summary>
    public class XmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        /// <summary>
        ///     The type expression
        /// </summary>
        private const string TypeExpression = "/doc/members/member[@name='T:{0}']";

        /// <summary>
        ///     The method expression
        /// </summary>
        private const string MethodExpression = "/doc/members/member[@name='M:{0}']";

        /// <summary>
        ///     The property expression
        /// </summary>
        private const string PropertyExpression = "/doc/members/member[@name='P:{0}']";

        /// <summary>
        ///     The field expression
        /// </summary>
        private const string FieldExpression = "/doc/members/member[@name='F:{0}']";

        /// <summary>
        ///     The parameter expression
        /// </summary>
        private const string ParameterExpression = "param[@name='{0}']";

        /// <summary>
        ///     The _document navigator
        /// </summary>
        private readonly XPathNavigator _documentNavigator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlDocumentationProvider" /> class.
        /// </summary>
        /// <param name="documentPath">The physical path to XML document.</param>
        /// <exception cref="System.ArgumentNullException">documentPath</exception>
        public XmlDocumentationProvider(string documentPath)
        {
            if (documentPath == null)
            {
                throw new ArgumentNullException("documentPath");
            }
            var xpath = new XPathDocument(documentPath);
            _documentNavigator = xpath.CreateNavigator();
        }

        /// <summary>
        ///     Gets the documentation.
        /// </summary>
        /// <param name="controllerDescriptor">The controller descriptor.</param>
        /// <returns></returns>
        public string GetDocumentation(HttpControllerDescriptor controllerDescriptor)
        {
            var typeNode = GetTypeNode(controllerDescriptor.ControllerType);
            return GetTagValue(typeNode, "summary");
        }

        /// <summary>
        ///     Gets the documentation based on <see cref="T:System.Web.Http.Controllers.HttpActionDescriptor" />.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>
        ///     The documentation for the controller.
        /// </returns>
        public virtual string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var methodNode = GetMethodNode(actionDescriptor);
            return GetTagValue(methodNode, "summary");
        }

        /// <summary>
        ///     Gets the documentation based on <see cref="T:System.Web.Http.Controllers.HttpParameterDescriptor" />.
        /// </summary>
        /// <param name="parameterDescriptor">The parameter descriptor.</param>
        /// <returns>
        ///     The documentation for the controller.
        /// </returns>
        public virtual string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var reflectedParameterDescriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;
            if (reflectedParameterDescriptor != null)
            {
                var methodNode = GetMethodNode(reflectedParameterDescriptor.ActionDescriptor);
                if (methodNode != null)
                {
                    var parameterName = reflectedParameterDescriptor.ParameterInfo.Name;
                    var parameterNode =
                        methodNode.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, ParameterExpression,
                            parameterName));
                    if (parameterNode != null)
                    {
                        return parameterNode.Value.Trim();
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Gets the response documentation.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns></returns>
        public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var methodNode = GetMethodNode(actionDescriptor);
            return GetTagValue(methodNode, "returns");
        }

        /// <summary>
        ///     Gets the documentation.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        public string GetDocumentation(MemberInfo member)
        {
            var memberName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(member.DeclaringType),
                member.Name);
            var expression = member.MemberType == MemberTypes.Field ? FieldExpression : PropertyExpression;
            var selectExpression = string.Format(CultureInfo.InvariantCulture, expression, memberName);
            var propertyNode = _documentNavigator.SelectSingleNode(selectExpression);
            return GetTagValue(propertyNode, "summary");
        }

        /// <summary>
        ///     Gets the documentation.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public string GetDocumentation(Type type)
        {
            var typeNode = GetTypeNode(type);
            return GetTagValue(typeNode, "summary");
        }

        /// <summary>
        ///     Gets the method node.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns></returns>
        private XPathNavigator GetMethodNode(HttpActionDescriptor actionDescriptor)
        {
            var reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
            if (reflectedActionDescriptor != null)
            {
                var selectExpression = string.Format(CultureInfo.InvariantCulture, MethodExpression,
                    GetMemberName(reflectedActionDescriptor.MethodInfo));
                return _documentNavigator.SelectSingleNode(selectExpression);
            }

            return null;
        }

        /// <summary>
        ///     Gets the name of the member.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        private static string GetMemberName(MethodInfo method)
        {
            var name = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(method.DeclaringType),
                method.Name);
            var parameters = method.GetParameters();
            if (parameters.Length != 0)
            {
                var parameterTypeNames = parameters.Select(param => GetTypeName(param.ParameterType)).ToArray();
                name += string.Format(CultureInfo.InvariantCulture, "({0})", string.Join(",", parameterTypeNames));
            }

            return name;
        }

        /// <summary>
        ///     Gets the tag value.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns></returns>
        private static string GetTagValue(XPathNavigator parentNode, string tagName)
        {
            if (parentNode != null)
            {
                var node = parentNode.SelectSingleNode(tagName);
                if (node != null)
                {
                    return node.Value.Trim();
                }
            }

            return null;
        }

        /// <summary>
        ///     Gets the type node.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private XPathNavigator GetTypeNode(Type type)
        {
            var controllerTypeName = GetTypeName(type);
            var selectExpression = string.Format(CultureInfo.InvariantCulture, TypeExpression, controllerTypeName);
            return _documentNavigator.SelectSingleNode(selectExpression);
        }

        /// <summary>
        ///     Gets the name of the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static string GetTypeName(Type type)
        {
            var name = type.FullName;
            if (type.IsGenericType)
            {
                // Format the generic type name to something like: Generic{System.Int32,System.String}
                var genericType = type.GetGenericTypeDefinition();
                var genericArguments = type.GetGenericArguments();
                var genericTypeName = genericType.FullName;

                // Trim the generic parameter counts from the name
                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                var argumentTypeNames = genericArguments.Select(t => GetTypeName(t)).ToArray();
                name = string.Format(CultureInfo.InvariantCulture, "{0}{{{1}}}", genericTypeName,
                    string.Join(",", argumentTypeNames));
            }
            if (type.IsNested)
            {
                // Changing the nested type name from OuterType+InnerType to OuterType.InnerType to match the XML documentation syntax.
                name = name.Replace("+", ".");
            }

            return name;
        }
    }
}