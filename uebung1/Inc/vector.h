#include "matrix.h"


//#define VECTOR_SIZE 2

class Vector
{
private:
    float * vector;

public:
    Vector(float *);

    int translation(float *);
    int skalierung(float);
    int rotation(Matrix);
    //float makeTransformationMatrice(float *);
    void printVector();

    int getVector(float *);
    float getVectorValue(int);
    int setVector(float *);
};


