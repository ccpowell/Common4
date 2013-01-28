using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.CustomEvents
{
    // Summary:
    //     Notifies clients that a property value has changed.
    public interface INotifyPropertyChange
    {
        // Summary:
        //     Occurs when a property value changes.
        event PropertyChangeEventHandler PropertyChange;
    }
}
