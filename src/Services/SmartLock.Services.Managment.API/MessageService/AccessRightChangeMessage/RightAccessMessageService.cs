//using SmartLock.MessageBroker;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SmartLock.Services.Managment.API.MessageService.AccessRightChangeMessage
//{
//    public class RightAccessMessageService:IAccessRightMessageService
//    {
//        private readonly MessageLogContext _integrationEventLogContext;
//        private readonly DbConnection _dbConnection;

//        public IntegrationEventLogService(DbConnection dbConnection)
//        {
//            _dbConnection = dbConnection ?? throw new ArgumentNullException("dbConnection");
//            _integrationEventLogContext = new IntegrationEventLogContext(
//                new DbContextOptionsBuilder<IntegrationEventLogContext>()
//                    .UseSqlServer(_dbConnection)
//                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
//                    .Options);
//        }

//        public Task SaveEventAsync(Message message, DbTransaction transaction)
//        {
//            if (transaction == null)
//            {
//                throw new ArgumentNullException("transaction", $"A {typeof(DbTransaction).FullName} is required as a pre-requisite to save the event.");
//            }

//            var eventLogEntry = new IntegrationEventLogEntry(@event);

//            _integrationEventLogContext.Database.UseTransaction(transaction);
//            _integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

//            return _integrationEventLogContext.SaveChangesAsync();
//        }

//        public Task MarkEventAsPublishedAsync(Message message)
//        {
//            var eventLogEntry = _integrationEventLogContext.IntegrationEventLogs.Single(ie => ie.EventId == @event.Id);
//            eventLogEntry.TimesSent++;
//            eventLogEntry.State = EventStateEnum.Published;

//            _integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

//            return _integrationEventLogContext.SaveChangesAsync();
//        }
//    }
//}
