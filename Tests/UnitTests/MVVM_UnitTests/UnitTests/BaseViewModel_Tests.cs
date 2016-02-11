using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTesting = Microsoft.VisualStudio.TestTools.UnitTesting;

using SeriusSoft.Frameworks.MVVM.Core;
using SeriusSoft.Frameworks.MVVM.Exceptions;
using SeriusSoft.Frameworks.MVVM.ViewModels;

using MVVM_UnitTests.ViewModelExamples;


namespace MVVM_UnitTests
{
  [TestClass]
  public class BaseViewModel_Tests
  {
    [TestMethod]
    public void TestViewModel_ChangeProperty_PropertyChangeFired_ControlTest()
    {
      var vm = new TestViewModel();
      var listOfPropertyNames = new List<string>();
      var expectedName = nameof(vm.Name);

      var propertyName = String.Empty;
      Action<object, PropertyChangedEventArgs> action = (sender, args) => { propertyName = args.PropertyName; };
      Action<object, PropertyChangedEventArgs> action2 = (sender, args) => { listOfPropertyNames.Add(args?.PropertyName); };

      action(null, new PropertyChangedEventArgs(expectedName));
      action2(null, new PropertyChangedEventArgs(expectedName));

      UnitTesting.Assert.AreEqual<string>(propertyName, expectedName);
      UnitTesting.CollectionAssert.Contains(listOfPropertyNames, expectedName);
      UnitTesting.Assert.IsTrue(listOfPropertyNames.Count == 1);
    }

    [TestMethod]
    public void TestViewModel_ChangeProperty_PropertyChangedFired()
    {
      var vm = new TestViewModel();
      var propertyName = String.Empty;
      var listOfPropertyNames = new List<string>();
      var expectedName = nameof(vm.Name);

      vm.PropertyChanged += (sender, args) => { propertyName = args?.PropertyName; };
      vm.PropertyChanged += (sender, args) => { listOfPropertyNames.Add(args?.PropertyName); };
      vm.Name = "We are updating the name property and expecting it to fire the Property Changed on the name";

      UnitTesting.Assert.AreEqual<string>(propertyName, expectedName);
      UnitTesting.CollectionAssert.Contains(listOfPropertyNames, expectedName);
      UnitTesting.Assert.IsTrue(listOfPropertyNames.Count == 1);
    }

    

    [TestMethod]
    public void TestViewModel_ChangeModelProperty_AllPropertyChangedFired()
    {
      var listOfPropertyNames = new List<string>();
      var vm = new TestViewModel();
      vm.PropertyChanged += (sender, args) => { listOfPropertyNames.Add(args?.PropertyName); };

      vm.SetBackingModel(new TestModel(name: "Test", id: 1));

      var expectedListOfNames = new List<string>() { "Model", nameof(vm.Name), nameof(vm.ID) };

      Assert.IsTrue(expectedListOfNames.Except(listOfPropertyNames).Count() == 0, $"All property names expected are NOT in the fired list: expected:[{HelperMethods.GetStringFromCollection(expectedListOfNames)}], fired:[{HelperMethods.GetStringFromCollection(listOfPropertyNames)}].");
      Assert.IsTrue(listOfPropertyNames.Except(expectedListOfNames).Count() == 0, $"All property names fired are NOT in the expected list: expected:[{HelperMethods.GetStringFromCollection(expectedListOfNames)}], fired:[{HelperMethods.GetStringFromCollection(listOfPropertyNames)}].");

      CollectionAssert.AreEqual(expectedListOfNames, listOfPropertyNames, $"The expected properties of the TestViewModel: expected:[{HelperMethods.GetStringFromCollection(expectedListOfNames)}], fired:[{HelperMethods.GetStringFromCollection(listOfPropertyNames)}]. They fired in an incorrect order...");
    }
  }
}
