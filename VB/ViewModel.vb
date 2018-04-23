Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading

Namespace Model
	Public Class ViewModel
		Implements INotifyPropertyChanged
		Private Shared SyncRoot As Object = New Object()
		Private timer As Timer
		Private random As New Random()

		Private Sub TimerCallback(ByVal state As Object)
			SyncLock SyncRoot
				OnAsyncProcessingStarted()
				For Each item As DataItem In Source
					item.Value = random.Next(100)
				Next item
				OnAsyncProcessingCompleted()
			End SyncLock
		End Sub

		Public Sub New()
			source_Renamed = DataItem.Data
			timer = New Timer(AddressOf TimerCallback, Nothing, 1000, 1000)
		End Sub

		Private source_Renamed As ObservableCollection(Of DataItem)
		Public Property Source() As ObservableCollection(Of DataItem)
			Get
				Return source_Renamed
			End Get
			Set(ByVal value As ObservableCollection(Of DataItem))
				If source_Renamed Is value Then
					Return
				End If
				source_Renamed = value
				OnPropertyChanged("Source")
			End Set
		End Property

		Public Event AsyncProcessingStarted As EventHandler
		Public Event AsyncProcessingCompleted As EventHandler

		Private Sub OnAsyncProcessingStarted()
			RaiseEvent AsyncProcessingStarted(Me, EventArgs.Empty)
		End Sub
		Private Sub OnAsyncProcessingCompleted()
			RaiseEvent AsyncProcessingCompleted(Me, EventArgs.Empty)
		End Sub

		#Region "INotifyPropertyChanged members"
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub OnPropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
		#End Region
	End Class

	Public Class DataItem
		Implements INotifyPropertyChanged
		Private Shared data_Renamed As ObservableCollection(Of DataItem)
		Public Shared ReadOnly Property Data() As ObservableCollection(Of DataItem)
			Get
				If data_Renamed Is Nothing Then
					data_Renamed = New ObservableCollection(Of DataItem)()
					For i As Integer = 0 To 99
						data_Renamed.Add(New DataItem() With {.Name = "Name" & i.ToString(), .Value = i})
					Next i
				End If
				Return data_Renamed
			End Get
		End Property

		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private valueField As Integer
		Public Property Value() As Integer
			Get
				Return valueField
			End Get
			Set(ByVal value As Integer)
				valueField = value
				OnPropertyChanged("Value")
			End Set
		End Property
		Public Overrides Function ToString() As String
			Return Name
		End Function

		#Region "INotifyPropertyChanged members"
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub OnPropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
		#End Region
	End Class
End Namespace
