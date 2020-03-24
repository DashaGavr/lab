#include "pch.h"
#include "matrix.h"

vector::vector(int n, double r)
{
	len = 3 * n;
	LEN = n;
	v = new vector_block[LEN];
	for (int i = 0; i < LEN; i++)
	{
		v[i] = vector_block(3, r);
	}


}
vector::vector(const vector& V)
{
	this->len = V.len;
	this->LEN = V.LEN;
	v = new vector_block[V.LEN];
	for (int i = 0; i < V.LEN; i++) {
		v[i] = V.v[i];
	}
}
vector::vector(int n, double* arr)
{
	LEN = n / 3;
	len = n;
	v = new vector_block[n];
	for (int i = 0; i < LEN; i++)
	{
		v[i] = vector_block(3,0.0);
		v[i].v[0] = arr[3 * i];
		v[i].v[1] = arr[3 * i + 1];
		v[i].v[2] = arr[3 * i + 2];
	}
}
vector_block& vector:: operator[](int i) {
	if (i > len) {
		throw ERROR_IND();
	}
	else {
		return v[i];
	}
}
vector& vector::operator+=(vector p)
{
	for (int i = 0; i < LEN; i++)
		this->v[i] += p.v[i];
	return (*this);
}
vector vector::operator+(vector p)
{
	vector res = vector(p.LEN);
	for (int i = 0; i < LEN; i++)
		res.v[i] = this->v[i] + p.v[i];
	return res;
}
vector vector::operator-(vector p)
{
	vector res = vector(p.LEN);
	for (int i = 0; i < LEN; i++)
		res.v[i] = this->v[i] - p.v[i];
	return res;
}
ostream& operator<<(ostream& s, const vector& V) {
	int i;
	for (i = 0; i < V.LEN; i++) {
		s << V.v[i] << "\n";
	}
	s << endl;
	return s;
}
void vector::save(string s)
{
	fstream f;
	f.open(s);
	if (f.fail())
	{
		cout << "\n Ошибка открытия файла";
		throw ERROR_0();
	}
	f << *this;
}
double* vector::ToDouble()
{
	double* tmp = new double[len];
	for (int i = 0; i < LEN; i++)
		for (int j = 0; j < 3; j++)
			tmp[i] = v[i].v[j];
	return tmp;
}

matrix::matrix()
{
	n = 9;
	N = 3;
	double* d1 = new double[9] { 9, 2, 7, 18, 8, 21, 6, 8, 1 };
	double* d2 = new double[9] { 0, 5, 11, 5, 4, 5, 12, 14, 21 };
	double* d3 = new double[9] { 1, 9, 0, 12, 7, 1, 16, 10, 12 };
	double* d4 = new double[9] { 1, 9, 0, 12, 7, 1, 16, 10, 12 };
	double* d5 = new double[9] { 1, 0, 11, 7, 7, 8, -1, 21, 14 };
	double* d6 = new double[9] { 0, 7, 7, 5, 9, 17, 7, 3, 18 };
	double* d7 = new double[9] { 1, 9, 7, 6, 15, 7, -6, 17, 7 };

	A = new matrix_block[2];
	A[0] = new matrix_block(d3);
	A[1] = new matrix_block(d6);
	B = new matrix_block[3];
	B[0] = new matrix_block(d1);
	B[1] = new matrix_block(d4);
	B[2] = new matrix_block(d7);
	C = new matrix_block[2];
	C[0] = new matrix_block(d2);
	C[1] = new matrix_block(d5);
}
matrix::matrix(int k)
{
	n = 3 * k;
	N = k;

	A = new matrix_block[k - 1];
	B = new matrix_block[k];
	C = new matrix_block[k - 1];

	for (int i = 0; i < k - 1; i++)
		A[i] = matrix_block(5.0);
	for (int i = 0; i < k; i++)
		B[i] = matrix_block(4.0);
	for (int i = 0; i < k - 1; i++)
		C[i] = matrix_block(3.0);
}
matrix::matrix(int k, bool flag)
{
	this->n = 3 * k;
	this->N = k;

	A = new matrix_block[k - 1];
	B = new matrix_block[k];
	C = new matrix_block[k - 1];

	for (int i = 0; i < k - 1; i++)
		A[i] = matrix_block(flag);
	for (int i = 0; i < k; i++)
		B[i] = matrix_block(flag);
	for (int i = 0; i < k - 1; i++)
		C[i] = matrix_block(flag);

}
matrix::matrix(const matrix& Copy) //  конструктор копирования - говно!
{
	this->N = Copy.N;
	for (int i = 0; i < N - 1; i++)
		A[i] = Copy.A[i];
	for (int i = 0; i < N; i++)
		B[i] = Copy.B[i];
	for (int i = 0; i < N - 1; i++)
		C[i] = Copy.C[i];
}
matrix matrix::operator+(const matrix& P)
{
	if (N != P.N)
		throw ERROR_SIZE();
	matrix Res = matrix(this->N, true);
	for (int i = 0; i < N - 1; i++)
		Res.A[i] = A[i] + P.A[i];
	for (int i = 0; i < N; i++)
		Res.A[i] = B[i] + P.A[i];
	for (int i = 0; i < N - 1; i++)
		Res.A[i] = C[i] + P.A[i];
	return Res;
}
matrix& matrix::operator+=(const matrix& P)
{
	if (N != P.N)
		throw ERROR_SIZE();
	//matrix Res = matrix(this->N, true);
	for (int i = 0; i < N - 1; i++)
		A[i] += P.A[i];
	for (int i = 0; i < N; i++)
		B[i] += P.B[i];
	for (int i = 0; i < N - 1; i++)
		C[i] += P.C[i];
	return *this;
}
matrix matrix::operator-(const matrix& P)
{
	if (N != P.N)
		throw ERROR_SIZE();
	matrix Res = matrix(this->N, true);
	for (int i = 0; i < N - 1; i++)
		Res.A[i] = A[i] - P.A[i];
	for (int i = 0; i < N; i++)
		Res.A[i] = B[i] - P.A[i];
	for (int i = 0; i < N; i++)
		Res.A[i] = C[i] - P.A[i];
	return Res;
}
matrix& matrix::operator-=(const matrix& P)
{
	if (N != P.N)
		throw ERROR_SIZE();
	//matrix Res = matrix(this->N, true);
	for (int i = 0; i < N - 1; i++)
		A[i] -= P.A[i];
	for (int i = 0; i < N; i++)
		B[i] -= P.B[i];
	for (int i = 0; i < N - 1; i++)
		C[i] -= P.C[i];
	return *this;
}
matrix matrix::operator*(double r) //умножени с той стороны? 
{
	matrix Res = matrix(*this);
	for (int i = 0; i < N - 1; i++)
		Res.A[i] = A[i] * r;
	for (int i = 0; i < N; i++)
		Res.A[i] = B[i] * r;
	for (int i = 0; i < N - 1; i++)
		Res.A[i] = C[i] * r;
	return Res;
}
matrix& matrix::operator*=(double r)
{
	*this = *this * r;
	return *this;
}
vector matrix::operator*(vector& v)
{
	if (v.len != n)
		throw ERROR_SIZE();
	vector res = vector(N, 0.0);
	res[0] = B[0] * v[0];
	res[0] += C[0] * v[1];
	for (int j = 1; j < N - 1; j++)
	{
		res[j] += A[j - 1] * v[j - 1];
		res[j] += B[j] * v[j];
		res[j] += C[j] * v[j + 1];
	}
	res[N - 1] = A[N - 2] * v[N - 2];
	res[N - 1] += B[N - 1] * v[N - 1];
	return res;
}
vector matrix::solve(vector F)
{
	matrix_block* alpha = new matrix_block[N];
	vector_block* beta = new vector_block[N + 1];
	alpha[1] = (B[0].inverse()) * C[0];
	beta[1] = (B[0]).inverse() * F[0];

	for (int i = 1; i < N - 1; i++)
	{
		alpha[i + 1] = (B[i] - A[i - 1] * alpha[i]).inverse() * C[i];
		beta[i + 1] = (B[i] - A[i - 1] * alpha[i]).inverse() * (F[i] - A[i - 1] * beta[i]);
	}
	beta[N] = (B[N - 1] - A[N - 2] * alpha[N - 1]).inverse() * (F[N - 1] - A[N - 2] * beta[N - 1]);

	vector X = vector(N);
	X[N - 1] = beta[N];
	for (int i = N - 2; i >= 0; i--)
	{
		X[i] = beta[i + 1] - alpha[i + 1] * X[i + 1];
	}
	return X;
}
void matrix::save_matrix(string s, vector X, vector F)
{
	std::ofstream f(s, std::ofstream::app);
	if (!f.is_open()) {
		return;
	}

	double** arr = new double* [n];
	for (int i = 0; i < n; i++)
		arr[i] = new double[n];
	f << "Порядок матрицы N = " << N << std::endl;
	for (int l = 0; l < 3; l++)
		for (int k = 0; k < 3; k++)
			arr[l][k] = (B[0]).array[l][k];
	for (int l = 0; l < 3; l++)
		for (int k = 0; k < 3; k++)
			arr[l][k + 3] = C[0].array[l][k];
	for (int l = 0; l < 3; l++)
		for (int k = 6; k < n; k++)
			arr[l][k] = 0;

	for (int i = 1; i < N - 1; i++)
	{
		for (int l = 0; l < 3; l++)
			for (int k = 0; k < 3; k++)
				arr[l + 3 * i][k + 3 * (i - 1)] = A[i - 1].array[l][k];
		for (int l = 0; l < 3; l++)
			for (int k = 0; k < 3; k++)
				arr[l + 3 * i][k + 3 * i] = B[i].array[l][k];
		for (int l = 0; l < 3; l++)
			for (int k = 0; k < 3; k++)
				arr[l + 3 * i][k + 3 * (i + 1)] = C[i].array[l][k];
		for (int l = 3 * i; l < 3 * i + 3; l++)
			for (int k = 3 * (i + 2); k < n; k++)
				arr[l][k] = 0;

	}
	for (int i = 3 * (N - 1); i < n; i++)
		for (int j = 0; j < 3 * (N - 2); j++)
			arr[i][j] = 0;
	for (int l = 0; l < 3; l++)
		for (int k = 0; k < 3; k++)
			arr[l + 3 * (N - 1)][k + 3 * (N - 2)] = (A[N - 2]).array[l][k];
	for (int l = 0; l < 3; l++)
		for (int k = 0; k < 3; k++)
			arr[l + 3 * (N - 1)][k + 3 * (N - 1)] = B[N - 1].array[l][k];

	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
			f << arr[i][j] << ' ';
		f << "\n";
	}
	f << endl;
	f << "X" << endl << X;
	f << endl;
	f << "F" << endl << F;
	f.close();
}
