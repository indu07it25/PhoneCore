using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Resources;
using PhoneCore.Framework.Configuration;

namespace PhoneCore.Framework.Diagnostic.Tracing
{
    /// <summary>
    /// Provides functionality to manage trace records
    /// </summary>
    public class DefaultTraceController
    {
        private readonly string _templateName;
        private readonly DefaultTrace _trace;
        private readonly Regex _templateRegex = new Regex(@"\{TEMPLATE_START\}(?<template>.*)\{TEMPLATE_END\}");
        
        public DefaultTraceController(DefaultTrace trace, string templateName)
        {
            _trace = trace;
            _templateName = templateName;
        }

        /// <summary>
        /// Removes records older than 3 days
        /// </summary>
        public void Cleanup()
        {
            var dataContext = GetDataContext();

            var records = (from Data.Record record in dataContext.Records
                           where record.Date > DateTime.Now.AddDays(-3)
                           select record);
            dataContext.Records.DeleteAllOnSubmit(records);
            dataContext.SubmitChanges();
        }

        /// <summary>
        /// generates report into TextWriter
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TextWriter GenerateReport()
        {
            var writer = GetWriter();

            var dataContext = GetDataContext();

            //get all categories
            var categories = (from Data.Record record in dataContext.Records
                              orderby record.Date descending
                              select record);

            var templateFileContent = GetTemplate();
            Match m = _templateRegex.Match(templateFileContent);
            string template = m.Groups["template"].Value;
            StringBuilder builder = new StringBuilder(512);
            foreach (var record in categories)
            {
                builder.AppendLine(template
                    .Replace("{Type.Name}", record.Type.Name)
                    .Replace("{Category.Name}", record.Category.Name)
                    .Replace("{Date}", record.Date.ToString())
                    .Replace("{Message}", record.Message)
                    .Replace("{SourceType}", record.SourceType)
                    .Replace("{StackTrace}", record.StackTrace)
                    .Replace("{Page}", record.Page)
                    .Replace("{Exception}", record.Exception));
            }

            string result = _templateRegex.Replace(templateFileContent, builder.ToString());

            writer.Write(result);
            return writer;
        }

        public TraceStatistic Statistic()
        {
            TraceStatistic stat = new TraceStatistic();

            var dataContext = GetDataContext();
            stat.TotalRecords = dataContext.Records.Count();
            stat.TotalErrors = (from Data.Record r in dataContext.Records
                                select r).Count(r => r.Category.Name == "Error");
            stat.TotalFatals = (from Data.Record r in dataContext.Records
                                select r).Count(r => r.Category.Name == "Fatal");
            stat.TotalWarns = (from Data.Record r in dataContext.Records
                                select r).Count(r => r.Category.Name == "Warn");
            return stat;
        }

        private string GetTemplate()
        {
            StreamResourceInfo stream = Application.GetResourceStream(new Uri(_templateName, UriKind.Relative));
            return (new StreamReader(stream.Stream)).ReadToEnd();
        }

        private Data.DefaultTraceDataContext GetDataContext()
        {

            if (!_trace.IsInitialized)
                throw new InvalidOperationException("Unable to generate report for non-initialized trace");

            var dataContext = _trace.GetUnderlyingStorage() as Data.DefaultTraceDataContext;
            if (dataContext == null)
                throw new InvalidOperationException("Unable to get DefaultTraceDataContext");

            return dataContext;
        }

        private static TextWriter GetWriter()
        {
            return new StringWriter();
        }

        public class TraceStatistic
        {
            public int TotalWarns { get; set; }
            public int TotalErrors { get; set; }
            public int TotalFatals { get; set; }
            public int TotalRecords { get; set; }
        }
    }
}
