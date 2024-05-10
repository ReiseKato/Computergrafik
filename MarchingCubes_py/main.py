'''
date: 08.05.2024
author: Kato, Thomas Reise
'''

import numpy as np
import io

import VoxelData as voxel

def convert_data(filename):
    return np.fromfile(filename, dtype='>f')

if __name__ == '__main__':
    filename = "Frog.vol"
    vd = voxel.VoxelData(1, 2, 3, 4, 5, 6, 7, 8)
    sc_data = convert_data(filename)
    #br = io.BufferedReader(sc_data)
    print(sc_data)
