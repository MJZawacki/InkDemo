//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InkDemo.Common;

namespace InkDemo.Model
{
    public class StaffLine : BindableBase
    {

        private Tuple<int, int, int, int> _line = default(Tuple<int, int, int, int>);

        public Tuple<int, int, int, int> Line
        {
            get { return _line; }
            set
            {
                _line = value;
                SetProperty(ref _line, value);
            }
        }
    }
}
