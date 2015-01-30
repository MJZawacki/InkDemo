using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InkDemo.Common;

namespace InkDemo.Model
{
    public class Note : BindableBase
    {

        private string _code = default(string);

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                SetProperty(ref _code, value);
            }
        }

        private int _length = default(int);

        public int Length
        {
            get { return _length; }
            set
            {
                _length = value;
                SetProperty(ref _length, value);
            }
        }

        private long _offset = default(long);

        public long Offset
        {
            get { return _offset; }
            set
            {
                _offset = value;
                SetProperty(ref _offset, value);
            }
        }

        private int _x = default(int);

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                SetProperty(ref _x, value);
            }
        }

        private int _y = default(int);

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                SetProperty(ref _y, value);
            }
        }

    }
}
