namespace CarRentalService.Models
{
    public class Customer
    {
        public string SocialSecurityNumber { get; set; }

        public Customer(string socialSecurityNumber)
        {
            SocialSecurityNumber = socialSecurityNumber;
        }
    }
}