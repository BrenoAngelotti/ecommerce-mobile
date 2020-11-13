﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Models;

namespace eCommerce.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Get();
    }

    public class ProductService : IProductService
    {
        static List<Product> Products = new List<Product>();

        public async Task<IEnumerable<Product>> Get()
        {
            //Mock
            await Task.Delay(2000);

            Products.Add(new Product()
            {
                Id = 0,
                Title = "Mouse sem fio Logitech MX Anywhere 2S",
                PictureURL = "https://images-na.ssl-images-amazon.com/images/I/6170mJHIsYL._AC_SX522_.jpg",
                UnitPrice = 335.0m
            });

            Products.Add(new Product()
            {
                Id = 1,
                Title = "Teclado sem fio Logitech MX Keys",
                PictureURL = "https://images-na.ssl-images-amazon.com/images/I/51McsA9O08L._AC_SX679_.jpg",
                UnitPrice = 705.0m
            });

            Products.Add(new Product()
            {
                Id = 2,
                Title = "Mouse Pad Extra Grande de Tecido Logitech G840",
                PictureURL = "https://images-na.ssl-images-amazon.com/images/I/61fS7SGgjML._AC_SX679_.jpg",
                UnitPrice = 296.9m
            });

            Products.Add(new Product()
            {
                Id = 3,
                Title = "Organizador de mesa porta canetas",
                PictureURL = "https://images-na.ssl-images-amazon.com/images/I/71X6AgMHU2L._AC_SX522_.jpg",
                UnitPrice = 34.9m
            });

            Products.Add(new Product()
            {
                Id = 4,
                Title = "Copo Térmico",
                PictureURL = "https://images-na.ssl-images-amazon.com/images/I/51X2UICkojL._AC_SX522_.jpg",
                UnitPrice = 24.8m
            });

            //End mock

            return await Task.FromResult(Products);
        }
    }
}
