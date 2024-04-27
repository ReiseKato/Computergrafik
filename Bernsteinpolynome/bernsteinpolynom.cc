#include <stdio.h>
#include <math.h>

unsigned long long factorial(int);
unsigned long long binomialCoefficient(int, int);
double bernsteinpolynom(double, int, int);

int main()
{
    /**
     * Bernsteinpolynome für n=3 und fuer i=0,...,3
    */
   double t;
   t = 0.5;
   for(int i = 0; i < 4; i++)
   {
        printf("%lf\n", bernsteinpolynom(t, i, 3));
   }

    return 0;
}

unsigned long long factorial(int n) {
    if (n == 0 || n == 1)
        return 1;
    return n * factorial(n - 1);
}

unsigned long long binomialCoefficient(int n, int k)
{
    return factorial(n) / (factorial(k) * factorial(n - k));
}

double bernsteinpolynom(double t, int i, int n)
{
    return binomialCoefficient(n, i) * pow(t, i) * pow(1 - t, n - i);
}

