Imports DevExpress.Xpf.Grid
Imports System
Imports System.Windows
Imports DevExpress.Xpf.Mvvm.UI

Namespace DXGridThreads

    Public Interface ICustomService
        Sub BeginUpdate()
        Sub EndUpdate()
    End Interface

    Public Class CustomService
        Inherits ServiceBase
        Implements ICustomService

        Public Property GridControl() As GridControl
            Get
                Return CType(GetValue(GridControlProperty), GridControl)
            End Get
            Set(ByVal value As GridControl)
                SetValue(GridControlProperty, value)
            End Set
        End Property

        Public Shared ReadOnly GridControlProperty As DependencyProperty = DependencyProperty.Register("GridControl", GetType(GridControl), GetType(CustomService), New PropertyMetadata(Nothing))

        Public Sub BeginUpdate() Implements ICustomService.BeginUpdate
            Dispatcher.Invoke(New Action(Sub()
                If Me.GridControl IsNot Nothing Then
                    Me.GridControl.BeginDataUpdate()
                End If
            End Sub))
        End Sub

        Public Sub EndUpdate() Implements ICustomService.EndUpdate
            Dispatcher.Invoke(New Action(Sub()
                If Me.GridControl IsNot Nothing Then
                    Me.GridControl.EndDataUpdate()
                End If
            End Sub))
        End Sub
    End Class
End Namespace
