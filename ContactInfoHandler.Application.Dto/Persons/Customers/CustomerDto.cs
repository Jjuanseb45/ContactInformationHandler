using System;

namespace ContactInfoHandler.Application.Dto.Persons.Customers
{
    class CustomerDto : PersonDto
    {
        public string FavoriteBrand { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
