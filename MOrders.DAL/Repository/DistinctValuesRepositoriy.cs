﻿using Microsoft.EntityFrameworkCore;
using MOrders.DAL.Data;
using MOrders.DAL.Interfaces;
using MOrders.DAL.Models;

namespace MOrders.DAL.Repository
{
    public class DistinctValuesRepositoriy : IDistinctRepository<DistinctValuesForSelect>
    {
        private readonly MOrdersContext _context;
        public DistinctValuesRepositoriy(MOrdersContext context)
        {
            _context = context;
        }
        public async Task<DistinctValuesForSelect> GetDistinct()
        {
            var number = await _context.Order.Select(o => o.Number).Distinct().AsNoTracking().ToListAsync();
            var providerName = await _context.Provider.Select(o => o.Name).Distinct().AsNoTracking().ToListAsync();
            var itemName = await _context.OrderItem.Select(o => o.Name).Distinct().AsNoTracking().ToListAsync();
            var Unit = await _context.OrderItem.Select(o => o.Unit).Distinct().AsNoTracking().ToListAsync();
            return new DistinctValuesForSelect() { Number = number, ItemName = itemName, ProviderName = providerName, Unit = Unit }; 
        }
    }
}