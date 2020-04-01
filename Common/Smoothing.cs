using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOrganizerWinForms.Common
{
    public class Smoothing
    {
        double[] _Buffer;
        int _Size = 0; // array size
        int _Num = 0; // number of current buffer elements
        int _Index = 0; // current buffer element
        double _Sum = 0; // sum of buffer
        public double Median = 0;
        public Smoothing(int size = 100)
        {
            if (size <= 0) throw new Exception($"Buffer size must be positive: {size}");
            _Size = size;
            _Buffer = new double[size];
        }
        public void Add(double d)
        {
            // ring buffer
            _Sum -= _Buffer[_Index];
            _Buffer[_Index] = d;
            _Sum += d;

            // increase indices
            _Num = Math.Min(_Size, _Num + 1);
            _Index = (_Index + 1) % _Size;

            // calculate median
            Median = _Num == 0 ? 0 : _Sum / _Num;
            return;
        }
        public void Clear()
        {
            _Buffer = new double[_Size];
            _Num = 0;
            _Index = 0;
        }
    }
}
