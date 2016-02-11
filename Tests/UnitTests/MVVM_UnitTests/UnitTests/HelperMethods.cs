using System;
using System.Collections.Generic;

namespace MVVM_UnitTests
{
  public static class HelperMethods
  {
    public static string GetStringFromCollection(IEnumerable<string> collection)
    {
      var joinedString = String.Join(", ", collection);
      return joinedString;
    }
  }
}
