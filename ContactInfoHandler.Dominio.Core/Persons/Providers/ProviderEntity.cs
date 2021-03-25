using ContactInfoHandler.Dominio.Core.Identifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContactInfoHandler.Dominio.Core.Persons.Providers
{
    public class ProviderEntity: BasePersonEntity
    {
        [Key]
        public Guid IdProvider { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public long ContactNumber { get; set; }

        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }
        public KindOfIdentificationEntity KindOfIdentification { get; set; }
    }
}
