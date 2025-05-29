using System;

namespace Machine.Common
{
    public sealed class ShowWithConditionInSettingsPanel : Attribute
    {
        public string VariableToCheck { get; private set; }
        public ShowWithConditionInSettingsPanel(string variable)
        {
            VariableToCheck = variable;
        }
    }
}
