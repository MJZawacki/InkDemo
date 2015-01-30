using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Windows.UI.Xaml.Shapes;
using InkDemo.Model;

namespace InkDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static MainPage Current;


        public MainPage()
        {
            this.InitializeComponent();

            // Initialize View Models
            _penmodel = new PenModel();
            this.DataContext = _penmodel;
            _penmodel.PropertyChanged +=_penmodel_PropertyChanged;
            _renderingPanelAttributes = new ObservableCollection<DrawingAttributes>();
            _renderingPanelAttributes.Add(new DrawingAttributes("Page 1"));
            _renderingPanelAttributes.Add(new DrawingAttributes("Page 2"));
            
            Current = this;


            // Set up Timer
            this.dispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            this.dispatcherTimer.Tick += dispatcherTimer_Tick;
        }
        private async void dispatcherTimer_Tick(object sender, object e)
        {
            this.dispatcherTimer.Stop();
            
            await this.RecognizeText();
        }

        private async Task RecognizeText()
        {
            if (_selectedItem.InkStrokeManager.GetStrokes().Count > 0)
                _selectedItem.RecoResults = await _selectedItem.InkStrokeManager.RecognizeAsync(InkRecognitionTarget.All);
        }
        /// <summary>
        /// Send the new drawing properties to the current page in view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _penmodel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // push changes to Drawing View model
            _selectedItem.UpdateStrokeStyle(_penmodel.BrushFitsToCurve, _penmodel.BrushColor, _penmodel.BrushSize, _penmodel.DisplayText);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DrawingsView.ItemsSource = _renderingPanelAttributes;
        }



        /// <summary>
        /// Update the page view model when it is brought into view (e.g. selected in the FlipView)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = (DrawingAttributes) ((FlipView) sender).SelectedItem;
            if (_selectedItem == null)
                return;
            var _container =
                ((FlipView) sender).ItemContainerGenerator.ContainerFromItem(((FlipView) sender).SelectedItem);

            // Update new selectedItem with drawing attributes
            _selectedItem.UpdateStrokeStyle(_penmodel.BrushFitsToCurve,_penmodel.BrushColor, _penmodel.BrushSize, _penmodel.DisplayText);
        }



        /// <summary>
        /// Start Tracking input devices that are being used to draw or erase ink data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkPanel_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var InkCanvas = sender as Canvas;
            dispatcherTimer.Stop();


            // Get information about the pointer location.
            PointerPoint pt = e.GetCurrentPoint(InkCanvas);
            _selectedItem.PreviousContactPoint = pt.Position;
            // Accept input only from a pen or mouse with the left button pressed. 
            PointerDeviceType pointerDevType = e.Pointer.PointerDeviceType;
            if (pointerDevType == PointerDeviceType.Pen ||
                    pointerDevType == PointerDeviceType.Mouse &&
                    pt.Properties.IsLeftButtonPressed)
            {

                if (pt.Properties.IsEraser)
                {
                    _selectedItem.InkStrokeManager.Mode = InkManipulationMode.Erasing;
                }
                else
                {
                    _selectedItem.InkStrokeManager.Mode = InkManipulationMode.Inking;
                }

                // Pass the pointer information to the InkManager.
                _selectedItem.InkStrokeManager.ProcessPointerDown(pt);

                // Track just this PointerId for inking
                _selectedItem.PenID = pt.PointerId;  
                e.Handled = true;
            }
            
            else if (pointerDevType == PointerDeviceType.Touch)
            {
                // Process touch input
            }
        }

        private void InkPanel_OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var InkCanvas = sender as Canvas;

            
            if (e.Pointer.PointerId == _selectedItem.PenID)
            {
                PointerPoint pt = e.GetCurrentPoint(InkCanvas);

                Point currentContactPt = pt.Position;

                if (_selectedItem.InkStrokeManager.Mode == InkManipulationMode.Inking)
                {

                        if (Distance(currentContactPt, _selectedItem.PreviousContactPoint) > _resolution)
                        {
                            Line line = new Line()
                            {
                                X1 = _selectedItem.PreviousContactPoint.X,
                                Y1 = _selectedItem.PreviousContactPoint.Y,
                                X2 = currentContactPt.X,
                                Y2 = currentContactPt.Y,
                                StrokeThickness = _selectedItem.BrushSize.Height * pt.Properties.Pressure,
                                Stroke = new SolidColorBrush(Windows.UI.Colors.Black),
                                StrokeEndLineCap = PenLineCap.Round

                            };

                 
                            _selectedItem.SheetMusic.InkStrokeSegments.Add(line);
         

                            _selectedItem.PreviousContactPoint = currentContactPt;

                            if (_penmodel.DebugFlag)
                            {
                                var width = _selectedItem.BrushSize.Width * pt.Properties.Pressure * 1.2;
                                var height = _selectedItem.BrushSize.Height * pt.Properties.Pressure * 1.2;
                                Ellipse ellipse = new Ellipse()
                                {
                                    Width = width,
                                    Height = height,
                                    Stroke = new SolidColorBrush(Windows.UI.Colors.Red),
                                    Fill = new SolidColorBrush(Windows.UI.Colors.Red)

                                };

                                // Draw the line on the canvas by adding the Line object as
                                // a child of the Canvas object.

                                Canvas.SetLeft(ellipse, currentContactPt.X - width / 2);
                                Canvas.SetTop(ellipse, currentContactPt.Y - height / 2);
                                Canvas.SetZIndex(ellipse, 99);
                                _selectedItem.SheetMusic.InkStrokeSegments.Add(ellipse);
                            }

                            // Pass the pointer information to the InkManager.
                            _selectedItem.InkStrokeManager.ProcessPointerUpdate(pt);
                        }
                    
                } // if Inking
                else if (_selectedItem.InkStrokeManager.Mode == InkManipulationMode.Erasing)
                {
                    var rect = (Windows.Foundation.Rect) _selectedItem.InkStrokeManager.ProcessPointerUpdate(pt);
                    if (rect.Height != 0 && rect.Width != 0)
                    {
                        // Need to clear canvas and re-add the existing lines
                        _selectedItem.SheetMusic.InkStrokeSegments.Clear();
                        RenderAllStrokes();
                    }
                }
            }

            e.Handled = true;

        }

        private void InkPanel_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var InkCanvas = sender as Canvas;

            if (e.Pointer.PointerId == _selectedItem.PenID)
            {
                PointerPoint pt = e.GetCurrentPoint(InkCanvas);

                // Send pointer data to InkManager
                _selectedItem.InkStrokeManager.ProcessPointerUp(pt);
            }

            _selectedItem.PenID = 0;
            _selectedItem.InkStrokeManager.Mode = InkManipulationMode.Inking;

            e.Handled = true;
            dispatcherTimer.Start();
        }

        private void RenderAllStrokes()
        {
            var strokes = _selectedItem.InkStrokeManager.GetStrokes();
            if (strokes.Count > 0)
            {
        
                foreach (var stroke in strokes)
                {
                    if (stroke.DrawingAttributes.FitToCurve)
                        RenderStrokeBezier(stroke);
                    else
                        RenderStroke(stroke);
                }
            }

        }

        private void RenderStroke(InkStroke stroke)
        {
            var segments = stroke.GetRenderingSegments();
            Point previousPoint;
            bool first = true;

            foreach (var segment in segments)
            {
                if (first)
                {
                    previousPoint = segment.Position;
                    first = false;
                }
                else
                {
                    var currentContactPt = segment.Position;
                    var line = new Line()
                        {
                            X1 = previousPoint.X,
                            Y1 = previousPoint.Y,
                            X2 = currentContactPt.X,
                            Y2 = currentContactPt.Y,
                            StrokeThickness = stroke.DrawingAttributes.Size.Height * segment.Pressure,
                            Stroke = new SolidColorBrush(Windows.UI.Colors.Black),
                            StrokeEndLineCap = PenLineCap.Round
                        };

                    _selectedItem.SheetMusic.InkStrokeSegments.Add(line);

                    _selectedItem.PreviousContactPoint = currentContactPt;

                    if (_penmodel.DebugFlag)
                    {
                        var width = stroke.DrawingAttributes.Size.Width * segment.Pressure * 1.2;
                        var height = stroke.DrawingAttributes.Size.Height * segment.Pressure * 1.2;
                        Ellipse ellipse = new Ellipse()
                        {
                            Width = width,
                            Height = height,
                            Stroke = new SolidColorBrush(Windows.UI.Colors.Red),
                            Fill = new SolidColorBrush(Windows.UI.Colors.Red)

                        };

                        // Draw the line on the canvas by adding the Line object as
                        // a child of the Canvas object.

                        Canvas.SetLeft(ellipse, currentContactPt.X - width / 2);
                        Canvas.SetTop(ellipse, currentContactPt.Y - height / 2);
                        Canvas.SetZIndex(ellipse, 99);
                        _selectedItem.SheetMusic.InkStrokeSegments.Add(ellipse);
                    }

                    previousPoint = currentContactPt;
                }
           }
                               
        }


        private void RenderStrokeBezier(InkStroke inkStroke)
        {


                PathGeometry pathGeometry = new PathGeometry();
                PathFigureCollection pathFigures = new PathFigureCollection();
                PathFigure pathFigure = new PathFigure();
                PathSegmentCollection pathSegments = new PathSegmentCollection();

                // Create a path and define its attributes.
                Windows.UI.Xaml.Shapes.Path path = new Windows.UI.Xaml.Shapes.Path();
                path.Stroke = new SolidColorBrush(Colors.Red);
                path.StrokeThickness = _selectedItem.BrushSize.Height;

                // Get the stroke segments.
                IReadOnlyList<InkStrokeRenderingSegment> segments;
                segments = inkStroke.GetRenderingSegments();

                // Process each stroke segment.
                bool first = true;
                foreach (InkStrokeRenderingSegment segment in segments)
                {
                    // The first segment is the starting point for the path.
                    if (first)
                    {
                        pathFigure.StartPoint = segment.BezierControlPoint1;
                        first = false;
                    }

                    // Copy each ink segment into a bezier segment.
                    BezierSegment bezSegment = new BezierSegment();
                    bezSegment.Point1 = segment.BezierControlPoint1;
                    bezSegment.Point2 = segment.BezierControlPoint2;
                    bezSegment.Point3 = segment.Position;

                    // Add the bezier segment to the path.
                    pathSegments.Add(bezSegment);
                }

                // Build the path geometerty object.
                pathFigure.Segments = pathSegments;
                pathFigures.Add(pathFigure);
                pathGeometry.Figures = pathFigures;

                // Assign the path geometry object as the path data.
                path.Data = pathGeometry;

                // Render the path by adding it as a child of the Canvas object.
                _selectedItem.SheetMusic.InkStrokeSegments.Add(path);
            
        }



        private double Distance(Point currentContact, Point previousContact)
        {
            return Math.Sqrt(Math.Pow(currentContact.X - previousContact.X, 2) +
                    Math.Pow(currentContact.Y - previousContact.Y, 2));
        }



        private DrawingAttributes _selectedItem;
        private Canvas _selectedCanvas;
        private ObservableCollection<DrawingAttributes> _renderingPanelAttributes;
        private PenModel _penmodel; // Used for live ink stats
        private DispatcherTimer dispatcherTimer;
         
        const double _resolution = 0.1;


        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            _selectedItem.SheetMusic.InkStrokeSegments.Clear();
        }

        private void ReplayButton_OnClickButton_OnClick(object sender, RoutedEventArgs e)
        {
            RenderAllStrokes();
        }
    }
}
