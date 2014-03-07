using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Linq;

namespace KaskKiosk.Invoker
{
    public enum KaskServiceType
    {
        APPLICATION = 0,
        APPLICANT = 1,
        APPLIED = 2,
        UNKNOWN = 0xff
    };

    public static class ServiceInvoker<T>
    {
        private static readonly ClientSection m_ClientSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
        private static readonly BindingsSection m_BindingsSection = ConfigurationManager.GetSection("system.serviceModel/bindings") as BindingsSection;
        private static readonly Dictionary<string, T> m_Services = new Dictionary<string, T>();

        public static T Create ()
        {
            foreach(ChannelEndpointElement endpoint in m_ClientSection.Endpoints)
            {
                string temp = endpoint.Contract;
                if (temp.IndexOf(typeof(T).Name) > -1)
                {
                    var b = GetFromConfig(endpoint.BindingConfiguration);
                    m_Services[typeof(T).FullName] = (T)ChannelFactory<T>.CreateChannel(b, new EndpointAddress(endpoint.Address));
                }
            }

            return m_Services[typeof(T).FullName];
        }

        private static Binding GetFromConfig(string configurationName)
        {
            var bingingsSection = BindingsSection.GetSection(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
            var bindingType = (from b in bingingsSection.BindingCollections
                               where b.ConfiguredBindings.Count > 0 && b.ContainsKey(configurationName)
                               select b.BindingType).FirstOrDefault();
            var binding = bindingType != null ? Activator.CreateInstance(bindingType, configurationName) : null;
            return (Binding)binding;
        }
    }
}