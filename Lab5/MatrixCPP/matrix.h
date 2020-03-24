#pragma once
#include "matrix_block.h"
#include <iostream>
#include <fstream>
#include <string.h>

class vector {
public:
	int len;
	int LEN;
	vector_block* v;

	//vector(int n, double* vals);

	vector(int n = 1, double r = 0.0);

	vector(const vector& V);  // полное переписывание с изменением размера или выдавать ошибку?

	//double operator()(const vector& V, const vector& M); //скал€рное произведение

	vector_block& operator[](int i);

	vector& operator+=(vector);

	vector operator+(vector);

	vector operator-(vector);

	void save(string s);

	friend ostream& operator<<(ostream& s, const vector& V);
};


class matrix
{
public:
	int n;
	int N;
	matrix_block* A;
	matrix_block* B;
	matrix_block* C;

	matrix();
	matrix(int k); //	Mr. Big 
	matrix(int k, bool); // same with zeros
	matrix(const matrix&);

	matrix operator*(double r);

	matrix& operator*=(double r);

	matrix operator+(const matrix& A);

	matrix& operator+=(const matrix& A);

	matrix operator-(const matrix& A);

	matrix& operator-=(const matrix& A);

	//matrix operator*(const matrix& A);

	//matrix& operator*=(const matrix& A);

	vector operator*(vector& v);

	//matrix operator-();
	vector	solve(vector x);

	~matrix();

	friend void matrix_block::free_mem(double**, int);

	void save_matrix(string s, vector X, vector F);

};

/*
void global_test(Matrix M, Vector V, double& begin, double& end)
{
	auto start_time = std::chrono::steady_clock::now();
	clock_t begin = clock();
	
	matrix TMP = matrix(M.N);
	for (int i = 0; i < M.N - 1; i++)
		for (int l = 0; i < 3; i++)
			for (int k = 0; k < 3; k++)
				TMP.A[i].array[l][k] = M.A[i].array[l][k];
	for (int i = 0; i < M.N; i++)
		for (int l = 0; i < 3; i++)
			for (int k = 0; k < 3; k++)
				TMP.B[i].array[l][k] = M.B[i].array[l][k];
	for (int i = 0; i < M.N - 1; i++)
		for (int l = 0; i < 3; i++)
			for (int k = 0; k < 3; k++)
				TMP.C[i].array[l][k] = M.C[i].array[l][k];
	vector f = vector(V.LEN);
	for (int i = 0; i < V.LEN; i++)
		for (int l = 0; i < 3; i++)
			f.v[i].v[l] = V.vec[i].v[l];
	auto start_time = std::chrono::steady_clock::now();

	vector x = TMP.solve(f);
	TMP.save_matrix("", x, f);
	clock_t end = clock();
	//double seconds = (double)(end - start) / CLOCKS_PER_SEC 
}
*/