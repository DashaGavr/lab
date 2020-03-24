#pragma once
#include "pch.h"
#include <iostream>
#include <string.h>

using namespace std;

class ERROR_0 : public exception {
public:
	virtual const char* what() {
		const char* com_err = "UNKNOWN ERROR\n";
		return com_err;
	}
};
class ERROR_IND : public exception {
public:
	virtual const char* what() {
		const char* ind_err = "ERROR! WRONG INDEX\n";
		return ind_err;
	}
};
class ERROR_SIZE : public exception {
public:
	virtual const char* what() {
		const char* size_err = "ERROR! WRONG SIZES\n";
		return size_err;
	}
};
class ERROR_DET : public exception {
public:
	virtual const char* what() {
		const char* det_err = "ERROR! DETERMINANT = 0\n";
		return det_err;
	}
};
class ERROR_SOLVE : public exception {
public:
	virtual const char* what() {
		const char* solve_err = "ERROR WITH SOLVE\n";
		return solve_err;
	}

};
class ERROR_SYNTAX : public exception {
public:
	virtual const char* what() {
		const char* syntax_err = "ERROR! WRONG SYNTAX\n";
		return syntax_err;
	}
};


class vector_block {
public:
	int len = 3;
	double* v;

	vector_block(int n, double* vals);

	vector_block(int n = 3, double r = 0.0);

	vector_block(const vector_block& V);  // полное переписывание с изменением размера или выдавать ошибку?

	double operator()(const vector_block& V, const vector_block& M); //скалярное произведение

	double& operator[](int i);

	vector_block& operator+=(vector_block);

	vector_block operator +(vector_block);

	vector_block operator -(vector_block);

	friend ostream& operator<<(ostream& s, const vector_block& V);
};

class matrix_block
{
public:
	int m = 3, n = 3;
	double** array;
	int det_sign = 1;
	static const double EPS;

	matrix_block(const matrix_block& A);

	matrix_block(double r);

	matrix_block(bool);

	matrix_block();

	matrix_block(double** arr, int n);

	matrix_block(double* v, int n);   // диагональ

	double to_value(char* str); // функция переводит строковое представление числа в вещественное

	matrix_block(const char* str);    // конструктор из строки

	static matrix_block identity();

	matrix_block set(int i, int j, double val);

	friend ostream& operator<<(ostream& s, const matrix_block& A);

	void transpose();

	//friend void get_minor(double**, double**, int, int);

	vector_block operator[](int i);               //для нормального индексирования

	matrix_block operator*(double r);

	matrix_block& operator*=(double r);

	matrix_block operator+(const matrix_block& A);

	matrix_block& operator+=(const matrix_block& A);

	matrix_block operator-(const matrix_block& A);

	matrix_block& operator-=(const matrix_block& A);

	matrix_block operator*(const matrix_block& A);

	matrix_block& operator*=(const matrix_block& A);

	vector_block operator*(vector_block v);

	matrix_block operator-();

	void free_mem(double**, int);

	/*bool operator==(const matrix& A) const;

	//bool operator!=(const matrix& A) const;

	//matrix operator|(const matrix& A);

	//matrix operator/(const matrix& A);*/

	matrix_block Gauss();                    //возвращает -1 или 1 для определителя = приведение к треугольному виду

	double det();

	double det2();

	//matrix& delete_columns(int i);  //удаляет столбец

	//matrix& delete_row(int i);      //удаляет строку

	matrix_block inverse();          //обращение

	void get_minor(double** from, double** to, int indRow, int indCol);

	//matrix operator|(vector& v);    //приписывание вектора

};



