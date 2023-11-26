using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork12
{
    abstract class Car
    {
        public string Name { get; private set; }
        public double Speed { get; protected set; }
        public double DistanceTraveled { get; private set; }

        public delegate void CarEventHandler(Car car);
        public event CarEventHandler Finish;

        protected Car(string name)
        {
            Name = name;
            DistanceTraveled = 0;
        }

        public void Drive(double time)
        {
            DistanceTraveled += Speed * time;
            if (DistanceTraveled >= 100)
            {
                Finish?.Invoke(this);
            }
        }

        public abstract void StartRace();
    }

    class SportsCar : Car
    {
        public SportsCar(string name) : base(name)
        {
        }

        public override void StartRace()
        {
            Random rnd = new Random();
            Speed = rnd.Next(10, 20);
        }
    }

    class Truck : Car
    {
        public Truck(string name) : base(name)
        {
        }

        public override void StartRace()
        {
            Random rnd = new Random();
            Speed = rnd.Next(5, 10);
        }
    }


    class Race
    {
        private List<Car> cars = new List<Car>();

        public void AddCar(Car car)
        {
            car.Finish += Car_Finished;
            cars.Add(car);
        }

        private void Car_Finished(Car car)
        {
            Console.WriteLine($"{car.Name} has finished the race!");
        }

        public void StartRace()
        {
            foreach (Car car in cars)
            {
                car.StartRace();
            }

            double time = 0;
            bool raceFinished = false;

            while (!raceFinished)
            {
                foreach (Car car in cars)
                {
                    car.Drive(1);
                    if (car.DistanceTraveled >= 100)
                    {
                        raceFinished = true;
                        break;
                    }
                }

                time += 1;
            }

            Console.WriteLine($"Race finished in {time} time units.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Race race = new Race();
            race.AddCar(new SportsCar("Sports Car 1"));
            race.AddCar(new Truck("Truck 1"));

            race.StartRace();
        }
    }
}