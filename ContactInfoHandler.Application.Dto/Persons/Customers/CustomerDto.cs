using System;

namespace ContactInfoHandler.Application.Dto.Persons.Customers
{
    public class CustomerDto : PersonDto
    {
        public Guid IdCustmer { get; set; }
        public string FavoriteBrand { get; set; }
        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }
    }
}
