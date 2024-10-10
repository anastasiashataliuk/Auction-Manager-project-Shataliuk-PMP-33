using AutoMapper;
using DAL.Concrete;
using DAL.Interface;
using DALEF.Concrete;
using DALEF.MappingProfile;
using DTO;

internal class Program
{
    private static void Main(string[] args)
    {
        string connStr = "Data Source=DESKTOP-7L66KNT\\SQLEXPRESS;Initial Catalog=AuctionDB;Integrated Security=True;TrustServerCertificate=True";

        // AutoMapper Configuration for Auctions and Products
        MapperConfiguration config = new MapperConfiguration(c => c.AddMaps(typeof(AuctionProfile).Assembly));

        Console.WriteLine("Welcome to Auction Manager!\n");
        char option = 's';

        while (option != 'q')
        {
            Console.WriteLine("Please select an option from the menu below:\n" +
                "1. - List all Auctions\n" +
                "2. - Get auction by ID \n" +
                "3. - Add a new Auction \n" +
                "4. - Update an Auction \n" +
                "5. - Delete an Auction \n" +
                "6. - List all Products \n" +
                "7. - Get Product by ID \n" +
                "8. - Add a new Product \n" +
                "9. - Update a Product \n" +
                "a. - Delete a Product \n" +
                "b. - List Products in an Auction \n" +
                "c. - Search Products by Name \n" +
                "d. Add Product to Auction \n" +
                "e. Update Product Quantity in Auction\n" +
                "f. Remove Product from Auction \n" +
                "Q. - Quit\n");

            string selectedOption = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(selectedOption) || selectedOption.Trim().Length > 1)
            {
                Console.WriteLine("Incorrect option selected!");
                continue;
            }

            option = Convert.ToChar(selectedOption.Trim().ToLower());

            if (option == 'q')
            {
                return;
            }

            switch (option)
            {
                case '1':
                    ListAllAuctions();
                    break;
                case '2':
                    GetAuctionById();
                    break;
                case '3':
                    AddAuction();
                    break;
                case '4':
                    UpdateAuction();
                    break;
                case '5':
                    DeleteAuction();
                    break;
                case '6':
                    ListAllProducts();
                    break;
                case '7':
                    GetProductById();
                    break;
                case '8':
                    AddProduct();
                    break;
                case '9':
                    UpdateProduct();
                    break;
                case 'a':
                    DeleteProduct();
                    break;
                case 'b':
                    ListProductsInAuction();
                    break;
                case 'c':
                    SearchProductsByName();
                    break;
                case 'd':
                    AddProductToAuction();
                    break;
                case 'e':
                    UpdateProductQuantityInAuction();
                    break;
                case 'f':
                    RemoveProductFromAuction();
                    break;
                default:
                    Console.WriteLine("Incorrect option selected!");
                    break;
            }
        }


        void AddAuction()
        {
            Console.WriteLine("Please enter Auction Start Date (yyyy-mm-dd):");
            string startDateInput = Console.ReadLine();
            DateTime startDate;

            // Перевірка, чи введена дата коректна
            while (!DateTime.TryParse(startDateInput, out startDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid Auction Start Date (yyyy-mm-dd):");
                startDateInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter Auction End Date (yyyy-mm-dd):");
            string endDateInput = Console.ReadLine();
            DateTime endDate;

            // Перевірка, чи введена дата коректна
            while (!DateTime.TryParse(endDateInput, out endDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid Auction End Date (yyyy-mm-dd):");
                endDateInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter Starting Price:");
            string startingPriceInput = Console.ReadLine();
            decimal startingPrice;

            // Перевірка, чи введена ціна коректна
            while (!decimal.TryParse(startingPriceInput, out startingPrice))
            {
                Console.WriteLine("Invalid price format. Please enter a valid Starting Price:");
                startingPriceInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter Buyout Price:");
            string buyoutPriceInput = Console.ReadLine();
            decimal buyoutPrice;

            // Перевірка, чи введена ціна коректна
            while (!decimal.TryParse(buyoutPriceInput, out buyoutPrice))
            {
                Console.WriteLine("Invalid price format. Please enter a valid Buyout Price:");
                buyoutPriceInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter Status:");
            string status = Console.ReadLine() ?? string.Empty; // Оголошуємо та присвоюємо значення

            // Тепер ви можете створити новий аукціон
            var auction = new Auction
            {
                Start_Date = startDate,
                End_Date = endDate,
                Starting_Price = startingPrice,
                Buyout_Price = buyoutPrice,
                Status = status // Тепер все має працювати
            };

            // Ваш код для вставки аукціону у базу даних
            var auctionDal = new AuctionDalEf(connStr, config.CreateMapper());
            Auction newAuction = auctionDal.Insert(auction);

            Console.WriteLine($"{newAuction.Auction_Id}.\t{newAuction.Start_Date.ToShortDateString()}\t{newAuction.End_Date.ToShortDateString()}\t{newAuction.Starting_Price}\t{newAuction.Buyout_Price}\t{newAuction.Status}");
        }




        // Метод для отримання аукціону за ID
        void GetAuctionById()
        {
            Console.WriteLine("Please enter Auction ID:");
            int auctionId = Convert.ToInt32(Console.ReadLine());

            var auctionDal = new AuctionDalEf(connStr, config.CreateMapper());
            Auction auction = auctionDal.GetById(auctionId);

            if (auction == null)
            {
                Console.WriteLine("Auction not found!");
                return;
            }

            Console.WriteLine($"{auction.Auction_Id}.\t{auction.Start_Date.ToShortDateString()}\t{auction.End_Date.ToShortDateString()}\t{auction.Starting_Price}\t{auction.Buyout_Price}\t{auction.Status}");
        }



        void ListAllAuctions()
        {
            var auctionDal = new AuctionDalEf(connStr, config.CreateMapper());
            List<Auction> auctions = auctionDal.GetAll();
            foreach (var auction in auctions)
            {
                Console.WriteLine($"{auction.Auction_Id}.\t{auction.Start_Date.ToShortDateString()}\t{auction.End_Date.ToShortDateString()}\t{auction.Starting_Price}\t{auction.Buyout_Price}\t{auction.Status}");
            }
        }


        void UpdateAuction()
        {
            Console.WriteLine("Please enter Auction ID to update:");
            int auctionId = Convert.ToInt32(Console.ReadLine());

            var auctionDal = new AuctionDalEf(connStr, config.CreateMapper());
            Auction auction = auctionDal.GetById(auctionId);

            if (auction == null)
            {
                Console.WriteLine("Auction not found!");
                return;
            }

            Console.WriteLine("Please enter new Start Date (leave blank to keep current):");
            string? startDateInput = Console.ReadLine();
            if (DateTime.TryParse(startDateInput, out DateTime startDate))
            {
                auction.Start_Date = startDate;
            }

            Console.WriteLine("Please enter new End Date (leave blank to keep current):");
            string? endDateInput = Console.ReadLine();
            if (DateTime.TryParse(endDateInput, out DateTime endDate))
            {
                auction.End_Date = endDate;
            }

            Console.WriteLine("Please enter new Starting Price (leave blank to keep current):");
            string? startingPriceInput = Console.ReadLine();
            if (decimal.TryParse(startingPriceInput, out decimal startingPrice))
            {
                auction.Starting_Price = startingPrice;
            }

            Console.WriteLine("Please enter new Buyout Price (leave blank to keep current):");
            string? buyoutPriceInput = Console.ReadLine();
            if (decimal.TryParse(buyoutPriceInput, out decimal buyoutPrice))
            {
                auction.Buyout_Price = buyoutPrice;
            }

            Console.WriteLine("Please enter new Status (leave blank to keep current):");
            string? status = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(status))
            {
                auction.Status = status;
            }

            auctionDal.Update(auction);

            Console.WriteLine($"Auction updated: {auction.Auction_Id}.\t{auction.Start_Date.ToShortDateString()}\t{auction.End_Date.ToShortDateString()}\t{auction.Starting_Price}\t{auction.Buyout_Price}\t{auction.Status}");
        }

        void DeleteAuction()
        {
            Console.WriteLine("Please enter Auction ID to delete:");
            int auctionId = Convert.ToInt32(Console.ReadLine());

            var auctionDal = new AuctionDalEf(connStr, config.CreateMapper());
            Auction auction = auctionDal.GetById(auctionId);

            if (auction == null)
            {
                Console.WriteLine("Auction not found!"); // Перевірте, чи існує аукціон перед видаленням
                return;
            }

            auctionDal.Delete(auctionId);
            Console.WriteLine($"Auction {auctionId} deleted successfully.");
        }

        // Method to List All Products
        void ListAllProducts()
        {
            var productDal = new ProductDalEf(connStr, config.CreateMapper());
            List<Product> products = productDal.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Product_Id}.\t{product.Name}\t{product.Description}\t{product.Category}");
            }
        }

        // Method to Get Product by ID
        void GetProductById()
        {
            Console.WriteLine("Please enter Product ID:");
            int productId = Convert.ToInt32(Console.ReadLine());

            var productDal = new ProductDalEf(connStr, config.CreateMapper());
            Product product = productDal.GetById(productId);

            Console.WriteLine($"{product.Product_Id}.\t{product.Name}\t{product.Description}\t{product.Category}");
        }

        // Method to Add a New Product

        // Method to Add a New Product
        void AddProduct()
        {
            Console.WriteLine("Please enter Product Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Please enter Product Description:");
            string description = Console.ReadLine();

            Console.WriteLine("Please enter Product Category:");
            string category = Console.ReadLine(); // Змінено на просто Category

            var product = new Product
            {
                Name = name,
                Description = description,
                Category = category // Змінено на просто Category
            };

            var productDal = new ProductDalEf(connStr, config.CreateMapper());
            Product newProduct = productDal.Insert(product);

            Console.WriteLine($"{newProduct.Product_Id}.\t{newProduct.Name}\t{newProduct.Description}\t{newProduct.Category}");
        }


        // Method to Update an Existing Product
        void UpdateProduct()
        {
            Console.WriteLine("Please enter Product ID to update:");
            int productId = Convert.ToInt32(Console.ReadLine());

            var productDal = new ProductDalEf(connStr, config.CreateMapper());
            Product product = productDal.GetById(productId);

            Console.WriteLine("Enter new Product Name :");
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                product.Name = name;
            }

            Console.WriteLine("Enter new Product Description:");
            string? description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                product.Description = description;
            }

            Console.WriteLine("Enter new Product Category:");
            string? category = Console.ReadLine(); // Змінено на просто Category
            if (!string.IsNullOrWhiteSpace(category))
            {
                product.Category = category; // Змінено на просто Category
            }

            productDal.Update(product); // Update the product in the database

            Console.WriteLine($"Product {product.Product_Id} updated successfully.");
        }

        // Method to Delete a Product
        void DeleteProduct()
        {
            Console.WriteLine("Please enter Product ID to delete:");
            int productId = Convert.ToInt32(Console.ReadLine());

            var productDal = new ProductDalEf(connStr, config.CreateMapper());
            productDal.Delete(productId); // Delete the product from the database

            Console.WriteLine($"Product {productId} deleted successfully.");
        }

        // Method to List Products in an Auction
        void ListProductsInAuction()
        {
            Console.WriteLine("Please enter Auction ID to list products:");
            int auctionId = Convert.ToInt32(Console.ReadLine());

            var auctionProductDal = new AuctionProductDalEf(connStr, config.CreateMapper());
            List<Product> products = auctionProductDal.GetProductsInAuction(auctionId);
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Product_Id}.\t{product.Name}\t{product.Description}\t{product.Category}");
            }
        }

        void SearchProductsByName()
        {
            Console.WriteLine("Please enter a keyword to search for products:");
            string? keyword = Console.ReadLine();

            // Перевірка на null або порожнє значення
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Keyword cannot be empty. Please enter a valid keyword.");
                return; // Виходимо з методу, якщо ключове слово порожнє
            }

            var productDal = new ProductDalEf(connStr, config.CreateMapper());
            List<Product> products = productDal.SearchByName(keyword);

            // Перевірка наявності продуктів
            if (products.Count == 0)
            {
                Console.WriteLine("No products found matching the keyword.");
                return; // Виходимо, якщо продукти не знайдено
            }

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Product_Id}.\t{product.Name}\t{product.Description}\t{product.Category}");
            }
        }


        void AddProductToAuction()
        {
            Console.WriteLine("Please enter Auction ID to add product:");
            int auctionId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter Product ID to add:");
            int productId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter Quantity:");
            decimal quantity = Convert.ToDecimal(Console.ReadLine());

            var auctionProductDal = new AuctionProductDAL(connStr);
            auctionProductDal.AddProductToAuction(auctionId, productId, quantity);

            Console.WriteLine($"Product {productId} with quantity {quantity} added to Auction {auctionId}.");
        }

        void UpdateProductQuantityInAuction()
        {
            Console.WriteLine("Please enter Auction ID to update product quantity:");
            int auctionId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter Product ID to update quantity:");
            int productId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter new Quantity:");
            decimal newQuantity = Convert.ToDecimal(Console.ReadLine());

            var auctionProductDal = new AuctionProductDAL(connStr);
            auctionProductDal.UpdateProductQuantityInAuction(auctionId, productId, newQuantity);

            Console.WriteLine($"Product {productId} quantity updated to {newQuantity} in Auction {auctionId}.");
        }

        void RemoveProductFromAuction()
        {
            Console.WriteLine("Please enter Auction ID to remove product:");
            int auctionId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter Product ID to remove:");
            int productId = Convert.ToInt32(Console.ReadLine());

            var auctionProductDal = new AuctionProductDAL(connStr);
            auctionProductDal.RemoveProductFromAuction(auctionId, productId);

            Console.WriteLine($"Product {productId} removed from Auction {auctionId}.");
        }
    }
}