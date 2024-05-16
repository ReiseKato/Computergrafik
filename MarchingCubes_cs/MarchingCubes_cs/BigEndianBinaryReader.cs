using System;
using System.IO;


// Credit: https://stackoverflow.com/questions/8620885/c-sharp-binary-reader-in-big-endian

class BigEndianBinaryReader : BinaryReader { 
    public BigEndianBinaryReader(System.IO.Stream stream)  : base(stream) { }

    public override int ReadInt32()
    {
        var data = base.ReadBytes(4);
        Array.Reverse(data);
        return BitConverter.ToInt32(data, 0);
    }

    public Int16 ReadInt16()
    {
        var data = base.ReadBytes(2);
        Array.Reverse(data);
        return BitConverter.ToInt16(data, 0);
    }

    public Int64 ReadInt64()
    {
        var data = base.ReadBytes(8);
        Array.Reverse(data);
        return BitConverter.ToInt64(data, 0);
    }

    public UInt32 ReadUInt32()
    {
        var data = base.ReadBytes(4);
        Array.Reverse(data);
        return BitConverter.ToUInt32(data, 0);
    }

}