#include <stdlib.h>
#include <cmath>
#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\vector.h"

Vector::Vector(float * vector, int length)
{
    this->vector = (float*)malloc(sizeof(float) * length);
    this->length = length;
    for(int i = 0; i < length; i++)
    {
        this->vector[i] = vector[i];
    }
}

int Vector::translation(float * points)
{
    Matrix transMatrice = makeTranslationMatrice(points);
    


    return 0;
}






Matrix Vector::makeTranslationMatrice(float * points)
{
    float * transArr = (float*)malloc(sizeof(float) * (this->length + 1) * (this->length + 1));
    for(int i = 0; i < this->length + 1; i++)
    {
        for(int j = 0; j < this->length + 1; j++)
        {
            if(i == j) transArr[i * (this->length + 1) + j] = 1.0;
            if(j == this->length + 1 + i && j != i) transArr[i * (this->length + 1) + j] = points[i];
        }
    }

    Matrix transMatrix = Matrix(transArr, this->length + 1, this->length + 1);

    free(transArr);

    return transMatrix;
}

Matrix Vector::makeScalarMatrice(float * points)
{
    float * scaleArr = (float*)malloc(sizeof(float) * (this->length + 1) * (this->length + 1));
    for(int i = 0; i < this->length + 1; i++)
    {
        for(int j = 0; j < this->length + 1; j++)
        {
            if(i == j && i != this->length) scaleArr[i * (this->length + 1) + j] = points[i];
            if(i == j && i == this->length) scaleArr[i * (this->length + 1) + j] = 1.0;
        }
    }

    Matrix scaleMatrix = Matrix(scaleArr, this->length + 1, this->length + 1);

    free(scaleArr);

    return scaleMatrix;
}


// only 3-dimensional Matrices
Matrix Vector::makeRotationMatrice(float degree, char axis)
{
    // converting to Radiant
    float rad = degree*3.14159/180;
    if(axis == 'x')
    {
        float rotationArr[] = {1.0, 0.0, 0.0, 0.0, cos(rad), -sin(rad), 0.0, sin(rad), cos(rad)};
        return Matrix(rotationArr, 3, 3);
    }
    if(axis == 'y')
    {
        float rotationArr[] = {cos(rad), 0.0, -sin(rad), 0.0, 1.0, 0.0, sin(rad), 0.0, cos(rad)};
        return Matrix(rotationArr, 3, 3);
    }
    float rotationArr[] = {cos(rad), -sin(rad), 0.0, sin(rad), cos(rad), 0.0, 0.0, 0.0, 1.0};
    return Matrix(rotationArr, 3, 3);
}







