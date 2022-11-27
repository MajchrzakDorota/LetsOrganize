using LetsOrganize.Entities;

namespace LetsOrganize.Models
{
    public class ElementDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public int MyListId { get; set; }

    }
}
