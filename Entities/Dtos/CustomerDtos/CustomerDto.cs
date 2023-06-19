using Entities.Dtos.BaseDtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Dtos.CustomerDtos
{
    public class CustomerDto : BaseDto
    {
        [DisplayName("Tc Kimlik No")]
        [Required(ErrorMessage = " {0} bu alan zorunludur.")] 
        public long GovernmentId { get; set; }

        [MaxLength(100)]
        [DisplayName("Ad")]
        [Required(ErrorMessage = " {0} bu alan zorunludur.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        [DisplayName("Soyad")]
        [Required(ErrorMessage = " {0} bu alan zorunludur.")]
        public string Surname { get; set; } = string.Empty;

        [DisplayName("Doğum Yılı")]
        [Required(ErrorMessage = " {0} bu alan zorunludur.")] 
        public int BirthYear { get; set; }

        [DisplayName("Uygulama")]
        [Required(ErrorMessage = " {0} bu alan zorunludur.")]
        public int ApplicationId {get;set;}

        [NotMapped]
        public string? ErrorMessage { get; set; } = string.Empty;
    }
}
