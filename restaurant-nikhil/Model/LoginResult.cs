using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace restaurant_nikhil.Model
{
    public class LoginResult
    {

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }

        //creating deafault methods for success and failure
        public static LoginResult SuccessResult(string id, string firstName)
        {
            return new LoginResult
            {
                UserId = id,
                FirstName = firstName,
                ErrorMessage = null,
                Success = true
            };
        }

        public static LoginResult FailureResult(string error) { 
            return new LoginResult { 
                Success = false, 
                ErrorMessage = error }; 
        }

    }
}