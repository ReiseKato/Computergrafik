#include <stdlib.h>
//#include <stdio.h>

#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\Matrix.hh"


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

void Matrix::printMatrix()
{
    int i;
    int j;
    printf("\n[");
    for(i = 0; i < row; i++)
    {
        printf("\n[");
        for(j = 0; j < column; j++)
        {
            printf("%.2f ", getMatrixValue(i, j));
        }
        printf("]");
    }
    printf("\n]\n");
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

float Matrix::getMatrixValue(int index)
{
    return this->matrix[index];
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


