using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderManagement.Data.Entities.OrderManagement;
using OrderManagement.Infrastructure.Extensions;
using OrderManagement.Models.DataTransferObjects;
using OrderManagement.Models.ViewModels;
using OrderManagement.Repositories.Abstractions;
using OrderManagement.Services.Abstractions;
using System;
using System.Linq.Expressions;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrderManagement.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IProviderService _providerService;
        private readonly IOrderItemService _orderItemService;

        public OrderService(IMapper mapper, IBaseRepository<Order> orderRepository, IProviderService providerService, IOrderItemService orderItemService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _providerService = providerService;
            _orderItemService = orderItemService;
        }

        public async Task<List<OrderDto>> GetAsync(OrderSearchViewModel searchModel)
        {
            searchModel.StartDate = !searchModel.StartDate.HasValue ? DateTime.Now.AddMonths(-1) : searchModel.StartDate;
            searchModel.EndDate = !searchModel.EndDate.HasValue ? DateTime.Now : searchModel.EndDate;

            Expression<Func<Order, bool>> mainPpredicate = _ => _.Date > searchModel.StartDate && _.Date < searchModel.EndDate;
            mainPpredicate = searchModel.ProviderId > 0 ? mainPpredicate.AndAlso(_ => _.ProviderId == searchModel.ProviderId) : mainPpredicate;
            if (!string.IsNullOrEmpty(searchModel.Number))
            {
                Expression<Func<Order, bool>> predicate = _ => false;
                var terms = searchModel.Number.ToLower().Split(" ");
                foreach (var term in terms)
                    predicate = predicate.OrElse(_ => _.Number.ToLower().Contains(term));
                mainPpredicate = mainPpredicate.AndAlso(predicate);
            }

            var entities = await _orderRepository.GetAsync(mainPpredicate);
            var models = _mapper.Map<List<OrderDto>>(entities.ToList());
            foreach (var model in models)
            {
                model.Provider = await _providerService.GetByIdAsync(model.ProviderId);
            }

            return models;
        }

        public async Task<OrderDto> GetByIdAsync(int Id)
        {
            var entity = await _orderRepository.GetOneWithIncludeAsync(_ => _.Id == Id, _ => _.Provider);
            var model = _mapper.Map<OrderDto>(entity);
            model.OrderItems = await _orderItemService.GetAsync(Id);
            return model;
        }

        public async Task<OrderViewModel> CreateEditModelAsync(int Id)
        {
            var model = new OrderViewModel();
            model.Providers = await _providerService.GetSelectListAsync();
            if (Id > 0)
            {
                var entity = await _orderRepository.GetByIdAsync(Id);
                _mapper.Map(entity, model);
                model.OrderItems = _mapper.Map<List<OrderItemViewModel>>(await _orderItemService.GetAsync(Id));
                _orderItemService.SetSerialNumbers(model.OrderItems);
            }
            else
                model.Date = DateTime.Now;

            return model;
        }

        public async Task<bool> NumberValidationAsync(NumberValidationViewModel model)
        {
            if (model.Id == 0)
            {
                var entity = await _orderRepository.GetOneAsync(_ => _.Number == model.Number && _.ProviderId == model.ProviderId);
                return !(entity != null);
            }
            else
            {
                var entity = await _orderRepository.GetOneAsync(_ => _.Number == model.Number && _.ProviderId == model.ProviderId);
                if (entity == null)
                    return true;
                else
                    return entity.Id == model.Id;
            }
        }

        public async Task CreateAsync(OrderViewModel model)
        {
            model.OrderItems.RemoveAll(_ => _.Id == 0 && _.IsDeleted);
            var newEntity = _mapper.Map<Order>(model);
            await _orderRepository.CreateAsync(newEntity);
            await _orderRepository.CommitAsync();
        }

        public async Task UpdateAsync(OrderViewModel model)
        {
            model.OrderItems.RemoveAll(_ => _.Id == 0 && _.IsDeleted);
            var entity = await _orderRepository.GetByIdAsync(model.Id);
            _mapper.Map(model, entity);
            _orderRepository.Update(entity);
            await _orderRepository.CommitAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _orderItemService.DeleteRangeAsync(Id);
                await _orderRepository.DeleteAsync(Id);
                await _orderRepository.CommitAsync();
                scope.Complete();
            }
        }

        public async Task<OrderSearchViewModel> GetSearchModelAsync()
        {
            return new OrderSearchViewModel
            {
                Providers = await _providerService.GetSelectListAsync()
            };
        }
    }
}
