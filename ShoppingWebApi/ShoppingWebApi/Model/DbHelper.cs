﻿using System;
using ShoppingWebApi.EfCore;

namespace ShoppingWebApi.Model
{
	public class DbHelper
	{
		private EF_DataContext _context;

		public DbHelper(EF_DataContext context)
		{
			_context = context;
		}

		//GET all products
		public List<ProductModel> GetProducts()
		{
			List<ProductModel> response = new List<ProductModel>();
			var dataList = _context.Products.ToList();
			dataList.ForEach(row => response.Add(new ProductModel()
			{
				brand = row.brand,
				id = row.id,
				name = row.name,
				size = row.size
			}));
			return response;
		}


        public ProductModel GetProductById(int id)
        {
            ProductModel response = new ProductModel();
            var row = _context.Products.Where(d=>d.id.Equals(id)).FirstOrDefault();
			return new ProductModel()
			{
                brand = row.brand,
                id = row.id,
                name = row.name,
                size = row.size
            };
        }

        //It serves the POST/PUT/Patch
        public void SaveOrder(OrderModel orderModel)
		{
			Order dbTable = new Order();
			if(orderModel.id > 0)
			{
				//PUT
				dbTable = _context.Orders.Where(d => d.id.Equals(orderModel.id)).FirstOrDefault();
				if(dbTable != null)
				{
					dbTable.phone = orderModel.phone;
					dbTable.Address = orderModel.Address;
				}
			}
            else
            {
                //POST
                dbTable.phone = orderModel.phone;
                dbTable.Address = orderModel.Address;
                dbTable.name = orderModel.name;
                dbTable.Product = _context.Products.Where(f => f.id.Equals(orderModel.product_id)).FirstOrDefault();
                _context.Orders.Add(dbTable);
            }
            _context.SaveChanges();
        }

		//DELETE
		public void DeleteOrder(int id)
		{
			var order = _context.Orders.Where(d => d.id.Equals(id)).FirstOrDefault();
			if(order != null)
			{
				_context.Orders.Remove(order);
				_context.SaveChanges();  
			}
        }
	}
}

