//#include <stdio.h>
//#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\Matrix.hh"
#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\Vector.hh"
#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\main.hh"
//#include "matrix.h"

int main()
{
    // Matirx test
    int row = 3;
    int column = 3;
    float arrMat[row * column];
    for(int i = 0; i < row * column; i++)
    {
        arrMat[i] = float(i);
        printf("%.2f ", float(i));
    }
    Matrix matrix1 = Matrix(arrMat, row, column);
    printf("\nStelle 2_1: %.2f", matrix1.getMatrixValue(2, 1));
    matrix1.printMatrix();

    // Vector test
    float vec[2] = {1.0, 3.0};
    float scale[2] = {2.0, 1.0};
    Vector vector = Vector(vec, column);
    Matrix scaleMat = vector.makeScalarMatrice(scale);
    scaleMat.printMatrix();
    vector.printVector();

    return 0;
}



