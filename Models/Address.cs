namespace EcomMVC.Models
{
    public class Address
    {
        public Address(string street, string locality, string city, string zipcode){
            Street = street;
            Locality = locality;
            City = city;
            Zipcode = zipcode;
        }
        public int Id { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }

        public string PhoneNumber  { get; set; }
        public int UserId { get; set; }
    }
}