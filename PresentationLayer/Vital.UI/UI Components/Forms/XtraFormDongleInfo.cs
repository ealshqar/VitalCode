using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormDongleInfo : DevExpress.XtraEditors.XtraForm
    {
        #region Private Variables

        #endregion

        #region Constructors

        public XtraFormDongleInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region Logic

        /// <summary>
        /// Check for dongle expiry
        /// </summary>
        private void CheckDongleExpiry()
        {
            var showDongleResetReminder = UiHelperClass.DongleState == DongleState.LessThanTenDays ||
                                          UiHelperClass.DongleState == DongleState.LessThanFiveDays;

            layoutControlGroupDongleResetReminder.Visibility = showDongleResetReminder ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        /// <summary>
        /// Show dongle reset dialog
        /// </summary>
        private void ShowDongleResetDialog()
        {
            Close();

            var xtraFormDongleReset = new XtraFormDongleReset(false);

            var result = xtraFormDongleReset.ShowDialog();

            if (result == DialogResult.OK)
            {
                UiHelperClass.ReadDongleInfo();
            }
        }

        #endregion

        #region Remaining Time Logic

        /// <summary>
        /// Get readable timespan
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public string GetReadableTimespan(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return "None";
            }

            var ts = endDate.Subtract(startDate);

            // formats and its cutoffs based on totalseconds
            var cutoff = new SortedList<long, string> { 
                   {60, "{3:S}" },
                   {60*60-1, "{2:M}, {3:S}"},
                   {60*60, "{1:H}"},
                   {24*60*60-1, "{1:H}, {2:M}"},
                   {24*60*60, "{0:D}"},
                   {Int64.MaxValue , "{0:D}, {1:H}"}
        };

            // find nearest best match
            var find = cutoff.Keys.ToList()
                          .BinarySearch((long)ts.TotalSeconds);
            // negative values indicate a nearest match
            var near = find < 0 ? Math.Abs(find) - 1 : find;
            // use custom formatter to get the string
            return String.Format(
                new HMSFormatter(),
                cutoff[cutoff.Keys[near]],
                ts.Days,
                ts.Hours,
                ts.Minutes,
                ts.Seconds);
        }

        /// <summary>
        /// formatter for forms of seconds/hours/day
        /// </summary>
        public class HMSFormatter : ICustomFormatter, IFormatProvider
        {
            // list of Formats, with a P customformat for pluralization
            static Dictionary<string, string> timeformats = new Dictionary<string, string> {
        {"S", "{0:P:Seconds:Second}"},
        {"M", "{0:P:Minutes:Minute}"},
        {"H","{0:P:Hours:Hour}"},
        {"D", "{0:P:Days:Day}"}
    };

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                return String.Format(new PluralFormatter(), timeformats[format], arg);
            }

            public object GetFormat(Type formatType)
            {
                return formatType == typeof(ICustomFormatter) ? this : null;
            }
        }

        /// <summary>
        /// formats a numeric value based on a format P:Plural:Singular
        /// </summary>
        public class PluralFormatter : ICustomFormatter, IFormatProvider
        {

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                if (arg != null)
                {
                    var parts = format.Split(':'); // ["P", "Plural", "Singular"]

                    if (parts[0] == "P") // correct format?
                    {
                        // which index postion to use
                        int partIndex = (arg.ToString() == "1") ? 2 : 1;
                        // pick string (safe guard for array bounds) and format
                        return String.Format("{0} {1}", arg, (parts.Length > partIndex ? parts[partIndex] : ""));
                    }
                }
                return String.Format(format, arg);
            }

            public object GetFormat(Type formatType)
            {
                return formatType == typeof(ICustomFormatter) ? this : null;
            }
        }
        
        #endregion

        #region Handlers

        /// <summary>
        /// On load event handler.
        /// </summary>
        private void XtraFormAbout_Load(object sender, EventArgs e)
        {
            UiHelperClass.ReadDongleInfo();

            if (UiHelperClass.CheckForDongle && UiHelperClass.DonglePresent)
            {
                try
                {
                    textEditDongleNumber.Text = UiHelperClass.DongleNumber;
                    textEditExpirationDate.Text = UiHelperClass.DongleExpiryDate.ToShortDateString();
                    textEditRemainingTime.Text = GetReadableTimespan(DateTime.Now, UiHelperClass.DongleExpiryDate);

                    CheckDongleExpiry();
                }
                catch (Exception)
                {
                    UiHelperClass.ShowDongleCheckMessage();
                    Close();
                }
            }
            else
            {
                UiHelperClass.ShowDongleCheckMessage();
                Close();
            }            
        }

        /// <summary>
        /// On key down event handler.
        /// </summary>
        private void XtraFormAbout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                Close();
            }
        }

        /// <summary>
        /// Handle dongle reset button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDongleReset_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonDongleReset_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ShowDongleResetDialog();
            }
        }

        #endregion
    }
}