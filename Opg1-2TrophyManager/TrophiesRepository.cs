using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opg1_2TrophyManager
{
    public class TrophiesRepository
    {
        private static int _nextID = 1;
        private List<Trophy> trophies = new();

        public TrophiesRepository()
        {
            trophies.Add(new Trophy() { ID = _nextID++, Competition = "Cycling", Year = 2001 });
            trophies.Add(new Trophy() { ID = _nextID++, Competition = "Badminton", Year = 1990 });
            trophies.Add(new Trophy() { ID = _nextID++, Competition = "Tennis", Year = 2010 });
            trophies.Add(new Trophy() { ID = _nextID++, Competition = "Running", Year = 2020 });
            trophies.Add(new Trophy() { ID = _nextID++, Competition = "Swimming", Year = 2016 });
        }

        public IEnumerable<Trophy> Get(string? compIncludes = null, int? yearAfter = null, string? orderBy = null)
        {
            IEnumerable<Trophy> result = new List<Trophy>(trophies);
            // Filtering
            if (compIncludes != null)
            {
                result = result.Where(trophy => trophy.Competition.Contains(compIncludes));
            }
            if (yearAfter != null)
            {
                result = result.Where(trophy => trophy.Year > yearAfter);
            }

            // Order by
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "comp":
                    case "comp_asc":
                        result = result.OrderBy(trophy => trophy.Competition);
                        break;
                    case "comp_desc":
                        result = result.OrderByDescending(trophy => trophy.Competition);
                        break;
                    case "year":
                    case "year_asc":
                        result = result.OrderBy(trophy => trophy.Year);
                        break;
                    case "year_desc":
                        result = result.OrderByDescending(trophy => trophy.Year);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public Trophy? GetByID(int id)
        {
            return trophies.Find(trophy => trophy.ID == id);
        }

        public Trophy Add(Trophy trophy)
        {
            trophy.Validate();
            trophy.ID = _nextID++;
            trophies.Add(trophy);
            return trophy;
        }

        public Trophy? Remove(int id)
        {
            Trophy? trophy = GetByID(id);
            if (trophy == null)
            {
                return null;
            }
            trophies.Remove(trophy);
            return trophy;
        }

        public Trophy? Update(int id, Trophy trophy)
        {
            trophy.Validate();
            Trophy? existingTrophy = GetByID(id);
            if (existingTrophy == null)
            {
                return null;
            }
            existingTrophy.Competition = trophy.Competition;
            existingTrophy.Year = trophy.Year;
            return existingTrophy;
        }
    }
}
