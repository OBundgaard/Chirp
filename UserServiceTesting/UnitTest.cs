using UserProfileService.Models;

namespace UserServiceTesting
{
    public class UnitTest
    {
        [Fact]
        public void TestAddUser()
        {
            // Arrange
            User user = new User
            {
                Username = "test_user",
                DisplayName = "Test User"
            };

            bool expected = true;

            // Act
            bool actual = UserProfileService.Program.AddUser(user.Username, user.DisplayName).Result;

            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGetUser()
        {
            // Arrange
            int userID = 0;
            string expected = "test_user";

            // Act
            string actual = UserProfileService.Program.GetUser(userID)?.Result?.Username ?? "";

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestDeleteUser()
        {
            // Arrange
            int userID = 0;
            bool expected = true;

            // Act
            bool actual = UserProfileService.Program.DeleteUser(userID).Result;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}