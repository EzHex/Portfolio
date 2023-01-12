namespace AutobusuStotis
{
    public class Price
    {
        public string ArrivalCity { get; private set; }
        public decimal TicketPrice { get; private set; }

        public Price(string arrivalCity, decimal ticketPrice)
        {
            this.ArrivalCity = arrivalCity;
            this.TicketPrice = ticketPrice;
        }
    }
}