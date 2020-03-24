#include "pch.h"
#include "matrix_block.h"
#include <iostream>
#include <string.h>

vector_block::vector_block(int n, double* vals) {
	len = n;
	v = new double[n];
	for (int i = 0; i < n; i++)
		v[i] = vals[i];
}
vector_block::vector_block(int n, double r) {
	v = new double[n];
	this->len = n;
	for (int i = 0; i < n; i++) {
		v[i] = r;
	}
}
vector_block::vector_block(const vector_block& V) { // полное переписывание с изменением размера или выдавать ошибку?
	this->len = V.len;
	v = new double[V.len];
	for (int i = 0; i < V.len; i++) {
		v[i] = V.v[i];
	}
}
double vector_block:: operator()(const vector_block& V, const vector_block& M) { //скалярное произведение
	double s = 0;
	if (V.len == M.len) {
		for (int i = 0; i < V.len; i++) {
			s += V.v[i] * M.v[i];
		}
		return s;
	}
}
double& vector_block:: operator[](int i) {
	if (i > len) {
		throw ERROR_IND();
	}
	else {
		return v[i];
	}
}
vector_block vector_block::operator+(vector_block p)
{
	if (this->len != p.len)
		throw ERROR_SIZE();
	vector_block  res = vector_block(p.len);
	for (int i = 0; i < len; i++)
	{
		res.v[i] = this->v[i] + p.v[i];
	}
	return res;
}
vector_block& vector_block::operator+=(vector_block v)
{
	for (int i = 0; i < len; i++)
		this->v[i] += v.v[i];
	return *this;
}
ostream& operator<<(ostream& s, const vector_block& V) {
	int i;
	s << "{ ";
	for (i = 0; i < V.len; ++i) {
		s << (V.v)[i] << ' ';
	}
	s << "}" << endl;
	return s;
}
vector_block vector_block::operator-(vector_block p)
{
	if (this->len != p.len)
		throw ERROR_SIZE();
	vector_block  res = vector_block(p.len);
	for (int i = 0; i < len; i++)
	{
		res.v[i] = this->v[i] - p.v[i];
	}
	return res;
}


matrix_block::matrix_block(const matrix_block& A) {
	int i, j;
	this->m = A.m;
	this->n = A.n;
	array = new double* [m];
	for (i = 0; i < m; i++) {
		array[i] = new double[n];
		for (j = 0; j < n; j++)
			array[i][j] = A.array[i][j];
	}
};
matrix_block::matrix_block()
{
	this->m = 3;
	this->n = 3;
	array = new double* [3];
	for (int i = 0; i < 3; i++) {
		array[i] = new double[3];
		for (int j = 0; j < 3; j++) {
			this->array[i][j] = 0.0;
		}
	}
}
matrix_block::matrix_block(bool r)
{
	this->m = 3;
	this->n = 3;
	array = new double* [3];
	for (int i = 0; i < 3; i++) {
		array[i] = new double[3];
		for (int j = 0; j < 3; j++) {
			if (r)
				this->array[i][j] = std::rand() % (RAND_MAX / 1000);
			else
				this->array[i][j] = 0;
		}
	}
}
matrix_block::matrix_block(double r) {
	this->m = 3;
	this->n = 3;
	array = new double* [3];
	for (int i = 0; i < 3; i++) {
		array[i] = new double[3];
		for (int j = 0; j < 3; j++) {
			array[i][j] = r;
		}
	}
};
matrix_block::matrix_block(double** arr, int n)
{
	this->m = n;
	this->n = n;
	array = new double* [n];
	for (int i = 0; i < n; i++) {
		array[i] = new double[n];
		for (int j = 0; j < n; j++) {
			this->array[i][j] = arr[i][j];
		}
	}
}
matrix_block::matrix_block(double* v, int n) // диагональная из массива длины n 
{
	int i;
	array = new double* [3];

	for (i = 0; i < 3; i++) {
		array[i] = new double[3]{ 0 };
		array[i][i] = v[i];
	}
	m = 3;
	this->n = 3;
}
double matrix_block::to_value(char* str) {
	double int_p = 0;
	double frac_p = 0, frac_pos = 1;
	int k = 0, len, flag_frac = 1, flag_value = 0, neg = 1;
	len = static_cast<int>(strlen(str));

	if (str[0] == '-') {
		neg = -1;
		len--;
		k = 1;
	}
	else if (str[0] == '+') {
		len--;
		k = 1;
		neg = 1;
	}

	while (len--) {
		if (!flag_value && str[k] == '.') {
			throw ERROR_SYNTAX();
		}
		else if (str[k] == '.') {
			flag_frac = 0;
			k++;
			len--;
		}

		if (str[k] >= '0' && str[k] <= '9') {
			flag_value = 1;
			if (flag_frac)
				int_p = int_p * 10 + (str[k] - '0');
			else {
				frac_pos *= 0.1;
				frac_p += frac_pos * (str[k] - '0');
			}
		}
		else {
			throw ERROR_SYNTAX();
		}
		k++;
	}
	return (int_p + frac_p) * (neg);
}
matrix_block::matrix_block(const char* str) {
	int i, j, k, open1, open2, flag = 0, lastn;
	char c = str[0];
	size_t len;
	len = strlen(str);
	k = 0;
	m = 0;
	n = 0;
	char* temp;
	temp = new char[len];
	for (i = 0; i < len; i++)
		temp[i] = '\0';
	if (c != '{') {
		throw ERROR_SYNTAX();
	}
	else open1 = 1;

	while (open1 && k < len) {
		k++;
		lastn = n;
		n = 0;
		if ((c = str[k]) != '{') {
			throw ERROR_SYNTAX();
		}

		open2 = 1;
		while (open2) {
			k++;
			if ((c = str[k]) == ',')
				n++;
			else if (c == '}')
				open2 = 0;
			/*else if (c >= '9' || c <= '0' || c != '-' || c != '+' || c != '.') {
				throw ERROR_SYNTAX();
			}*/
		}
		n++;
		flag++;
		if (lastn != n && flag > 1) {
			throw ERROR_SYNTAX();
		}
		k++;
		if ((c = str[k]) == '}') {
			m++;
			open1 = 0;
		}
		else if (c != ',') {
			throw ERROR_SYNTAX();
		}
		else m++;
	}

	if (m == 1 && n == 1) {
		array = new double* [1];
		array[0] = new double[1];
		array[0][0] = 0;
		this->n = 1;
		this->m = 1;
	}
	else {
		int q = 0;
		k = 2;
		array = new double* [m];
		for (i = 0; i < m; i++) {
			if (i != 0) k++;
			array[i] = new double[n];
			for (j = 0; j < n; j++) {
				q = 0;
				while (str[k] != ',' && str[k] != '}') {
					temp[q++] = str[k++];
				}
				temp[q] = '\0';
				array[i][j] = to_value(temp);
				k++;
				memcpy(temp, "\0", len);
			}
			k++;
		}
		//cout<<*this;
	}

}
/*
matrix matrix::diagonal(const double* vals, int n) {
	int i, j;
	matrix NEW(n, n);
	for (i = 0; i < n; i++) {
		for (j = 0; j < n; j++) {
			NEW.array[i][i] = vals[i];
		}
	}
	return NEW;
}
*/
matrix_block matrix_block::identity() {
	int i, j;
	matrix_block NEW(double(0));
	for (i = 0; i < 3; i++) {
		for (j = 0; j < 3; j++) {
			NEW.array[i][i] = 1;
		}
	}
	return NEW;
}
matrix_block matrix_block::set(int i, int j, double val) {
	if (i > m || j > n)
		throw ERROR_IND();
	this->array[i][j] = val;
	return *this;
}
ostream& operator <<(ostream& s, const matrix_block& A) {
	int i, j;
	for (i = 0; i < A.n; i++) {
		s.width(3);
		for (j = 0; j < A.n; j++) {
			s << ' ' << left << A.array[i][j] << " ";
		}
		s << endl;
	}
	s << endl;
	return s;
}
void matrix_block::transpose() {

	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < 3; j++) {
			double tmp = array[i][j];
			array[i][j] = array[j][i];
			array[j][i] = tmp;
		}
	}
}
/*
const matrix_block matrix_block::operator[](int i) const {
	int j;
	if (i < m && i >= 0) {
		matrix_block c(m, 1);
		for (j = 0; j < m; j++) {
			c[0][j] = array[i][j];
		}
		return c;
	}
	else if (i < n && i >= 0) {
		matrix_bock c(1, n);
		for (j = 0; j < n; j++) {
			c[j][0] = array[j][i];
		}
		return c;
	}
	else
		throw ERROR_IND();
}
*/
vector_block matrix_block::operator[](int i)
{
	int j;
	if (i < m && i >= 0) {
		return vector_block(3, array[i]);
	}
	else
		throw ERROR_IND();
}
matrix_block matrix_block::operator*(double r) {
	int i, j;
	matrix_block NEW(0.0);
	for (i = 0; i < 3; i++) {
		for (j = 0; j < 3; j++) {
			if (array[i][j] != 0)
				NEW.array[i][j] = array[i][j] * r;
		}
	}
	return NEW;
}
matrix_block& matrix_block::operator*=(double r) {
	int i, j;
	for (i = 0; i < m; i++) {
		for (j = 0; j < n; j++) {
			array[i][j] *= r;
		}
	}
	return *this;
}
matrix_block matrix_block::operator+(const matrix_block& A) {
	int i, j;
	if (this->m != A.m || this->n != A.n) {
		throw ERROR_SIZE();
	}
	matrix_block NEW(0.0);
	for (i = 0; i < m; i++) {
		for (j = 0; j < n; j++) {
			NEW.array[i][j] = this->array[i][j] + A.array[i][j];
		}
	}
	return NEW;
}
matrix_block& matrix_block::operator+=(const matrix_block& A) {
	*this = *this + A;
	return *this;
}
matrix_block matrix_block::operator-(const matrix_block& A) {
	int i, j;
	matrix_block NEW(0.0);
	for (i = 0; i < m; i++) {
		for (j = 0; j < n; j++) {
			NEW.array[i][j] = this->array[i][j] - A.array[i][j];
		}
	}
	return NEW;
}
matrix_block& matrix_block::operator-=(const matrix_block& A) {
	*this = *this - A;
	return *this;
}
matrix_block matrix_block::operator*(const matrix_block& A) {
	if (n != A.m)
		throw ERROR_SIZE();
	int i, j, k;
	matrix_block c(0.0);
	for (i = 0; i < 3; i++) {
		for (j = 0; j < 3; j++) {
			for (k = 0; k < 3; k++) {
				c.array[i][j] += array[i][k] * A.array[k][j];
			}
		}
	}
	return c;
}
matrix_block& matrix_block::operator*=(const matrix_block& A) {
	*this = *this * A;
	return *this;
}
/*vector_block matrix_block::operator*(vector_block v)
{
	int i, j;
	vector_block res = vector_block(3, 0.0);

	if (this->m != v.len || this->n != v.len)
		throw ERROR_SIZE();
	for (i = 0; i < n; i++) {
		for (j = 0; j < n; j++) {
			res[i] += array[i][j] * v[j];
		}
	}
	return res;
}
vector_block matrix_block::operator*(matrix_block m, vector_block v)
{
	int i, j;
	vector_block res = vector_block(3, 0.0);
	if (m.m != v.len || m.n != v.len)
		throw ERROR_SIZE();
	for (i = 0; i < 3; i++) {
		for (j = 0; j < 3; j++) {
			res[i] += m.array[i][j] * v[j];
		}
	}
	return res;
}*/

vector_block matrix_block::operator*(vector_block v)
{
	int i, j;
	vector_block res = vector_block(3, 0.0);

	for (i = 0; i < 3; i++) {
		for (j = 0; j < 3; j++) {
			res[i] += array[i][j] * v[j];
		}
	}
	return res;
}
matrix_block matrix_block::operator-() {
	return (*this) * -1;
}
/*
matrix_block matrix_block::operator|(const matrix_block& A) {
	int j;
	if (m != A.m)
		throw ERROR_SIZE();
	matrix NEW(*this);
	for (int i = 0; i < m; i++) {
		array[i] = new double[n + A.n];
		for (j = 0; j < n; j++)
			array[i][j] = NEW.array[i][j];
		for (j = 0; j < A.n; j++) {
			array[i][j + n] = A.array[i][j];
		}

	}
	this->n = n + A.n;
	return *this;
}
matrix matrix::operator/(const matrix& A) {
	if (n != A.n)
		throw ERROR_SIZE();
	matrix NEW(m + A.m, n);
	for (int i = 0; i < m; i++) {
		for (int j = 0; j < n; j++)
			NEW.array[i][j] = array[i][j];
	}
	for (int i = 0; i < A.m; i++)
		for (int j = 0; j < n; j++)
			NEW.array[i + m][j] = A.array[i][j];
	*this = NEW;
	return NEW;
}*/
matrix_block matrix_block::Gauss() {
	int i, k, j;
	int count = 1;
	const double EPS = 0.0001;
	matrix_block tmp = matrix_block(*this);
	for (i = 0; i < m; i++) {
		int maxi = i;
		for (j = i + 1; j < m; j++) {
			if (abs(tmp.array[j][i]) > abs(tmp.array[maxi][i])) {
				maxi = j;
			}
		}
		if (abs(tmp.array[maxi][i]) < EPS)
			continue;
		for (k = 0; k < n; k++)
			swap(tmp.array[i][k], tmp.array[maxi][k]);
		count *= count * (i != maxi ? -1 : 1);
		for (j = i + 1; j < m; ++j) {
			double q = -tmp.array[j][i] / tmp.array[i][i];
			for (k = n - 1; k >= i; --k)
				tmp.array[j][k] += q * tmp.array[i][k];
		}
	}
	tmp.det_sign = count;// -1 или 1 для определителя
	return tmp;
}

double matrix_block::det() {
	if (m != n) {
		throw ERROR_SIZE();
	}
	double x = array[0][0] * array[1][1] * array[2][2] - array[0][0] * array[1][2] * array[2][1]
		- array[0][1] * array[1][0] * array[2][2] + array[0][1] * array[1][2] * array[2][0]
		+ array[0][2] * array[1][0] * array[2][1] - array[0][2] * array[1][1] * array[2][0];

	return x;

}

double matrix_block::det2() {
	if (m != n) {
		throw ERROR_SIZE();
	}
	return array[0][0] * array[1][1] - array[0][1] * array[1][0];

}

void matrix_block::get_minor(double** from, double** to, int indRow, int indCol)
{
	int ki = 0;
	for (int i = 0; i < 3; i++) {
		if (i != indRow) {
			for (int j = 0, kj = 0; j < 3; j++) {
				if (j != indCol) {
					to[ki][kj] = from[i][j];
					kj++;
				}
			}
			ki++;
		}
	}
}

matrix_block matrix_block::inverse() {
	double MD = this->det();
	matrix_block inverse(0.0);
	if (MD) {
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				int m = n - 1;
				double** temp_matr = new double* [m];
				for (int k = 0; k < m; k++)
					temp_matr[k] = new double[m];
				get_minor(this->array, temp_matr, i, j);
				matrix_block TMP = matrix_block(temp_matr, 2);
				double D = TMP.det2();
				inverse.array[j][i] = pow(-1.0, i + j + 2) * D / MD;
				//free_mem(temp_matr, m);
			}
		}
	}
	return (inverse);
}

void matrix_block::free_mem(double** matr, int n)
{
	for (int i = 0; i < n; i++)
		delete[] matr[i];
	delete[] matr;
}
