using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Dto.Persons;

namespace ContactInfoHandler.Application.Core.Persons
{
    internal class BaseService
    {
        protected static void ValidateKindOfPerson(PersonDto person) 
        {
            switch (person.KindOfPerson)
            {
                case null: throw new InvalidKindOfPerson("Ingrese razon social");
            }
            if (person.KindOfPerson.ToUpper() != "JURIDICA" && person.KindOfPerson.ToUpper() != "NATURAL")
            {
                throw new InvalidKindOfPerson("La razón social solo puede ser Juridica o Natural");
            }
        }

    }
}
