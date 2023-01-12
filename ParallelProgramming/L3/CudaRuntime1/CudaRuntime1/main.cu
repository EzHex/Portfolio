#define __CUDACC__

#include "device_launch_parameters.h"
#include "cuda_runtime.h"
#include <iostream>
#include <fstream>
#include <iomanip>

using namespace std;

const int Blocks_Count = 2;
const int Threads_Count = 32;

__device__ int BlockStart[Blocks_Count]; // indekso pradzia
__device__ int Elements_for_Block[Blocks_Count];  // elementu kiekis blokui
__device__ int Positions_for_Result[Blocks_Count] = {};

struct BusinessManager
{
    char name[20];
    int workYears;
    double averageProfit;
};

struct Result
{
    char res[24];
};

__device__ Result ComputeLine(const BusinessManager& manager)
{
	Result result;

    int sum = 0;
	for (int i = 0; i < manager.workYears * manager.workYears; i++)
	{
		for (int i2 = 0; i2 < manager.workYears * manager.workYears; i2++)
		{
            for (int i3 = 0; i3 < manager.workYears; i3++)
                sum += manager.averageProfit;
		}
	}
    
    if (sum > 10000000)
    {
        result.res[0] = manager.workYears > 5 ? 'S' : 'J'; // S - senior, J - junior
        result.res[1] = manager.averageProfit > 80 ? 'H' : 'L'; // H - high, L - low
        result.res[2] = '-';
        for (int i = 3; i < 23; i++)
        {
            if (manager.name[i-3] != 'Ģ')
                result.res[i] = manager.name[i - 3];
        }
        
        return result;
    }
    
    return result;
}

__global__ void Task(Result* rez, BusinessManager* managers)
{
	int thread_count = blockDim.x;
	int thread_id = threadIdx.x;
	int block_id = blockIdx.x;
	
	int lines_to_take = Elements_for_Block[block_id] / thread_count;
	int start_from = BlockStart[block_id] + lines_to_take * thread_id;

	if (thread_id + 1 == thread_count)
	{
		lines_to_take = lines_to_take + Elements_for_Block[block_id] % thread_count;
	}

    for (int i = 0; i < lines_to_take; i++)
    {
        Result resultLine;
        BusinessManager manager = managers[start_from + i];
        resultLine = ComputeLine(manager);
        if (resultLine.res[0])
        {
            int rezInd = BlockStart[block_id] + atomicAdd(&Positions_for_Result[block_id], 1);
            rez[rezInd] = resultLine;
        }
    }
}

int main() {

    //ifstream fd("IFF06ArnasBurba_L3_dat_1.txt"); // dalis
    //ifstream fd("IFF06ArnasBurba_L3_dat_2.txt"); // mazai
	ifstream fd("IFF06ArnasBurba_L3_dat_3.txt"); // viskas
    ofstream fr("res.txt");

    BusinessManager businessManagers[300];
    size_t count = 0;

    cudaDeviceProp prop{};
    cudaGetDeviceProperties(&prop, 0);

    while (!fd.eof())
    {
        BusinessManager manager;
        string tempName;
        fd >> tempName;
		strcpy(manager.name, tempName.c_str());
        fd >> manager.workYears;
        fd >> manager.averageProfit;

        businessManagers[count++] = manager;
    }

	fd.close();
    
    //cpu
	Result* result = new Result[count];
    
    //gpu
    BusinessManager* input_managers;
	Result* output_result;
    
    cudaError_t errors;

    // nuo kada prasideda blokas
    int Block_start[Blocks_Count];
    // kiek elementu blokui
    int Block_elements[Blocks_Count];
	
    // bloku padalinimas
    int size = count;
    int bloc = Blocks_Count;
    for (int i = 0; i < Blocks_Count; i++)
    {
        Block_elements[i] = size / bloc;
        size -= Block_elements[i];
        bloc--;
        if (i == 0) {
            Block_start[i] = 0;
        }
        else
        {
            Block_start[i] = Block_start[i - 1] + Block_elements[i - 1];
        }
    }

	//perduodam gpu bloku pradzia ir elementu kiekius
    errors = cudaMemcpyToSymbol(BlockStart, Block_start, sizeof(Block_start), 0, cudaMemcpyHostToDevice);
    if (errors != cudaSuccess) { return 1; }
    errors = cudaMemcpyToSymbol(Elements_for_Block, Block_elements, sizeof(Block_elements), 0, cudaMemcpyHostToDevice);
    if (errors != cudaSuccess) { return 2; }

    // isskiriam vietos 
    errors = cudaMalloc((void**)&input_managers, count * sizeof(BusinessManager));
    if (errors != cudaSuccess) { return 3; }

    // kopijuojam duomenis
    errors = cudaMemcpy(input_managers, businessManagers, count * sizeof(BusinessManager), cudaMemcpyHostToDevice);
    if (errors != cudaSuccess) { return 4; }

    // isskiriama vietos rezultato masyvui
    errors = cudaMalloc((void**)&output_result, count * sizeof(Result));
    if (errors != cudaSuccess) { return 5; }

	//paleidziama uzduotis
    Task <<<Blocks_Count, Threads_Count >> > (output_result, input_managers);

    //sulaukiam visu giju pabaigos
    errors = cudaDeviceSynchronize();
    if (errors != cudaSuccess) { return 6; }

    // Nusikopijuojame rezultatus i cpu/host
    errors = cudaMemcpy(result, output_result, count * sizeof(Result), cudaMemcpyDeviceToHost);
    if (errors != cudaSuccess) { return 7; }

    // atlaisviname atminti
    cudaFree(input_managers);
    cudaFree(output_result);

    printf("Printing to file\n");

    for (int i = 0; i < Blocks_Count; i++)
    {
        for (int j = 0; j < Block_elements[i]; j++)
        {
            if (!result[Block_start[i] + j].res[0])
            {
                break;
            }

            fr << result[Block_start[i] + j].res << endl;
        }
    }
    
    return 0;
}

