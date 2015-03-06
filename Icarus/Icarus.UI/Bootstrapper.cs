﻿using Icarus.Infrastructure.InputProviders;
using StructureMap;
using Icarus.Core.Interfaces;
using log4net;
using Icarus.Infrastructure.CommandFactory;
using Icarus.Infrastructure.Communication;
using Icarus.DroneClients;

namespace Icarus.UI
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var logger = ConfigureLogger();
            var container = new Container(x =>
            {
                x.For<ILog>().Use(logger);
                x.For<IDroneClient>().Use<WifiDroneClient>();
                x.For<ICommandFactory>().Use<CommandFactory>();
                x.For<ICommunicator>().Use<CommandInvoker>();
                x.For<IInputProviderAdapter>().Use<InputProviderAdapter>();
            });
            return container;
        }

        ILog ConfigureLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
            return LogManager.GetLogger("DroneLogger");
        }
    }
}
