using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Net;
using InsuranceCalculator.DataAccessLayer;
using InsuranceCalculator.HelperClasses;
namespace InsuranceCalculator
{
    public partial class Company : Form
    {
        internal static string remotePathPrefix = $"https://vijayapps.s3-us-west-1.amazonaws.com/clients/pic/";
        internal static string applicationName = "pic";

        public Company()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtExcelFromTeam.Text = openFileDialog.FileName;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            ObjectWorkBook objectWorkBook = new ObjectWorkBook();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(txtExcelFromTeam.Text, false))
            {
                foreach (Sheet sheet in spreadsheetDocument.WorkbookPart.Workbook.Sheets.Elements())
                {
                    if (sheet.Name.ToString().Trim().ToUpper() == "CALCULATION")
                        continue;

                    ObjectSheet objectSheet = new ObjectSheet();
                    objectSheet.SheetName = sheet.Name;
                    objectWorkBook.ObjectSheets.Add(objectSheet);

                    WorksheetPart worksheetPart = (WorksheetPart)spreadsheetDocument.WorkbookPart.GetPartById(sheet.Id);

                    foreach (SheetData sheetData in worksheetPart.Worksheet.Elements<SheetData>())
                    {
                        foreach (Row r in sheetData.Elements<Row>())
                        {
                            ObjectSheetRow objectSheetRow = new ObjectSheetRow();
                            objectSheet.ObjectSheetRows.Add(objectSheetRow);

                            foreach (Cell c in r.Elements<Cell>())
                            {
                                if (c.InnerText.Length > 0)
                                {
                                    string val = c.InnerText;

                                    if (c.DataType != null)
                                    {
                                        switch (c.DataType.Value)
                                        {
                                            case CellValues.SharedString:

                                                var stringTable = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

                                                if (stringTable != null)
                                                {
                                                    val = stringTable.SharedStringTable.ElementAt(int.Parse(val)).InnerText;
                                                }
                                                break;

                                            case CellValues.Boolean:

                                                switch (val)
                                                {
                                                    case "0":
                                                        val = "FALSE";
                                                        break;
                                                    default:
                                                        val = "TRUE";
                                                        break;
                                                }
                                                break;
                                        }
                                    }

                                    objectSheetRow.ObjectSheetRowCells.Add(val);
                                }
                            }
                        }
                    }
                }
            }

            txtPreview.Text = objectWorkBook.Serialize().ToString();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPreview.Text))
            {
                CommonFunctions.ShowErrorMessage("Xml cannot be empty");
                return;
            }

            if (string.IsNullOrEmpty(txtCompanyCode.Text))
            {
                CommonFunctions.ShowErrorMessage("Company code cannot be empty");
                return;
            }

            if (string.IsNullOrEmpty(txtCompanyName.Text))
            {
                CommonFunctions.ShowErrorMessage("Company name cannot be empty");
                return;
            }


            try
            {
                XElement itemXml = XElement.Parse(txtPreview.Text);

                CompanyData companyData = new CompanyData();
                companyData.Code = txtCompanyCode.Text;
                companyData.Name = txtCompanyName.Text;
                companyData.ClassName = txtClassName.Text;
                companyData.FactorXml = itemXml;

                bool result = DataAcessLayer.UpdateCompanyData(companyData);

                if (!string.IsNullOrEmpty(txtSampleInputSheet.Text) && File.Exists(txtSampleInputSheet.Text))
                {
                    bool inputSheetresult = DataAcessLayer.UpdateInputSheet(companyData.Code, txtSampleInputSheet.Text);
                }                    

                if(result)
                    CommonFunctions.ShowInformationMessage("Upload successfull.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCompanyCode.Text))
            {
                CommonFunctions.ShowErrorMessage("Company code is needed to fetch !!!");
                return;
            }

            try
            {
                CompanyData companyData = DataAcessLayer.GetCompanyData(txtCompanyCode.Text);

                txtCompanyName.Text = companyData.Name;
                txtClassName.Text = companyData.ClassName;
                txtPreview.Text = companyData.FactorXml.ToString();
            }
            catch
            {
                CommonFunctions.ShowErrorMessage("Company code doesn't exists !!!");
            }
        }

        private void btnBrowseInputSheet_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSampleInputSheet.Text = openFileDialog.FileName;
            }
        }

        private void Company_Load(object sender, EventArgs e)
        {
            this.Text = CommonFunctions.GetMessageCaption();
        }
    }

    public class ObjectWorkBook
    {
        public ObjectWorkBook()
        {
            this.ObjectSheets = new List<ObjectSheet>();
        }

        public List<ObjectSheet> ObjectSheets { get; set; }
        public XElement Serialize()
        {
            XElement element = new XElement("factorsheet");
            foreach(ObjectSheet objectSheet in this.ObjectSheets)
            {
                element.Add(objectSheet.Serialize());
            }

            return element;
        }
    }
    public class ObjectSheet
    {
        public ObjectSheet()
        {
            this.SheetName = string.Empty;
            this.ObjectSheetRows = new List<ObjectSheetRow>();
        }

        public string SheetName { get; set; }
        public List<ObjectSheetRow> ObjectSheetRows { get; set; }

        public XElement Serialize()
        {
            XElement element = new XElement("parameter");
            element.SetAttributeValue("name", this.SheetName);

            if (this.ObjectSheetRows.Count > 0)
            {
                ObjectSheetRow headerRow = this.ObjectSheetRows[0];

                foreach (ObjectSheetRow objectSheetRow in this.ObjectSheetRows)
                {
                    if (objectSheetRow != headerRow)
                        element.Add(objectSheetRow.Serialize(headerRow.ObjectSheetRowCells));
                }
            }

            return element;
        }
    }
    public class ObjectSheetRow
    {
        public ObjectSheetRow()
        {
            this.ObjectSheetRowCells = new List<string>();
        }

        public List<string> ObjectSheetRowCells { get; set; }

        public XElement Serialize(List<string> columnName)
        {
            XElement element = new XElement("item");

            for (int iloop = 0; iloop < this.ObjectSheetRowCells.Count; iloop++)
                element.SetAttributeValue(columnName[iloop], this.ObjectSheetRowCells[iloop]);

            return element;
        }
    }
}
