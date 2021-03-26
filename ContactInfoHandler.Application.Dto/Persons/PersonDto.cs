using ContactInfoHandler.Dominio.Core.Persons;
using System;

namespace ContactInfoHandler.Application.Dto.Persons
{
    public abstract class PersonDto
    {
        public long IdNumber { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public virtual KindOfPerson KindOfPerson { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTimeOffset SignUpDate { get; set; }
    }
}   
