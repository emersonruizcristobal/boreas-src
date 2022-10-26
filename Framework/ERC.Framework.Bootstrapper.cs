using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Web;

namespace ERC.Framework.Bootstrapper {
    public class IoC {
        private static IDependencyResolver _resolver;
        public static void InitializeWith(IDependencyResolverFactory factory) {
            if (factory == null) {
                throw new System.ArgumentNullException("factory");
            }
            IoC._resolver = factory.CreateInstance();
        }
        public static void Register<T>(T instance) {
            if (instance == null) {
                throw new System.ArgumentNullException("instance");
            }
            IoC._resolver.Register<T>(instance);
        }
        public static void Inject<T>(T existing) {
            if (existing == null) {
                throw new System.ArgumentNullException("existing");
            }
            IoC._resolver.Inject<T>(existing);
        }
        public static T Resolve<T>(System.Type type) {
            if (type == null) {
                throw new System.ArgumentNullException("type");
            }
            return IoC._resolver.Resolve<T>(type);
        }
        public static T Resolve<T>(System.Type type, string name) {
            if (type == null) {
                throw new System.ArgumentNullException("type");
            }
            if (name == null) {
                throw new System.ArgumentNullException("name");
            }
            return IoC._resolver.Resolve<T>(type, name);
        }
        public static T Resolve<T>() {
            return IoC._resolver.Resolve<T>();
        }
        public static T Resolve<T>(string name) {
            if (string.IsNullOrEmpty(name)) {
                throw new System.ArgumentNullException("name");
            }
            return IoC._resolver.Resolve<T>(name);
        }
        public static System.Collections.Generic.IEnumerable<T> ResolveAll<T>() {
            return IoC._resolver.ResolveAll<T>();
        }
    }

    public class DependencyResolverFactory : IDependencyResolverFactory {
        private readonly System.Type _resolverType;
        public DependencyResolverFactory()
            : this(ConfigurationManager.AppSettings["UnityBootstrapperDependencyResolverTypeName"]) {
        }
        public DependencyResolverFactory(string resolverTypeName) {
            if (string.IsNullOrEmpty(resolverTypeName)) {
                throw new System.ArgumentNullException("resolverTypeName");
            }
            this._resolverType = System.Type.GetType(resolverTypeName, true, true);
        }
        public IDependencyResolver CreateInstance() {
            return System.Activator.CreateInstance(this._resolverType) as IDependencyResolver;
        }
    }

    public class UnityDependencyResolver : IDependencyResolver {
        private readonly IUnityContainer _container;
        public UnityDependencyResolver()
            : this(new UnityContainer()) {
        }
        public UnityDependencyResolver(IUnityContainer container) {
            if (container == null) {
                throw new System.ArgumentNullException("container");
            }
            this._container = container;
            this.ConfigureContainer(this._container);
        }
        protected virtual void ConfigureContainer(IUnityContainer container) {
            System.Type[] array = (
                from a in System.Reflection.Assembly.Load(ConfigurationManager.AppSettings["UnityBootstrapperAssembly"]).GetTypes()
                where a.Namespace == ConfigurationManager.AppSettings["UnityBootstrapperNamespace"]
                select a).ToArray<System.Type>();
            for (int i = 0; i < array.Length; i += 2) {
                if (!array[i].Name.StartsWith("<>")) {
                    System.Type type = array[i];
                    System.Type type2 = array[i + 1];
                    if (type.IsClass && type2.IsInterface) {
                        UnityContainerExtensions.RegisterType(container, type2, type, new InjectionMember[0]);
                        
                    }
                }
            }
        }
        public void Register<T>(T instance) {
            if (instance == null) {
                throw new System.ArgumentNullException("instance");
            }
            UnityContainerExtensions.RegisterInstance<T>(this._container, instance);
        }
        public void Inject<T>(T existing) {
            if (existing == null) {
                throw new System.ArgumentNullException("existing");
            }
            UnityContainerExtensions.BuildUp<T>(this._container, existing, new ResolverOverride[0]);
        }
        public T Resolve<T>(System.Type type) {
            if (type == null) {
                throw new System.ArgumentNullException("type");
            }
            return (T)((object)UnityContainerExtensions.Resolve(this._container, type, new ResolverOverride[0]));
        }
        public T Resolve<T>(System.Type type, string name) {
            if (type == null) {
                throw new System.ArgumentNullException("type");
            }
            if (name == null) {
                throw new System.ArgumentNullException("name");
            }
            return (T)((object)this._container.Resolve(type, name, new ResolverOverride[0]));
        }
        public T Resolve<T>() {
            return UnityContainerExtensions.Resolve<T>(this._container, new ResolverOverride[0]);
        }
        public T Resolve<T>(string name) {
            if (string.IsNullOrEmpty(name)) {
                throw new System.ArgumentNullException("name");
            }
            return UnityContainerExtensions.Resolve<T>(this._container, name, new ResolverOverride[0]);
        }
        public System.Collections.Generic.IEnumerable<T> ResolveAll<T>() {
            System.Collections.Generic.IEnumerable<T> enumerable = UnityContainerExtensions.ResolveAll<T>(this._container, new ResolverOverride[0]);
            T t = default(T);
            try {
                t = UnityContainerExtensions.Resolve<T>(this._container, new ResolverOverride[0]);
            } catch (ResolutionFailedException) {
            }
            if (object.Equals(t, default(T))) {
                return enumerable;
            }
            return new System.Collections.ObjectModel.ReadOnlyCollection<T>(new System.Collections.Generic.List<T>(enumerable)
			{
				t
			});
        }
    }

    public class UnityPerExecutionContextLifetimeManager : LifetimeManager {
        private class ContainerExtension : IExtension<OperationContext> {
            public object Value {
                get;
                set;
            }
            public void Attach(OperationContext owner) {
            }
            public void Detach(OperationContext owner) {
            }
        }
        private System.Guid _key;
        public UnityPerExecutionContextLifetimeManager()
            : this(System.Guid.NewGuid()) {
        }
        private UnityPerExecutionContextLifetimeManager(System.Guid key) {
            if (key == System.Guid.Empty) {
                throw new System.ArgumentException("Key cannot be empty");
            }
            this._key = key;
        }
        public override object GetValue() {
            object result = null;
            if (OperationContext.Current != null) {
                UnityPerExecutionContextLifetimeManager.ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<UnityPerExecutionContextLifetimeManager.ContainerExtension>();
                if (containerExtension != null) {
                    result = containerExtension.Value;
                }
            } else {
                if (HttpContext.Current != null) {
                    if (HttpContext.Current.Items[this._key.ToString()] != null) {
                        result = HttpContext.Current.Items[this._key.ToString()];
                    }
                } else {
                    if (System.AppDomain.CurrentDomain.IsFullyTrusted) {
                        result = System.Runtime.Remoting.Messaging.CallContext.GetData(this._key.ToString());
                    }
                }
            }
            return result;
        }
        public override void RemoveValue() {
            if (OperationContext.Current != null) {
                UnityPerExecutionContextLifetimeManager.ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<UnityPerExecutionContextLifetimeManager.ContainerExtension>();
                if (containerExtension != null) {
                    OperationContext.Current.Extensions.Remove(containerExtension);
                    return;
                }
            } else {
                if (HttpContext.Current != null) {
                    if (HttpContext.Current.Items[this._key.ToString()] != null) {
                        HttpContext.Current.Items[this._key.ToString()] = null;
                        return;
                    }
                } else {
                    System.Runtime.Remoting.Messaging.CallContext.FreeNamedDataSlot(this._key.ToString());
                }
            }
        }
        public override void SetValue(object newValue) {
            if (OperationContext.Current != null) {
                if (OperationContext.Current.Extensions.Find<UnityPerExecutionContextLifetimeManager.ContainerExtension>() == null) {
                    UnityPerExecutionContextLifetimeManager.ContainerExtension item = new UnityPerExecutionContextLifetimeManager.ContainerExtension {
                        Value = newValue
                    };
                    OperationContext.Current.Extensions.Add(item);
                    return;
                }
            } else {
                if (HttpContext.Current != null) {
                    if (HttpContext.Current.Items[this._key.ToString()] == null) {
                        HttpContext.Current.Items[this._key.ToString()] = newValue;
                        return;
                    }
                } else {
                    if (System.AppDomain.CurrentDomain.IsFullyTrusted) {
                        System.Runtime.Remoting.Messaging.CallContext.SetData(this._key.ToString(), newValue);
                    }
                }
            }
        }
    }

    public class UnityServiceLocator : ServiceLocatorImplBase {
        private readonly IUnityContainer _unityContainer;
        public UnityServiceLocator(IUnityContainer unityContainer) {
            this._unityContainer = unityContainer;
        }
        protected override object DoGetInstance(System.Type serviceType, string key) {
            return this._unityContainer.Resolve(serviceType, key, new ResolverOverride[0]);
        }
        protected override System.Collections.Generic.IEnumerable<object> DoGetAllInstances(System.Type serviceType) {
            return this._unityContainer.ResolveAll(serviceType, new ResolverOverride[0]);
        }
    }

    public interface IDependencyResolver {
        void Register<T>(T instance);
        void Inject<T>(T existing);
        T Resolve<T>(System.Type type);
        T Resolve<T>(System.Type type, string name);
        T Resolve<T>();
        T Resolve<T>(string name);
        System.Collections.Generic.IEnumerable<T> ResolveAll<T>();
    }

    public interface IDependencyResolverFactory {
        IDependencyResolver CreateInstance();
    }
}
