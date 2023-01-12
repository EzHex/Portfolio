package edu.ktu.ds.lab3.Fibo;

public class FibonacciHeap<T extends Comparable<T>> {

    Node<T> minimum = null;
    int size = 0;

    public FibonacciHeap (){}

    public boolean isEmpty(){
        return this.size == 0;
    }

    public void Insert (T key, boolean marked){

        if (key == null){
            throw new IllegalArgumentException("Key equals null");
        }

        Node<T> newNode = new Node<>(key, marked);
        newNode.setParent(null);
        newNode.setChild(null);
        Insert(newNode);
    }

    public void Insert(Node<T> newNode) {

        if (minimum == null){
            newNode.setNext(newNode);
            newNode.setPrev(newNode);
            minimum = newNode;
        }
        else {
            Node<T> minimumNext = minimum.getNext();
            minimum.setNext(newNode);
            newNode.setPrev(minimum);
            newNode.setNext(minimumNext);

            minimumNext.setPrev(newNode);

            if (newNode.getKey().compareTo(minimum.getKey()) < 0){
                minimum = newNode;
            }
        }
        size++;
    }

    public static <T extends Comparable<T>> FibonacciHeap<T> Union (FibonacciHeap<T> H1, FibonacciHeap<T> H2){

        FibonacciHeap<T> newHeap = new FibonacciHeap<>();

        newHeap.minimum = H1.minimum;

        newHeap.minimum = mergeHeaps(newHeap.minimum, H2.minimum);


        if(H1.minimum == null || (H2.minimum != null && H2.minimum.getKey().compareTo(H1.minimum.getKey()) < 0)) {
            newHeap.minimum = H2.minimum;
        }

        newHeap.size = H1.size + H2.size;

        return newHeap;
    }

    private static <T extends Comparable<T>> Node<T> mergeHeaps(Node<T> one, Node<T> two) {

        if (one == null && two == null) {
            return null;
        }
        else if (one != null && two == null) {
            return one;
        }
        else if (one == null && two != null) {
            return two;
        }
        else {
            Node<T> oneNext = one.getNext();

            one.setNext(two.getNext());
            one.getNext().setPrev(one);
            two.setNext(oneNext);
            two.getNext().setPrev(two);

            return one;
        }
    }

    public Node<T> ExtractMinimum(){
        Node<T> z = this.minimum;

        if (z != null){
            Node<T> node = z.getChild();
            Node<T> check = node;
            if ( node != null) {
                do {
                    Node<T> c = node.getNext();
                    Insert(node);
                    node.setParent(null);
                    node = c;
                } while (node != null && node != check);

                z.setChild(null);
            }

            z.getPrev().setNext(z.getNext());
            z.getNext().setPrev(z.getPrev());
            z.setChild(null);

            if (z == z.getNext()){
                this.minimum = null;
                this.size--;
            }
            else {
                this.minimum = z.getNext();
                this.size--;

                Consolidate();
            }
        }

        return z;
    }

    public void Consolidate(){
        int arraySize = 0;
        Node<T> checkN = this.minimum;
        Node<T> node = this.minimum;
        do{
            arraySize += Math.pow(2, node.getDegree());
            node = node.getNext();
        } while (node != checkN);

        Node<T>[] array = new Node[arraySize+1];
        for (int i = 0; i < array.length; i++){
            array[i] = null;
        }

        Node<T> w = minimum;

        if (w != null){
            Node<T> check = minimum;
            do {
                Node<T> x = w;
                int d = x.getDegree();
                while (d < size && array[d] != null ){
                    Node<T> y = array[d];
                    if (x.getKey().compareTo(y.getKey()) > 0){
                        Node<T> temp = x;
                        x = y;
                        y = temp;
                        w = x;
                    }
                    Link(y, x);
                    if (check == y) check = x;
                    array[d] = null;
                    d++;
                }

                array[d] = x;
                w = w.getNext();
            } while (w != null && w != check);

            this.minimum = null;
            this.size = 0;

            for(int i = 0; i < array.length; i++){
                if (array[i] != null){
                    Insert(array[i]);
                }
            }
        }
    }

    public void Link(Node<T> y, Node<T> x){

        Node<T> yPrevious = y.getPrev();
        Node<T> yNextas = y.getNext();

        yPrevious.setNext(yNextas);
        yNextas.setPrev(yPrevious);

        Node<T> p = x.getChild();
        if (p == null) {
            y.setNext(y);
            y.setPrev(y);
        } else {
            y.setNext(p);
            y.setPrev(p.getPrev());
            p.getPrev().setNext(y);
            p.setPrev(y);
        }
        y.setParent(x);
        x.setChild(y);
        x.setDegree(x.getDegree() + 1);
        y.setMarked(false);
    }

    public void Print(){
        if (!isEmpty()){
            Print(minimum);
            System.out.println();
        } else {
            System.out.println("Heap is empty");
        }
    }

    private void Print(Node<T> c) {
        System.out.print("(");
        if (c == null) {
            System.out.print(")");
            return;
        } else {
            Node<T> temp = c;
            do {
                System.out.print(temp.getKey());
                Node<T> k = temp.getChild();
                Print(k);
                System.out.print("->");
                temp = temp.getNext();
            } while (temp != c);
            System.out.print(")");
        }
    }

    public void DecreaseKey (Node<T> x, T k){
        if ( k.compareTo(x.getKey()) > 0){
            throw new IllegalArgumentException("New key is greater than old key");
        }

        x.setKey(k);
        Node<T> y = x.getParent();
        if (y != null && x.getKey().compareTo(y.getKey()) <= 0){
            Cut(x, y);
            CascadingCut(y);
        }

        if (x.getKey().compareTo(this.minimum.getKey()) <= 0){
            this.minimum = x;
        }
    }

    private void Cut(Node<T> x, Node<T> y){
        Node<T> xNextas = x.getNext();
        Node<T> xPrevous = x.getPrev();

        if (x.getNext() != x){
            y.setChild(x.getNext());
        }
        else{
            y.setChild(null);
        }

        xNextas.setPrev(xPrevous);
        xPrevous.setNext(xNextas);

        y.setDegree(y.getDegree() - 1);

        x.setNext(null);
        x.setPrev(null);
        x.setParent(null);
        x.setMarked(false);
        Insert(x);

    }

    private void CascadingCut(Node<T> y){
        Node<T> z = y.getParent();
        if (z != null) {
            if (!y.isMarked())
                y.setMarked(true);
            else {
                Cut(y, z);
                CascadingCut(z);
            }
        }
    }

    public void Delete(Node<T> x) {
        DecreaseKey(x, minimum.getKey());
        Node<T> p = ExtractMinimum();
    }

    public Node<T> Find (T k) {
        Node<T> found = null;
        return Find(k, this.minimum, found);
    }

    private Node<T> Find(T key, Node<T> c, Node<T> found) {
        if (found != null || c == null)
            return found;
        else {
            Node<T> temp = c;
            do {
                if (key.compareTo(temp.getKey()) == 0)
                    found = temp;
                else {
                    Node<T> k = temp.getChild();
                    found = Find(key, k, found);
                    temp = temp.getNext();
                }
            } while (temp != c && found == null);
        }

        return found;
    }

    public int TreeCount(){
        int count = -1;
        if(this.minimum != null)
        {
            count = 0;
            Node<T> checkN = this.minimum;
            Node<T> node = this.minimum;
            do{
                if (node.getDegree() > 0){
                    count++;
                }
                node = node.getNext();
            } while (node != checkN);
        }

        return count;
    }

}
