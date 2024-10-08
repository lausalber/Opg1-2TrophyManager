using Opg1_2TrophyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opg1_2TrophyManagerTest
{
    [TestClass]
    public class TrophyUnitTests
    {
        private Trophy goodTrophy = new Trophy() { Competition = "Tennis", Year = 2010 };
        private Trophy nullComp = new Trophy() { Competition = null, Year = 2010 };
        private Trophy shortComp = new Trophy() { Competition = "Te", Year = 2010 };
        private Trophy emptyComp = new Trophy() { Competition = "", Year = 2010 };
        private Trophy yearLow = new Trophy() { Competition = "Tennis", Year = 1960 };
        private Trophy yearHigh = new Trophy() { Competition = "Tennis", Year = 2060 };

        [TestMethod]
        public void ValidateTest()
        {
            goodTrophy.Validate();
            Assert.ThrowsException<ArgumentNullException>(() => nullComp.Validate());
            Assert.ThrowsException<ArgumentException>(() => shortComp.Validate());
            Assert.ThrowsException<ArgumentException>(() => emptyComp.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => yearLow.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => yearHigh.Validate());
        }

        [TestMethod]
        [DataRow(1970)]
        [DataRow(2005)]
        [DataRow(2024)]
        public void ValidateYearTest(int year)
        {
            goodTrophy.Year = year;
            goodTrophy.Validate();
        }
    }
}
