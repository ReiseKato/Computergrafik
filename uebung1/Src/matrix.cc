#include <stdlib.h>
//#include <stdio.h>

#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\matrix.h"


Matrix::Matrix(float * inputMatrix, int row, int column)
{
    this->matrix = (float*)malloc(sizeof(float) * row * column);
    this->row = row;
    this->column = column;
    for(int i = 0; i < row*column; i++)
    {
        this->matrix[i] = inputMatrix[i];
    }
}

int Matrix::getMatrix(float * matrix)
{
    matrix = this->matrix;

    return 0;
}

float Matrix::getMatrixValue(int row, int column)
{
    return this->matrix[row * 3 + column];
}

int Matrix::setMatrix(float * matrix)
{
    for(int i = 0; i < row*column; i++)
    {
        this->matrix[i] = matrix[i];
    }

    return 0;
}

int Matrix::setMatrix(float * matrix, int row, int column)
{
    free(Matrix::matrix);
    this->matrix = (float*)malloc(sizeof(float) * row * column);
    this->row = row;
    this->column = column;
    setMatrix(matrix);

    return 0;
}

int Matrix::getRow()
{
    return this->row;
}

int Matrix::getColumn()
{
    return this->column;
}


