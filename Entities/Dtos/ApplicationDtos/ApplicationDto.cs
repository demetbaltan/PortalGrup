using Entities.Dtos.BaseDtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Dtos.ApplicationDtos
{
    public class ApplicationDto :BaseDto
    {
        [MaxLength(100)]
        [DisplayName("Uygulamanın Adı")]
        [Required(ErrorMessage = "{0} bu alan zorunludur.")]
        public string? Name { get; set; }

        [DisplayName("Nvi Aktif mi?")]
        public bool IsNviActive { get; set; } = false;

        [NotMapped]
        public string? ErrorMessage { get; set; } = string.Empty;
    }
}
