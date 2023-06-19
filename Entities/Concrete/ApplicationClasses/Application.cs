using Entities.Concrete.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete.ApplicationClasses
{
    public class Application : Base
    {
        [MaxLength(100)]
        public string? Name { get; set; }
        public bool IsNviActive { get; set; } = false;
    }
}
