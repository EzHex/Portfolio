namespace AutobusuStotis
{
    public class PriceNode
    {
        public Price Data { get; private set; }
        public PriceNode Link { get; set; }

        public PriceNode(Price data, PriceNode link)
        {
            this.Data = data;
            this.Link = link;
        }
    }
}