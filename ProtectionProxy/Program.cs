using System;

namespace ProtectionProxy
{
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }

    public class Driver
    {
        public int Age { get; set; }
    }

    public class CarProxy : ICar
    {
        private Driver driver;
        private Car car = new Car();

        public CarProxy(Driver driver)
        {
            this.driver = driver;
        }

        public void Drive()
        {
            if (driver.Age >= 16)
                car.Drive();
            else
                Console.WriteLine("too young");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var car = new CarProxy(new Driver { Age = 12 });
            car.Drive();

            var car2 = new CarProxy(new Driver { Age = 16 });
            car2.Drive();
        }
    }
}
