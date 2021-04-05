using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
