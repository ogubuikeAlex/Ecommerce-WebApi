using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingsStoreApi.Model.DataTransferObjects.AddressServiceDTO
{
    public class AddAddressDTO
    {
        public string Country { get; set; }
        public string HouseNumber { get; set; }
        public string street { get; set; }
        public string City { get; set; }
    }
}
