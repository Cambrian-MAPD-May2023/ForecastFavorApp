using System;
namespace ForecastFavorApp.Models
{
    /// <summary>
    /// Represents user profile information.
    /// 
    /// Properties:
    /// - Id: A unique identifier for each user profile.
    /// - Name: The name of the user.
    /// - ProfilePictureUrl: URL to the user's profile picture.
    /// - Email: User's email address.
    /// - PhoneNumber: User's contact number.
    /// - PasswordHash: Hashed password for authentication.
    /// </summary>

    public class UserProfile
	{
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; } // to store the hashed password
    }
}

