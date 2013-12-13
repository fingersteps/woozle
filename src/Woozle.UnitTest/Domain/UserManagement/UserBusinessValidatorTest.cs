using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Woozle.Domain.UserManagement;
using Woozle.Model;

namespace Woozle.UnitTest.Domain.UserManagement
{
    [TestClass]
    public class UserBusinessValidatorTest
    {
        [TestMethod]
        public void ValidationTest()
        {
            var validator = new UserBusinessValidator(null);
            validator.EditMode = true;

            var result = validator.Validate(new User
                {
                    Username = "pro2",
                    UserMandatorRoles = new ObservableCollection<UserMandatorRole>
                        {
                            new UserMandatorRole
                                {
                                    MandatorRole = new MandatorRole
                                        {
                                            Id = 10
                                        }
                                }

                        }
                });

            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }
    }
}
