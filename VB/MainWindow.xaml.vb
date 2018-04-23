' Developer Express Code Central Example:
' How to display data which is being updated on another thread
' 
' Let's suppose that your data is being updated on another thread, by the timer in
' this example. You should take a special action to correctly reflect those
' changes in the grid - wrap them inside BeginDataUpdate/EndDataUpdate
Imports System
Imports System.Windows
Imports System.Windows.Threading

Namespace DXGridThreads
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

    End Class
End Namespace
