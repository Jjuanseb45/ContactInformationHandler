using ContactInfoHandler.Application.Dto.Base;
using System;

namespace ContactInfoHandler.Application.Dto.Persons
{
    public abstract class PersonDto: DtoBase
    {
        public long IdNumber { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public virtual string KindOfPerson { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTimeOffset SignUpDate { get; set; }


    }
}   
