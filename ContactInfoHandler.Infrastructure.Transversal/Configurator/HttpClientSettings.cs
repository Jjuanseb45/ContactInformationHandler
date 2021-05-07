﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ContactInfoHandler.Infrastructure.Transversal.Configurator
{
    {
        
        public string ServiceProtocol { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string Context { get; set; }

        public void CopyFrom(HttpClientSettings settings)
        {
            ServiceProtocol = settings.ServiceProtocol;
            Port = settings.Port;
            Context = settings.Context;
            Hostname = settings.Hostname;
        }

        public Uri GetServiceUrl() => new UriBuilder
        {
            Host = Hostname,
            Port = Port,
            Path = Context,
            Scheme = ServiceProtocol
        }.Uri;
    }
}