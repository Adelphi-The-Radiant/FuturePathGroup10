using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Learner;

namespace FuturePathGroup10.Advisory
{
    public class ExportResult
    {
        public Learner.Learner learner { get; private set; }
        public string fullReport { get; private set; }
        public string summary { get; private set; }

        public ExportResult(Learner.Learner learner,string fullReport,string summary)
        {
            this.learner = learner;
            this.fullReport = fullReport;
            this.summary = summary;
        }

        public string getReportSummary()
        {
            throw new NotImplementedException();
        }

        public string getFullReport()
        {
            throw new NotImplementedException();
        }
    }
}
