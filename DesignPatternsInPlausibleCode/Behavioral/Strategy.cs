using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternsInPlausibleCode.Behavioral
{
    // example modified from https://en.wikipedia.org/wiki/Strategy_pattern
    // each class will get a comment, indicating what class it corresponds to in the UML class diagram of the pattern

    public static class Strategy
    {
        public static void Run()
        {
            var normalBillingStrategy = new NormalBillingStrategy();
            var happyHourBillingStrategy = new HappyHourBillingStrategy();

            var firstCustomer = new CustomerBill(normalBillingStrategy);

            // Normal billing
            firstCustomer.AddDrinks(price: 1.0, quantity: 1);

            // Start Happy Hour
            firstCustomer.BillingStrategy = happyHourBillingStrategy;
            firstCustomer.AddDrinks(price: 1.0, quantity: 2);
            
            CustomerBill secondCustomer = new CustomerBill(happyHourBillingStrategy);
            secondCustomer.AddDrinks(price: 0.8, quantity: 1);
            
            firstCustomer.Pay();

            // End Happy Hour
            secondCustomer.BillingStrategy = normalBillingStrategy;
            secondCustomer.AddDrinks(price: 1.3, quantity: 2);
            secondCustomer.AddDrinks(price: 2.5, quantity: 1);

            secondCustomer.Pay();
        }
    }

    // acts as the Context
    class CustomerBill
    {
        private readonly IList<double> drinksCosts;

        public IBillingStrategy BillingStrategy { get; set; }

        public CustomerBill(IBillingStrategy billingStrategy)
        {
            drinksCosts = new List<double>();
            BillingStrategy = billingStrategy;
        }

        public void AddDrinks(double price, int quantity)
        {
            drinksCosts.Add(BillingStrategy.GetActualPrice(price * quantity));
        }

        public void Pay()
        {
            Console.WriteLine($"Total due: {drinksCosts.Sum()}.");

            drinksCosts.Clear();
        }
    }

    // acts as the Strategy
    interface IBillingStrategy
    {
        double GetActualPrice(double rawPrice);
    }
    
    // acts as the ConcreteStrategyA
    class NormalBillingStrategy : IBillingStrategy
    {
        public double GetActualPrice(double rawPrice) => rawPrice;
    }

    // acts as the ConcreteStrategyB
    class HappyHourBillingStrategy : IBillingStrategy
    {
        public double GetActualPrice(double rawPrice) => rawPrice * 0.5;
    }
}
