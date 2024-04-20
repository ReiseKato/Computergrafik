#include "Matrix.hh"


//#define VECTOR_SIZE 2

class Vector
{
private:
    float * vector;
    int length;

public:
    Vector(float *, int);

    int translation(float *);
    int translation(Matrix);
    int skalierung(float);
    int sklaierung(float *);
    int skalierung(Matrix);
    int rotation(float, char);
    int rotation(Matrix);
    Matrix makeTranslationMatrice(float *);
    Matrix makeScalarMatrice(float *);
    Matrix makeRotationMatrice(float, char);
    float * multiplyMatriceVector(Matrix, Vector, int);
    void printVector();

    float * getVector();
    float getVectorValue(int);
    int setVector(float *, int);
    int setVectorValue(float, int);
};


