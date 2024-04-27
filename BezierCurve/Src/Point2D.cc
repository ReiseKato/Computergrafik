#include "Point2D.hh"

Point2D::Point2D()
{
    m_x = 0.0;
    m_y = 0.0;
}

Point2D::Point2D(double x, double y)
{
    m_x = x;
    m_y = y;
}

double Point2D::getX()
{
    return m_x;
}

double Point2D::getY()
{
    return m_y;
}

void Point2D::setX(double x)
{
    m_x = x;
}

void Point2D::setY(double y)
{
    m_y = y;
}
