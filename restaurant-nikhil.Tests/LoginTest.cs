using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using restaurant_nikhil.Model;
using restaurant_nikhil.Services;

namespace restaurant_nikhil.Tests
{
    [TestClass]
    
    public class LoginTest
    {
        
        private const string TestEmail = "nikhil@gmail.com"; //correct email
        private const string TestPassword = "nikhil"; // correct password
        

        [TestMethod]
        public void AuthenticateAdmin_WithCorrectCredentials_ReturnsSuccess()
        {
            
            LoginService loginService = new LoginService();

           
            LoginResult result = loginService.AuthenticateUser(TestEmail, TestPassword);

            
            Assert.IsTrue(result.Success, "Login Successfull");
            Assert.IsFalse(string.IsNullOrEmpty(result.UserId), "User ID should not be null");
            Assert.IsFalse(string.IsNullOrEmpty(result.FirstName), "FirstName should not be null");
        }

        [TestMethod]
        public void AuthenticateAdmin_WithWrongPassword_ReturnsFailure()
        {
            
            LoginService loginService = new LoginService();
            string wrongPassword = "123344";

           
            LoginResult result = loginService.AuthenticateUser(TestEmail, wrongPassword);

            
            Assert.IsFalse(result.Success, "Login failed");
            Assert.AreEqual("Wrong Password", result.ErrorMessage);
        }

        [TestMethod]
        public void AuthenticateAdmin_WithNonExistentUser_ReturnsFailure()
        {
            
            LoginService loginService = new LoginService();
            string wrongEmail = "no@g.com";

            
            LoginResult result = loginService.AuthenticateUser(wrongEmail, "1111111");

            
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Admin does not exist", result.ErrorMessage);
        }
    }
}
