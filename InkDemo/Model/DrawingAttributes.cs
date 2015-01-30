//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using InkDemo.Common;

namespace InkDemo.Model
{
    /// <summary>
    /// A simple data model that tracks the drawing parameters for an inking surface.
    /// </summary>
    public class DrawingAttributes : BindableBase
    {
        public DrawingAttributes()
            : this(Colors.Black, new Size(4, 4), false)
        {
            PageName = "Blank";
        }

        public DrawingAttributes(string name) : this(Colors.Black, new Size(4, 4), false)
        {
            PageName = name;
        }

        public DrawingAttributes(Color brushColor, Size brushSize, bool brushFitsToCurve, [Optional] IEnumerable<Color> activePaletteColors)
        {
            _brushColor = brushColor;
            _brushSize = brushSize;
            _brushFitsToCurve = brushFitsToCurve;
            _displayText = false;
          
            
            SheetMusic = new SheetMusic();
            var newAttributes = new InkDrawingAttributes { FitToCurve = _brushFitsToCurve, Color = _brushColor, Size = _brushSize};
            InkStrokeManager.SetDefaultDrawingAttributes(newAttributes);
        }

        public void UpdateStrokeStyle(bool fitToCurve, Color brushColor, Size brushSize, bool displayText)
        {
            BrushColor = brushColor;
            BrushSize = brushSize;
            BrushFitsToCurve = fitToCurve;
            DisplayText = displayText;
            var newAttributes = new InkDrawingAttributes { FitToCurve = _brushFitsToCurve, Color = _brushColor, Size = _brushSize};
            InkStrokeManager.SetDefaultDrawingAttributes(newAttributes);
        }

        private SheetMusic _sheetMusic = default(SheetMusic);

        public SheetMusic SheetMusic
        {
            get { return _sheetMusic; }
            set
            {
                _sheetMusic = value;
                SetProperty(ref _sheetMusic, value);
            }
        }

        private IReadOnlyList<InkRecognitionResult> _recoResults = default(IReadOnlyList<InkRecognitionResult>);

        public IReadOnlyList<InkRecognitionResult> RecoResults
        {
            get { return _recoResults; }
            set
            {
                _recoResults = value;
                // set TextRecoResults
                string newResults = "";
                SheetMusic.RecognizedText.Clear();
                foreach (var result in _recoResults)
                {
                    var textstrings = result.GetTextCandidates();
                    var thistext = textstrings.FirstOrDefault();
                    newResults += thistext + " ";
                    SheetMusic.RecognizedText.Add(new TextResult((int) (result.BoundingRect.Left + result.BoundingRect.Width / 2.0), 
                        (int) (result.BoundingRect.Bottom),
                        thistext));
                }

                TextRecoResults = newResults;
                SetProperty(ref _recoResults, value);
            }
        }
        private string _textRecoResults = "";
        /// <summary>
        /// text recognition results
        /// </summary>
        public string TextRecoResults
        {
            get { return _textRecoResults; }
            set { SetProperty(ref _textRecoResults, value); }
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
                    var newAttributes = new InkDrawingAttributes {Color = BrushColor};
                    InkStrokeManager.SetDefaultDrawingAttributes(newAttributes);
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
                var newAttributes = new InkDrawingAttributes {FitToCurve = _brushFitsToCurve};
                InkStrokeManager.SetDefaultDrawingAttributes(newAttributes);
                SetProperty(ref _brushFitsToCurve, value);
            }
        }

        private string _pageName = default(string);

        public string PageName
        {
            get { return _pageName; }
        
            set { SetProperty(ref _pageName, value); }
            
        }

        private List<Point> _replayBuffer = new List<Point>();
        public List<Point> ReplayBuffer
        {
            get { return _replayBuffer; }
            set { SetProperty(ref _replayBuffer, value); }
        }

        private Point _previousContactPoint = default(Point);
        public Point PreviousContactPoint
        {
            get { return _previousContactPoint; }
            set { SetProperty(ref _previousContactPoint, value); }
        }

        private uint _penID = default(uint);
        public uint PenID
        {
            get { return _penID; }
            set { SetProperty(ref _penID, value); }
        }

        private InkManager _inkManager = new InkManager();
        public InkManager InkStrokeManager
        {
            get { return _inkManager; }
            set { SetProperty(ref _inkManager, value); }
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
                    var newAttributes = new InkDrawingAttributes { Size = _brushSize };
                    InkStrokeManager.SetDefaultDrawingAttributes(newAttributes);
                    SetProperty(ref _brushSize, value);
                }
            }
        }



    }

    /// <summary>
    /// A collection of supported drawing brush sizes.
    /// </summary>
    public class BrushSizesCollection : IReadOnlyCollection<Size>
    {
        private static readonly Size[] Sizes = { new Size(1, 1), new Size(2, 2), new Size(4, 4), new Size(10, 10), new Size(20, 20), new Size(40, 40) };
        
        public BrushSizesCollection()
        {
        }

        public int Count
        {
            get { return Sizes.Length; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Sizes.GetEnumerator();
        }

        IEnumerator<Size> IEnumerable<Size>.GetEnumerator()
        {
            return Sizes.Cast<Size>().GetEnumerator();
        }
    }


}
