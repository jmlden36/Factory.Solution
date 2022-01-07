using System.Collections.Generic;

namespace Factory.Models
{
  public class Location
  {
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public virtual ICollection<Machine> Machines { get; set; }
    public virtual ICollection<Engineer> Engineers { get; set; }
    public Gym()
    {
      this.Machines = new HashSet<Machine>();
      this.Engineers = new HashSet<Engineer>();
    }
  }
}