using System;
using System.Configuration;
using System.Reflection;
using DDD.SimpleExample.Common.Events;
using DDD.SimpleExample.ReadSide.Interfaces;
using DDD.SimpleExample.ReadSide.Updaters;
using DDD.SimpleExample.ReadSide.Updaters.Customer;
using DDD.SimpleExample.ReadSide.Updaters.Project;
using MassTransit;
using MassTransit.Util;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using Topshelf;
using Topshelf.Logging;

namespace DDD.SimpleExample.ReadSide
{
    internal class UpdateService : ServiceControl
    {
        private readonly LogWriter _logger = HostLogger.Get<UpdateService>();
        private readonly Container _container = new Container();

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

                x.ReceiveEndpoint(host, "customer_events", e =>
                {
                    e.Consumer(() =>
                    {
                        using (_container.BeginExecutionContextScope())
                        {
                            return _container.GetInstance<CustomerUpdater>();
                        }
                    });
                });

                x.ReceiveEndpoint(host, "project_events", e =>
                {
                    e.Consumer(() =>
                    {
                        using (_container.BeginExecutionContextScope())
                        {
                            return _container.GetInstance<ProjectUpdater>();
                        }
                    });
                });
            });

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
            _container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();
            //_container.RegisterCollection(typeof(IEventHandler<>), Assembly.GetAssembly(typeof(CustomerUpdater)));
            _container.Register<IModelUpdater, ModelContext>(Lifestyle.Scoped);
            _container.Register<CustomerUpdater>();
            _container.Register<ProjectUpdater>();
            _container.Verify();
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