using AgingInHome.BLL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgingInHome.DAL;

namespace AgingInHome.Models
{
    public class ServicesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal OnetimePrice { get; set; }
        public decimal RecPrice { get; set; }
        public string PromoteType { get; set; }
        public List<ServicesModel> servicelist { get; set; }
        public CheckoutDetailModel checkoutDetailModel { get; set; }
        public List<ServicesModel> GetAllServices()
        {
            var servicesList = new Services().GetAllServices();
            var _servicesList = Mapper.Map<List<ServicesModel>>(servicesList);
            return _servicesList;
        }

        public List<UsState> GetUsStates()
        {
            return new Services().GetAllUsStates();
        }
        public List<ServicesModel> AllSelectedServices(OrderModel _OrderModel)
        {
            ServicesModel servicesModel = new ServicesModel();
            var Result =
                from service in servicesModel.GetAllServices()
                join serviceId in _OrderModel.ServiceSelectedIds on service.Id equals serviceId
                select new ServicesModel { Id = service.Id, Name = service.Name, OnetimePrice = service.OnetimePrice, RecPrice = service.RecPrice,PromoteType= _OrderModel.PromoteType};
            return Result.ToList();
        }
    }
}