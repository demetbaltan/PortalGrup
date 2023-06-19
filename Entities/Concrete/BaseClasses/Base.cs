using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete.BaseClasses
{
    public class Base
    {
        [Key] //benzersiz
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //otomatik arttırma
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime? ModificationDate { get; set; } //?null olabilir

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;
    }
}

