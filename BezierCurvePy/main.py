'''
author: Kato, Thomas Reise
date: 27.04.2024
'''


import matplotlib.pyplot as plt
import math
import numpy as np

def bernsteinpolynom(t, i, n):
    '''
    calculate the value of Bernstein polynomial for given t, i, n

    :param t: percentage of curve -> [0,1]
    :param i: index
    :param n: degree of Bernstein polynomial
    :return: value of bernstein polynomial for given t, i, n
    '''
    return math.comb(n, i) * math.pow(t, i) * math.pow(1 - t, n - i)

def bezier_curve(control_points: np.array, num_points=100):
    '''
    This function calculates points on the Bezier curve. To calculate the points the function needs control points
    and the number of points the user wants to have calculated. The default is set to 100. The higher the number,
    the more accurate the curve is.

    :param control_points: control points. should be of type np.array
    :param num_points: number of points to calculate on the Bezier curve
    :return: 2-dimensional np.array of points on the Bezier curve of given control points
    '''
    n = len(control_points) - 1
    bezier = np.zeros((num_points + 1, 2)) # empty numpy for curve points with dimension [control_points, dimension]

    for i in range(num_points + 1):
        t = i / num_points
        bezier[i] = sum(control_points[j] * bernsteinpolynom(t, j, n) for j in range(n + 1))

    return bezier

def bezier_curve_given_t(control_points: np.array, t):
    '''
    This function calculates the point on the Bezier curve of given parameter t and control points.

    :param control_points: 2-dimensional np.array
    :param t: percentage of curve -> [0,1]
    :return: point on Bezier curve at t
    '''
    n = len(control_points) - 1
    return sum(control_points[i] * bernsteinpolynom(t, i, n) for i in range(n + 1))
def bezier_surface(control_points: np.array, num_points_u=100, num_points_v=100):
    '''
    This function calculates the Bezier surface based on a set of control points. Similar to Bezier curve.

    :param control_points: 3-dimensional np.array
    :param num_points_u: horizontal parameter to surface
    :param num_points_v: vertical parameter to surface
    :return: 3-dimensional np.array of points on the Bezier surface
    '''
    n, m, _ = control_points.shape
    n -= 1
    m -= 1
    bezier = np.zeros((num_points_u + 1, num_points_v + 1, 3)) # empty array for surface

    for i in range(num_points_u + 1):
        for j in range(num_points_v + 1):
            u, v = i / num_points_u, j / num_points_v
            bezier[i, j] = sum(sum(control_points[k, l] * bernsteinpolynom(u, k, n) * bernsteinpolynom(v, l, m)
                                   for k in range(n + 1)) for l in range(m + 1))

    return bezier



if __name__ == '__main__':
    '''
    1. create control points for Bezier curve or surface
    2. calculate the points on the curve or surface
    3. plot these points using matplotlib (alternative: control points -> scatter plot)
    '''
    # make control points for Bezier curve
    control_points = np.array([[0, 0], [-100, 700], [0, 800], [600, 600], [600, 500], [-800, 300], [500, 0]]) # "R"
    # make control points for bezier surface
    control_points_surface = np.array([[[0, 0, 0], [100, 0, 100], [200, 0, 0]], # curve one
                                       [[-50, 70, -30], [50, 60, 40], [150, 70, 40]], # points to bend surface in the middle (not necessarily needed)
                                       [[0, 150, 0], [100, 150, -100], [200, 150, 0]]]) # curve two
    # contact surface control points: [0, 0, 0], [0, 0, 2], [0, 2, 2], [2, 2, 2]

    curve = bezier_curve(control_points)
    surface = bezier_surface(control_points_surface)
    # print(bezier_curve_given_t(control_points, 0.5)) # calculate point on bezier curve for given t

    # start of plotting Bezier Curve
    # coord for Bezier curve
    x_curve = curve[:, 0]
    y_curve = curve[:, 1]

    plt.plot(x_curve, y_curve, label="Bezier Curve")
    plt.scatter(control_points[:, 0], control_points[:, 1], c='red', label="control points")
    plt.legend()
    plt.xlabel("X")
    plt.ylabel("Y")

    # start of plotting Bezier Surface
    fig = plt.figure()
    ax = fig.add_subplot(projection='3d')

    # coord of Bezier surface
    x_surface = surface[:, :, 0]
    y_surface = surface[:, :, 1]
    z_surface = surface[:, :, 2]

    ax.plot_surface(x_surface, y_surface, z_surface, cmap='inferno', label="Bezier Surface")
    # ax.plot_wireframe(x_surface, y_surface, z_surface, label="Bezier Surface (wireframe) # wireframe (can be plotted on top of each other)

    ax.scatter(control_points_surface[:, :, 0], control_points_surface[:, :, 1], control_points_surface[:, :, 2],
               c='red', label="control points")

    ax.set_xlabel('X')
    ax.set_ylabel('Y')
    ax.set_zlabel('Z')
    ax.legend()

    plt.show()


