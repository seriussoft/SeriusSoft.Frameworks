using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_UnitTests.ViewModelExamples
{
  ///<summary>
  /// Should work with IoC/DI containers like Microsoft.UnityContainer
  ///</summar>
  public class TestModel
  {
    public bool IsEmpty { get { return IsDefaultParameters(); } }
    public string Name { get; set; }
    public int ID { get; set; }

    public TestModel(string name = null, int id = 0)
    {
      this.Name = name;
      this.ID = id;
    }

    protected bool IsDefaultParameters()
    {
      return
        this.Name == default(string)
        && this.ID == default(int);
    }
  }
}
