//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.StartScreen;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Microsoft.VisualBasic.CompilerServices;
using InkDemo.Common;


namespace InkDemo.Model
{
    public class SheetMusic : BindableBase
    {

        public SheetMusic()
        {
            StaffsPerPage = 8;
            PageHeight = 3300;
            PageWidth = 2400;
            Lines = new ObservableCollection<Tuple<int, int, int, int>>();
            RecognizedText = new ObservableCollection<TextResult>();
            BackgroundObjects = new ObservableCollection<object>();
            InkStrokeSegments = new ObservableCollection<Shape>();
            calculateBackground();
        }


        private int _staffsPerPage = default(int);

        public int StaffsPerPage
        {
            get { return _staffsPerPage; }
            set
            {
                _staffsPerPage = value;
                SetProperty(ref _staffsPerPage, value);
            }
        }

        private int _pageHeight = default(int);

        public int PageHeight
        {
            get { return _pageHeight; }
            set
            {
                _pageHeight = value;
                SetProperty(ref _pageHeight, value);
            }
        }

        private int _pageWidth = default(int);

        public int PageWidth
        {
            get { return _pageWidth; }
            set
            {
                _pageWidth = value;
                SetProperty(ref _pageWidth, value);
            }
        }

        private ObservableCollection<Shape> _inkStrokeSegments = default(ObservableCollection<Shape>);

        public ObservableCollection<Shape> InkStrokeSegments
        {
            get { return _inkStrokeSegments; }
            set
            {
                _inkStrokeSegments = value;
                SetProperty(ref _inkStrokeSegments, value);
            }
        }

        private void calculateBackground()
        {


            m_leftMarginPixel = (int) (PageWidth * leftMarginPct);
            m_rightMarginPixel = (int) (PageWidth * (1 - rightMarginPct));
            m_topMarginPixel = (int) (PageHeight * topMarginPct);
            m_bottomMarginPixel = (int) (PageHeight * (1 - bottomMarginPct));
            m_staffHeightPlusMargin = (int) (((1.0f - (topMarginPct + bottomMarginPct)) * PageHeight) / StaffsPerPage);


            // Since the unit mode is set to pixels in CreateDeviceResources(), here we scale the line thickness by the composition scale so that elements 
            // are rendered in the same position but larger as you zoom in. Whether or not the composition scale should be factored into the size or position 
            // of elements depends on the app's scenario.

            // Draw grid lines.
            for (int j = 0; j < StaffsPerPage; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    //var line = new Tuple<Point, Point>(
                    //    new Point(m_leftMarginPixel,
                    //        m_topMarginPixel + i*NOTE_HEIGHT + j*m_staffHeightPlusMargin),
                    //    new Point(m_rightMarginPixel,
                    //        m_topMarginPixel + i*NOTE_HEIGHT + j*m_staffHeightPlusMargin));
                        var line = new Tuple<int, int,int,int>(m_leftMarginPixel,
                        (m_topMarginPixel + i * NOTE_HEIGHT + j * m_staffHeightPlusMargin),
                            m_rightMarginPixel,
                            m_topMarginPixel + i * NOTE_HEIGHT + j * m_staffHeightPlusMargin);
                        BackgroundObjects.Add(line);

                }
            }

            // Draw Clefs
            for (int j = 0; j < StaffsPerPage; j++)
            {
                var clef = new Clef(m_leftMarginPixel + 25, m_topMarginPixel - 170 + j * m_staffHeightPlusMargin, 'G');
                BackgroundObjects.Add(clef);
            }

            // Draw Key

            // Draw Time Signature



        }

        private ObservableCollection<object> _backgroundObjects = default(ObservableCollection<object>);

        public ObservableCollection<object> BackgroundObjects
        {
            get { return _backgroundObjects; }
            set
            {
                _backgroundObjects = value;
                SetProperty(ref _backgroundObjects, value);
            }
        }

        private ObservableCollection<TextResult> _recognizedText = default(ObservableCollection<TextResult>);

        public ObservableCollection<TextResult> RecognizedText
        {
            get { return _recognizedText; }
            set
            {
                _recognizedText = value;
                SetProperty(ref _recognizedText, value);
            }
        }


        //public ObservableCollection<Tuple<Point, Point>> Lines = new ObservableCollection<Tuple<Point, Point>>();
        private ObservableCollection<Tuple<int,int,int,int>> _lines = default(ObservableCollection<Tuple<int,int,int,int>>);

        public ObservableCollection<Tuple<int,int,int,int>> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                SetProperty(ref _lines, value);
            }
        }

        private ObservableCollection<Note> _notes = default(ObservableCollection<Note>);

        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                SetProperty(ref _notes, value);
            }
        }

        private int _staffLength;
        private int _vertMargin;
        private int _horizMargin;
        private int _staffSpacing;
        private int _insertLocation;

        private int m_leftMarginPixel ;
        private int m_rightMarginPixel ;
        private int m_topMarginPixel ;
        private int m_bottomMarginPixel ;
        private int m_staffHeightPlusMargin ;

        const int NOTE_HEIGHT = 30;
        const float STAFF_HEIGHT = NOTE_HEIGHT * 5;
        //const float SNAP_TOLERANCE = 30.0f;
        //private float VERTICAL_LINE_TOLERANCE = PageWidth / 100;

        const float topMarginPct = 0.1f;
        const float bottomMarginPct = 0.1f;
        const float leftMarginPct = 0.05f;
        const float rightMarginPct = 0.05f;


    }
}
