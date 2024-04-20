#include <stdlib.h>
#include <cmath>
#include "C:\Users\reise\code\Computergrafik\uebung1\Inc\Vector.hh"

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
    multiplyMatriceVector(transMatrice, *this, 1);

    return 0;
}

int Vector::translation(Matrix translationMatrice)
{
    multiplyMatriceVector(translationMatrice, *this, 1);

    return 0;
}

int Vector::skalierung(float scale)
{
    for(int i = 0; i < this->length; i++)
    {
        this->vector[i] *= scale;
    }

    return 0;
}

int Vector::sklaierung(float * scale)
{
    Matrix scaleMatrice = makeScalarMatrice(scale);
    multiplyMatriceVector(scaleMatrice, *this, 1);

    return 0;
}

int Vector::skalierung(Matrix scaleMatrice)
{
    multiplyMatriceVector(scaleMatrice, *this, 1);

    return 0;
}

int Vector::rotation(float degree, char axis)
{
    Matrix rotationMatrice = makeRotationMatrice(degree, axis);
    multiplyMatriceVector(rotationMatrice, *this, 1);

    return 0;
}

int Vector::rotation(Matrix rotationMatrice)
{
    multiplyMatriceVector(rotationMatrice, *this, 1);

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


float * Vector::multiplyMatriceVector(Matrix mat, Vector vec, int override)
{
    float * vec_trans = (float*)malloc(sizeof(float) * mat.getRow());
    for(int i = 0; i < mat.getRow(); i++) {
        vec_trans[i] = 0.0;
    }
    int arr_index = 0;
    int mat_index = 0;
    int vec_index = 0;
    for(mat_index = 1; mat_index <= mat.getRow() * mat.getColumn(); mat_index++)
    {
        vec_trans[arr_index] += mat.getMatrixValue(mat_index - 1) * vec.getVectorValue(vec_index);
        if(mat_index%mat.getColumn() == 0) vec_index++;
        if(vec_index == 2) arr_index++;
    }
    if(override) this->vector = vec_trans;

    return vec_trans;
}

void Vector::printVector()
{
    int i;
    printf("\n[");
    for(i = 0; i < length - 1; i++)
    {
        printf("%.2f ", vector[i]);
    }
    printf("%.2f]\n", vector[i]);
}

/**
 * getter- and setter-functions
*/
float * Vector::getVector()
{
    return vector;
}

float Vector::getVectorValue(int index)
{
    return this->vector[index];
}

int Vector::setVector(float * new_vec, int new_length)
{
    if(length != new_length) return 1;
    for(int i = 0; i < length; i++)
    {
        vector[i] = new_vec[i];
    }

    return 0;
}

int Vector::setVectorValue(float new_value, int index)
{
    if(index >= length) return 1;
    vector[index] = new_value;

    return 0;
}
