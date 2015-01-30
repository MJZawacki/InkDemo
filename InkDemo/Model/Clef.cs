//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InkDemo.Common;

namespace InkDemo.Model
{
    public class Clef : BindableBase
    {


        public Clef()
        {
            
        }

        public Clef(int x, int y, char type)
        {
            X = x;
            Y = y;
            ClefType = type;
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
        // G or ?
        private char _clefType = default(char);

        public char ClefType
        {
            get { return _clefType; }
            set
            {
                _clefType = value;
                SetProperty(ref _clefType, value);
            }
        }
    }
}
