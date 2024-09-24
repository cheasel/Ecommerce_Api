using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Address;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class AddressMapper
    {
        public static AddressDto ToAddressDto(this Address addressModel){
            return new AddressDto{
                Id = addressModel.Id,
                AddressType = addressModel.AddressType,
                Description = addressModel.Description,
                City = addressModel.City,
                State = addressModel.State,
                PostalCode = addressModel.PostalCode,
                Country = addressModel.Country,
            };
        }
    }
}