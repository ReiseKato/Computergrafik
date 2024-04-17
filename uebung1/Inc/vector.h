#include "matrix.h"


//#define VECTOR_SIZE 2

class Vector
{
private:
    float * vector;
    int length;

public:
    Vector(float *, int);

    int translation(float *);
    int skalierung(float);
    int rotation(Matrix);
    Matrix makeTranslationMatrice(float *);
    Matrix makeScalarMatrice(float *);
    Matrix makeRotationMatrice(float, char);
    void printVector();

    int getVector(float *);
    float getVectorValue(int);
    int setVector(float *);
};


