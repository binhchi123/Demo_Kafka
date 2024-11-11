using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentAPI.Infrastructure.Models
{
    public class Class
    {
        [Key]
        public int    ClassId         { get; set; }

        [StringLength(10)]
        public string ClassName       { get; set; }

        [Range(0, 20)]
        public int    NumberOfStudent { get; set; }

        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }
    }
}
