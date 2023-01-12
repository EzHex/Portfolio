namespace AutobusuStotis
{
    public sealed class BusLinkedList
    {
        public string HeadCity { get; private set; }

        private BusNode head;
        private BusNode tail;
        private BusNode d;

        public BusLinkedList(string headCity)
        {
            this.HeadCity = headCity;
        }
        /// <summary>
        /// Makes a copy of the list
        /// </summary>
        /// <returns></returns>
        public BusLinkedList BusLinkedListCopy()
        {
            return (BusLinkedList)this.MemberwiseClone();
        }
        /// <summary>
        /// Adds the node to the tail of the list
        /// </summary>
        /// <param name="newBus"> new node </param>
        public void AddBusNode(BusNode newBus)
        {
            if (this.head != null)
            {
                this.tail.Link = newBus;
                this.tail = newBus;
            }
            else
            {
                this.head = newBus;
                this.tail = newBus;
            }
        }
        /// <summary>
        /// Bubble sort
        /// </summary>
        public void Sort()
        {
            if (head == null) return;
            bool done = true;
            while (done)
            {
                done = false;
                var headn = head;
                while (headn.Link != null)
                {
                    if (headn.Data > headn.Link.Data)
                    {
                        Bus St = headn.Data;
                        headn.Data = headn.Link.Data;
                        headn.Link.Data = St;
                        done = true;
                    }
                    headn = headn.Link;
                }
            }
        }
        /// <summary>
        /// Deletes transit bus routes
        /// </summary>
        public void DeleteTransits()
        {
            if (head.Link != null)
            {
                for (BusNode i = head; i != null; i = i.Link)
                {
                    Bus bus = i.Data;
                    if (bus.DepartureCity != this.HeadCity)
                    {
                        if (i == head)
                        {
                            BusNode node = head;
                            head = head.Link;
                            node.Link = null;
                            i = head;
                        }
                        else if (i == tail)
                        {
                            BusNode beforeTarget = AddressBefore(i);
                            beforeTarget.Link = null;
                            i = head;
                        }
                        else
                        {
                            BusNode beforeTarget = AddressBefore(i);
                            beforeTarget.Link = i.Link;
                            i.Link = null;
                            i = head;
                        }
                    }
                }
            }    
        }
        /// <summary>
        /// Finds address of the node before searching node
        /// </summary>
        /// <param name="vv">Searching node</param>
        /// <returns>Node before the searching one </returns>
        private BusNode AddressBefore(BusNode vv)
        {
            BusNode rr;
            for (rr = head; rr.Link != vv; rr = rr.Link) ;
            return rr;
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
        public Bus Get()
        {
            return d.Data;
        }



    }
}