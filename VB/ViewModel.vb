Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Threading

Namespace Model

    Public Class ViewModel
        Implements INotifyPropertyChanged

        Private Shared SyncRoot As Object = New Object()

        Private timer As Timer

        Private random As Random = New Random()

        Private Sub TimerCallback(ByVal state As Object)
            SyncLock SyncRoot
                OnAsyncProcessingStarted()
                For Each item As DataItem In Source
                    item.Value = random.Next(100)
                Next

                OnAsyncProcessingCompleted()
            End SyncLock
        End Sub

        Public Sub New()
            sourceField = DataItem.Data
            timer = New Timer(AddressOf TimerCallback, Nothing, 1000, 1000)
        End Sub

        Private sourceField As ObservableCollection(Of DataItem)

        Public Property Source As ObservableCollection(Of DataItem)
            Get
                Return sourceField
            End Get

            Set(ByVal value As ObservableCollection(Of DataItem))
                If sourceField Is value Then Return
                sourceField = value
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

'#Region "INotifyPropertyChanged members"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub OnPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
'#End Region
    End Class

    Public Class DataItem
        Implements INotifyPropertyChanged

        Private Shared dataField As ObservableCollection(Of DataItem)

        Public Shared ReadOnly Property Data As ObservableCollection(Of DataItem)
            Get
                If dataField Is Nothing Then
                    dataField = New ObservableCollection(Of DataItem)()
                    For i As Integer = 0 To 100 - 1
                        Call dataField.Add(New DataItem() With {.Name = "Name" & i.ToString(), .Value = i})
                    Next
                End If

                Return dataField
            End Get
        End Property

        Public Property Name As String

         ''' Cannot convert FieldDeclarationSyntax, System.NotSupportedException: VolatileKeyword is not supported!
'''    at ICSharpCode.CodeConverter.VB.SyntaxKindExtensions.ConvertToken(SyntaxKind t, TokenContext context)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.ConvertModifier(SyntaxToken m, TokenContext context)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.<>c__DisplayClass30_0.<ConvertModifiersCore>b__3(SyntaxToken x)
'''    at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
'''    at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
'''    at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
'''    at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.ConvertModifiersCore(IReadOnlyCollection`1 modifiers, TokenContext context, Boolean isConstructor, Boolean isNestedType)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitFieldDeclaration(FieldDeclarationSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
'''         volatile int valueField;
''' 
'''  Public Property Value As Integer
            Get
                Return Me.valueField
            End Get

            Set(ByVal value As Integer)
                Me.valueField = value
                OnPropertyChanged("Value")
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

'#Region "INotifyPropertyChanged members"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub OnPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
'#End Region
    End Class
End Namespace
