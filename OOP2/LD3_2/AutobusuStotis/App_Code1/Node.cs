using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutobusuStotis
{
    public sealed class Node<type> where type :
        IComparable<type>, IEquatable<type>
    {
        public type Data { get; set; }
        public Node<type> Link { get; set; }
        public Node(type value, Node<type> link)
        {
            this.Data = value;
            this.Link = link;
        }
    }
}