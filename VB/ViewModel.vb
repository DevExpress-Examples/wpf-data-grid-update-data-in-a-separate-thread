Imports DevExpress.Xpf.Mvvm
Imports System
Imports System.Collections.ObjectModel
Imports System.Threading

Namespace DXGridThreads
    Public Class ViewModel
        Inherits ViewModelBase

        Private Shared SyncRoot As New Object()

        Protected timer As Timer
        Protected random As New Random()

        Public ReadOnly Property CustomService() As ICustomService
            Get
                Return Me.GetService(Of ICustomService)()
            End Get
        End Property

        Protected Sub TimerCallback(ByVal state As Object)
            SyncLock SyncRoot
                If Me.CustomService IsNot Nothing Then
                    Me.CustomService.BeginUpdate()

                    For Each item As DataItem In Source
                        item.Value = random.Next(100)
                    Next item

                    Me.CustomService.EndUpdate()
                End If
            End SyncLock
        End Sub

        Public Sub New()
            timer = New Timer(AddressOf TimerCallback, Nothing, 1000, 1000)
        End Sub

        Private _Source As ObservableCollection(Of DataItem)
        Public ReadOnly Property Source() As ObservableCollection(Of DataItem)
            Get
                If Me._Source Is Nothing Then
                    Me._Source = New ObservableCollection(Of DataItem)()
                    For i As Integer = 0 To 99
                        Me._Source.Add(New DataItem() With { _
                            .Name = "Name" & i.ToString(), _
                            .Value = i _
                        })
                    Next i
                End If
                Return Me._Source
            End Get

        End Property

    End Class
End Namespace
