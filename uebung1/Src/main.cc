//#include <stdio.h>
#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\matrix.h"
#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\main.h"
//#include "matrix.h"

int main()
{
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
    

    return 0;
}



