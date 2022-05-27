Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Threading

Namespace DXGridThreads

    Public Class DataItem
        Inherits BindableBase

        Public Property Name As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property Value As Integer
            Get
                Return GetValue(Of Integer)()
            End Get

            Set(ByVal value As Integer)
                SetValue(value)
            End Set
        End Property
    End Class

    Public Class ViewModel
        Inherits ViewModelBase

        Public Property Source As ObservableCollection(Of DataItem)

        Public ReadOnly Property GridUpdateService As IGridUpdateService
            Get
                Return GetService(Of IGridUpdateService)()
            End Get
        End Property

        Public Sub New()
            timer = New Timer(AddressOf TimerCallback, Nothing, 1000, 1000)
            Source = New ObservableCollection(Of DataItem)(GetData())
        End Sub

        Private Shared SyncRoot As Object = New Object()

        Private timer As Timer

        Private random As Random = New Random()

        Private Sub TimerCallback(ByVal state As Object)
            SyncLock SyncRoot
                If GridUpdateService IsNot Nothing Then
                    GridUpdateService.BeginUpdate()
                    For Each item As DataItem In Source
                        item.Value = random.Next(100)
                    Next

                    GridUpdateService.EndUpdate()
                End If

            End SyncLock
        End Sub

        Private Shared Iterator Function GetData() As IEnumerable(Of DataItem)
            Dim i = 0
            While i < 100
                Yield New DataItem() With {.Name = $"Name {i}", .Value = i}
                Interlocked.Increment(i)
            End While
        End Function
    End Class
End Namespace
