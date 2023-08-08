using System;
using System.Collections.Generic;
using Big_Bang3_Assessment.Model;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test_UserModel_Properties()
        {
            // Arrange & Act
            var user = new User
            {
                User_Id = 1,
                User_Name = "John Doe",
                User_Email = "johndoe@example.com",
                User_Phone = "123-456-7890",
                User_Gender = "Male",
                User_Location = "New York",
                User_Password = "password123"
            };

            // Assert
            Assert.Equal(1, user.User_Id);
            Assert.Equal("John Doe", user.User_Name);
            Assert.Equal("johndoe@example.com", user.User_Email);
            Assert.Equal("123-456-7890", user.User_Phone);
            Assert.Equal("Male", user.User_Gender);
            Assert.Equal("New York", user.User_Location);
            Assert.Equal("password123", user.User_Password);
        }


        [Fact]
        public void Test_UserModel_BookingsAndFeedbacks()
        {
            // Arrange
            var user = new User();
            var booking1 = new Booking();
            var booking2 = new Booking();
            var feedback1 = new FeedBack();
            var feedback2 = new FeedBack();

            // Act
            user.bookings = new List<Booking> { booking1, booking2 };
            user.feedBacks = new List<FeedBack> { feedback1, feedback2 };

            // Assert
            Assert.Collection(user.bookings,
                b => Assert.Equal(booking1, b),
                b => Assert.Equal(booking2, b)
            );

            Assert.Collection(user.feedBacks,
                f => Assert.Equal(feedback1, f),
                f => Assert.Equal(feedback2, f)
            );
        }
    }
}
