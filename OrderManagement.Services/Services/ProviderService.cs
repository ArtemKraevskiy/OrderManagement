using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderManagement.Data.Entities.OrderManagement;
using OrderManagement.Infrastructure.Extensions;
using OrderManagement.Models.DataTransferObjects;
using OrderManagement.Models.ViewModels;
using OrderManagement.Repositories.Abstractions;
using OrderManagement.Services.Abstractions;
using System.Linq.Expressions;

namespace OrderManagement.Services.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Provider> _providerRepository;

        public ProviderService(IMapper mapper, IBaseRepository<Provider> providerRepository)
        {
            _mapper = mapper;
            _providerRepository = providerRepository;
        }

        public async Task<List<ProviderDto>> GetAsync(ProviderSearchViewModel searchModel)
        {
            Expression<Func<Provider, bool>> mainPpredicate = _ => true;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                Expression<Func<Provider, bool>> predicate = _ => false;
                var terms = searchModel.Name.ToLower().Split(" ");
                foreach (var term in terms)
                    predicate = predicate.OrElse(_ => _.Name.ToLower().Contains(term));
                mainPpredicate = mainPpredicate.AndAlso(predicate);
            }
            var entities = await _providerRepository.GetAsync(mainPpredicate);
            var models = _mapper.Map<List<ProviderDto>>(entities);
            return models;
        }

        public async Task<ProviderDto> GetByIdAsync(int Id)
        {
            var entity = await _providerRepository.GetByIdAsync(Id);
            return _mapper.Map<ProviderDto>(entity);
        }

        public async Task<List<SelectListItem>> GetSelectListAsync()
        {
            var entities = await _providerRepository.AllAsync();
            var selectList = entities.Select(entity => new SelectListItem
            {
                Value = entity.Id.ToString(),
                Text = entity.Name
            }).ToList();
            selectList.Insert(0, new SelectListItem { Value = null, Text = "" });
            return selectList;
        }
    }
}
