using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SeriusSoft.Frameworks.MVVM.Core;
using SeriusSoft.Frameworks.MVVM.Exceptions;

namespace SeriusSoft.Frameworks.MVVM.ViewModels
{
  /// <summary>
  /// <para>Note that this is almost the same as the BaseViewModel.</para>
  /// <para>The difference is that if you use the Begin_ViewModelUpdates() and End_ViewModelUpdates() methods, calls to RaisePropertyChanged will be ignored so long as:</para>
  /// <para> - this.IsInBatchMode is false</para>
  /// <para> - OR this.OverrideBatchModeLock is true</para>
  /// <para> - Note that this means that the RaiseAllBackedPropertiesChanged will essentially do nothing even when overridden because you will be unable to trigger the PropertyChanged events w/o overriding the RaisePropertyChanged method.</para>
  /// </summary>
  public abstract class BatchableBaseViewModel : BaseViewModel, IBatchable, INotifyPropertyChanged, IBackedByModel
  {
    public bool IsInBatchMode { get; set; }
    public bool OverrideBatchModeLock { get; set; }

    public void Begin_ViewModelUpdates()
    {
      if ( this.IsInBatchMode )
        throw new AlreadyInBatchModeException(this.GetType().FullName);

      this.IsInBatchMode = true;
    }

    public void End_ViewModelUpdates()
    {
      if (!this.IsInBatchMode)
        throw new NotInBatchModeException(this.GetType().FullName);

      this.IsInBatchMode = false;
    }

    protected override void RaisePropertyChanged([CallerMemberName] string member = "")
    {
      if ( !this.OverrideBatchModeLock && this.IsInBatchMode )
        return;

      base.RaisePropertyChanged(member);
    }
  }
}
