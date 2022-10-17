using System.ComponentModel.DataAnnotations;

namespace LetsOrganize.Entities
{
    public class MyList
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public List<Element> ElementsList = new List<Element>();
    }
}
