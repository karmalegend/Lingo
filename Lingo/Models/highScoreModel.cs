namespace Lingo.Models
{
    public class HighScoreModel
    {
        public long Id { get; set; }
        public int Score { get; set; }
        public string User { get; set; }

        public HighScoreModel() { }

        public HighScoreModel(int score, string user) {
            this.Score = score;
            this.User = user;
        }
    }
}
