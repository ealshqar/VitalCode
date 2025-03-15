using System;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Vital.UI.Properties;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlRealTimeChart : XtraUserControl
    {
        #region Constructors

        /// <summary>
        /// The constructor.
        /// </summary>
        public XtraUserControlRealTimeChart()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Variables
        
        private const int TimeInterval = 10;
        private double _valueToBeDrawn = 10.0;
        private double _valueFromMeter;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool IsPlaying
        {
            get; set;
        }

        /// <summary>
        /// Gets the interval.
        /// </summary>
        private static int Interval
        {
            get { return 40; }
        }

        /// <summary>
        /// The X Axis of the plot.
        /// </summary>
        AxisRange AxisXRange
        {
            get
            {
                var diagram = chartControlReadings.Diagram as SwiftPlotDiagram;
                return diagram != null ? diagram.AxisX.Range : null;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            timerChart.Stop();
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            timerChart.Start();
        }

        /// <summary>
        /// Sets the meter value to be drawn.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetNextValue(double value)
        {
            _valueFromMeter = value;

            if(!IsPlaying)
            {
                _valueToBeDrawn = _valueFromMeter;
                TickLogic();
            }
                
        }

        /// <summary>
        /// Pauses the timeline.
        /// </summary>
        public void PauseTimeline()
        {
            simpleButtonPlayPauseTimeline.Image = Resources.play_timeline;
            StopTimer();
            IsPlaying = false;
        }

        /// <summary>
        /// Plays the timeline.
        /// </summary>
        public void PlayTimeline()
        {
            simpleButtonPlayPauseTimeline.Image = Resources.pause_timeline;
            StartTimer();
            IsPlaying = true;
        }

        /// <summary>
        /// The logic of drawing the points
        /// </summary>
        private void TickLogic()
        {
            var chartSeries = chartControlReadings.Series[0];

            if (chartSeries == null)
                return;

            var argument = DateTime.Now;

            var pointsToUpdate = new SeriesPoint[Interval];

            for (var i = 0; i < Interval; i++)
            {
                pointsToUpdate[i] = new SeriesPoint(argument, _valueToBeDrawn);
                argument = argument.AddMilliseconds(1);
                UpdateValues();
            }

            var minimumDate = argument.AddSeconds(-TimeInterval);

            var pointsToRemoveCount = 0;

            foreach (SeriesPoint point in chartSeries.Points)
                if (point.DateTimeArgument < minimumDate)
                    pointsToRemoveCount++;

            if (pointsToRemoveCount < chartSeries.Points.Count)
                pointsToRemoveCount--;

            chartSeries.Points.AddRange(pointsToUpdate);

            if (pointsToRemoveCount > 0)
            {
                chartSeries.Points.RemoveRange(0, pointsToRemoveCount);
            }

            if (AxisXRange != null)
            {
                AxisXRange.SetMinMaxValues(minimumDate, argument);
            }
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the values.
        /// </summary>
        private void UpdateValues()
        {
            _valueToBeDrawn = _valueFromMeter;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the tick event for the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerChart_Tick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(timerChart_Tick), sender, e);
                }
                catch
                {
                }
            }
            else
            {
                TickLogic();
            }
            
        }

        /// <summary>
        /// Handles the click on the play/pause button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonPlayPauseTimeline_Click(object sender, EventArgs e)
        {
            if(IsPlaying)
            {
                PauseTimeline();
            }
            else
            {
                PlayTimeline();
            }
        }

        #endregion
    }
}
