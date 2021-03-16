using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Photo.Manufacture.Infrastructure.Data;
using Photo.Manufacture.SharedKernel.Interfaces;
using Module = Autofac.Module;
using Photo.Manufacture.Core.Services;
using Photo.Manufacture.Core.Interfaces;

namespace Photo.Manufacture.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly List<Assembly> assemblies = new();

        public DefaultInfrastructureModule(Assembly callingAssembly =  null)
        {
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository<>));
            assemblies.Add(infrastructureAssembly);
            if (callingAssembly != null)
            {
                assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder) => RegisterCommonDependencies(builder);

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(EfProductRepository).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<BinPackingService>()
                .As<IBinPackingService>()
                .InstancePerLifetimeScope();
        }
    }
}
