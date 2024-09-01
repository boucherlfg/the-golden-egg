using System;
using System.Collections.Generic;
using System.Linq;

public class ServiceManager : Singleton<ServiceManager> {
    private List<object> _services = new();

    public T Get<T>(Func<T> generator = null) where T : class
    {
        generator ??= DefaultConstructor<T>;

        var service = _services.FirstOrDefault(x => x is T);
        if(service != null) return service as T;

        service = generator();
        _services.Add(service);
        return service as T;
    }

    private static T DefaultConstructor<T>() where T : class => typeof(T).GetConstructor(new Type[]{}).Invoke(new object[]{}) as T;
}