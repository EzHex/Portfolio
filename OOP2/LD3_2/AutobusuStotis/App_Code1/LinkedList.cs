using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace AutobusuStotis
{
    public sealed class LinkedList<type> : IEnumerable<type>
        where type : IComparable<type>, IEquatable<type>
    {
        public string HeadCity { get; private set; }

        private Node<type> head;
        private Node<type> tail;
        private Node<type> d;

        public LinkedList(string headCity)
        {
            this.HeadCity = headCity;
            this.head = null;
            this.tail = null;
            this.d = null;
        }

        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            this.d = null;
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
        public type Get()
        {
            return d.Data;
        }
        /// <summary>
        /// Adds the node to the tail of the list
        /// </summary>
        /// <param name="newNode"> new node </param>
        public void AddNode(Node<type> newNode)
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
        /// Selection sort
        /// </summary>
        public void Sort()
        {
            for (Node<type> d1 = head; d1 != null ; d1=d1.Link)
            {
                Node<type> maxv = d1;
                for (Node<type> d2 = d1; d2 != null; d2 = d2.Link)
                {
                    if (d2.Data.CompareTo(maxv.Data) == -1)
                    {
                        maxv = d2;
                    }
                }
                type St = d1.Data;
                d1.Data = maxv.Data;
                maxv.Data = St;
            }
        }
        /// <summary>
        /// Foreach implementation
        /// </summary>
        /// <returns>Values</returns>
        public IEnumerator<type> GetEnumerator()
        {
            for (Node<type> dd = head; dd != null; dd = dd.Link)
            {
                yield return dd.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}