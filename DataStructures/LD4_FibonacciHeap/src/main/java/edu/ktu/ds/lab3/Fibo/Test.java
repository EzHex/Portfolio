package edu.ktu.ds.lab3.Fibo;

public class Test {
    public static void main(String[] args) {
        FibonacciHeap<Integer> H1 = new FibonacciHeap<>();

        H1.Insert(8,true);
        //H1.Print();
        H1.Insert(7,true);
        //H1.Print();
        H1.Insert(5,true);
        //H1.Print();
        H1.Insert(50,true);
        //H1.Print();
        H1.Insert(2,true);
        H1.Print();

        System.out.println(H1.TreeCount());

        System.out.println("---------------------");

        FibonacciHeap<Integer> H2 = new FibonacciHeap<>();

        H2.Insert(20, true);
        H2.Insert(35, true);
        H2.Insert(12, true);
        H2.Insert(1, true);

        H1 = FibonacciHeap.Union(H1, H2);
        H1.Print();

        System.out.println("---------------------");

        do {
            System.out.println("Minimum: " + H1.ExtractMinimum().getKey());
            H1.Print();
            System.out.println(H1.TreeCount());
            System.out.println("---------------------");
        } while (!H1.isEmpty());

        Node<Integer> testNode = new Node<Integer> (50, true);
        Node<Integer> testNode2 = new Node<Integer> (7, true);

        H1.Insert(8,true);
        H1.Print();
        H1.Insert(testNode2);
        H1.Print();
        H1.Insert(5,true);
        H1.Print();
        H1.Insert(testNode);
        H1.Print();
        H1.Insert(2,true);
        H1.Print();

        System.out.println("---------------------");

        System.out.println("Deleting node testNode2 = " + testNode.getKey() + ":");

        H1.Delete(testNode);

        H1.Print();
        System.out.println("---------------------");

        System.out.println("Deleting node testNode2 = " + testNode2.getKey() + ":");

        H1.Delete(testNode2);

        H1.Print();
        System.out.println("---------------------");

        System.out.println("Searching for key = 5:");
        Node<Integer> node = H1.Find(5);

        System.out.println("Deleting node = " + node.getKey() + ":");
        H1.Delete(node);
        H1.Print();
        System.out.println("---------------------");

    }
}
