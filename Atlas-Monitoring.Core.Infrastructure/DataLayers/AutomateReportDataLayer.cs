using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class AutomateReportDataLayer : IAutomateReportDataLayer
    {
        #region Properties

        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public AutomateReportDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task CreateNewReport(AutomateReportWriteViewModel newReport)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                AutomateReport newBddReport = new()
                {
                    Id = Guid.NewGuid(),
                    Entity = newReport.EntityName == string.Empty ? null : _context.Entity.Where(item => item.Name == newReport.EntityName).FirstOrDefault(),
                    AppName = newReport.AppName,
                    Status = newReport.Status,
                    GlobalMessage = newReport.GlobalMessage,
                    DurationInSeconds = newReport.DurationInSeconds,
                    CreatedAt = DateTime.Now
                };

                await _context.AutomateReport.AddAsync(newBddReport);

                foreach (AutomateLogWriteViewModel automateLog in newReport.ListOfLogs)
                {
                    AutomateLog newLog = new()
                    {
                        Id = Guid.NewGuid(),
                        AutomateReport = newBddReport,
                        Comment = automateLog.Comment,
                        LogLevel = automateLog.LogLevel
                    };

                    await _context.AutomateLog.AddAsync(newLog);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new CustomDataBaseException($"An unhandled Exception occured ! Message : {ex.Message}");
            }
        }
        #endregion

        #region Read
        public async Task<List<AutomateReportReadViewModel>> GetReportBetweenDate(DateTime startDate, DateTime endDate)
        {
            List<AutomateReport> listOfBddReports = await _context.AutomateReport.Where(item => item.CreatedAt >= startDate && item.CreatedAt <= endDate).Include(item => item.Entity).ToListAsync();
            List<AutomateReportReadViewModel> listOfReadViewModel = new();

            foreach (AutomateReport bddAutomateReport in listOfBddReports)
            {
                listOfReadViewModel.Add(TransformDataBaseModelToViewModel(bddAutomateReport, await _context.AutomateLog.Where(item => item.AutomateReport.Id == bddAutomateReport.Id).ToListAsync()));
            }

            return await Task.FromResult(listOfReadViewModel);
        }
        public async Task<AutomateReportReadViewModel> GetOneReportById(Guid reportId)
        {
            if (await _context.AutomateReport.Where(item => item.Id == reportId).AnyAsync())
            {
                return TransformDataBaseModelToViewModel(await _context.AutomateReport.Where(item => item.Id == reportId).SingleAsync(), await _context.AutomateLog.Where(item => item.AutomateReport.Id == reportId).ToListAsync());

            }
            else
            {
                throw new CustomNoContentException($"No data with ReportId {reportId}");
            }
        }
        #endregion

        #region Delete
        public async Task DeleteOneReportById(Guid reportId)
        {
            if (await _context.AutomateReport.Where(item => item.Id == reportId).AnyAsync())
            {
                //Delete Report Logs
                await _context.Database.ExecuteSqlAsync($"DELETE FROM AutomateLog WHERE AutomateReportId = {reportId}");
                await _context.SaveChangesAsync();

                //Delete Report
                await _context.Database.ExecuteSqlAsync($"DELETE FROM AutomateReport WHERE Id = {reportId}");
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomNoContentException($"No data with ReportId {reportId}");
            }
        }
        #endregion
        #endregion

        #region Private Methods
        private AutomateReportReadViewModel TransformDataBaseModelToViewModel(AutomateReport automateReport, List<AutomateLog> automateLogs)
        {
            AutomateReportReadViewModel result = new AutomateReportReadViewModel()
            {
                Id = automateReport.Id,
                EntityName = automateReport.Entity is not null ? automateReport.Entity.Name : string.Empty,
                AppName = automateReport.AppName,
                Status = automateReport.Status,
                GlobalMessage = automateReport.GlobalMessage,
                DurationInSeconds = automateReport.DurationInSeconds,
                CreatedAt = automateReport.CreatedAt,
                ListOfLogs = new()
            };

            foreach (AutomateLog log in automateLogs)
            {
                result.ListOfLogs.Add(new()
                {
                    Id = log.Id,
                    AutomateId = automateReport.Id,
                    Comment = log.Comment,
                    LogLevel = log.LogLevel
                });
            }

            return result;
        }
        #endregion
    }
}
