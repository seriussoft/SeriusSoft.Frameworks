using System;

namespace SeriusSoft.Frameworks.MemoryManagement
{
  /// <summary>
  /// If an object implements this interface, then it should support Disposing of internals in a Polymorphic manner.
  /// It should also be capable of handling large heap and unmanaged resources as well as working better with the GC.
  /// <para>You are strongly suggested to extend the <see cref="BaseOverridableDisposableObject"/> class instead of implementing this interface directly.</para>
  /// <para>If you choose to implement this interface rather than using the base object, <see cref="BaseOverridableDisposableObject"/>, 
  /// it is strongly suggested to explicitly implement the properties so that outside classes do not have access to your disposal process. </para>
  /// <para>See the <see cref="BaseOverridableDisposableObject"/> implementation for more details.</para>
  /// </summary>
  public interface IOverridableDisposableObject : IDisposable
  {
    bool IsDisposed { get; set; }
    bool IsUsingFinalizerForUnanagedCode { get; set; }
  }

  /// <summary>
  /// This is is intended to aide in the process of properly disposing of your internals in a manner that can be extended and overridden in descendant objects.
  /// <para>It is strongly suggested that you extend this object as your base rather than implementing the <see cref="IOverridableDisposableObject"/> interface directly. </para>
  /// </summary>
  public abstract class BaseOverridableDisposableObject : IDisposable, IOverridableDisposableObject
  {
    #region IDisposable Support
    bool IOverridableDisposableObject.IsDisposed { get; set; } = false; // To detect redundant calls
    /// <summary>
    /// Set this to true if you are overriding the finalizer so we know to suppress the finalizer in the garbage collector when marked as disposed
    /// </summary>
    bool IOverridableDisposableObject.IsUsingFinalizerForUnanagedCode { get; set; } = false;

    /// <summary>
    /// Use this method to gain access to the <see cref="IOverridableDisposableObject.IsDisposed"/> 
    /// and <see cref="IOverridableDisposableObject.IsUsingFinalizerForUnanagedCode"/> properties.
    /// </summary>
    /// <returns></returns>
    protected IOverridableDisposableObject CastAsDisposer()
    {
      return this as IOverridableDisposableObject;
    }

    private void Dispose(bool disposing)
    {
      var disposer = CastAsDisposer();
      if (!disposer.IsDisposed)
      {
        //typically, if you have unmanaged objects, you are likely to have pointers/refs to them in your managed space
        if (disposing)
          DisposeManagedObjects();

        DisposeUnmanagedObjects();

        disposer.IsDisposed = true;
      }
    }

    /// <summary>
    /// Do NOT call directly. Instead, this should be overridden to specify what you are disposing.
    /// <para>This is for dealing with: unmanaged resources, large heap items and unknowns dealt with here (if they exist)...</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void DisposeUnmanagedObjects()
    {
      //does nothing until overridden.
    }

    /// <summary>
    /// Do NOT call directly. Instead, this should be overridden to specify what you are disposing.
    /// <para>If you are overriding this, then you should set your <see cref="IsUsingFinalizerForUnanagedCode"/> property to true and override your finalizer like below:</para>
    /// <code>~BaseOverridableDisposableObject() { Dispose(false); } </code>
    /// </summary>
    protected virtual void DisposeManagedObjects()
    {
      //does nothing until overridden
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~BaseWorkingSetItemNavigator() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    /// <summary>
    /// Dispose of your object. This disposition method supports handling Unmanaged and Managed resources (off and on the heap).
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      var disposer = CastAsDisposer();
      /*******************************************
       * Should only run IF the finalizer is overridden. 
       * This allows us to suppress finalizer call to object under-the-hood until we have successfully marked unmanaged objects as ready for memory reallocation.
       * In short, this will prevent a redundant pass by the GC
       * 
       * See (MS Doc): https://msdn.microsoft.com/en-us/library/system.gc.suppressfinalize(v=vs.110).aspx
       * See (Jon Skeet's and Eric Lippert's answers): http://stackoverflow.com/questions/8011001/can-anyone-explain-this-finalisation-behaviour
       ***/
      if (disposer.IsUsingFinalizerForUnanagedCode)
        GC.SuppressFinalize(this);
    }
    #endregion
  }
}