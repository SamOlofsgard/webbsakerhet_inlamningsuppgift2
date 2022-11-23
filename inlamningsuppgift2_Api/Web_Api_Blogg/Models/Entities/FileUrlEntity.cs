using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Api_Blogg.Models.Entities
{
    public class FileUrlEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; } = null!;
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string ImgUrl { get; set; } = null!;
    }
}
