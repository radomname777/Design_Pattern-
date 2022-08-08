using System;

namespace ConsoleAppObserver
{


    interface IRestaurant
    {
        void Update(Veggies veggies);
    }


    class Restaurant : IRestaurant
    {
        private string _name;
        private double _purchase;

        public Restaurant(string name, double purchase)
        {
            _name = name;
            _purchase = purchase;
        }

        public void Update(Veggies veggie)
        {
            Console.WriteLine($"Notified {_name} of {veggie.GetType().Name}'s price change to ${veggie.Price}\n");
            if (veggie.Price <= _purchase)
            {
                Console.WriteLine($"{_name} to buy {veggie.GetType().Name}!\n");
            }
        }
    }


    abstract class Veggies
    {
        private double _price;
        private List<IRestaurant> _restaurants = new List<IRestaurant>();

        public Veggies(double price)
        {
            _price = price;
        }

        public void Attach(IRestaurant restaurant)
        {
            _restaurants.Add(restaurant);
        }

        public void Notify()
        {
            foreach (IRestaurant restaurant in _restaurants)
            {
                restaurant.Update(this);
            }

            Console.WriteLine("\n\n");
        }



        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }
    }


    class Patates : Veggies
    {
        public Patates(double price) : base(price) { }
    }
    class Tomato : Veggies
    {
        public Tomato(double price) : base(price) { }
    }



    internal class Program
    {
        static void Main(string[] args)
        {

            Patates patates = new Patates(0.82);

            patates.Attach(new Restaurant("Arthur", 0.77));
            patates.Attach(new Restaurant("Jeannette", 0.74));
            patates.Attach(new Restaurant("Elinor", 0.75));
            patates.Price = 0.79;
            patates.Price = 0.76;
            patates.Price = 0.74;
            patates.Price = 0.81;

            Tomato tomato = new Tomato(1.50);

            tomato.Attach(new Restaurant("Richard", 1.10));
            tomato.Attach(new Restaurant("Bruce", 1.74));
            tomato.Attach(new Restaurant("Nannie", 0.90));
            tomato.Price = 1.79;
            tomato.Price = 0.76;
            tomato.Price = 1.34;
            tomato.Price = 1.15;

            Console.ReadKey();
        }
    }
}