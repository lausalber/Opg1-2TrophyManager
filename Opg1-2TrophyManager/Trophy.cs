namespace Opg1_2TrophyManager
{
    public class Trophy
    {
        public int ID { get; set; }
        public string Competition { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(ID)}={ID.ToString()}, {nameof(Competition)}={Competition}, {nameof(Year)}={Year.ToString()}}}";
        }

        public void ValidateCompetition()
        {
            if (Competition == null)
            {
                throw new ArgumentNullException("Competition can't be null");
            }
            if (Competition.Length < 3)
            {
                throw new ArgumentException("Competition must be at least 3 characters", nameof(Competition));
            }
        }

        public void ValidateYear()
        {
            if (Year < 1970 || Year > 2024)
            {
                throw new ArgumentOutOfRangeException("Year has to be between 1970 and 2024");
            }
        }

        public void Validate()
        {
            ValidateCompetition();
            ValidateYear();
        }
    }
}
