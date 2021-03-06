// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.ServiceModel.Description
{
    using Microsoft.Xml;
    using System.ServiceModel.Channels;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public abstract class PolicyConversionContext
    {
        private readonly ContractDescription _contract;

        protected PolicyConversionContext(ServiceEndpoint endpoint)
        {
            if (endpoint == null)
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
            _contract = endpoint.Contract;
        }

        public abstract BindingElementCollection BindingElements { get; }

        internal virtual BindingParameterCollection BindingParameters
        {
            get
            {
                return null;
            }
        }

        public ContractDescription Contract { get { return _contract; } }

        public abstract PolicyAssertionCollection GetBindingAssertions();
        public abstract PolicyAssertionCollection GetOperationBindingAssertions(OperationDescription operation);
        public abstract PolicyAssertionCollection GetMessageBindingAssertions(MessageDescription message);
        public abstract PolicyAssertionCollection GetFaultBindingAssertions(FaultDescription fault);

        internal static XmlElement FindAssertion(ICollection<XmlElement> assertions, string localName, string namespaceUri, bool remove)
        {
            XmlElement result = null;
            foreach (XmlElement assertion in assertions)
            {
                if ((assertion.LocalName == localName) &&
                    ((namespaceUri == null) || (assertion.NamespaceURI == namespaceUri)))
                {
                    result = assertion;
                    if (remove)
                        assertions.Remove(result);
                    break;
                }
            }
            return result;
        }
    }
}
