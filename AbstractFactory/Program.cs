using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            //AbstractFactory : Toplu nesne kullanım ihtiyaçlarında, nesnenin kullanımını kolaylaştırmak, standart nesneler ile çalışıyorsak bir mantığa göre nesne oluşturmak
            //Örn : Bir evrak takip sistemninde bazı kullanıcılar mail bazı kullanıcılara sms göndermek gibi business geliştirme
            //Örn : Dosyaya, db ye, redise, memory ye cache ve loglama gibi sistemlerin business süreçleri için mimari tasarlanabilir
            //İş süreçlerinde sürekli if ile koşul vermek koşula bağlı işlemler yapmak yerinde bu pattern kullanılabilir.
            //Duruma göre bir nesne üretecek bir fabrika(factory) ye ihtiyaç duyar


            ProductManager productManager = new ProductManager(new Factory1());//İş sınıfım Hangi factory ile çalışacaksa ctor a gönderirim.(Factory1 ve Factory2 ile çalışabilir.)
            productManager.GetAll();
            Console.ReadLine();
        }
    }

    public abstract class Logging 
    {
        public abstract void Log(string message);
    }

    public class Log4NetLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with Log4Net");
        }
    }

    public class NLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with NLogger");
        }
    }

    public abstract class Caching
    {
        public abstract void Cache(string data);
    }

    public class MemCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with MemCache");
        }
    }

    public class RedisCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with RedisCache");
        }
    }

    public abstract class CrossCuttingConcernsFactory
    {
        //duruma göre nesne üretecek abstrack factory kısım.abstract olduğu için inherit edilecek class larda bu mekanizmalar olmalı. Bu sayede Fabrikalarda artabilir demektir..
        public abstract Logging CreateLogger();
        public abstract Caching CreateCaching();
    }
    public class Factory1 : CrossCuttingConcernsFactory
    {
        //Fabrika - İş süreçleri demektir. Burada Cache olarak redis kullan. log olarak ise log4net kullan diye bir factory yarattık
        //İş süreçlerinde sürekli if ile koşul vermek koşula bağlı işlemler yapmak yerinde bu pattern kullanılabilir.
        
        public override Caching CreateCaching()
        {
            return new RedisCache();
        }

        public override Logging CreateLogger()
        {
            return new Log4NetLogger();
            
        }
    }

    public class Factory2 : CrossCuttingConcernsFactory
    {
        //Fabrika - İş süreçleri demektir. Burada Cache olarak Memcache kullan. log olarak ise NLogger kullan diye bir factory yarattık
        public override Caching CreateCaching()
        {
            return new MemCache();
        }

        public override Logging CreateLogger()
        {
            return new NLogger();

        }
    }

    public class ProductManager
    {
        //Client tarafı - Örn:Business süreci

        private CrossCuttingConcernsFactory _crossCuttingConcernsFactory;
        //Aşağıdaki gibi de kullanılabilir.
        private Logging _logging;
        private Caching _caching;

        public ProductManager(CrossCuttingConcernsFactory crossCuttingConcernsFactory)
        {
            _crossCuttingConcernsFactory = crossCuttingConcernsFactory;
            _logging = crossCuttingConcernsFactory.CreateLogger();//Bu şekildede kullanılabilir.
            _caching = crossCuttingConcernsFactory.CreateCaching();
        }

        public void GetAll()
        {
            _crossCuttingConcernsFactory.CreateLogger().Log("ProductListed successfuly");

            _logging.Log("ProductListed successfuly"); //ikinci yol
            _caching.Cache("Data");//ikinci yol

            Console.WriteLine("Products listed!");
            //Ek olarak bu işi yaptığına dair loglama ve cacheleme çalıştırmak istiyoruz
        }
    }
}
