using KingsStoreApi.Services.Interfaces;
using System;

namespace KingsStoreApi.Services.Implementations
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public T GetServices<T>() where T : class
        {
            var newService = _serviceProvider.GetService(typeof(T));

            return (T)newService;
        }
    }
}
