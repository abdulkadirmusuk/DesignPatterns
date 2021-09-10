using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            //Bellekte tek bir defa üretilecek olan nesneler için kullanılır
            //Örneğin bir web sitesinde anlık olarak aktif kullanıcı sayısının herkese gösterilmesi işlemi
            //Herkes o nesneye erişir ve değerini görür
            var customerManager = CustomerManager.CreateAsSingleton();//new yapılamaz ,böyle üretilir
            customerManager.Save();
        }
    }
    class CustomerManager
    {
        //static bir class nesnesi properties olmalı
        private static CustomerManager _customerManager;
        private CustomerManager()
        {
            //Boş bir private ctor olmalı
        }
        public static CustomerManager CreateAsSingleton()
        {
            //static bir o class nesnesini döndüren method olmalı. Eğer null ise yeni oluşturmalı
            //customer manager varsa döndür yok ise (??) new ile yeni yarat demektir
            return _customerManager ?? (_customerManager = new CustomerManager());
        }
        public void Save()
        {
            Console.WriteLine("Saved!!");
        }
    }
}
