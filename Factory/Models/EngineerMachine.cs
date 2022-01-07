namespace Factory.Models
{
  public class EngineerMachine
  {
    public EngineerMachine()
    {
      Complete = false;
    }

    public int EngineerMachineId { get; set; }
    public int EngineerId { get; set; }
    public int MachineId { get; set; }
    public bool Complete { get; set; }
    public virtual Engineer Engineer { get; set; }
    public virtual Machine Machine { get; set; }
  }
}