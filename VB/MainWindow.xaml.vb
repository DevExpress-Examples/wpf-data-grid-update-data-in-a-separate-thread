Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports Model

Namespace DXGridThreads

    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Public Partial Class MainWindow
        Inherits Window

        Private viewModel As ViewModel

        Public Sub New()
            Me.InitializeComponent()
            viewModel = New ViewModel()
            AddHandler viewModel.AsyncProcessingStarted, New EventHandler(AddressOf viewModel_AsyncProcessingStarted)
            AddHandler viewModel.AsyncProcessingCompleted, New EventHandler(AddressOf viewModel_AsyncProcessingCompleted)
            DataContext = viewModel
        End Sub

        Private Sub viewModel_AsyncProcessingStarted(ByVal sender As Object, ByVal e As EventArgs)
            Me.grid.Dispatcher.Invoke(New Action(Sub() Me.grid.BeginDataUpdate()), DispatcherPriority.DataBind)
        End Sub

        Private Sub viewModel_AsyncProcessingCompleted(ByVal sender As Object, ByVal e As EventArgs)
            Me.grid.Dispatcher.Invoke(New Action(Sub() Me.grid.EndDataUpdate()), DispatcherPriority.DataBind)
        End Sub
    End Class
End Namespace
