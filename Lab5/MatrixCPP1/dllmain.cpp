// dllmain.cpp : Определяет точку входа для приложения DLL.
#include <stdio.h>
#include "pch.h"
#include "matrix.h"
#include <time.h>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:

    case DLL_THREAD_ATTACH:

    case DLL_THREAD_DETACH:

    case DLL_PROCESS_DETACH:

        break;


    }
    return TRUE;
}


extern "C" __declspec(dllexport) void global(double* a, double* b, double* c, double* f, double* x, int n)
{
	vector X = vector(int(n/3));
	vector F = vector(int(n/3), 0.0);
	matrix A = matrix(int (n/3), true);
		
	int qa = 0, K = int(n / 3) - 1, L = int(n / 3);
		
	for (int l = 0; l < K; l++)
	{
		for (int i = 0; i < 3; i++)
			for (int k = 0; k < 3; k++)
			{
				(A.A[l]).array[i][k] = a[qa];
				A.C[l].array[i][k] = c[qa];
				qa++;
			}
	}
	qa = 0;
	for (int l = 0; l < L; l++)
	{
		for (int i = 0; i < 3; i++)
			for (int k = 0; k < 3; k++)
			{
				A.B[l].array[i][k] = b[qa];
				qa++;
			}
	}
	qa = 0;
	for (int l = 0; l < L; l++)
	{
		for (int k = 0; k < 3; k++)
			{
				F.v[l].v[k] = f[qa];
				qa++;
			}
	}
	//создание матрицы matrix из Matrix из arr
	
	X = A.solve(F);
	for (int i = 0; i < X.LEN; i++)
		for (int j = 0; j < 3; j++)
			x[3 * i + j] = X.v[i].v[j];
	A.save_matrix("testCPP.txt", X, F);
}