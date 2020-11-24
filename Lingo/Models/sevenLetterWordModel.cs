using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public class sevenLetterWordModel
    {
        public long Id { get; set; }
        [MaxLength(7)]
        public string word { get; set; }
    }
}
