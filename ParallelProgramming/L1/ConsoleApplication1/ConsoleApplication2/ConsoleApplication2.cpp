#include <algorithm>
#include <iostream>
#include <thread>
#include <string>
#include <vector>
#include <functional>
#include <mutex>
#include <fstream>
#include <condition_variable>
#include <iomanip>

using namespace std;

mutex mtx;

bool pendingData = true;

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

struct Monitor
{
    BusinessManager managers[12];
    const int maxManagers = 12;
    condition_variable cv;
    condition_variable c;

    int count = 0;

    void add(BusinessManager manager)
    {
        unique_lock<mutex> lock(mtx);
        if (count >= maxManagers)
        {
            //cout << count << ", waiting for space" << endl;
                
            cv.wait(lock);
        }
        
        if (count >= maxManagers)
        {
            cout << "Monitor max managers reached" << endl;
            return;
        }
        
        this -> managers[count] = manager;
        count++;
        //cout << "added one, new count: " << count << endl;
        c.notify_all();
    }
    

    BusinessManager remove(int index)
    {
        unique_lock<mutex> lock(mtx);
        while (count == 0 && pendingData)
        {
            c.wait(lock);
        }

        if (index >= count)
        {
            //cout << index << " is index out of bounds" << endl;
            return BusinessManager();
        }
        
        
        BusinessManager result = managers[index];
        for (int i = index; i < 11; i++)
        {
            this -> managers[i] = this -> managers[i + 1];
        }
        count--;
        //cout << "removed one, new count: " << count << endl;
        cv.notify_all();
        return result;
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
    
};

struct ResultMonitor
{
    BusinessManager managers[25];
    const int maxManagers = 25;
    condition_variable cv;

    int count = 0;

    void insert(BusinessManager manager)
    {
        if (count >= maxManagers)
        {
            cout << "ResultMonitor max managers reached" << endl;
            return;
        }

        unique_lock<mutex> lock(mtx);

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

    void remove(int index)
    {
        if (index >= count)
        {
            cout << "Index out of bounds" << endl;
            return;
        }

        unique_lock<mutex> lock(mtx);

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
    double f = (double)rand() / RAND_MAX;
    return fMin + f * (fMax - fMin);
}

double calculateProfit(double averageProfit, int workYears)
{
    double result = 0;

    for (int i = 0; i < workYears; i++)
    {
        result += (averageProfit + fRand(0, 100));
    }
    
    return result;
}

void execute(Monitor &monitor, ResultMonitor &resultMonitor, double criteria)
{
    
    while ( monitor.count > 0 || pendingData)
    {
        BusinessManager manager = monitor.remove(0);
        manager.profit = calculateProfit(manager.averageProfit, manager.workYears);

        if (manager.profit > criteria)
        {
            resultMonitor.insert(manager);
        }
    }
}

int main(int argc, char* argv[])
{
    setlocale(LC_ALL, "Lithuanian");
    
    vector<BusinessManager> managers;

    ifstream f ("IFF06_BurbaArnas_L1_dat_1.txt");
    
    BusinessManager temp;

    //read from file to vector
    while (f >> temp.name >> temp.workYears >> temp.averageProfit)
    {
        managers.push_back(temp);
    }

    f.close();

    Monitor monitor;
    ResultMonitor resultMonitor;

    //create threads
    vector<thread> threads(managers.size() / 4);

    for(auto &t : threads)
    {
        t = thread(execute, ref(monitor), ref(resultMonitor), 500);
    }

    //add managers to monitor and wait if there is no space

    for (int i = 0; i < managers.size(); i++)
    {
        monitor.add(managers[i]);
    }

    pendingData = false;

    //wait for all threads to finish
    for_each(threads.begin(), threads.end(), mem_fn(&thread::join));

    //print results
    resultMonitor.printToFile();
    //cout << resultMonitor.count << endl;

    cout << "Done" << endl;
    
    return 0;
}
