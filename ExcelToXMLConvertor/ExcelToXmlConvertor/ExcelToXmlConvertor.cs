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
namespace ExcelToXmlConvertor
{
    public partial class ExcelToXmlConvertor : Form
    {
        internal static string remotePathPrefix = $"https://vijayapps.s3-us-west-1.amazonaws.com/clients/pic/";
        internal static string applicationName = "pic";

        public ExcelToXmlConvertor()
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
                MessageBox.Show("Xml cannot be empty", ExcelToXmlConvertor.GetMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtCompanyCode.Text))
            {
                MessageBox.Show("Company code cannot be empty", ExcelToXmlConvertor.GetMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                XElement itemXml = XElement.Parse(txtPreview.Text);

                string tempFolder = Path.GetTempPath() + $"{txtCompanyCode.Text}";

                if (!Directory.Exists(tempFolder))
                    Directory.CreateDirectory(tempFolder);

                string filePath = Path.Combine(tempFolder, $"{txtCompanyCode.Text}.xml");

                itemXml.Save(filePath);

                Upload(txtCompanyCode.Text, filePath);

                MessageBox.Show("Upload successfull.", ExcelToXmlConvertor.GetMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Upload(string companyCode, string fileName)
        {
            try
            {
                string fName = Path.GetFileName(fileName);

                string uploadURL = remotePathPrefix + $"{companyCode}/{fName}";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                using (WebClient client = new WebClient())
                {
                    client.UploadFile(uploadURL, "PUT", fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public static string GetMessageCaption()
        {
            return "Excel to Xml Convertor";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {

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
            XElement element = new XElement("company");
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
