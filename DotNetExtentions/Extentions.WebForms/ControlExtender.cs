using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Extentions.WebForms
{
    public static class ControlExtender
    {
        /// <summary>
        /// Finds controls of Type T, started from the root and search all child controls.
        /// </summary>
        /// <typeparam name="T">Type of the returned controls</typeparam>
        /// <param name="root">Root control to starts from</param>
        public static IEnumerable<T> GetControls<T>(this Control root) where T : Control
        {
            List<T> found = new List<T>();
            Queue<Control> controls = new Queue<Control>(root.Controls.Cast<Control>());
            
            while (controls.Count > 0)
            {
                Control ctlr = controls.Dequeue();
                if (ctlr is T)
                {
                    found.Add(ctlr as T);
                }
                else
                {
                    if (ctlr.HasControls())
                    {
                        foreach (Control control in ctlr.Controls)
                        {
                            controls.Enqueue(control);
                        }
                    }
                }
            }

            return found;
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
