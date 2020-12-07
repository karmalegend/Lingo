using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public class highScoreModel
    {
        public long Id { get; set; }
        public int score { get; set; }
        public string user { get; set; }

        public highScoreModel() { }

        public highScoreModel(int score, string user) {
            this.score = score;
            this.user = user;
        }
    }
}
