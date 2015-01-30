//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Input.Inking;
using InkDemo.Common;

namespace InkDemo.Model
{
    public class PenModel : BindableBase
    {

        public PenModel()
        {
            _brushSizes = new BrushSizesCollection();
            _brushColor = Colors.Black;
            _brushSize = new Size(4, 4);
            _brushFitsToCurve = false;
            _displayText = false;
        }

        private double _penX = default(double);

        public double PenX
        {
            get { return _penX; }
            set
            {
                _penX = value;
                SetProperty(ref _penX, value);
            }
        }

        private double _penY = default(double);

        public double PenY
        {
            get { return _penY; }
            set
            {
                _penY = value;
                SetProperty(ref _penY, value);
            }
        }

        private float _pressure = default(float);

        public float Pressure
        {
            get { return _pressure; }
            set
            {
                _pressure = value;
                SetProperty(ref _pressure, value);
            }
        }

        private int _lineSegments = default(int);

        public int LineSegments
        {
            get { return _lineSegments; }
            set
            {
                _lineSegments = value;
                SetProperty(ref _lineSegments, value);
            }
        }

        private int _bufferSize = default(int);

        public int BufferSize
        {
            get { return _bufferSize; }
            set
            {
                _bufferSize = value;
                SetProperty(ref _bufferSize, value);
            }
        }


        private bool _brushIsEraser = false;
        /// <summary>
        /// Whether the brush is in erase mode.
        /// </summary>
        public bool BrushIsEraser
        {
            get { return _brushIsEraser; }
            set { SetProperty(ref _brushIsEraser, value); }
        }

        private bool _displayText = default(bool);

        public bool DisplayText
        {
            get { return _displayText; }
            set
            {
                _displayText = value;
                SetProperty(ref _displayText, value);
            }
        }

        private Color _brushColor = Colors.Black;
        /// <summary>
        /// Color of the current drawing brush.
        /// </summary>
        public Color BrushColor
        {
            get { return _brushColor; }
            set
            {
                if (value != default(Color))
                {
                    SetProperty(ref _brushColor, value);
                }
            }
        }

        private bool _brushFitsToCurve = false;
        /// <summary>
        /// Whether the current drawing brush smooths strokes.
        /// </summary>
        public bool BrushFitsToCurve
        {
            get { return _brushFitsToCurve; }
            set
            {
                SetProperty(ref _brushFitsToCurve, value);
            }
        }

        private Size _brushSize;
        /// <summary>
        /// Size of the current drawing brush.
        /// </summary>
        public Size BrushSize
        {
            get { return _brushSize; }
            set
            {
                if (value != new Size(0, 0))
                {
                    SetProperty(ref _brushSize, value);
                }
            }
        }

        private bool _debugFlag = default(bool);

        public bool DebugFlag
        {
            get { return _debugFlag; }
            set
            {
                _debugFlag = value;
                SetProperty(ref _debugFlag, value);
            }
        }

        private BrushSizesCollection _brushSizes = null;
        /// <summary>
        /// All supported sizes for a drawing brush.
        /// </summary>
        public BrushSizesCollection BrushSizes
        {
            get { return _brushSizes; }
        }

        private bool _isErasing = default(bool);

        public bool IsErasing
        {
            get { return _isErasing; }
            set
            {
                _isErasing = value;
                SetProperty(ref _isErasing, value);
            }
        }

        
    }
}
