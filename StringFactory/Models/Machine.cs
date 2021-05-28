using System.Collections.Generic;

namespace StringFactory.Models
{
  public class Machine
  {
    public Machine()
    {
      this.JoinEntities = new HashSet<MechanicMachine>();
    }

    public int MachineId { get; set; }
    public string Description { get; set; }

    public virtual ICollection<MechanicMachine> JoinEntities { get;}
  }
}
