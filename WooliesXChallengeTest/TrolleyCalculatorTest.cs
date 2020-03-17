using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services.Helpers;
using Xunit;

namespace WooliesXChallengeTest
{
    public class TrolleyCalculatorTest
    {
        [Fact]
        public void CalculateLowestTotalTest()
        {
            var trolleys = JsonConvert.DeserializeObject<List<Trolley>>(File.ReadAllText("TrolleyTestData.json"));
            var calculator1 = new TrolleyCalculator(trolleys[0]);
            var result1 = calculator1.CalculateLowestTotal();
            var calculator2 = new TrolleyCalculator(trolleys[1]);
            var result2 = calculator2.CalculateLowestTotal();
            Assert.Equal(80, result1);
            Assert.Equal(40, result2);
        }
    }
}
