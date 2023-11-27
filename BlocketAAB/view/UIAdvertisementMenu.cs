using BlocketAAB.Entities;
using BlocketAAB.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocketAAB.view
{
    public class UIAdvertisementMenu
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IAdvertisementService advertisementService;

        public UIAdvertisementMenu(IAdvertisementService advertisementService)
        {
            this.advertisementService = advertisementService;
        }


        public void DisplayAdvertisementMenu()
        {
            Console.Clear(); 

            int choice;

            Console.WriteLine($"                            ╔════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"                            ║                  Advertisement Management Menu                     ║");
            Console.WriteLine($"                            ╠════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"                            ║  1. View All Advertisements                                        ║");
            Console.WriteLine($"                            ║  2. Add Advertisement                                              ║");
            Console.WriteLine($"                            ║  3. Update Advertisement                                           ║");
            Console.WriteLine($"                            ║  4. Delete Advertisement                                           ║");
            Console.WriteLine($"                            ║  5. Search Advertisement by Category or Title                      ║");
            Console.WriteLine($"                            ║  6. Back to Main Menu                                              ║");
            Console.WriteLine($"                            ╚════════════════════════════════════════════════════════════════════╝");

            do
            {
              
                Console.Write("Enter your choice (1, 2, 3, 4, 5, or 6): ");
            
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid option (1, 2, 3, 4, 5, or 6).");
                    Console.Write("Enter your choice: ");
                }
                
                PerformAdvertisementMenuAction(choice);
               

            } while (choice != 6);
        }
        private void PerformAdvertisementMenuAction(int choice)
        {
            switch (choice)
            {
                case 1:
                    ViewAllAdvertisements();
                    break;
                case 2:
                    AddAdvertisement();
                    break;
                case 3:
                    UpdateAdvertisement();
                    break;
                case 4:
                    DeleteAdvertisement();
                    break;
                case 5:
                    SearchAdvertisement();
                    break;
                case 6:
                   
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        public void ViewAllAdvertisements()
        {
            var advertisements = advertisementService.GetAll();
            DisplayAllAdvertisements(advertisements);
        }

        public void DisplayAllAdvertisements(List<Advertisement> advertisements)
        {
            Console.WriteLine($"                    ╔═════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"                    ║                                  All Advertisements                                 ║");
            Console.WriteLine($"                    ╠═════════════════════════════════════════════════════════════════════════════════════╣");

            if (advertisements != null && advertisements.Count > 0)
            {
                foreach (var ad in advertisements)
                {
                    Console.WriteLine($"                    ║  ID: {ad.AdvertisementId,-3}  Title: {ad.Title,-30} Price: {ad.Price,-10:C}                 ║");
                    Console.WriteLine($"                    ║  Description: {ad.Description,-70}║");
                    Console.WriteLine($"                    ║  -----------------------------------------------------------------------------------║");
                }
            }
            else
            {
                Console.WriteLine($"║  No advertisements found.                                         ║");
            }

            Console.WriteLine($"                    ╚═════════════════════════════════════════════════════════════════════════════════════╝");
        }


        public void AddAdvertisement()
        {
            Console.Write("Enter the title of the new advertisement: ");
            string title = Console.ReadLine();

            Console.Write("Enter the description of the new advertisement: ");
            string description = Console.ReadLine();

            decimal price;
            while (true)
            {
                Console.Write("Enter the price of the new advertisement: ");
                if (decimal.TryParse(Console.ReadLine(), out price))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid price. Please enter a valid decimal value.");
                }
            }

            int categoryId;
            while (true)
            {
                Console.Write("Enter the category ID of the new advertisement: ");
                if (int.TryParse(Console.ReadLine(), out categoryId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid category ID. Please enter a valid integer value.");
                }
            }

            var newAdvertisement = new Advertisement
            {
                Title = title,
                Description = description,
                Price = price,
                CategoryId = categoryId
            };

            bool isAdded = advertisementService.Add(newAdvertisement);

            if (isAdded)
            {
                Console.WriteLine($"Advertisement '{title}' added successfully.");
            }
            else
            {
                Console.WriteLine($"Error adding advertisement '{title}'.");
            }
        }

        public void UpdateAdvertisement()
        {
            int advertisementId;
            while (true)
            {
                Console.Write("Enter the ID of the advertisement to update: ");
                if (int.TryParse(Console.ReadLine(), out advertisementId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid advertisement ID. Please enter a valid integer value.");
                }
            }

            var existingAdvertisement = advertisementService.Get(advertisementId);

            if (existingAdvertisement != null)
            {
                Console.Write("Enter the new title for the advertisement: ");
                string newTitle = Console.ReadLine();

                Console.Write("Enter the new description for the advertisement: ");
                string newDescription = Console.ReadLine();

                decimal newPrice;
                while (true)
                {
                    Console.Write("Enter the new price for the advertisement: ");
                    if (decimal.TryParse(Console.ReadLine(), out newPrice))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid new price. Please enter a valid decimal value.");
                    }
                }

                int newCategoryId;
                while (true)
                {
                    Console.Write("Enter the new category ID for the advertisement: ");
                    if (int.TryParse(Console.ReadLine(), out newCategoryId))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid new category ID. Please enter a valid integer value.");
                    }
                }

                var updatedAdvertisement = new Advertisement
                {
                    AdvertisementId = advertisementId,
                    Title = newTitle,
                    Description = newDescription,
                    Price = newPrice,
                    CategoryId = newCategoryId
                };

                bool isUpdated = advertisementService.Update(updatedAdvertisement);

                if (isUpdated)
                {
                    Console.WriteLine($"Advertisement with ID '{advertisementId}' updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Error updating advertisement with ID '{advertisementId}'.");
                }
            }
            else
            {
                Console.WriteLine($"Advertisement with ID '{advertisementId}' does not exist.");
            }
        }

        public void DeleteAdvertisement()
        {
            int advertisementId;
            while (true)
            {
                Console.Write("Enter the ID of the advertisement to delete: ");
                if (int.TryParse(Console.ReadLine(), out advertisementId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid advertisement ID. Please enter a valid integer value.");
                }
            }

            advertisementService.Delete(advertisementId);
            Console.WriteLine($"Deleting advertisement with ID '{advertisementId}'.");
        }

        public void SearchAdvertisement()
        {
            Console.Write("Enter the search term (category or title): ");
            string searchTerm = Console.ReadLine();

            var searchResults = advertisementService.Search(searchTerm);
            DisplayAllAdvertisements(searchResults);
        }
        public void DisplayInvalidChoiceMessage()
        {
            Console.WriteLine($"╔════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║            Invalid choice. Please enter a valid option.            ║");
            Console.WriteLine($"╚════════════════════════════════════════════════════════════════════╝");
        }
    }
}
