    using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Nest;
using Shop.DataAccess.DTOs;
using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repository.IRepositories.Repositories
{
    public class LinqOpeations:ILinqOpeations
    {
        private readonly ShopDbContext _shopDbContext;
        public LinqOpeations(ShopDbContext shopDbContext)
        {
          _shopDbContext = shopDbContext;
        }

        // Method Syntax(Fluent Syntax or Lambda Syntax)

        public async Task<LinqOperationResultDto> Some_Linq_operation()
        {

            // Get products with price greater than the threshold
            var expensiveProducts = await _shopDbContext.Products
                                                        .Where(p => p.Price > 200)
                                                        .Select(p => new ProductDto{Name = p.Name,Price = p.Price})
                                                        .ToListAsync();
            // Get a specific product by name
            var specificProduct = await _shopDbContext.Products.Where(p => p.Name == "BAG")
                                                       .Select(p => new ProductDto { Name = p.Name, Price = p.Price })
                                                     .ToListAsync();
            // Get products that belong to a specific category
            var categoryProducts = await _shopDbContext.Products
                                                       .Where(p => p.CategoryId == 5)
                                                       .Select(p => new ProductDto1
                                                       {
                                                           Name = p.Name,
                                                           Price = p.Price,
                                                           CategoryId = 5,
                                                         
                                                       })
                                                       .ToListAsync();


           // var result1 =  await  _shopDbContext.Products.Sum(p => p.Price);
            var result2= await _shopDbContext.Products.Where(p => p.Name == "Bag")
                                                      .Select(p => new ProductDto { Name = p.Name,Price = p.Price })
                                                      .FirstOrDefaultAsync();
            /*  var result2 = await _context.Departments.Where(d => d.DepartmentName == "string").ToListAsync();
            var result3 = await _context.Departments.Where(d => d.DepartmentName == "string" && d.DepartmentId == 0).ToListAsync();
            var result4 = await _context.Departments.Where(d => d.DepartmentName.Contains("n")).ToListAsync();
            var result5 = await _context.Departments.Where(d => d.DepartmentName.Length == 5).ToListAsync();
            var result6 = await _context.Departments.Where(d => d.DepartmentName.StartsWith("s")).ToListAsync();
            var result7 = await _context.Departments.Where(d => d.DepartmentName == "Human Resources" && d.Roles.Any(x => x.DepartmentId == 1)).ToListAsync();
            var result8 = await _context.Departments.Where(d => d.DepartmentName == "string" && d.Roles.Any(x => x.RoleName == "string")).ToListAsync();
             * 
             */
            // Create and return the DTO with the results
            var response = new LinqOperationResultDto
            {
                ExpensiveProducts= expensiveProducts,
                SpecificProduct = specificProduct,
                CategoryProducts= categoryProducts ,
                //Result1 = result1,   
                Result2 = result2
            };
            return response;
        }
        public async Task<LinqOperationResultDto> where_filter_operation()
        {
            // Example: Filtering products with a price greater than 1000
            var expensiveProducts = await (from p in _shopDbContext.Products
                                where p.Price>200
                                select  new ProductDto
                                { 
                                    ProductId=p.ProductId,
                                    Name=p.Name,
                                    Price=p.Price
                                }).ToListAsync();

            // Example: Filtering a specific product by ProductId
            var specificProductId = 1;
            var specificProduct=await (from p in _shopDbContext.Products
                                       where p.ProductId== specificProductId
                                       select new ProductDto
                                       {
                                           ProductId=p.ProductId,
                                           Price=p.Price,
                                           Name=p.Name
                                       }
                                       ).ToListAsync();

            // Example: Filtering products by CategoryId
            int categoryId = 2; // Replace with actual CategoryId
            var categoryProducts = await (
                                            from p in _shopDbContext.Products
                                            where p.CategoryId == categoryId
                                            select new ProductDto1
                                            {
                                                CategoryId=categoryId,
                                                Name=p.Name,
                                                Price=p.Price,
                                                ProductId=p.ProductId,
                                                ProductDescription=p.ProductDescription,
                                                ProductImageUrl=p.ProductImageUrl,
                                                ProductShortName=p.ProductShortName,
                                                ProductSku=p.ProductSku,
                                                CategoryName=p.CategoryName
                                            }
                                        ).ToListAsync();
            // Example: Combining multiple_conditions in a where clause
            var multiple = await (
                                    from p in _shopDbContext.Products
                                    where p.Price>200 && p.CategoryId==4
                                    select new ProductDto1
                                    {
                                        Name=p.Name , 
                                        Price=p.Price, 
                                        ProductId=p.ProductId,
                                        CategoryId=p.ProductId,
                                        CategoryName =p.CategoryName,
                                        ProductDescription=p.ProductDescription,
                                        ProductImageUrl=p.ProductImageUrl,
                                        ProductShortName=p.ProductShortName,
                                        ProductSku=p.ProductSku,

                                    }
                                ).ToListAsync();

          
            var response = new LinqOperationResultDto
            {
                ExpensiveProducts=expensiveProducts,
                SpecificProduct=specificProduct,
                CategoryProducts=categoryProducts,
                Multiple_conditions=multiple,
            };

            return response;
        }
        public async Task<LinqOperationResultDto> Join_operations()
        {
            var join_operation = await (from c in _shopDbContext.Carts
                                        join p in _shopDbContext.Products on c.ProductId equals p.ProductId
                                        join u in  _shopDbContext.UsersInfos on c.UserId equals u.UserId
                                        select new ProductCartDto
                                        {
                                            CartId = c.CartId,
                                            UserId = c.UserId,
                                            ProductId = c.ProductId,
                                            Quantity = c.Quantity,
                                            CategoryName=p.CategoryName,
                                            Name=p.Name,
                                            Price = p.Price
                                                                                     
                                        }).ToListAsync();

            var response = new LinqOperationResultDto
            {
                JoinOperation = join_operation

            };

            return response;
        }
        // join operation using Query Syntax (Query Expression Syntax)
        public async Task<bool> UpdateCartAsync(int cartId, CartDto updatedCartDto)
        {
            // Step 1: Retrieve the existing cart item from the database
           // var cartItem = await _shopDbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId); //method approch
    
            var cartItem = await (from c in _shopDbContext.Carts
                                   where  c.CartId == cartId
                                   select c
                                 ).FirstOrDefaultAsync();
            if (cartItem == null)
            {
                // If the cart item does not exist, return false
                return false;
            }

            // Step 2: Modify the entity with the updated values
            cartItem.Quantity = updatedCartDto.Quantity;

            // Optionally update other fields if necessary
            // cartItem.ProductId = updatedCartDto.ProductId;
            // cartItem.UserId = updatedCartDto.UserId;
            // etc.

            // Step 3: Save the changes back to the database
           var check= await _shopDbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteCartAsync(int cartId)
        {
            var cartItem = await _shopDbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);

            if (cartItem == null)
            {
                return false; // Return false if the cart item does not exist
            }

            _shopDbContext.Carts.Remove(cartItem); // Mark the cart item for deletion
            await _shopDbContext.SaveChangesAsync(); // Save changes to the database

            return true; // Return true if the deletion was successful
        }
        public async Task<bool> AddCartAsync(CartDto newCartDto)
        {
            var cartItem = new Cart
            {
                UserId = newCartDto.UserId,
                ProductId = newCartDto.ProductId,
                Quantity = newCartDto.Quantity
            };

            _shopDbContext.Carts.Add(cartItem); // Add the new cart item to the context
            await _shopDbContext.SaveChangesAsync(); // Save changes to the database

            return true; // Return true if the insertion was successful
        }
        public async Task<List<ProductDto1>> FilterProductsAsync(CardDto card)
        {
            var query = _shopDbContext.Products.AsQueryable();

            if (card.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == card.CategoryId.Value);
            }

            if (card.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= card.MinPrice.Value);
            }

            if (card.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= card.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(card.ProductName))
            {
                query = query.Where(p => p.Name.Contains(card.ProductName));
            }

            if (!string.IsNullOrEmpty(card.ProductSku))
            {
                query = query.Where(p => p.ProductSku == card.ProductSku);
            }

            if (card.CreatedAfter.HasValue)
            {
                query = query.Where(p => p.CreatedDate >= card.CreatedAfter.Value);
            }

            var filteredProducts = await query
                .Select(p => new ProductDto1
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.CategoryName,
                    ProductSku = p.ProductSku,
                    ProductImageUrl = p.ProductImageUrl
                })
                .ToListAsync();

            return filteredProducts;
        }
        
    }
}
/***
 * ToList
 * FirstOrDefault
 * SingleOrDefault 
 * Where
 * Select
 * OrderBy() / OrderByDescending()
 * Count
 * Any
 * Find
 * include
 */