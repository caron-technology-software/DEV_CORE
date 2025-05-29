using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI.Controls
{
    //public class MinMaxProperty
    //{

    //    public event EventHandler<PropertyEventArgs> ValueChanged;

    //    protected void OnValueChanged(PropertyEventArgs e)
    //    {
    //        ValueChanged?.Invoke(this, e);
    //    }

    //    public int? MinValue { get; set; } = null;
    //    public int? MaxValue { get; set; } = null;

    //    private int value = int.MinValue;
    //    public int Value
    //    {
    //        get
    //        {
    //            return value;
    //        }

    //        set
    //        {
    //            if (this.value == value)
    //            {
    //                return;
    //            }

    //            if ((MaxValue != null) && (MinValue != null))
    //            {
    //                if (value > MaxValue)
    //                {
    //                    this.value = (int)MaxValue;
    //                    return;
    //                }
    //                else if (value < MinValue)
    //                {
    //                    this.value = (int)MinValue;
    //                    return;
    //                }
    //            }

    //            this.value = value;
    //        }
    //    }

    //    public MinMaxProperty()
    //    {
    //        //--
    //    }
    //}
}
