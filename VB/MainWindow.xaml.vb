Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Threading
Imports Model

Namespace DXGridThreads
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Private viewModel As ViewModel

		Public Sub New()
			InitializeComponent()
			viewModel = New ViewModel()
			AddHandler viewModel.AsyncProcessingStarted, AddressOf viewModel_AsyncProcessingStarted
			AddHandler viewModel.AsyncProcessingCompleted, AddressOf viewModel_AsyncProcessingCompleted
			DataContext = viewModel
		End Sub

		Private Sub viewModel_AsyncProcessingStarted(ByVal sender As Object, ByVal e As EventArgs)
			grid.Dispatcher.Invoke(New Action(Function() AnonymousMethod1()), DispatcherPriority.DataBind)
		End Sub
		
		Private Function AnonymousMethod1() As Boolean
			grid.BeginDataUpdate()
			Return True
		End Function
		Private Sub viewModel_AsyncProcessingCompleted(ByVal sender As Object, ByVal e As EventArgs)
			grid.Dispatcher.Invoke(New Action(Function() AnonymousMethod2()), DispatcherPriority.DataBind)
		End Sub
		
		Private Function AnonymousMethod2() As Boolean
			grid.EndDataUpdate()
			Return True
		End Function
	End Class
End Namespace
