namespace RankadeParser.Models
{
    public class PlayerRank
    {
        public string Name { get; private set; }
        public int Rank { get; private set; }

        public PlayerRank(string name, int rank)
        {
            Name = name;
            Rank = rank;
        }
    }
}