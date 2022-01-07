using System.Collections.Generic;
using System;

namespace Factory.Models
{
  public class Machine
  {
    public Machine()
    {
      this.JoinEntities = new HashSet<EngineerMachine>();
    }

    public int MachineId { get; set; }
    public string Model { get; set; }
    public string Description { get; set; }
    public virtual ICollection<EngineerMachine> JoinEntities { get; set; }
    public virtual Location Location { get; set; }
    public int LocationId { get; set; }
  }
}