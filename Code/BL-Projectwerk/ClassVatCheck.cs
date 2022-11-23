using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk
{
    public class ClassVatCheck
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Valid { get; set; }
        public string Result { get; set; }

        public ClassVatCheck()
        {

        }

        public void TestVatNumber()
        {
            try
            {
                using (var client = new ServiceReference1.checkVatPortTypeClient())
                {
                    string name;
                    bool valid;
                    string address;

                    string countryCode = "BE";
                    string vatNumber = "0412121524";

                    Task<ServiceReference1.checkVatResponse> result = client.checkVatAsync(new ServiceReference1.checkVatRequest(countryCode, vatNumber));
                    name = result.Result.name;
                    address = result.Result.address;
                    valid = result.Result.valid;
                    Name = name;
                    Address = address;
                    Valid = valid;
                    Result = result.Result.ToString();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
