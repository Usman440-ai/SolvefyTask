using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SolvefyTask.Data;
using SolvefyTask.Models;

namespace SolvefyTask.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {
            if (productDTO.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "the image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(productDTO);
            }
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssff");
            newFileName += Path.GetExtension(productDTO.ImageFile!.FileName);
            string imageFullPath = environment.WebRootPath + "/Product/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDTO.ImageFile.CopyTo(stream);
            }
            Product product = new Product()
            {
                Name = productDTO.Name,
                Brand = productDTO.Brand,
                Price = productDTO.Price,
                Descprition = productDTO.Descprition,
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now,
            };
            context.Products.Add(product);
            context.SaveChanges();




            return RedirectToAction("Index", "Products");
        }
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            var productDTO = new ProductDTO()
            {
                Name = product.Name,
                Brand = product.Brand,
                Cateogory = product.Cateogory,
                Price = product.Price,
                Descprition = product.Descprition,

            };
            ViewData["ProductId"] = product.Id;
            ViewData["ImageFileName"] = product.ImageFileName;
            ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");

            return View(productDTO);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductDTO productDTO)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index,Products");
            }



            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["ImageFileName"] = product.ImageFileName;
                ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
                return View(productDTO);
            }
            string newFileName = product.ImageFileName;
            if (productDTO.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssff");
                newFileName += Path.GetExtension(productDTO.ImageFile!.FileName);
                string imageFullPath = environment.WebRootPath + "/Product/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDTO.ImageFile.CopyTo(stream);
                }
                string oldimageFullpath = environment.WebRootPath + "/Product/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDTO.ImageFile.CopyTo(stream);
                }


                string oldImageFullPath = environment.WebRootPath + "/Product/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);

            }


            product.Name = productDTO.Name;
            product.Brand = productDTO.Brand;
            product.Cateogory = productDTO.Cateogory;
            product.Price = productDTO.Price;
            product.Descprition = productDTO.Descprition;
            product.ImageFileName = newFileName;


            context.SaveChanges();
            return RedirectToAction("Index", "Products");






        }
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");


            }
            string imageFullpath = environment.WebRootPath + "/Product/" + product.ImageFileName;
            System.IO.File.Delete(imageFullpath);
            context.Products.Remove(product);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Products");

        }
    }
}