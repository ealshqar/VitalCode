using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.UI.Enums;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmFreeMeter : Form
    {
        public frmFreeMeter()
        {
            InitializeComponent();
        }


        #region HW_Logic

        /// <summary>
        /// Start the reading.
        /// </summary>
        private void StartReading()
        {
            CsaEmdUnitManager.Instance.Clear();

            ResetHardwareHandlers();

            CsaEmdUnitManager.Instance.StartReading();
        }

        /// <summary>
        /// Reset the hardware handlers.
        /// </summary>
        private void ResetHardwareHandlers()
        {
            RemoveHardwareHandlers();
            AddHardwareHandlers();
        }

        /// <summary>
        /// Remove the hardware handlers.
        /// </summary>
        private void RemoveHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged -= _csaManager_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone -= _csaManager_ReadingDone;
        }

        /// <summary>
        /// Add the hardware handlers.
        /// </summary>
        private void AddHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged += _csaManager_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone += _csaManager_ReadingDone;
        }

        /// <summary>
        /// Stop the reading.
        /// </summary>
        private void StopReading()
        {
            CsaEmdUnitManager.Instance.StopReading();
            RemoveHardwareHandlers();
        }

        #endregion

        #region Events

        #region HW_Events

        /// <summary>
        /// Handel the meter value changed.
        /// </summary>
        void _csaManager_ReadingDone(object sender, int reading, int min, int max, int fall, int rise)
        {
            _csaManager_MeterValueChanged(sender, reading, min, max);

            var csaManager = sender as CsaEmdUnitManager;

            if (csaManager != null && csaManager.IsReadingOn == false) return;

            if (InvokeRequired)
            {
                Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ReadingDone), sender, reading, min, max, fall, rise);
            }
            else
            {
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                StopReading();

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.WaitingToRelease, string.Empty, 0);
            }
        }

        /// <summary>
        /// Handel the reading done event.
        /// </summary>
        void _csaManager_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(_csaManager_MeterValueChanged), sender,
                           reading, min, max);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                xtraUserControlReadingGaugeScheduleLine.ReadingValue = reading;

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.Reading, string.Empty, 0);

            }

        }

        /// <summary>
        /// Handel the CSA released  event.
        /// </summary>
        /// <param name="sender">The sender as CSA manager.</param>
        void Csa_Instance_Released(object sender)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReleased(Csa_Instance_Released), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //This check for prevent the released [@UI level only] action get executing twice.
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                xtraUserControlReadingGaugeScheduleLine.SetReadingStatusBarMode(TestBarStateEnum.TakeReading, string.Empty, 0);

                StartReading();

            }
        }

        #endregion

        #region UI_Events

        /// <summary>
        /// Handel the form closing to stop the reading and remove the events handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormProductDosages_FormClosing(object sender, FormClosingEventArgs e)
        {
            CsaEmdUnitManager.Instance.DisposeConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
        }

        /// <summary>
        /// Handel the form load to start reading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFreeMeter_Load(object sender, EventArgs e)
        {
            CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_ReadingDone, _csaManager_MeterValueChanged);
            StartReading();
        }

        #endregion

        

        #endregion
    }
}
