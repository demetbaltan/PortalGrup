namespace Entities.Dtos.BaseDtos
{
    public class BaseDto
    {
        public int? Id { get; set; }
        public DateTime? CredationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
