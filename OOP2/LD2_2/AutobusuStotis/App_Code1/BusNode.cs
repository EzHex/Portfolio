namespace AutobusuStotis
{
    public sealed class BusNode
    {
        public Bus Data { get; set; }
        public BusNode Link { get; set; }

        public BusNode(Bus data, BusNode link)
        {
            this.Data = data;
            this.Link = link;
        }
    }
}