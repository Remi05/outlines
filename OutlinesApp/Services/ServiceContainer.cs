using System;
using System.Collections.Generic;

namespace OutlinesApp.Services
{
    public class ServiceContainer
    {
        private static ServiceContainer instance;
        public static ServiceContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceContainer();
                }
                return instance;
            }
        }

        private Dictionary<Type, object> Services { get; set; } = new Dictionary<Type, object>();

        private ServiceContainer() { }

        public void AddService(Type serviceType, object serviceInstance)
        {
            Services[serviceType] = serviceInstance;
        }

        public T GetService<T>() where T : class
        {
            object service;
            Services.TryGetValue(typeof(T), out service);
            return service as T;
        }
    }
}
