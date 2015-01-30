using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InkDemo.Common;

namespace InkDemo.Model
{
    public class TextResult : BindableBase
    {


        public TextResult()
        {
            
        }

        public TextResult(int x, int y, string text)
        {
            X = x;
            Y = y;
            Text = text;
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
        private string _text   = default(string);

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                SetProperty(ref _text, value);
            }
        }
    }
}
