using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ProRob.Extensions.String;

using Machine.UI.Controls;
using Machine.UI.Communication;
using Machine.Common;

namespace Machine.UI.Common
{
    public class SettingsManager<T> where T : Machine.Settings.MachineGroupOfSettings
    {
        private readonly Machine.Settings.MachineGroupOfSettings settings;
        private readonly Machine.UI.Controls.MachineEditableItemsListbox listbox;
        private readonly List<string> propertiesToRemove;

        public string Resource { get; private set; }

        private Func<string, string> Translator { get; set; } = null;
        private UserType? CurrentUserType { get; set; } = null;
        private string MessageBoxMessage { get; set; } = "";
        public SettingsManager(MachineEditableItemsListbox listbox, string resource, Func<string, string> translator = null, UserType userType = 0, string notAllowedMessage = "", IEnumerable<string> propertiesToRemove = null, uint enableImperialYard = 0)
        {
            this.listbox = listbox;

            var json = Communicator.SendHttpGetRequest(resource);
            settings = ProRob.Json.Deserialize<T>(json);

            Resource = resource;
            Translator = translator;
            CurrentUserType = userType;

            MessageBoxMessage = notAllowedMessage;

            listbox.PropertyChanged += ListboxPropertyChanged;

            if (propertiesToRemove != null)
            {
                this.propertiesToRemove = propertiesToRemove.ToList();
            }

            AddSettings(enableImperialYard);
        }

        private void AddSettings(uint enableImperialYard = 0)
        {
            listbox.Clear();

            // Get the properties of 'Type' class object.
            PropertyInfo[] propertiesInfo = settings.GetType().GetProperties();

            //Generic value
            Machine.UI.Controls.MachineEditableItem item = null;

            foreach (var p in propertiesInfo)
            {
                //------------------------------------
                // Properties to remove 
                //------------------------------------
                if (propertiesToRemove != null && propertiesToRemove.Contains(p.Name))
                {
                    continue;
                }

                //------------------------------------
                // Visibility 
                //------------------------------------
                var notShowInSettingsPanel = p.GetCustomAttribute<NotShowInSettingsPanel>();
                if (notShowInSettingsPanel != null)
                {
                    continue;
                }

                //------------------------------------
                // User 
                //------------------------------------
                UserType? readEnableUserType = null;
                UserType? writeEnableUserType = null;

                var propertyUserAccessAttribute = p.GetCustomAttribute<UserAccess>();

                if (propertyUserAccessAttribute != null)
                {
                    readEnableUserType = propertyUserAccessAttribute.ReadEnableUserType;
                    writeEnableUserType = propertyUserAccessAttribute.WriteEnableUserType;
                }

                var setting = p.GetValue(settings, null);
                //var type = setting.GetType();

                switch (setting)
                {
                    case Machine.Settings.BooleanMachineSetting t:
                        item = new Machine.UI.Controls.MachineNumericEditableItem()
                        {
                            StringID = p.Name,
                            PropertyName = Translator is null ? p.Name.ToSentenceCase() : Translator(p.Name),
                            PropertyValue = ((Machine.Settings.BooleanMachineSetting)setting).Value ? 1 : 0,
                            MinValue = 0,
                            MaxValue = 1,
                        };
                        break;

                    case Machine.Settings.NumericMachineSetting t:
                        item = new Machine.UI.Controls.MachineNumericEditableItem()
                        {
                            StringID = p.Name,
                            PropertyName = Translator is null ? p.Name.ToSentenceCase() : Translator(p.Name),
                            PropertyValue = ((Machine.Settings.NumericMachineSetting)setting).Value,
                            MinValue = ((Machine.Settings.NumericMachineSetting)setting).MinValue,
                            MaxValue = ((Machine.Settings.NumericMachineSetting)setting).MaxValue,
                        };
                        //GPIx258 material setting yard imperial
                        if (enableImperialYard == 1)
                        {
                            if (p.Name == "TableLength")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "ZeroPosition")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "DistanceInductorMalfunctionCheck")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "FixedEndCatcherPosition")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "LoadUnloadPosition")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "ZeroPointCutter")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "ZeroPointTubular")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "DistanceEndCatcherSecondPosition")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }

                            if (p.Name == "DistanceSlowStartInSync")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "MaterialDescentPreFeed")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "OverfeedUnderfeedDistanceInitialSpreading")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                            if (p.Name == "OverfeedUnderfeedDistanceEndSpreading")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = true;
                                ((MachineNumericEditableItem)item).NeedYard = true;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                        }
                        else
                        {
                            if (p.Name == "TableLength")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "ZeroPosition")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "DistanceInductorMalfunctionCheck")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "FixedEndCatcherPosition")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "LoadUnloadPosition")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "ZeroPointCutter")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "ZeroPointTubular")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "DistanceEndCatcherSecondPosition")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }

                            if (p.Name == "DistanceSlowStartInSync")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "MaterialDescentPreFeed")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "OverfeedUnderfeedDistanceInitialSpreading")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                            if (p.Name == "OverfeedUnderfeedDistanceEndSpreading")
                            {
                                ((MachineNumericEditableItem)item).IsImperialYard = false;
                                ((MachineNumericEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineNumericEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                        }
                        //GPFx258
                        break;

                    case Machine.Settings.NumericOddMachineSetting t:
                        item = new Machine.UI.Controls.MachineNumericEditableItem()
                        {
                            StringID = p.Name,
                            PropertyName = Translator is null ? p.Name.ToSentenceCase() : Translator(p.Name),
                            PropertyValue = ((Machine.Settings.NumericOddMachineSetting)setting).Value,
                            MinValue = ((Machine.Settings.NumericOddMachineSetting)setting).MinValue,
                            MaxValue = ((Machine.Settings.NumericOddMachineSetting)setting).MaxValue,
                        };
                        break;

                    case Machine.Settings.NumericEvenMachineSetting t:
                        item = new Machine.UI.Controls.MachineNumericEditableItem()
                        {
                            StringID = p.Name,
                            PropertyName = Translator is null ? p.Name.ToSentenceCase() : Translator(p.Name),
                            PropertyValue = ((Machine.Settings.NumericEvenMachineSetting)setting).Value,
                            MinValue = ((Machine.Settings.NumericEvenMachineSetting)setting).MinValue,
                            MaxValue = ((Machine.Settings.NumericEvenMachineSetting)setting).MaxValue,
                        };
                        break;

                    case Machine.Settings.FloatingMachineSetting t:
                        item = new Machine.UI.Controls.MachineFloatingEditableItem()
                        {
                            StringID = p.Name,
                            PropertyName = Translator is null ? p.Name.ToSentenceCase() : Translator(p.Name),
                            PropertyValue = (float)((Machine.Settings.FloatingMachineSetting)setting).Value,
                            MinValue = (float)((Machine.Settings.FloatingMachineSetting)setting).MinValue,
                            MaxValue = (float)((Machine.Settings.FloatingMachineSetting)setting).MaxValue,
                        };
                        //GPIx258 material setting yard imperial
                        if (enableImperialYard == 1)
                        {
                            if (p.Name == "ThicknessMaterial")
                            {
                                ((MachineFloatingEditableItem)item).IsImperialYard = true;
                                ((MachineFloatingEditableItem)item).NeedYard = true;
                                ((MachineFloatingEditableItem)item).slPropertyValue.Visible = false;
                                ((MachineFloatingEditableItem)item).slPropertyValueYard.Visible = true;
                            }
                        }
                        else
                        {
                            if (p.Name == "ThicknessMaterial")
                            {
                                ((MachineFloatingEditableItem)item).IsImperialYard = false;
                                ((MachineFloatingEditableItem)item).slPropertyValue.Visible = true;
                                ((MachineFloatingEditableItem)item).slPropertyValueYard.Visible = false;
                            }
                        }
                        break;
                }

                //------------------------------------
                // Permissions 
                //------------------------------------
                if (propertyUserAccessAttribute is null)
                {
                    listbox.Add(item);
                }
                else if (propertyUserAccessAttribute != null && CurrentUserType != null)
                {
                    if (CurrentUserType.Value >= readEnableUserType.Value)
                    {
                        //Console.WriteLine($"PROPERTY ADDED: {item.PropertyName} {propertyAttribute.UserType}");

                        listbox.Add(item);
                        item.MessageBoxText = MessageBoxMessage;
                        item.SetEditPermission((UserType)CurrentUserType, (UserType)writeEnableUserType);
                    }
                    else
                    {
                        //Console.WriteLine($"PROPERTY DESCARDED: {item.PropertyName} {propertyAttribute.UserType}");
                    }
                }
            }
        }

        public void ClearListbox()
        {
            listbox.Clear();
        }

        private void ListboxPropertyChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void UpdateControl()
        {
            var dict = new Dictionary<string, dynamic>();

            //Get valori e memorizzazione su dizionario
            for (int i = 0; i < listbox.NumberOfElements; i++)
            {
                var c = listbox.GetControl(i);

                //Console.WriteLine($"{c.StringID}-{c.PropertyName}-{c.PropertyValue}");

                dict.Add(c.StringID, c.GetValue());
            }

            //Set valori
            foreach (var p in settings.GetType().GetProperties())
            {
                var setting = p.GetValue(settings, null);

                var stringID = p.Name;

                if (dict.ContainsKey(stringID))
                {
                    switch (setting)
                    {
                        case Machine.Settings.BooleanMachineSetting _:
                            ((Machine.Settings.BooleanMachineSetting)setting).Value = dict[stringID] > 0;
                            break;

                        case Machine.Settings.NumericMachineSetting _:
                            ((Machine.Settings.NumericMachineSetting)setting).Value = (int)dict[stringID];
                            break;

                        case Machine.Settings.NumericEvenMachineSetting _:
                            ((Machine.Settings.NumericEvenMachineSetting)setting).Value = (int)dict[stringID];
                            break;

                        case Machine.Settings.NumericOddMachineSetting _:
                            ((Machine.Settings.NumericOddMachineSetting)setting).Value = (int)dict[stringID];
                            break;

                        case Machine.Settings.FloatingMachineSetting _:
                            ((Machine.Settings.FloatingMachineSetting)setting).Value = (float)dict[stringID];
                            break;
                    }
                }
            }

            //Update
            Communicator.SendHttpPostRequest(Resource, settings);
        }
    }
}
