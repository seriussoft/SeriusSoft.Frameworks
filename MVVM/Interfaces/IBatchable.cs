namespace SeriusSoft.Frameworks.MVVM.Core
{
  public interface IBatchable
  {
    bool IsInBatchMode { get; set; }
    void Begin_ViewModelUpdates();
    void End_ViewModelUpdates();
  }
}
