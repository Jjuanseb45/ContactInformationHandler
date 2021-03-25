using ContactInfoHandler.Application.Dto.Identifications;
using ContactInfoHandler.Dominio.Core.Identifications;
using System;
using System.Collections.Generic;
using System.Text;

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
        //RELACION CON TIPO DE IDENTIFICACION
        public Guid KindOfIdentificationId { get; set; }
        public KindOfIdentificationEntity KindOfIdentification { get; set; }
    }

    public enum KindOfPerson
    {
        Natural = 1,
        Juridica = 2
    }
}
