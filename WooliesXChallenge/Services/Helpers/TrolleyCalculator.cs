using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXChallenge.Models;

namespace WooliesXChallenge.Services.Helpers
{
    class State
    {
        public Dictionary<string, decimal> ProductsRest { get; set; }
        public HashSet<int> AvailableRuleIndexSet { get; set; }
        public decimal LastSpeciaValue { get; set; }
    }

    class Rule
    {
        public Dictionary<string, decimal> RuleItems { get; set; }
        public decimal RuleResult { get; set; }
    }

    public class TrolleyCalculator
    {
        private readonly Dictionary<string, decimal> _productPrice = new Dictionary<string, decimal>();
        private readonly List<Rule> _rules = new List<Rule>();
        private readonly Dictionary<string, decimal> _productQuantity = new Dictionary<string, decimal>();
        public TrolleyCalculator(Trolley trolley)
        {
            foreach(var p in trolley.Products)
            {
                _productPrice.Add(p.Name, p.Price);
            }
            foreach(var s in trolley.Specials)
            {
                var ruleItems = new Dictionary<string, decimal>();
                foreach(var i in s.Items)
                {
                    ruleItems.Add(i.Name, i.Quantity);
                }
                _rules.Add(new Rule() { RuleItems = ruleItems, RuleResult = s.Total });
            }
            foreach (var p in trolley.Quantities)
            {
                _productQuantity.Add(p.Name, p.Quantity);
            }

        }

        public decimal CalculateLowestTotal()
        {
            var queue = new Queue<State>();
            var state = new State()
            {
                ProductsRest = _productQuantity,
                AvailableRuleIndexSet = GetAvailableRule(_productQuantity),
                LastSpeciaValue = decimal.Zero
            };
            queue.Enqueue(state);
            decimal lowestTotal = decimal.MaxValue;
            while(queue.Count != 0)
            {
                var s = queue.Dequeue();
                foreach(int ruleIndex in s.AvailableRuleIndexSet)
                {
                    decimal price = decimal.Zero;
                    Dictionary<string, decimal> productsRest = null;
                    if (TryApply(s.ProductsRest, _rules[ruleIndex], out price, out productsRest))
                    {
                        lowestTotal = Math.Min(lowestTotal, price + s.LastSpeciaValue);
                        var availableRule = GetAvailableRule(productsRest);
                        {
                            if(null != availableRule && availableRule.Count > 0)
                            {
                                queue.Enqueue(new State
                                {
                                    ProductsRest = productsRest,
                                    AvailableRuleIndexSet = availableRule,
                                    LastSpeciaValue = _rules[ruleIndex].RuleResult
                                });
                            }
                        }
                    }
                }
            }
            return lowestTotal;
        }

        private bool TryApply(Dictionary<string, decimal> products,
            Rule rule, 
            out decimal price, 
            out Dictionary<string, decimal> productsRest)
        { 
            price = 0;
            productsRest = null;
            if(null == products || products.Count == 0 || null == rule)
            {
                return false;
            }
            var originalPrice = CalculatePrice(products);
            if (CanApply(products, rule))
            {
                productsRest = new Dictionary<string, decimal>();
                foreach (var p in products)
                {
                    if (rule.RuleItems.ContainsKey(p.Key))
                    {
                        if(p.Value.CompareTo(rule.RuleItems[p.Key]) > 0)
                        {
                            productsRest[p.Key] = p.Value - rule.RuleItems[p.Key];
                        }
                    }
                    else
                    {
                        productsRest[p.Key] = p.Value;

                    }
                }
                price = CalculatePrice(productsRest) + rule.RuleResult;
                if (originalPrice.CompareTo(price) < 0) 
                {
                    price = 0;
                    productsRest = null;
                    return false; 
                }
                return true;
            }
            return false;
        }

        private HashSet<int> GetAvailableRule(Dictionary<string, decimal> products)
        {
            HashSet<int> availableRules = new HashSet<int>();
            for (var i = 0; i< _rules.Count; i++)
            {
                if(CanApply(products, _rules[i]))
                {
                    availableRules.Add(i);
                }
            }
            return availableRules;
        }

        private decimal CalculatePrice(Dictionary<string, decimal> products) 
            => products.Aggregate(0m, (acc, cur) => acc + _productPrice[cur.Key] * cur.Value);

        private bool CanApply(Dictionary<string, decimal> products, Rule rule)
            => rule.RuleItems.All(x => products.ContainsKey(x.Key) && products[x.Key] >= x.Value);
    }
}
