﻿using ContactInfoHandler.Dominio.Core.Identifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContactInfoHandler.Dominio.Core.Persons.Customers
{
    public class CustomerEntity:BasePersonEntity
    {
        [Key]
        public Guid IdProvider { get; set; }
        [Required]
        public string FavoriteBrand { get; set; }
        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }
        public KindOfIdentificationEntity KindOfIdentification { get; set; }
    }
}