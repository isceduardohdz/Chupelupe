using System;
using System.Collections.Generic;
using Chupelupe.Helpers;
using Chupelupe.UnitTest.Helpers;

namespace Chupelupe.UnitTest.Helpers
{
    public class DependencyServicesStub : IDependencyService
    {
        readonly Dictionary<Type, object> registreredServices =
            new Dictionary<Type, object>();

        public void Registrer<T>(object impl)
        {
            if (registreredServices == null)
            {
                return;
            }
            registreredServices[typeof(T)] = impl;
            
        }

        public T Get<T>() where T : class
        {
            var service = (T)registreredServices[typeof(T)];
            return service;
        }
    }
}
