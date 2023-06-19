using Entities.Concrete.ApplicationClasses;
using Entities.Concrete.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete.CustomerClasses
{
    public class Customer : Base
    {
        public long GovernmentId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Surname { get; set; } = string.Empty;

        public int BirthYear { get; set; }

        public Application Application { get;set;}
        public int ApplicationId { get; set;}
    }
}
