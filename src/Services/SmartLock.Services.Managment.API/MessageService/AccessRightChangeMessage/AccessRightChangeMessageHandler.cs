//using SmartLock.MessageBroker;
//using SmartLock.Services.Managment.API.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SmartLock.Services.Managment.API.MessageService
//{
//    public class AccessRightChangeMessageHandler : IMessageHandler<AccessRightChangeMessage>
//    {
//        private readonly ManagmentDbContext _accessRightDbContext;
//        private readonly IAccessRightMessageService _accessRightMessageService;

//        public AccessRightChangeMessageHandler(ManagmentDbContext accessRightDbContext, IAccessRightMessageService accessRightMessagetService)
//        {
//            _accessRightDbContext = accessRightDbContext;
//            _accessRightMessageService = accessRightMessagetService;
//        }

//        public async Task Handle(AccessRightChangeMessage message)
//        {
//            //var confirmedOrderStockItems = new List<ConfirmedOrderStockItem>();

//            //foreach (var orderStockItem in command.OrderStockItems)
//            //{
//            //    var catalogItem = _catalogContext.CatalogItems.Find(orderStockItem.ProductId);
//            //    var hasStock = catalogItem.AvailableStock >= orderStockItem.Units;
//            //    var confirmedOrderStockItem = new ConfirmedOrderStockItem(catalogItem.Id, hasStock);

//            //    confirmedOrderStockItems.Add(confirmedOrderStockItem);
//            //}

//            var confirmedIntegrationEvent = confirmedOrderStockItems.Any(c => !c.HasStock)
//                ? (IntegrationEvent)new OrderStockRejectedIntegrationEvent(command.OrderId, confirmedOrderStockItems)
//                : new OrderStockConfirmedIntegrationEvent(command.OrderId);

//            await _accessRightMessageService.SaveMessageAndAccessRightContextChangesAsync(confirmedIntegrationEvent);
//            await _accessRightMessageService.PublishThroughMessageBrokerAsync(confirmedIntegrationEvent);
//        }
//    }
//}
