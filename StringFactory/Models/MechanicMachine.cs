namespace StringFactory.Models
{
  public class MechanicMachine
  {       
    public int MechanicMachineId { get; set; }
    public int MachineId { get; set; }
    public int MechanicId { get; set; }
    public virtual Machine Machine { get; set; }
    public virtual Mechanic Mechanic { get; set; }
  }
}