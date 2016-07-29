using System;
using System.Configuration;
using CommonDomain;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using DDD.SimpleExample.Domain;
using DDD.SimpleExample.Domain.Customer.Service;
using DDD.SimpleExample.Domain.Project.Service;
using DDD.SimpleExample.WriteSide.Handlers.Customer;
using DDD.SimpleExample.WriteSide.Handlers.Project;
using MassTransit;
using MassTransit.Util;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using Topshelf;
using Topshelf.Logging;

namespace DDD.SimpleExample.WriteSide
{
    internal class WriteSide : ServiceControl
    {
        private readonly LogWriter _logger = HostLogger.Get<WriteSide>();
        private readonly Container _container = new Container();

        private IBusControl _busControl;
        private BusHandle _busHandle;

        public bool Start(HostControl hostControl)
        {
            _logger.Info("Creating bus...");
            ConfigureContainer();
            _busControl = _container.GetInstance<IBusControl>();
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

            _container.Register(() => Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(GetHostAddress(), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["RabbitMQUsername"]);
                    h.Password(ConfigurationManager.AppSettings["RabbitMQPassword"]);
                });

                x.ReceiveEndpoint(host, "customer", e =>
                {
                    e.Consumer(() =>
                    {
                        using (_container.BeginExecutionContextScope())
                        {
                            return _container.GetInstance<CustomerHandler>();
                        }
                    });
                });

                x.ReceiveEndpoint(host, "project", e =>
                {
                    e.Consumer(() =>
                    {
                        using (_container.BeginExecutionContextScope())
                        {
                            return _container.GetInstance<ProjectHandler>();
                        }
                    });
                });
            }), Lifestyle.Singleton);

            _container.Register(() => EventStoreConfig.Create(_container), Lifestyle.Singleton);

            _container.Register<IDetectConflicts, ConflictDetector>(Lifestyle.Scoped);
            _container.Register<IRepository, EventStoreRepository>(Lifestyle.Scoped);
            _container.Register<IConstructAggregates, AggregateFactory>(Lifestyle.Scoped);

            _container.Register<IProjectService, ProjectService>();
            _container.Register<ICustomerService, CustomerService>();

            _container.Register<CustomerHandler>();
            _container.Register<ProjectHandler>();
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