using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriusSoft.Frameworks.MVVM.ViewModels;

namespace MVVM_UnitTests.ViewModelExamples
{
  public class TestViewModel : BaseViewModel, IBackedByModel
  {
    private string _name;
    public string Name
    {
      get { return this._name; }
      set
      {
        this._name = value;
        RaisePropertyChanged(); //you can also do RaisePropertyChanged(nameof(this.ID)), but i believe that [CallerMemberName] is available older than c#6??? (double check, don't take my word for it)
      }
    }

    private int _id;
    public int ID
    {
      get { return this._id; }
      set
      {
        this._id = value;
        RaisePropertyChanged(); //you can also do RaisePropertyChanged(nameof(this.ID)), but i believe that [CallerMemberName] is available older than c#6??? (double check, don't take my word for it)
      }
    }

    private TestModel _model;
    protected TestModel Model
    {
      get { return this._model ?? (this.Model = new TestModel()); }
      set
      {
        this._model = value;
        if ( this._model != null )
          this.IsBacked = true;

        RaiseAllBackedPropertiesChanged();
      }
    }

    public TestViewModel() { }

    public TestViewModel(TestModel model)
    {
      this.Model = model;
    }

    public void SetBackingModel(TestModel model)
    {
      this.Model = model;
    }

    protected void UpdateModelFromCurrentPropertyValues(bool refreshVMforAllUpdates = false)
    {
      var isDefaultModel = this.Model.IsEmpty;

      if ( isDefaultModel || this.Model.Name != this.Name )
        this.Model.Name = this.Name;

      if ( isDefaultModel || this.Model.ID != this.ID )
        this.Model.ID = this.ID;

      if ( refreshVMforAllUpdates )
        RaiseAllBackedPropertiesChanged();
    }

    protected override void RaiseAllBackedPropertiesChanged()
    {
      //or you can just call the prop changed like this (C#6 +) - this is safe because if you make a change to a property's name, it will get updated for free :) :
      this.RaisePropertyChanged(nameof(this.Model));
      this.RaisePropertyChanged(nameof(this.Name));
      this.RaisePropertyChanged(nameof(this.ID));

      //or you can do it the old fashioned way if you are in an older version of c#
      //this.RaisePropertyChanged("Model");
      //this.RaisePropertyChanged("Name");
      //this.RaisePropertyChanged("ID");
    }
  }
}
