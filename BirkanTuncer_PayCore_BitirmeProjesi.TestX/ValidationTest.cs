using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using Xunit;

namespace BirkanTuncer_PayCore_BitirmeProjesi.TestX
{
    public class ValidationTest
    {
        AccountDto accountDto = new AccountDto();
        CategoryDto categorydto = new CategoryDto();

        [Fact]
        public void AccountDTO_Test_1()
        {
            accountDto.Password = "br121";
            int passwordLength = accountDto.Password.Length;

            Assert.NotInRange(passwordLength, 8, 20);
        }
        [Fact]
        public void AccountDTO_Test_2()
        {
            accountDto.Name = "PaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycorePaycore";
            

            Assert.NotInRange(accountDto.Name.Length, 0, 125);
        }
        [Fact]
        public void AccountDTO_Test_3()
        {
            accountDto.Password = "br121";
            int passwordLength = accountDto.Password.Length;

            Assert.NotInRange(passwordLength, 8, 20);
        }

        [Fact]
        public void CategoryDto_Test_1()
        {            

            Assert.Null(categorydto.CategoryName);
        }
        [Fact]
        public void CategoryDto_Test_2()
        {
            categorydto.CategoryName = "Test";
            Assert.NotNull(categorydto);
        }
        






    }
}
