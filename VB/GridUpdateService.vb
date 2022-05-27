Imports DevExpress.Xpf.Grid
Imports System
Imports DevExpress.Mvvm.UI

Namespace DXGridThreads

    Public Interface IGridUpdateService

        Sub BeginUpdate()

        Sub EndUpdate()

    End Interface

    Public Class GridUpdateService
        Inherits ServiceBase
        Implements IGridUpdateService

        Private ReadOnly Property GridControl As GridControl
            Get
                Return TryCast(AssociatedObject, GridControl)
            End Get
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
