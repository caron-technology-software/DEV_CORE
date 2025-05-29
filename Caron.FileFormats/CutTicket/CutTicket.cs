using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.FileFormats.CutTicketX
{
    // NOTA: con il codice generato potrebbe essere richiesto almeno .NET Framework 4.5 o .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class CutTicket
    {
        private decimal versionField;

        private string jobFileField;

        private object workOrderFileField;

        private object workOrderNameField;

        private object commentsField;

        private string spreaderConfigurationId;

        private CutTicketSpread[] spreadListField;

        //private CutTicketSpreadProgressList spreadProgressListField;

        private CutTicketSpreadProgress[] spreadProgressListField;

        private string ticketStatusField;

        private CutTicketMarkerReport markerReportField;

        private CutTicketSplice[] splicesField;

        private CutTicketLabel[] labelsField;

        /// <remarks/>
        public decimal Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public string SpreaderConfigurationId
        {
            get
            {
                return this.spreaderConfigurationId;
            }
            set
            {
                this.spreaderConfigurationId = value;
            }
        }

        /// <remarks/>
        public string JobFile
        {
            get
            {
                return this.jobFileField;
            }
            set
            {
                this.jobFileField = value;
            }
        }

        /// <remarks/>
        public object WorkOrderFile
        {
            get
            {
                return this.workOrderFileField;
            }
            set
            {
                this.workOrderFileField = value;
            }
        }

        /// <remarks/>
        public object WorkOrderName
        {
            get
            {
                return this.workOrderNameField;
            }
            set
            {
                this.workOrderNameField = value;
            }
        }

        /// <remarks/>
        public object Comments
        {
            get
            {
                return this.commentsField;
            }
            set
            {
                this.commentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Spread", IsNullable = false)]
        public CutTicketSpread[] SpreadList
        {
            get
            {
                return this.spreadListField;
            }
            set
            {
                this.spreadListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SpreadProgress", IsNullable = false)]
        public CutTicketSpreadProgress[] SpreadProgressList
        {
            get
            {
                return this.spreadProgressListField;
            }
            set
            {
                this.spreadProgressListField = value;
            }
        }

        /// <remarks/>
        public string TicketStatus
        {
            get
            {
                return this.ticketStatusField;
            }
            set
            {
                this.ticketStatusField = value;
            }
        }

        /// <remarks/>
        public CutTicketMarkerReport MarkerReport
        {
            get
            {
                return this.markerReportField;
            }
            set
            {
                this.markerReportField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Splice", IsNullable = false)]
        public CutTicketSplice[] Splices
        {
            get
            {
                return this.splicesField;
            }
            set
            {
                this.splicesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Label", IsNullable = false)]
        public CutTicketLabel[] Labels
        {
            get
            {
                return this.labelsField;
            }
            set
            {
                this.labelsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSpread
    {

        private string colorField;

        private int plyField;

        private int pliesSpreadField;

        private decimal lengthField;

        /// <remarks/>
        public string Color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }

        /// <remarks/>
        public int Ply
        {
            get
            {
                return this.plyField;
            }
            set
            {
                this.plyField = value;
            }
        }

        /// <remarks/>
        public int PliesSpread
        {
            get
            {
                return this.pliesSpreadField;
            }
            set
            {
                this.pliesSpreadField = value;
            }
        }

        /// <remarks/>
        public decimal Length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSpreadProgress
    {

        private CutTicketSpreadProgressSpreadingProgress spreadingProgressField;

        private CutTicketSpreadProgressCuttingProgrssList cuttingProgrssListField;

        /// <remarks/>
        public CutTicketSpreadProgressSpreadingProgress SpreadingProgress
        {
            get
            {
                return this.spreadingProgressField;
            }
            set
            {
                this.spreadingProgressField = value;
            }
        }

        /// <remarks/>
        public CutTicketSpreadProgressCuttingProgrssList CuttingProgrssList
        {
            get
            {
                return this.cuttingProgrssListField;
            }
            set
            {
                this.cuttingProgrssListField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSpreadProgressSpreadingProgress
    {

        private string idField;

        private int pliesSpreadField;

        private CutTicketSpreadProgressSpreadingProgressStatus[] statusesField;

        private CutTicketSpreadProgressSpreadingProgressStepProgress[] stepProgressListField;

        private string barCodeCutTicketField;

        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public int PliesSpread
        {
            get
            {
                return this.pliesSpreadField;
            }
            set
            {
                this.pliesSpreadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Status", IsNullable = false)]
        public CutTicketSpreadProgressSpreadingProgressStatus[] Statuses
        {
            get
            {
                return this.statusesField;
            }
            set
            {
                this.statusesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("StepProgress", IsNullable = false)]
        public CutTicketSpreadProgressSpreadingProgressStepProgress[] StepProgressList
        {
            get
            {
                return this.stepProgressListField;
            }
            set
            {
                this.stepProgressListField = value;
            }
        }

        /// <remarks/>
        public string BarCodeCutTicket
        {
            get
            {
                return this.barCodeCutTicketField;
            }
            set
            {
                this.barCodeCutTicketField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSpreadProgressSpreadingProgressStatus
    {

        private String timeStampField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string timeStamp
        {
            get
            {
                return this.timeStampField;
            }
            set
            {
                this.timeStampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSpreadProgressSpreadingProgressStepProgress
    {

        private int stepIdField;

        private string stepColorField;

        private int stepPliesSpreadField;

        /// <remarks/>
        public int StepId
        {
            get
            {
                return this.stepIdField;
            }
            set
            {
                this.stepIdField = value;
            }
        }

        /// <remarks/>
        public string StepColor
        {
            get
            {
                return this.stepColorField;
            }
            set
            {
                this.stepColorField = value;
            }
        }

        /// <remarks/>
        public int StepPliesSpread
        {
            get
            {
                return this.stepPliesSpreadField;
            }
            set
            {
                this.stepPliesSpreadField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSpreadProgressCuttingProgrssList
    {

        private CutTicketSpreadProgressCuttingProgrssListCuttingProgrss cuttingProgrssField;

        /// <remarks/>
        public CutTicketSpreadProgressCuttingProgrssListCuttingProgrss CuttingProgrss
        {
            get
            {
                return this.cuttingProgrssField;
            }
            set
            {
                this.cuttingProgrssField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSpreadProgressCuttingProgrssListCuttingProgrss
    {

        private string idField;

        private string jobGUIDField;

        private CutTicketSpreadProgressCuttingProgrssListCuttingProgrssStatus[] statusesField;

        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string jobGUID
        {
            get
            {
                return this.jobGUIDField;
            }
            set
            {
                this.jobGUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Status", IsNullable = false)]
        public CutTicketSpreadProgressCuttingProgrssListCuttingProgrssStatus[] Statuses
        {
            get
            {
                return this.statusesField;
            }
            set
            {
                this.statusesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSpreadProgressCuttingProgrssListCuttingProgrssStatus
    {

        private string timeStampField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string timeStamp
        {
            get
            {
                return this.timeStampField;
            }
            set
            {
                this.timeStampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketMarkerReport
    {
        private string markerNameField;

        private string markerDescriptionField;

        private string orderNumberField;

        private decimal yieldField;

        private decimal lengthField;

        private decimal widthField;

        private string spreadModeField;

        private string splicePlacementField;

        /// <remarks/>
        public string MarkerName
        {
            get
            {
                return this.markerNameField;
            }
            set
            {
                this.markerNameField = value;
            }
        }

        /// <remarks/>
        public string MarkerDescription
        {
            get
            {
                return this.markerDescriptionField;
            }
            set
            {
                this.markerDescriptionField = value;
            }
        }

        /// <remarks/>
        public string OrderNumber
        {
            get
            {
                return this.orderNumberField;
            }
            set
            {
                this.orderNumberField = value;
            }
        }

        /// <remarks/>
        public decimal Yield
        {
            get
            {
                return this.yieldField;
            }
            set
            {
                this.yieldField = value;
            }
        }

        /// <remarks/>
        public decimal Length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }

        /// <remarks/>
        public decimal Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        public string SpreadMode
        {
            get
            {
                return this.spreadModeField;
            }
            set
            {
                this.spreadModeField = value;
            }
        }

        /// <remarks/>
        public string SplicePlacement
        {
            get
            {
                return this.splicePlacementField;
            }
            set
            {
                this.splicePlacementField = value;
            }
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketSplice
    {
        private float startField;

        private float endField;

        /// <remarks/>
        public float Start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
            }
        }

        /// <remarks/>
        public float End
        {
            get
            {
                return this.endField;
            }
            set
            {
                this.endField = value;
            }
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CutTicketLabel
    {

        private decimal centerXField;

        private decimal centerYField;

        private ushort rotationField;

        private bool fitField;

        private int pieceNumberField;

        private string nameField;

        private object descriptionField;

        private string categoryField;

        private string sizeField;

        private string bundleIdField;

        private string modelNameField;

        private string leftOrRightField;

        private string flipField;

        private bool addedField;

        private bool foldedField;

        private object messageField;

        /// <remarks/>
        public decimal CenterX
        {
            get
            {
                return this.centerXField;
            }
            set
            {
                this.centerXField = value;
            }
        }

        /// <remarks/>
        public decimal CenterY
        {
            get
            {
                return this.centerYField;
            }
            set
            {
                this.centerYField = value;
            }
        }

        /// <remarks/>
        public ushort Rotation
        {
            get
            {
                return this.rotationField;
            }
            set
            {
                this.rotationField = value;
            }
        }

        /// <remarks/>
        public bool Fit
        {
            get
            {
                return this.fitField;
            }
            set
            {
                this.fitField = value;
            }
        }

        /// <remarks/>
        public int PieceNumber
        {
            get
            {
                return this.pieceNumberField;
            }
            set
            {
                this.pieceNumberField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public object Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string Category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        public string Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }

        /// <remarks/>
        public string BundleId
        {
            get
            {
                return this.bundleIdField;
            }
            set
            {
                this.bundleIdField = value;
            }
        }

        /// <remarks/>
        public string ModelName
        {
            get
            {
                return this.modelNameField;
            }
            set
            {
                this.modelNameField = value;
            }
        }

        /// <remarks/>
        public string LeftOrRight
        {
            get
            {
                return this.leftOrRightField;
            }
            set
            {
                this.leftOrRightField = value;
            }
        }

        /// <remarks/>
        public string Flip
        {
            get
            {
                return this.flipField;
            }
            set
            {
                this.flipField = value;
            }
        }

        /// <remarks/>
        public bool Added
        {
            get
            {
                return this.addedField;
            }
            set
            {
                this.addedField = value;
            }
        }

        /// <remarks/>
        public bool Folded
        {
            get
            {
                return this.foldedField;
            }
            set
            {
                this.foldedField = value;
            }
        }

        /// <remarks/>
        public object Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
}//namespace Caron.FileFormats.CutTicket

//EOF