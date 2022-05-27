' Developer Express Code Central Example:
' How to display data which is being updated on another thread
' 
' Let's suppose that your data is being updated on another thread, by the timer in
' this example. You should take a special action to correctly reflect those
' changes in the grid - wrap them inside BeginDataUpdate/EndDataUpdate
' calls.
' When using the MVVM pattern, it is not possible to call grid's methods
' directly from the view model. Your view model can provide additional events to
' expose such changes of its state to the view. There are OnAsyncProcessingStarted
' and OnAsyncProcessingCompleted events in this example. Now you can handle these
' events in the view and force the grid to stop/start listening for data updates
' before/after asynchronous data modifications.
' Please note even though this
' approach requires several code lines in View's code-behind, ViewModel in this
' situation is completely independent from GridControl. Thus, this approach
' conforms the MVVM pattern.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3322
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.235
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------
Namespace DXGridThreads.Properties

    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")>
    Friend NotInheritable Partial Class Settings
        Inherits Global.System.Configuration.ApplicationSettingsBase

        Private Shared defaultInstance As DXGridThreads.Properties.Settings = CType((Global.System.Configuration.ApplicationSettingsBase.Synchronized(New DXGridThreads.Properties.Settings())), DXGridThreads.Properties.Settings)

        Public Shared ReadOnly Property [Default] As Settings
            Get
                Return DXGridThreads.Properties.Settings.defaultInstance
            End Get
        End Property
    End Class
End Namespace
