using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Extentions.WebForms
{
    public static class ControlExtender
    {
        /// <summary>
        /// Recursively finds controls of Type T, started from the root.
        /// </summary>
        /// <typeparam name="T">Type of the returned controls</typeparam>
        /// <param name="root">Root control to starts from</param>
        public static IEnumerable<T> GetControlsRecursive<T>(this Control root) where T : Control
        {
            foreach (T control in root.Controls.OfType<T>())
            {
                yield return (T)control;

                if (control.HasControls())
                {
                    foreach (T childControl in GetControls<T>(control))
                    {
                        yield return childControl;
                    }
                }
            }
        }

        /// <summary>
        /// Finds controls of Type T.
        /// </summary>
        /// <typeparam name="T">Type of the returned controls</typeparam>
        /// <param name="controlToSearch">Control to search</param>
        public static IEnumerable<T> GetControls<T>(this Control controlToSearch) where T : Control
        {
            foreach (T child in controlToSearch.Controls.OfType<T>())
            {
                yield return (T)child;
            }
        }

        /// <summary>
        /// Find a control from 'root' recursive with that has id 'controlID'.
        /// </summary>
        /// <param name="root">Control to starts from</param>
        /// <param name="controlID">ID of the control to find</param>
        /// <returns>Found control or null if not found</returns>
        public static Control FindControlRecursive(this Control root, string controlID)
        {
            if (!root.HasControls())
            {
                return null;
            }

            Control ctrl = root.FindControl(controlID);
            if (ctrl != null)
            {
                return ctrl;
            }
            else
            {
                return root.FindControlRecursive(controlID);
            }
        }
        
        public static IEnumerable<T> GetDataKeyValuesFromGridItemCollection<T>(this GridItemCollection items, string dataKeyValye)
        {
            if (items == null)
            {
                return default(List<T>);
            }

            List<T> selectedValues = new List<T>();

            foreach (GridDataItem item in items)
            {
                object selected = item.GetDataKeyValue(dataKeyValye);
                T converted = (T)selected;
                selectedValues.Add(converted);
            }

            return selectedValues;
        }
    }
}
