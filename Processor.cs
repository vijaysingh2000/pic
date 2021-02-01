using InsuranceCalculator.Calculator;
using InsuranceCalculator.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InsuranceCalculator
{
    public class Processor
    {
        private string _fileName;
        private string _newFileName;
        private CompanyData _company;
        private BaseCalculator _companyCalculator;
        private XElement _companyMetaData;
        private Action<ProgressChangedEventArgs> _progress;
        private Action<RunWorkerCompletedEventArgs, string> _completed;
        private BackgroundWorker _backgroundWorker;

        public Processor(string fileName, CompanyData company, Action<ProgressChangedEventArgs> progress, Action<RunWorkerCompletedEventArgs, string> completed)
        {
            this._fileName = fileName;
            this._company = company;
            this._progress = progress;
            this._completed = completed;
            this._companyCalculator = Factory.GetCompanyCalculator(company);
            this._companyMetaData = Factory.GetCompanyMetaData(company);

            this._newFileName = Path.GetFileNameWithoutExtension(this._fileName) + $"_{company.Code}" + Path.GetExtension(this._fileName);
            this._newFileName = Path.Combine(Path.GetDirectoryName(this._fileName), this._newFileName);

            if (File.Exists(this._newFileName))
                File.Delete(this._newFileName);
        }

        public void StartAsync()
        {
            this._backgroundWorker = new BackgroundWorker();
            this._backgroundWorker.WorkerReportsProgress = true;
            this._backgroundWorker.DoWork += _backgroundWorker_DoWork;
            this._backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            this._backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;

            this._backgroundWorker.RunWorkerAsync();
        }

        public void Start()
        {
            string[] arrLines = File.ReadAllLines(this._fileName);
            string[] headerItems = null;

            _backgroundWorker.ReportProgress(0, arrLines.Length);

            for (int iloop = 0; iloop < arrLines.Length; iloop++)
            {
                _backgroundWorker.ReportProgress(iloop + 1, $"Processing line number {iloop}");

                string line = arrLines[iloop];

                if (iloop == 0)
                {
                    headerItems = line.Split(',');

                    string newLine = line + ",Premium Calculated" +  Environment.NewLine;
                    File.AppendAllText(_newFileName, newLine);
                }
                else
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();

                    if (headerItems == null)
                        return;

                    string[] lineItems = line.Split(',');

                    for (int jLoop = 0; jLoop < lineItems.Length; jLoop++)
                    {
                        string key = headerItems[jLoop];
                        string value = lineItems[jLoop];

                        parameters.Add(key.Trim().ToUpper(), value);
                    }

                    double calculatedValue = _companyCalculator.Calculate(parameters, _companyMetaData);

                    string newLine = line + "," + calculatedValue.ToString() + Environment.NewLine;
                    File.AppendAllText(_newFileName, newLine);
                }
            }
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this._progress != null)
                this._progress(e);
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this._completed != null)
                this._completed(e, _newFileName);
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Start();
        }
    }
}
