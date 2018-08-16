Imports DevExpress.Xpf.Grid
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

        Protected ReadOnly Property ActualGridControl As GridControl
            Get
                If GridControl Is Nothing Then
                    Return TryCast(AssociatedObject, GridControl)
                End If
                Return GridControl
            End Get
        End Property

        Public Sub BeginUpdate() Implements ICustomService.BeginUpdate
            Dispatcher.Invoke(Sub()
                                  If ActualGridControl IsNot Nothing Then
                                      ActualGridControl.BeginDataUpdate()
                                  End If
                              End Sub)
        End Sub

        Public Sub EndUpdate() Implements ICustomService.EndUpdate
            Dispatcher.Invoke(Sub()
                                  If ActualGridControl IsNot Nothing Then
                                      ActualGridControl.EndDataUpdate()
                                  End If
                              End Sub)
        End Sub
    End Class
End Namespace
