namespace AutobusuStotis
{
    public class PriceLinkedList
    {
        private PriceNode head;
        private PriceNode tail;
        private PriceNode d;

        public PriceLinkedList()
        {

        }
        /// <summary>
        /// Adds the node to the tail of the list
        /// </summary>
        /// <param name="newNode">new node</param>
        public void AddPriceNode(PriceNode newNode)
        {
            if (this.head != null)
            {
                this.tail.Link = newNode;
                this.tail = newNode;
            }
            else
            {
                this.head = newNode;
                this.tail = newNode;
            }
        }
        /// <summary>
        /// Set the d pointer to the head of the list
        /// </summary>
        public void Begin()
        {
            d = head;
        }
        /// <summary>
        /// Set pointer d to the next node in the list
        /// </summary>
        public void Next()
        {
            d = d.Link;
        }
        /// <summary>
        /// Checks if node exist
        /// </summary>
        public bool Exist()
        {
            return d != null;
        }
        /// <summary>
        /// Gets data from the node
        /// </summary>
        /// <returns>Data</returns>
        public Price Get()
        {
            return d.Data;
        }
    }
}