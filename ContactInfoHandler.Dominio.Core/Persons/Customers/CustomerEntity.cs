using ContactInfoHandler.Dominio.Core.Identifications;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactInfoHandler.Dominio.Core.Persons.Customers
{
    public class CustomerEntity:BasePersonEntity
    {
        [Key]
        public Guid IdCustmer { get; set; }
        [Required]
        public string FavoriteBrand { get; set; }
        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }
    }
}
