//#include "config.h"
#include <stdio.h>


class Matrix
{
private:
    float * matrix;
    int row;
    int column;

public:
    Matrix(float *, int, int);
    void printMatrix();

    int getMatrix(float *);
    float getMatrixValue(int, int);
    float getMatrixValue(int);
    int getRow();
    int getColumn();
    int setMatrix(float *);
    int setMatrix(float *, int, int);
};


