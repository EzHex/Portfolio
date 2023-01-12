package edu.ktu.ds.lab3.Fibo;

import org.openjdk.jmh.annotations.*;
import org.openjdk.jmh.runner.Runner;
import org.openjdk.jmh.runner.RunnerException;
import org.openjdk.jmh.runner.options.Options;
import org.openjdk.jmh.runner.options.OptionsBuilder;

import java.util.ArrayList;
import java.util.PriorityQueue;
import java.util.Random;
import java.util.concurrent.TimeUnit;

@BenchmarkMode(Mode.AverageTime)
@State(Scope.Benchmark)
@OutputTimeUnit(TimeUnit.MICROSECONDS)
@Warmup(time = 1, timeUnit = TimeUnit.SECONDS)
@Measurement(time = 1, timeUnit = TimeUnit.SECONDS)
public class TestingInsert {
    @Param({"64000","100","1000","2000","10000","40000","64000","100000","300000"})
    public int elementCount;

    ArrayList<Integer> keys;

    @Setup(Level.Iteration)
    public void generateKeys() {
        keys = GenerateKeys(elementCount);
    }

    private ArrayList<Integer> GenerateKeys(int elementCount) {
        ArrayList<Integer> result = new ArrayList<>();
        Random random = new Random();

        for (int i = 1; i <= elementCount; i++){
            result.add(random.nextInt());
        }
        return result;
    }

    @Benchmark
    public FibonacciHeap<Integer> InsertToFibHeap(){
        FibonacciHeap<Integer> fibonacciHeap = new FibonacciHeap<>();
        for (Integer key : keys){
            fibonacciHeap.Insert(key, true);
        }
        return fibonacciHeap;
    }

    @Benchmark
    public PriorityQueue<Integer> InsertToPriorityQueue(){
        PriorityQueue<Integer> priorityQueue = new PriorityQueue<>();
        priorityQueue.addAll(keys);
        return priorityQueue;
    }

    public static void main(String[] args) throws RunnerException {
        Options opt = new OptionsBuilder()
                .include(TestingInsert.class.getSimpleName())
                .forks(1)
                .build();
        new Runner(opt).run();
    }
}
