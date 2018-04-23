// Developer Express Code Central Example:
// How to display data which is being updated on another thread
// 
// Let's suppose that your data is being updated on another thread, by the timer in
// this example. You should take a special action to correctly reflect those
// changes in the grid - wrap them inside BeginDataUpdate/EndDataUpdate
// calls.
// When using the MVVM pattern, it is not possible to call grid's methods
// directly from the view model. Your view model can provide additional events to
// expose such changes of its state to the view. There are OnAsyncProcessingStarted
// and OnAsyncProcessingCompleted events in this example. Now you can handle these
// events in the view and force the grid to stop/start listening for data updates
// before/after asynchronous data modifications.
// Please note even though this
// approach requires several code lines in View's code-behind, ViewModel in this
// situation is completely independent from GridControl. Thus, this approach
// conforms the MVVM pattern.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3322

using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DXGridThreads")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("DXGridThreads")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
)]


// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
