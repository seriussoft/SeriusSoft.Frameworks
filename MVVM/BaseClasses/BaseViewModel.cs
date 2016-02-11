using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SeriusSoft.Frameworks.MVVM.ViewModels
{
  public abstract class BaseViewModel : INotifyPropertyChanged, IBackedByModel
  {
    public virtual bool IsNew { get { return true; } }
    public virtual bool IsBacked { get; set; }

    ///<summary>
    /// if you are having trouble with the [CallerMemberName] attribute due to versioning,
    /// you can check out http://stackoverflow.com/questions/301809/workarounds-for-nameof-operator-in-c-typesafe-databinding for supplying a name to this method
    /// as well as the inner workings of new'ing up an Exception to determine how they use the call-stack to find out who called the method or which method call it is on or just finished 
    ///<summary>
    protected virtual void RaisePropertyChanged([CallerMemberName] string member = "")
    {
      var copy = PropertyChanged;
      if ( copy != null )
      {
        var changedEvent = new PropertyChangedEventArgs(member);
        copy(this, changedEvent);
      }
    }

    protected abstract void RaiseAllBackedPropertiesChanged();

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}
