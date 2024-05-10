class VoxelData:
    def __init__(self, numX, numY, numZ, sizeX, sizeY, sizeZ, borderSize, data):
        self.numX = numX
        self.numY = numY
        self.numZ = numZ
        self.sizeX = sizeX
        self.sizeY = sizeY
        self.sizeZ = sizeZ
        self.borderSize = borderSize
        self.data = data