Imports DevExpress.Xpf.Grid
Imports System
Imports System.Windows
Imports DevExpress.Mvvm.UI

Namespace DXGridThreads

    Public Interface IGridUpdateService

        Sub BeginUpdate()

        Sub EndUpdate()

    End Interface

    Public Class GridUpdateService
        Inherits ServiceBase
        Implements IGridUpdateService

        Public Shared ReadOnly GridControlProperty As DependencyProperty = DependencyProperty.Register("GridControl", GetType(GridControl), GetType(GridUpdateService), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property GridControl As GridControl
            Get
                Return CType(GetValue(GridControlProperty), GridControl)
            End Get

            Set(ByVal value As GridControl)
                SetValue(GridControlProperty, value)
            End Set
        End Property

        Public Sub BeginUpdate() Implements IGridUpdateService.BeginUpdate
            Dispatcher.Invoke(New Action(Sub()
                If GridControl IsNot Nothing Then
                    GridControl.BeginDataUpdate()
                End If
            End Sub))
        End Sub

        Public Sub EndUpdate() Implements IGridUpdateService.EndUpdate
            Dispatcher.Invoke(New Action(Sub()
                If GridControl IsNot Nothing Then
                    GridControl.EndDataUpdate()
                End If
            End Sub))
        End Sub
    End Class
End Namespace
