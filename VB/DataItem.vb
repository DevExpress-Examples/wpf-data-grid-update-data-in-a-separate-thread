Imports System.ComponentModel

Namespace DXGridThreads
    Public Class DataItem
        Implements INotifyPropertyChanged


        Protected _Name As String

        Public Property Name() As String
            Get
                Return Me._Name
            End Get

            Set(ByVal value As String)
                If Me._Name <> value Then
                    Me._Name = value
                    Me.OnPropertyChanged("Name")
                End If
            End Set
        End Property

        Protected _Value As Integer

        Public Property Value() As Integer
            Get
                Return Me._Value
            End Get

            Set(ByVal value As Integer)
                If Me._Value <> value Then
                    Me._Value = value
                    Me.OnPropertyChanged("Value")
                End If
            End Set
        End Property


        Public Overrides Function ToString() As String
            Return Name
        End Function


        Public Sub OnPropertyChanged(ByVal info As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace
