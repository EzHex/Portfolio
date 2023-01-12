package edu.ktu.ds.lab3.Fibo;

public class Node<T extends Comparable<T>> {
    private Node<T> parent;
    private Node<T> child;
    private Node<T> next;
    private Node<T> prev;

    private int degree = 0;
    private boolean marked;

    private T key;

    Node (T key, boolean mark){
        this.next = this;
        this.prev = this;
        this.key = key;
        this.marked = mark;
    }

    public Node<T> getParent() {
        return parent;
    }

    public void setParent(Node<T> parent) {
        this.parent = parent;
    }

    public Node<T> getChild() {
        return child;
    }

    public void setChild(Node<T> child) {
        this.child = child;
    }

    public Node<T> getNext() {
        return next;
    }

    public void setNext(Node<T> next) {
        this.next = next;
    }

    public int getDegree() {
        return degree;
    }

    public void setDegree(int degree) {
        this.degree = degree;
    }

    public void setKey(T key){
        this.key = key;
    }

    public T getKey() {
        return key;
    }

    public Node<T> getPrev() {
        return prev;
    }

    public void setPrev(Node<T> prev) {
        this.prev = prev;
    }

    public boolean isMarked() {
        return marked;
    }

    public void setMarked(boolean marked) {
        this.marked = marked;
    }
}
