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

        Public Shared ReadOnly GridControlProperty As DependencyProperty = DependencyProperty.Register("GridControl", GetType(GridControl), GetType(CustomService), New PropertyMetadata(Nothing))

        Public Property GridControl() As GridControl
            Get
                Return CType(GetValue(GridControlProperty), GridControl)
            End Get
            Set(ByVal value As GridControl)
                SetValue(GridControlProperty, value)
            End Set
        End Property
        Protected ReadOnly Property ActualGridControl() As GridControl
            Get
                Return If(GridControl IsNot Nothing, GridControl, TryCast(AssociatedObject, GridControl))
            End Get
        End Property

        Public Sub BeginUpdate() Implements ICustomService.BeginUpdate
            Dispatcher.Invoke(New Action(Sub()
                If ActualGridControl IsNot Nothing Then
                    ActualGridControl.BeginDataUpdate()
                End If
            End Sub))
        End Sub

        Public Sub EndUpdate() Implements ICustomService.EndUpdate
            Dispatcher.Invoke(New Action(Sub()
                If ActualGridControl IsNot Nothing Then
                    ActualGridControl.EndDataUpdate()
                End If
            End Sub))
        End Sub
    End Class
End Namespace