using System;
using System.Collections.Generic;

namespace _02.Services
{
    public class ServiceContainer
    {
        public static readonly ServiceContainer Instance = new ServiceContainer();

        private readonly Dictionary<Type, object> _services;

        private ServiceContainer()
        {
            _services = new Dictionary<Type, object>();
        }

        public void Add<T>(T service) where T : class
        {
            _services[typeof(T)] = service;
        }

        public T? Get<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var value))
                return value as T;
            return null;
        }
    }
}