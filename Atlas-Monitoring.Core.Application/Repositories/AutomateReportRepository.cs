using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;

namespace Atlas_Monitoring.Core.Application.Repositories
{
    public class AutomateReportRepository : IAutomateReportRepository
    {
        #region Properties
        private readonly IAutomateReportDataLayer _automateReportDataLayer;
        #endregion

        #region Constructor
        public AutomateReportRepository(IAutomateReportDataLayer automateReportDataLayer)
        {
            _automateReportDataLayer = automateReportDataLayer;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task CreateNewReport(AutomateReportWriteViewModel newReport)
        {
            CheckData(newReport);

            await _automateReportDataLayer.CreateNewReport(newReport);
        }
        #endregion

        #region Read
        public async Task<AutomateReportReadViewModel> GetOneReportById(Guid reportId)
        {
            return await _automateReportDataLayer.GetOneReportById(reportId);
        }

        public async Task<List<AutomateReportReadViewModel>> GetReportBetweenDate(DateTime startDate, DateTime endDate)
        {
            DateTime startDateMinTime = new(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, DateTimeKind.Utc);
            DateTime endDateMaxTime = new(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, DateTimeKind.Utc);

            if (startDateMinTime > endDateMaxTime) { throw new CustomModelException($"Started date can't be superior than the end date !"); }

            return await _automateReportDataLayer.GetReportBetweenDate(startDateMinTime, endDateMaxTime);
        }
        #endregion

        #region Update

        #endregion

        #region Delete
        public async Task DeleteOneReportById(Guid reportId)
        {
            await _automateReportDataLayer.DeleteOneReportById(reportId);
        }
        #endregion
        #endregion

        #region Private Methods
        private static void CheckData(AutomateReportWriteViewModel newReport)
        {
            if (newReport.AppName == string.Empty) { throw new CustomModelException($"AppName can't be empty !"); }
            if (newReport.GlobalMessage == string.Empty) { throw new CustomModelException($"GlobalMessage can't be empty !"); }

            if (newReport.AppName.Length > 60) { throw new CustomModelException($"Property 'AppName' would be truncate (60 characters max)"); }
            if (newReport.GlobalMessage.Length > 240) { throw new CustomModelException($"Property 'GlobalMessage' would be truncate (60 characters max)"); }

            foreach (AutomateLogWriteViewModel log in newReport.ListOfLogs)
            {
                if (log.Comment == string.Empty) { throw new CustomModelException($"Comment in one log can't be empty !"); }

                if (log.Comment.Length > 240) { throw new CustomModelException($"Property 'Comment' in one log would be truncate (240 characters max)"); }
            }
        }
        #endregion
    }
}
