using ContactInfoHandler.Dominio.Core.Identifications;
using System;


namespace ContactInfoHandler.Application.Dto.Persons.Providers
{
    public class ProviderDto:PersonDto
    {
        public Guid IdProvider { get; set; }
        public string CompanyName { get; set; }
        public long ContactNumber { get; set; }
        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }

    }
}
