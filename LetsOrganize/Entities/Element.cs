using System.ComponentModel.DataAnnotations;

namespace LetsOrganize.Entities
{
    public class Element
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

        public MyList MyList { get; set; }
        [Required]
        public int? MyListId { get; set; }
    }
}
