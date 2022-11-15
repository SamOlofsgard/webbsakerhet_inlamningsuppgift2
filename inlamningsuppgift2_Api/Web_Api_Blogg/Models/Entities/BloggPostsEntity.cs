using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Api_Blogg.Models.Entities
{
    public class BloggPostsEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Titel { get; set; } = null!;
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Body { get; set; } = null!;


    }
}
