#include <stdio.h>
#include <math.h>
#include <windows.h>
#include <wingdi.h>
#include "main.hh"

int main()
{
    // create window
    //HDC hdc = GetDC(GetConsoleWindow());
    
    // init points
    Point2D points[4];
    points[0] = Point2D(50, 50);
    points[1] = Point2D(100, 200);
    points[2] = Point2D(200, 200);
    points[3] = Point2D(250, 50);

    // specify values for Bezier Curve
    int numberOfControlPoints = 8;
    int numberOfBezierPoints = 20;
    int n_grad = numberOfControlPoints - 1;
    Point2D bezierPoints[numberOfBezierPoints + 1];
    // calculate Bezier curve/points on curve
    for(int i = 0; i <= numberOfBezierPoints; i++)
    {
        double t = double(i)/double(numberOfBezierPoints); // get t for Bezier Point
        Point2D bezierPoint = calculateBezierPoint(t, n_grad, points);
        printf("Punkt bei t = %.2lf: (%.2lf, %.2lf)\n", t, bezierPoint.getX(), bezierPoint.getY());
        bezierPoints[i] = bezierPoint;
    }

    // uebung 2
    double t;
    t = 0.5;
    Point2D bezierPoint = calculateBezierPoint(t, n_grad, points);
    printf("Punkt bei t = %.2lf: (%.2lf, %.2lf)\n", t, bezierPoint.getX(), bezierPoint.getY());

    return 0;
}

int factorial(int n) 
{
    if (n == 0 || n == 1) return 1;
    return n * factorial(n - 1);
}

int binomialCoefficient(int n, int k)
{
    return factorial(n) / (factorial(k) * factorial(n - k));
}

double bernsteinpolynom(double t, int i, int n_grad)
{
    return binomialCoefficient(n_grad, i) * pow(t, i) * pow(1 - t, n_grad - i);
}

Point2D calculateBezierPoint(double t, int numberOfControlPoints, Point2D * points)
{
    int n_grad = numberOfControlPoints - 1;
    double x = 0.0, y = 0.0;
    for(int i = 0; i <= n_grad; i++)
    {
        x += points[i].getX() * bernsteinpolynom(t, i, n_grad);
        y += points[i].getY() * bernsteinpolynom(t, i, n_grad);
    }
    return Point2D(x, y);
}

