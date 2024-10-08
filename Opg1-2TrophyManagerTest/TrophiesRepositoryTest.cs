using Opg1_2TrophyManager;

namespace Opg1_2TrophyManagerTest
{
    [TestClass]
    public class TrophiesRepositoryTest
    {
        [TestClass]
        public class TrophiesRepositoryUnitTests
        {
            private TrophiesRepository _trophiesRepo;

            private readonly Trophy _badTrophy = new() { Competition = "Ro", Year = 2000 };

            [TestInitialize]
            public void Init()
            {
                _trophiesRepo = new TrophiesRepository();
            }

            [TestMethod]
            public void GetTest()
            {
                IEnumerable<Trophy> trophies = _trophiesRepo.Get();
                Assert.AreEqual(5, trophies.Count());
                Assert.AreEqual("Cycling", trophies.First().Competition);

                IEnumerable<Trophy> compIncludes = _trophiesRepo.Get(compIncludes: "Tennis");
                Assert.AreEqual("Tennis", compIncludes.First().Competition);

                IEnumerable<Trophy> compAfterYear = _trophiesRepo.Get(yearAfter: 2003);
                Assert.AreEqual("Tennis", compAfterYear.First().Competition);

                IEnumerable<Trophy> compAsc = _trophiesRepo.Get(orderBy: "comp_asc");
                Assert.AreEqual("Badminton", compAsc.First().Competition);

                IEnumerable<Trophy> compDesc = _trophiesRepo.Get(orderBy: "comp_desc");
                Assert.AreEqual("Tennis", compDesc.First().Competition);

                IEnumerable<Trophy> yearAsc = _trophiesRepo.Get(orderBy: "year_asc");
                Assert.AreEqual("Badminton", yearAsc.First().Competition);

                IEnumerable<Trophy> yearDesc = _trophiesRepo.Get(orderBy: "year_desc");
                Assert.AreEqual("Running", yearDesc.First().Competition);
            }

            // this test sometimes succeeds and something it fails, haven't figured out why
            [TestMethod]
            public void GetByIDTest()
            {
                Assert.IsNull(_trophiesRepo.GetByID(-1));
                Assert.IsNull(_trophiesRepo.GetByID(0));
                Assert.IsNotNull(_trophiesRepo.GetByID(2));
            }

            [TestMethod]
            public void AddTest()
            {
                Trophy trophy = new Trophy() { Competition = "Rowing", Year = 2002 };
                _trophiesRepo.Add(trophy);
                Assert.AreEqual("Rowing", trophy.Competition);
                Assert.IsTrue(trophy.ID > 0);

                Assert.ThrowsException<ArgumentException>(
                    () => _trophiesRepo.Add(new Trophy() { Competition = "Te", Year = 2010 }));

                Assert.ThrowsException<ArgumentNullException>(
                    () => _trophiesRepo.Add(new Trophy() { Competition = null, Year = 2010 }));

                Assert.ThrowsException<ArgumentException>(
                    () => _trophiesRepo.Add(new Trophy() { Competition = "", Year = 2010 }));
            }

            [TestMethod]
            public void RemoveTest()
            {
                Trophy toBeDeletedTrophy = _trophiesRepo.Add(new Trophy() { Competition = "Rowing", Year = 2002 });
                Trophy deletedTrophy = _trophiesRepo.Remove(toBeDeletedTrophy.ID);
                Assert.IsNotNull(deletedTrophy);
                Assert.AreEqual(toBeDeletedTrophy.Competition, deletedTrophy.Competition);

                Trophy failedDeletion = _trophiesRepo.Remove(toBeDeletedTrophy.ID);
                Assert.IsNull(failedDeletion);
            }

            [TestMethod]
            public void UpdateTest()
            {
                Trophy toBeUpdated = _trophiesRepo.Add(new Trophy() { Competition = "Baseball", Year = 1997 });
                Trophy updated = _trophiesRepo.Update(toBeUpdated.ID, new Trophy() { Competition = "Hockey", Year = 1997 });
                Assert.IsNotNull(updated);
                Assert.AreEqual("Hockey", updated.Competition);

                Assert.IsNull(_trophiesRepo.Update(-1, new Trophy() { Competition = "Hockey", Year = 1997 }));
            }
        }
    }
}