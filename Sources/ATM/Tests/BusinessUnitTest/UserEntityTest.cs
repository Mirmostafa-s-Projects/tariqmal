using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mohammad.Projects.TariqMal.Business.Entities;
using Mohammad.Projects.TariqMal.Business.Model;

namespace BusinessUnitTest
{
    [TestClass]
    public class UserEntityTest
    {
        [TestMethod]
        public void RegisterUser()
        {
            UserEntity bll = new UserEntity();
            var args = new RegisterUserArgument()
            {
                CountryId = Guid.Empty,
                Email = "mimostafa@hotmail.com",
                Language = "Farsi",
                FirstName = "محمد",
                LastName = "میرمصطفی",
                MobileNumber = "09124438164",
                Password = "123",
                UserName = "mirmostafa",
            };

            var register = bll.Register(args, Language.Fa);
        }
    }
}
