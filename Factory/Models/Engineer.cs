using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    public string Name { get; set; }
    [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }

    public int MachinesCompleted { get; set; }
    public virtual ICollection<EngineerMachine> JoinEntities { get; set; }
    public virtual Location Location { get; set; }
    public int LocationId { get; set; }
    public Engineer()
    {
      this.JoinEntities = new HashSet<EngineerMachine>();
    }

    public int MachinesCompletedCounter() 
    {
      var count = 0;
      foreach(EngineerMachine engineermachine in JoinEntities) 
      {
        if(engineermachine.Complete == true) 
        {
          count++;
        }
      }
      return count;
    }    
  }
}