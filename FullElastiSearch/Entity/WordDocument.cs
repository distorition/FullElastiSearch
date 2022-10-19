using ElasticSearch.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullElastiSearch.Entity
{
    [Table("WordDocument")]
    [Index(nameof(WordId))]//делаем индекс слова не уникальным чтобы база работала быстрее 
    [Index(nameof(WordId), nameof(DocumentId), IsUnique = true)]
    public class WordDocument
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }
        [ForeignKey(nameof(Document))]
        public int DocumentId { get; set; }
        [ForeignKey(nameof(Word))]
        public int WordId { get; set; }

        public virtual Document Document { get; set; }
        public virtual Word Word { get; set; }
    }
}
