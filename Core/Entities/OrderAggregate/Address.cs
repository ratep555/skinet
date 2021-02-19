namespace Core.Entities.OrderAggregate
{
    //address will be owned by order, which means it will live on the same row
    //in our table as the order itself, we are not giving it id
    public class Address
    {
        //we are also adding parentheless constructor since entityframework needs one
        //otherwise we will get some copmlaints while performing migrations
        public Address()
        {
        }

        public Address(string firstName, string lastName, string street, string city, string state, string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            Zipcode = zipcode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}