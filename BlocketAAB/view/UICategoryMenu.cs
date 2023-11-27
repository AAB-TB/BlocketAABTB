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
    public class UICategoryMenu
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ICategoryService categoryService;

        public UICategoryMenu(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public void DisplayCategoryMenu()
        {
            Console.Clear();
            int choice;

            
                Console.WriteLine($"                                              ╔══════════════════════════════════════════╗");
                Console.WriteLine($"                                              ║        Category Management Menu          ║");
                Console.WriteLine($"                                              ╠══════════════════════════════════════════╣");
                Console.WriteLine($"                                              ║  1. View All Categories                  ║");
                Console.WriteLine($"                                              ║  2. Add Category                         ║");
                Console.WriteLine($"                                              ║  3. Delete Category                      ║");
                Console.WriteLine($"                                              ║  4. Back to Main Menu                    ║");
                Console.WriteLine($"                                              ╚══════════════════════════════════════════╝");
                
            do
            {
                Console.Write("Enter your choice (1, 2, 3, or 4): ");
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid option (1, 2, 3, or 4).");
                    Console.Write("Enter your choice: ");
                }

                PerformCategoryMenuAction(choice);

            } while (choice != 4);
        }

        private void PerformCategoryMenuAction(int choice)
        {
            switch (choice)
            {
                case 1:
                    ViewAllCategories();
                    break;
                case 2:
                    AddCategory();
                    break;
                case 3:
                    DeleteCategory();
                    break;
                case 4:
                    Console.WriteLine("Returning to the Main Menu.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        public void ViewAllCategories()
        {
            var categories = categoryService.GetAll();
            DisplayAllCategories(categories);
        }

        public void DisplayAllCategories(List<Category> categories)
        {
            Console.WriteLine($"                                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"                                          ║                    All Categories                  ║");
            Console.WriteLine($"                                          ╠════════════════════════════════════════════════════╣");

            if (categories != null && categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    Console.WriteLine($"                                          ║ {category.CategoryId,-3}. {category.CategoryName,-45} ║");
                }
            }
            else
            {
                Console.WriteLine($"                                              ║  No categories found.                               ║");
            }

            Console.WriteLine($"                                          ╚════════════════════════════════════════════════════╝");
        }

        public void AddCategory()
        {
            Console.Write("Enter the name of the new category: ");
            string categoryName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(categoryName))
            {
                Console.WriteLine("Category name cannot be empty. Please enter a valid name.");
                Console.Write("Enter the name of the new category: ");
                categoryName = Console.ReadLine();
            }

            var newCategory = new Category { CategoryName = categoryName };
            int categoryId = categoryService.Add(newCategory);

            if (categoryId != -1)
            {
                Console.WriteLine($"Category '{categoryName}' added successfully with ID: {categoryId}");
            }
            else
            {
                Console.WriteLine($"Error adding category '{categoryName}'. Category may already exist.");
            }
        }

        public void DeleteCategory()
        {
            int categoryIdToDelete;

            while (true)
            {
                Console.Write("Enter the ID of the category to delete: ");
                if (int.TryParse(Console.ReadLine(), out categoryIdToDelete))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid category ID. Please enter a valid integer value.");
                }
            }

            bool isDeleted = categoryService.Delete(categoryIdToDelete);

            if (isDeleted)
            {
                Console.WriteLine($"Category with ID '{categoryIdToDelete}' deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Error deleting category with ID '{categoryIdToDelete}'. Category may not exist.");
            }
        }
        public void DisplayInvalidChoiceMessage()
        {
            Console.WriteLine($"╔════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║            Invalid choice. Please enter a valid option.            ║");
            Console.WriteLine($"╚════════════════════════════════════════════════════════════════════╝");
        }
    }
}
