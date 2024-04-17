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

    int getMatrix(float *);
    float getMatrixValue(int, int);
    int getRow();
    int getColumn();
    int setMatrix(float *);
    int setMatrix(float *, int, int);
};


