using API.DTOs;
using Core.Entities;

namespace API.Extensions
{
    public static class AddressMappingExtensions
    {
        public static AddressDto? ToDto(this Address? address)
        {
            if(address == null) return null;

            return new AddressDto
            {
                Line1 = address.Line1,
                Line12 = address.Line12,
                Country = address.Country,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode
            };
        }

        public static Address ToEntity(this AddressDto addressDto)
        {
            if(addressDto == null) throw new ArgumentNullException(nameof(addressDto));

            return new Address
            {
                Line1 = addressDto.Line1,
                Line12 = addressDto.Line12,
                Country = addressDto.Country,
                City = addressDto.City,
                State = addressDto.State,
                PostalCode = addressDto.PostalCode
            };
        }

        public static void ToUpdateFromDto(this Address address, AddressDto addressDto)
        {
            if(addressDto == null) throw new ArgumentNullException(nameof(addressDto));
            if(address == null) throw new ArgumentNullException(nameof(address));

            address.Line1 = addressDto.Line1;
            address.Line12 = addressDto.Line12;
            address.Country = addressDto.Country;
            address.City = addressDto.City;
            address.State = addressDto.State;
            address.PostalCode = addressDto.PostalCode;
        }
    }
}