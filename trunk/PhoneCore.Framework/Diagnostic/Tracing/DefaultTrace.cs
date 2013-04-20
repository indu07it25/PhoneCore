using System;
using System.Linq;
using PhoneCore.Framework.Configuration;

namespace PhoneCore.Framework.Diagnostic.Tracing
{
    /// <summary>
    /// Represents defaul trace which logs messages into local database
    /// </summary>
    public sealed class DefaultTrace: ITrace
    {
        private Data.DefaultTraceDataContext _dataContext;
        private readonly ConfigSection _config;
        public DefaultTrace(ConfigSection config)
        {
            _config = config;

            //datacontext
            var storage = _config.GetString("storage/@connectionString");
            _dataContext = new Data.DefaultTraceDataContext(storage);

            Level = config.GetInt("@level"); 

            InitializeUnderlyingStorage();
        }

        #region ITrace members
        public void Info(string message)
        {
            WriteRecord(RecordType.Info, new TraceRecord(){ Message = message});
        }

        public void Info(ITraceCategory category, string message)
        {
            WriteRecord(RecordType.Info, new TraceRecord() { Category = category, Message = message });
        }

        public void Info(TraceRecord record)
        {
            WriteRecord(RecordType.Info, record);
        }

        public void Warn(string message)
        {
            WriteRecord(RecordType.Warn, new TraceRecord() { Message = message });
        }

        public void Warn(ITraceCategory category, string message)
        {
            WriteRecord(RecordType.Warn, new TraceRecord() { Category = category, Message = message });
        }

        public void Warn(TraceRecord record)
        {
            WriteRecord(RecordType.Warn, record);
        }

        public void Error(string message, Exception exception)
        {
            WriteRecord(RecordType.Error, new TraceRecord() { Message = message, Exception = exception });
        }

        public void Error(ITraceCategory category, string message, Exception exception)
        {
            WriteRecord(RecordType.Error, new TraceRecord() { Category = category, Message = message, Exception = exception });
        }

        public void Error(TraceRecord record)
        {
            WriteRecord(RecordType.Error, record);
        }

        public void Fatal(string message, Exception exception)
        {
            WriteRecord(RecordType.Fatal, new TraceRecord() { Message = message, Exception = exception });
        }

        public void Fatal(ITraceCategory category, string message, Exception exception)
        {
            WriteRecord(RecordType.Fatal, new TraceRecord() { Category = category, Message = message, Exception = exception});
        }

        public void Fatal(TraceRecord record)
        {
            WriteRecord(RecordType.Fatal, record);
        }

        /// <summary>
        /// Level of tracing
        /// </summary>
        public int Level { get; set; }


        public bool IsInitialized { get; set; }

        #endregion

        private void WriteRecord(RecordType type, TraceRecord record)
        {
            //level is higher than type of trace record
            if (Level > (int)type)
                return;
            //initialize storage
            InitializeUnderlyingStorage();

            //get list of categories by name
            var categories = (from Data.Category c in _dataContext.Categories
                              select c).Where(dc => dc.Name == record.Category.Name);

            Data.Category dataCategory;
            if(!categories.Any())
            {
                //create category
                dataCategory = new Data.Category() {Name = record.Category.Name};
                _dataContext.Categories.InsertOnSubmit(dataCategory);
                _dataContext.SubmitChanges();
            }
            else
                dataCategory = categories.Single();

            //get record type
            var dataType = (from Data.Type t in _dataContext.Types
                            select t).Single(t => t.Name == type.ToString());

            //create data entity from domain entity
            Data.Record dataRecord = new Data.Record()
            {
                Category = dataCategory,
                Type = dataType,
                Message = record.Message,
                Date = record.Date == DateTime.MinValue ? DateTime.Now: record.Date,
                Exception = record.Exception == null? "": record.Exception.Message,
                Page = record.Page ?? "",
                StackTrace = record.Exception == null ? "" : record.Exception.StackTrace.Substring(0, 2040),
                SourceType = record.SourceType == null? "": record.SourceType.FullName
            };

            //insert&submit changes
            _dataContext.Records.InsertOnSubmit(dataRecord);
            _dataContext.SubmitChanges();
        }

        public void Dispose()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }

        public object GetUnderlyingStorage()
        {
            return _dataContext;
        }


        private void InitializeUnderlyingStorage()
        {
            if (!_dataContext.DatabaseExists())
            {
                //create storage
                _dataContext.CreateDatabase();
                //get default trace types
                foreach (var name in new String[]{"Info", "Warn", "Error", "Fatal"})
                    _dataContext.Types.InsertOnSubmit(new Data.Type() {Name = name});
                _dataContext.SubmitChanges();
            }
            IsInitialized = true;
        }

        #region nested classes

        internal enum RecordType
        {
            Info,
            Warn,
            Error,
            Fatal
        }

        #endregion


    }
}
