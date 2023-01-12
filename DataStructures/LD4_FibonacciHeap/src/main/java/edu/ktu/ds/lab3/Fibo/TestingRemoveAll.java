package edu.ktu.ds.lab3.Fibo;

import org.openjdk.jmh.annotations.*;
import org.openjdk.jmh.runner.Runner;
import org.openjdk.jmh.runner.RunnerException;
import org.openjdk.jmh.runner.options.Options;
import org.openjdk.jmh.runner.options.OptionsBuilder;

import java.util.ArrayList;
import java.util.PriorityQueue;
import java.util.concurrent.TimeUnit;

@BenchmarkMode(Mode.AverageTime)
@State(Scope.Benchmark)
@OutputTimeUnit(TimeUnit.MICROSECONDS)
@Warmup(time = 1, timeUnit = TimeUnit.SECONDS)
@Measurement(time = 1, timeUnit = TimeUnit.SECONDS)
public class TestingRemoveAll {
    @Param({"64000","100","1000","2000","10000","40000","64000","100000","300000"})
    public int elementCount;

    ArrayList<Integer> keys;
    FibonacciHeap<Integer> fibonacciHeap = new FibonacciHeap<>();
    PriorityQueue<Integer> priorityQueue = new PriorityQueue<>();

    @Setup(Level.Iteration)
    public void generateKeys() {
        keys = GenerateKeys(elementCount);
    }

    @Setup(Level.Invocation)
    public void AddToHeaps () {
        for (Integer key : keys){
            fibonacciHeap.Insert(key, true);
            priorityQueue.add(key);
        }
    }

    private ArrayList<Integer> GenerateKeys(int elementCount) {
        ArrayList<Integer> result = new ArrayList<>();
        for (int i = 1; i <= elementCount; i++){
            result.add(i);
        }
        return result;
    }

    @Benchmark
    public void RemoveAllHeap(){
        while (!fibonacciHeap.isEmpty()){
            fibonacciHeap.ExtractMinimum();
        }
    }

    @Benchmark
    public void RemoveAllPriorityQueue(){
        while(!priorityQueue.isEmpty()){
            priorityQueue.remove();
        }
    }

    public static void main(String[] args) throws RunnerException {
        Options opt = new OptionsBuilder()
                .include(TestingRemoveAll.class.getSimpleName())
                .forks(1)
                .build();
        new Runner(opt).run();
    }
}
