#pragma once
#include "pch.h"
#include "matrix_block.h"
#include <iostream>
#include <chrono>
#include <fstream>
#include <string.h>

class vector {
public:
	int len;
	int LEN;
	vector_block* v;

	vector(int n, double* vals);

	vector(int n = 3, double r = 0.0);

	vector(const vector& V);  // полное переписывание с изменением размера или выдавать ошибку?

	vector_block& operator[](int i);

	vector& operator+=(vector);

	vector operator+(vector);

	vector operator-(vector);

	double* ToDouble();

	void save(string s);

	friend ostream& operator<<(ostream& s, const vector& V);
};


class matrix
{
public:
	int n = 3;
	int N = 3;
	matrix_block* A = new matrix_block[3];
	matrix_block* B = new matrix_block[3];
	matrix_block* C = new matrix_block[3];

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

	vector operator*(vector& v);

	vector	solve(vector x);

	friend void matrix_block::free_mem(double**, int);

	void save_matrix(string s, vector X, vector  F);// , double** arr);

	//void save(string s, double** arr);

	//void save_matrix(string s, double* f, double* x, double** arr);
};

