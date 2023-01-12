#include <iostream>
#include <string>
#include <vector>
#include <fstream>
#include <omp.h>
#include <thread>
#include<cstdlib>

using namespace std;

struct BusinessManager
{
    string name;
    int workYears;
    double averageProfit;
    double profit;

    string toString() const
    {
        return "| " + name + " | " + to_string(workYears) + " | " + to_string(averageProfit) + " | " + to_string(profit) + " |\n";
    }
};

struct ResultMonitor
{
    BusinessManager managers[25];
    const int maxManagers = 25;

    int count = 0;

    void insert(BusinessManager manager)
    {
        if (count >= maxManagers)
        {
            cout << "ResultMonitor max managers reached" << endl;
            return;
        }

#pragma omp critical (insert_item)
        {
            int index = binarySearch(manager.profit);
            if (index == -1)
            {
                index = 0;
            }
        
            for (int i = count; i > index; i--)
            {
                this -> managers[i] = this -> managers[i - 1];
            }
        
            this -> managers[index] = manager;
                
            count++;
        }
        
    }

    void remove(int index)
    {
        if (index >= count)
        {
            cout << "Index out of bounds" << endl;
            return;
        }
        
        for (int i = index; i < 24; i++)
        {
            this -> managers[i] = this -> managers[i + 1];
        }
        count--;
    }

    int binarySearch(double profit)
    {
        int left = 0;
        int right = count - 1;
        int mid = 0;
        while (left <= right)
        {
            mid = (left + right) / 2;
            if (managers[mid].profit == profit)
            {
                return mid;
            }
            else if (managers[mid].profit < profit)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return left;
    }

    void print()
    {
        if (count == 0)
        {
            cout << "Monitor is empty" << endl;
            return;
        }
        
        for (int i = 0; i < count; i++)
        {
            cout << this -> managers[i].toString();
        }
    }

    void printToFile()
    {
        ofstream fr ("result.txt");
        if (count == 0)
        {
            fr << "Monitor is empty" << endl;
            return;
        }
        
        for (int i = 0; i < count; i++)
        {
            fr << this -> managers[i].toString();
        }
    }
};

double fRand(double fMin, double fMax)
{
    srand(time(NULL));
    double f = (double)rand() / RAND_MAX;
    return fMin + f * (fMax - fMin);
}

double calculateProfit(double averageProfit, int workYears)
{
    double result = 0;

    for (int i = 0; i < workYears; i++)
    {
        auto rndm = fRand(0, 100);
        result += (averageProfit + rndm);
    }
    
    return result;
}

int main(int argc, char* argv[])
{
    setlocale(LC_ALL, "Lithuanian");
    
    vector<BusinessManager> managers;
    ifstream f ("IFF06_BurbaArnas_L1_dat_1.txt");
    BusinessManager temp;
    double criteria = 500;
    
    //read from file to vector
    while (f >> temp.name >> temp.workYears >> temp.averageProfit)
    {
        managers.push_back(temp);
    }
    
    f.close();

    double AccumForAverageProfit = 0;
    int AccumForWorkYear = 0;
    int numOfThreads = managers.size() / 4;
    ResultMonitor result_monitor;

#pragma omp parallel num_threads(numOfThreads) shared(managers, result_monitor) reduction(+:AccumForAverageProfit,AccumForWorkYear)
    {
        int p;  //pointer to first element to be processed by thread
        int totalProcessed;
        int numberOfUnequals = managers.size() % omp_get_num_threads(); //number of threads with 1 more object to process
        int threadNumber = omp_get_thread_num();    //get current thread number

        if (threadNumber < numberOfUnequals)
        {
            totalProcessed = managers.size() / omp_get_num_threads() + 1;
            p = threadNumber * totalProcessed;
        }
        else
        {
            totalProcessed = managers.size() / omp_get_num_threads();
            p = threadNumber * totalProcessed + numberOfUnequals;
        }
        
        for (int i = 0; i < totalProcessed; i++)
        {
            BusinessManager manager = managers[p + i];
            manager.profit = calculateProfit(manager.averageProfit, manager.workYears);
            if (manager.profit < criteria)
                continue;

            AccumForAverageProfit += manager.averageProfit;
            AccumForWorkYear += manager.workYears;
            result_monitor.insert(manager);
        }        
    }

    printf("Sum of average profit: %f\n", AccumForAverageProfit);
    printf("Sum of work years: %i\n", AccumForWorkYear);

    
    result_monitor.printToFile();
    
    
    return 0;
}
