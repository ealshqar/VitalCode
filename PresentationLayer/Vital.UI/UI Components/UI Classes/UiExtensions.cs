using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.UI_Classes
{
    public static class UiExtensions
    {
        /// <summary>
        /// Sets the status of the StateIndicatorComponent;
        /// </summary>
        public static void SetState (this DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent stateIndicatorComponent, IndicatorComponentStatus status)
        {
            stateIndicatorComponent.StateIndex = (int) status;
        }

        /// <summary>
        /// Gets the status of the StateIndicatorComponent;
        /// </summary>
        public static IndicatorComponentStatus GetState(this DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent stateIndicatorComponent, IndicatorComponentStatus status)
        {
            return (IndicatorComponentStatus) stateIndicatorComponent.StateIndex;
        }
        
        /// <summary>
        /// Post lookupEdit control values to Lookup BO.
        /// </summary>
        /// <param name="lookUpEdit">The lookupEdit control.</param>
        /// <param name="lookup">The Lookup BO.</param>
        public static void PostLookupValues(this LookUpEdit lookUpEdit, Business.Shared.DomainObjects.Lookups.Lookup lookup)
        {
            if(lookup == null)
                return;

            //This code is needed to make sure the  lookup object gets updated on change which help printing the lookup name in reports
            //since the binding is only made using ID and that is not enough for report to show data.
            lookUpEdit.DoValidate();
            var selectedStatusLookup = (Business.Shared.DomainObjects.Lookups.Lookup)lookUpEdit.GetSelectedDataRow();
            if (selectedStatusLookup == null)
                return;
            lookup.Type = selectedStatusLookup.Type;
            lookup.Key = selectedStatusLookup.Key;
            lookup.Value = selectedStatusLookup.Value;
        }
    }
}
