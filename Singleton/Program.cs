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
            //Thread Safe Singleton pattern ise multi thread ortamda bir nesneyi aynı anda 2 kişi isterse nesne ilk erişilen kişi anında locklanır
        }
    }
    class CustomerManager
    {
        //static bir class nesnesi properties olmalı
        private static CustomerManager _customerManager;
        static object _lockObject = new object();//thread safe locking
        private CustomerManager()
        {
            //Boş bir private ctor olmalı
        }
        public static CustomerManager CreateAsSingleton()
        {
            //static bir o class nesnesini döndüren method olmalı. Eğer null ise yeni oluşturmalı
            //customer manager varsa döndür yok ise (??) new ile yeni yarat demektir
            lock (_lockObject)//thread safe lock
            {
                if (_customerManager==null)
                {
                    _customerManager = new CustomerManager();
                }
                return _customerManager;

            }
        }
        public void Save()
        {
            Console.WriteLine("Saved!!");
        }
    }
}
