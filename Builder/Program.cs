using System;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ortaya bir nesne örneği çıkarmayı hedefler. Birbiri arkası atılacak adımların  sırasıyla işlenmesi sonucunda ortaya çıkar
            //Genellikle iş katmanında veya arayüz katmanında kodları if ile yazmak yerine ilgili üreticinin enjekte edilmesi ve ona göre ortaya bir nesne çıkarılması amacıyla kullanılıbilir.
            //Örneğin hamburger üretici varsa ve müşterinin isteğine bağlı ne çeşit hamburger üretilecğine karar veren sistem. Vejeteryan için ayrı bir çeşidin olması gibi örnek verilebilir.

            //Burada kullanilacak örnek ise; örn yeni kayıt olmuş müşterinin göreceği ekran ile eski üyenin göreceği ekran farklı şekilde gösterilmesi 

            ProductDirector productDirector = new ProductDirector();
            var builder = new NewCustomerProductBuilder();
            productDirector.GenerateProduct(builder); //Product oluştur ve bunu yeni müşteri için yap dedik

            var model = builder.GetModel();

            Console.WriteLine("Id : "+model.Id);
            Console.WriteLine("Product Name : "+model.ProductName);
            Console.WriteLine("CategoryName : "+model.CategoryName);
            Console.WriteLine("Discount Applied : "+model.DiscountApplied);
            Console.WriteLine("Discount Price : "+model.DiscountedPrice);
            Console.WriteLine("Unit Price : "+model.UnitPrice);


            Console.ReadLine();
        }
    }

    class ProductViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool DiscountApplied { get; set; }
    }

    abstract class ProductBuilder
    {
        public abstract void GetProductData();
        public abstract void ApplyDiscount();
        public abstract ProductViewModel GetModel(); //işlemler sonucunda üretilen modeli geriye döndürür.
    }

    class NewCustomerProductBuilder : ProductBuilder
    {
        ProductViewModel model = new ProductViewModel();
        public override void ApplyDiscount()
        {
            model.DiscountedPrice =  model.UnitPrice * (decimal) 0.90;//yeni müşteriye %10 indriim uygulandı
            model.DiscountApplied = true;
        }

        public override void GetProductData()
        {
            model.Id = 1;
            model.CategoryName = "Bevereges";
            model.ProductName = "Chai";
            model.UnitPrice = 20;
        }

        public override ProductViewModel GetModel()
        {
            return model;
        }
    }

    class OldCustomerProductBuilder : ProductBuilder
    {
        ProductViewModel model = new ProductViewModel();
        public override void ApplyDiscount()
        {
            model.DiscountedPrice = model.UnitPrice;//eski müşteriye indirim uygulanmadı
            model.DiscountApplied = false;
        }

        public override void GetProductData()
        {
            model.Id = 1;
            model.CategoryName = "Bevereges";
            model.ProductName = "Chai";
            model.UnitPrice = 20;
        }

        public override ProductViewModel GetModel()
        {
            return model;
        }
    }

    class ProductDirector
    {
        //nesneyi üretecek direktör. Buraya hangi builder kullanılacağını göndermek gerekir.
        public void GenerateProduct(ProductBuilder productBuilder)
        {
            //arka arkaya iki iş çalışır
            productBuilder.GetProductData();
            productBuilder.ApplyDiscount();
        }
    }
}
