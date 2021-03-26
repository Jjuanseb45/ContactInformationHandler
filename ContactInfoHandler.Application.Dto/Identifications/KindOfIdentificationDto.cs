using ContactInfoHandler.Application.Dto.Persons;
using System;
using System.Collections.Generic;

namespace ContactInfoHandler.Application.Dto.Identifications
{
    public class KindOfIdentificationDto
    {
        public Guid KindOfIdentificationId { get; set; }
        public string IdentificationName { get; set; }
        public ICollection<PersonDto> ListOfPersons { get; set; }
    }
}
