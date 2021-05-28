using System.Collections.Generic;

namespace Factory.Models
{
  public class Mechanic
  {
    public Mechanic()
    {
      this.JoinEntities = new HashSet<MechanicMachine>();
    }

    public int MechanicId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<MechanicMachine> JoinEntities { get; set; }
  }
}
