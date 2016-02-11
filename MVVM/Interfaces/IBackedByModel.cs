namespace SeriusSoft.Frameworks.MVVM.ViewModels
{
  public interface IBackedByModel
  {
    bool IsNew { get; }
    bool IsBacked { get; set; }
  }
}
