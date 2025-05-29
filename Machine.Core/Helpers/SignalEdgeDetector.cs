using Machine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Utility
{
    public unsafe class SignalEdgeDetector
    {
        public event EventHandler PositiveEdgeValueChanged;
        public event EventHandler NegativeEdgeValueChanged;
        public event EventHandler ValueChanged;

        public void OnValueChanged(ValueEventArgs<bool> e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public void OnPositiveValueChanged(ValueEventArgs<bool> e)
        {
            PositiveEdgeValueChanged?.Invoke(this, e);
        }

        public void OnNegativeValueChanged(ValueEventArgs<bool> e)
        {
            NegativeEdgeValueChanged?.Invoke(this, e);
        }

        public bool PrecedentValue { get; private set; }
        public bool CurrentValue { get; private set; }

        public SignalEdgeDetector(bool input)
        {
            CurrentValue = input;
            PrecedentValue = input;
        }

        public bool Check(bool value)
        {
            CurrentValue = value;

            bool changed = (CurrentValue != PrecedentValue);

            PrecedentValue = CurrentValue;

            if (changed)
            {
                OnValueChanged(new ValueEventArgs<bool>(CurrentValue));

                if (CurrentValue)
                {
                    OnPositiveValueChanged(new ValueEventArgs<bool>(CurrentValue));
                }
                else
                {
                    OnNegativeValueChanged(new ValueEventArgs<bool>(CurrentValue));
                }
            }

            return changed;
        }
    }
}