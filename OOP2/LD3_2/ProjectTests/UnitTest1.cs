using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutobusuStotis;
using System;
using FluentAssertions;

namespace ProjectTests
{

    [TestClass]
    public class BusTest
    {
        [TestMethod]
        public void Param4BusConstructor()
        {
            DateTime departureTime = DateTime.Parse("00:00");
            DateTime arrivalTime = DateTime.Parse("23:59");

            Bus ob1 = new Bus("A", departureTime, "B", arrivalTime, "Monday");
            ob1.DepartureCity.Should().Be("A");
            ob1.DepartureTime.Should().Be(departureTime);
            ob1.ArrivalCity.Should().Be("B");
            ob1.ArrivalTime.Should().Be(arrivalTime);
            ob1.Day.Should().Be("Monday");
        }

        [TestMethod]
        public void MoreOrEqual()
        {
            DateTime departureTime1 = DateTime.Parse("00:00");
            DateTime arrivalTime1 = DateTime.Parse("23:59");

            Bus ob1 = new Bus("A", departureTime1, "B", arrivalTime1, "Monday");

            DateTime departureTime2 = DateTime.Parse("10:00");
            DateTime arrivalTime2 = DateTime.Parse("12:59");

            Bus ob2 = new Bus("A", departureTime2, "B", arrivalTime2, "Monday");

            Assert.IsTrue(ob2 >= ob1, "Objektas ob2 turėtų būti didesnis už ob1");
        }

        [TestMethod]
        public void MoreOrEqual_BothEqual()
        {
            DateTime departureTime1 = DateTime.Parse("00:00");
            DateTime arrivalTime1 = DateTime.Parse("23:59");

            Bus ob1 = new Bus("A", departureTime1, "B", arrivalTime1, "Monday");

            DateTime departureTime2 = DateTime.Parse("00:00");
            DateTime arrivalTime2 = DateTime.Parse("23:59");

            Bus ob2 = new Bus("A", departureTime2, "B", arrivalTime2, "Monday");

            Assert.IsTrue(ob2 >= ob1, "Objektai nėra lygus");
        }

        [TestMethod]
        public void Equal()
        {
            DateTime departureTime1 = DateTime.Parse("00:00");
            DateTime arrivalTime1 = DateTime.Parse("23:59");

            Bus ob1 = new Bus("A", departureTime1, "B", arrivalTime1, "Monday");

            DateTime departureTime2 = DateTime.Parse("00:00");
            DateTime arrivalTime2 = DateTime.Parse("23:59");

            Bus ob2 = new Bus("A", departureTime2, "B", arrivalTime2, "Monday");

            Assert.IsTrue(ob2.Equals(ob1), "Objektai nėra lygus");
        }
    }

    [TestClass]
    public class PriceTest
    {
        [TestMethod]
        public void Param2PriceContructor()
        {
            Price ob1 = new Price("A", 20m);

            ob1.ArrivalCity.Should().Be("A");
            ob1.TicketPrice.Should().Be(20m);
        }
    }

    [TestClass]
    public class LinkedListTest
    {
        [TestMethod]
        public void AddNode_ToEmptyLinkedList_ShouldBecomeHead()
        {
            var myLinkedList = new LinkedList<int>();

            var nodeToAdd = new Node<int>(10, null);
            myLinkedList.AddNode(nodeToAdd);

            myLinkedList.Begin();

            Assert.AreEqual(10, myLinkedList.Get());
        }

        [TestMethod]
        public void LinkedListSorting()
        {
            var myLinkedList = new LinkedList<int>();
            myLinkedList.AddNode(new Node<int>(15, null));
            myLinkedList.AddNode(new Node<int>(20, null));
            myLinkedList.AddNode(new Node<int>(10, null));

            myLinkedList.Sort();

            myLinkedList.Begin();
            Assert.AreEqual(10, myLinkedList.Get());
            myLinkedList.Next();
            Assert.AreEqual(15, myLinkedList.Get());
            myLinkedList.Next();
            Assert.AreEqual(20, myLinkedList.Get());

            //int i = 10;
            //for (myLinkedList.Begin(); myLinkedList.Exist(); myLinkedList.Next())
            //{
            //    Assert.AreEqual(i, myLinkedList.Get());
            //    i += 5;
            //}

        }
    }
}
