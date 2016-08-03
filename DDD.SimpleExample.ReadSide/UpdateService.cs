using System;
using System.Configuration;
using AutoMapper;
using DDD.SimpleExample.ReadSide.Interfaces;
using MassTransit;
using MassTransit.Util;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.NamedScope;
using Topshelf;
using Topshelf.Logging;

namespace DDD.SimpleExample.ReadSide
{
    internal class UpdateService : ServiceControl
    {
        private readonly LogWriter _logger = HostLogger.Get<UpdateService>();
        private readonly StandardKernel _kernel = new StandardKernel();

        private IBusControl _busControl;
        private BusHandle _busHandle;

        public bool Start(HostControl hostControl)
        {
            _logger.Info("Creating bus...");
            ConfigureContainer();
            _busControl = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(GetHostAddress(), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["RabbitMQUsername"]);
                    h.Password(ConfigurationManager.AppSettings["RabbitMQPassword"]);
                });

                x.ReceiveEndpoint(host, "events", e =>
                {
                    e.LoadFrom(_kernel);
                });

                x.ReceiveEndpoint(host, "requests", e =>
                {
                    e.LoadFrom(_kernel);
                });
            });

            var observer = _kernel.Get<ScopeObserver>();
            _busControl.ConnectReceiveObserver(observer);
            _logger.Info("Starting bus...");
            _busHandle = _busControl.Start();

            TaskUtil.Await(() => _busHandle.Ready);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _logger.Info("Stopping bus...");

            _busHandle?.Stop(TimeSpan.FromSeconds(30));

            return true;
        }

        private void ConfigureContainer()
        {
            _kernel.Bind<StandardKernel>().ToConstant(_kernel).InSingletonScope();
            _kernel.Bind<ScopeObserver>().ToSelf().InThreadScope();
            _kernel.Bind<IMapper>().ToConstant(MapConfig.CreateMapper()).InSingletonScope();
            _kernel.Bind<IModelUpdater>().To<ModelContext>().InScope(context =>
            {
                var scopeObserver = context.Kernel.Get<ScopeObserver>();
                return scopeObserver.Current;
            });
            _kernel.Bind<IModelReader>().To<ModelContext>().InScope(context =>
            {
                var scopeObserver = context.Kernel.Get<ScopeObserver>();
                return scopeObserver.Current;
            });
            _kernel.Bind(x => x
                .FromThisAssembly()
                .IncludingNonePublicTypes()
                .SelectAllClasses()
                .InheritedFrom(typeof(IConsumer))
                .BindToSelf());
        }

        static Uri GetHostAddress()
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = "rabbitmq",
                Host = ConfigurationManager.AppSettings["RabbitMQHost"]
            };

            return uriBuilder.Uri;
        }
    }
}