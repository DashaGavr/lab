#include "matrix.h"

vector::vector(int n, double r)
{
	len = 3 * n;
	LEN = n;
	v = new vector_block[LEN];
	for (int i = 0; i < LEN; i++)
		v[i] = vector_block(3, r);
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
/*double vector:: operator()(const vector& V, const vector& M) { //��������� ������������
	double s = 0;
	if (V.len == M.len) {
		for (int i = 0; i < V.LEN; i++) {
			s += (V.v[i],  M.v[i]);  // , M.v[i]);
		}
		return s;
	}
}*/
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
	for (i = 0; i < V.len; i++) {
		s << V.v[i] << '\n';
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
		cout << "\n ������ �������� �����";
		throw ERROR_0();
	}
	f << *this;
}

//????????????
matrix::matrix() //��������� ����� ��� �����
{
	n = 9; // �������
	N = 3;
	//array_b = 
	A = new matrix_block[2]{ matrix_block(1.0), matrix_block(1.0) };
	B = new matrix_block[3]{ matrix_block(2.0), matrix_block(2.0), matrix_block(2.0) };
	C = new matrix_block[2]{ matrix_block(3.0), matrix_block(3.0) };
}
matrix::matrix(int k)
{
	n = 3 * k;
	N = k;

	A = new matrix_block[k - 2];
	B = new matrix_block[k - 1];
	C = new matrix_block[k - 2];

	for (int i = 0; i < k - 2; i++)
		A[i] = matrix_block(5.0);
	for (int i = 0; i < k - 1; i++)
		B[i] = matrix_block(4.0);
	for (int i = 0; i < k - 2; i++)
		C[i] = matrix_block(3.0);
}
matrix::matrix(int k, bool flag)
{
	n = 3 * k;
	N = k;

	A = new matrix_block[k - 2];
	B = new matrix_block[k - 1];
	C = new matrix_block[k - 2];

	for (int i = 0; i < k - 2; i++)
		A[i] = matrix_block(0.0);
	for (int i = 0; i < k - 1; i++)
		B[i] = matrix_block(0.0);
	for (int i = 0; i < k - 2; i++)
		C[i] = matrix_block(0.0);

}
matrix::matrix(const matrix& Copy)  //  ����������� ����������� - �����!
{
	this->N = Copy.N;
	for (int i = 0; i < N - 2; i++)
		A[i] = Copy.A[i];
	for (int i = 0; i < N - 1; i++)
		B[i] = Copy.B[i];
	for (int i = 0; i < N - 2; i++)
		C[i] = Copy.C[i];
}
matrix matrix::operator+(const matrix& P)
{
	if (N != P.N)
		throw ERROR_SIZE();
	matrix Res = matrix(this->N, true);
	for (int i = 0; i < N - 2; i++)
		Res.A[i] = A[i] + P.A[i];
	for (int i = 0; i < N - 1; i++)
		Res.A[i] = B[i] + P.A[i];
	for (int i = 0; i < N - 2; i++)
		Res.A[i] = C[i] + P.A[i];
	return Res;
}
matrix& matrix::operator+=(const matrix& P)
{
	if (N != P.N)
		throw ERROR_SIZE();
	//matrix Res = matrix(this->N, true);
	for (int i = 0; i < N - 2; i++)
		A[i] += P.A[i];
	for (int i = 0; i < N - 1; i++)
		B[i] += P.B[i];
	for (int i = 0; i < N - 2; i++)
		C[i] += P.C[i];
	return *this;
}
matrix matrix::operator-(const matrix& P)
{
	if (N != P.N)
		throw ERROR_SIZE();
	matrix Res = matrix(this->N, true);
	for (int i = 0; i < N - 2; i++)
		Res.A[i] = A[i] - P.A[i];
	for (int i = 0; i < N - 1; i++)
		Res.A[i] = B[i] - P.A[i];
	for (int i = 0; i < N - 2; i++)
		Res.A[i] = C[i] - P.A[i];
	return Res;
}
matrix& matrix::operator-=(const matrix& P)
{
	if (N != P.N)
		throw ERROR_SIZE();
	//matrix Res = matrix(this->N, true);
	for (int i = 0; i < N - 2; i++)
		A[i] -= P.A[i];
	for (int i = 0; i < N - 1; i++)
		B[i] -= P.B[i];
	for (int i = 0; i < N - 2; i++)
		C[i] -= P.C[i];
	return *this;
}
matrix matrix::operator*(double r) //�������� � ��� �������? 
{
	matrix Res = matrix(*this);
	for (int i = 0; i < N - 2; i++)
		Res.A[i] = A[i] * r;
	for (int i = 0; i < N - 1; i++)
		Res.A[i] = B[i] * r;
	for (int i = 0; i < N - 2; i++)
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
	vector res = vector(N);
	res[0] = B[0] * v[0];
	res[0] += C[0] * v[1];
	for (int j = 1; j < v.LEN - 1; j++)
	{
		res[j] += A[j - 1] * v[j - 1];
		res[j] += B[j] * v[j];
		res[j] += C[j - 1] * v[j + 1];
	}
	res[N - 1] = A[N - 1] * v[N - 1];
	res[N - 1] += B[N - 1] * v[N - 1];
	return res;
}
matrix:: ~matrix()
{
	/*for (int i = 0; i < N - 1; i++)
	{
		~A[i];	//.free_mem(A[i].array, A[i].n);
		~B[i];	// .free_mem(B[i].array, B[i].n);
		~C[i];	// .free_mem(C[i].array, C[i].n);
	}
	~B[N - 1];	// .free_mem(B[N - 1].array, B[N - 1].n);*/
	delete[] A; // ������� ��� ������� � ��������� ����������
	delete[] B;
	delete[] C;
}
vector matrix::solve(vector F)
{
		matrix_block* alpha = new matrix_block[N];
		vector_block* beta = new vector_block[N + 1];
		alpha[0] = (C[0] * B[0]).inverse();
		beta[0] = (C[0]).inverse() * F[0];

		for (int i = 1; i < N; i++)
		{
			alpha[i] = (C[i - 1] - A[i - 1] * alpha[i - 1]).inverse() * B[i - 1];
			beta[i] = (C[i - 1] - A[i - 1] * alpha[i - 1]).inverse() * (F[i - 1] - A[i-1] * beta[i - 1]);
		}
		beta[N] = (C[N - 1] - A[N - 1] * alpha[N - 1]).inverse() * (F[N - 1] - A[N - 1] * beta[N - 1]);

		vector X = vector(N);
		X[N - 1] = beta[N];
		for (int i = N - 1; i >= 0; i--)
		{
			X[i] = beta[i + 1] - alpha[i + 1] * X[i + 1];
		}
		delete[] alpha;
		delete[] beta;
		return X;
}
void matrix::save_matrix(string s, vector X, vector F)
//void matrix::save_matrix(string s, double* f, double* x, double** arr)
{
	fstream f;
	f.open(s);
	if (f.fail())
	{
		cout << "\n ������ �������� �����";
		throw ERROR_0();
	}
	double** arr = new double* [n];
	for (int i = 0; i < n; i++)
		arr[i] = new double[n];

	for (int i = 0; i < N; i++)
		for (int j = 0; j < N; j++)
		{
			if (i == 0)
			{
				for (int l = 0; l < 3; l++)
					for (int k = 0; k < 3; k++)
						arr[l][k] = B[0].array[l][k];
				for (int l = 0; l < 3; l++)
					for (int k = 0; k < 3; k++)
						arr[l][k + 3] = C[0].array[l][k];
				for (int l = 0; l < 3; l++)
					for (int k = 6; k < n; k++)
						arr[l][k] = 0;
			}
			else if (i != N - 1)
			{
				for (int l = 0; l < 3; l++)
					for (int k = 0; k < 3; k++)
						arr[l + 3 * i][k + 3*(i-1)] = A[i - 1].array[l][k];
				for (int l = 0; l < 3; l++)
					for (int k = 0; k < 3; k++)
						arr[l + 3*i][k + 3* i] = B[i].array[l][k];
				for (int l = 0; l < 3; l++)
					for (int k = 0; k < 3; k++)
						arr[l + 3 * i][k + 3 * (i + 1)] = C[i].array[l][k];
				for (int l = 3 * i ; l < 3 * i + 3; l++)
					for (int k = 3 * (i + 2); k < n; k++)
						arr[l][k] = 0;
			}
			else
			{
				for (int l = 0; l < 3; l++)
					for (int k = 0; k < 3; k++)
						arr[l + 3 * i][k + 3 * (i - 1)] = A[i - 1].array[l][k];
				for (int l = 0; l < 3; l++)
					for (int k = 0; k < 3; k++)
						arr[l + 3 * i][k + 3 * i] = B[i].array[l][k];
			}
		}
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
			f << arr[i][j] << ' ';
		f << "\n";
	}
	f << X;
	f << F;
}

