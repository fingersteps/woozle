using System.Collections.ObjectModel;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Xunit;

namespace Woozle.UnitTest.Domain.UserManagement
{
    public class UserBusinessValidatorTest
    {
        [Fact]
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

            Assert.True(result.IsValid);
            Assert.Equal(result.Errors.Count, 0);
        }
    }
}
