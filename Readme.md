# How to display data which is being updated on another thread


<p>Let's suppose that your data is being updated on another thread, by the timer in this example. You should take a special action to correctly reflect those changes in the grid - wrap them inside BeginDataUpdate/EndDataUpdate calls.</p>
<p>When using the MVVM pattern, it is not possible to call grid's methods directly from the view model. Your view model can provide additional events to expose such changes of its state to the view. There are OnAsyncProcessingStarted and OnAsyncProcessingCompleted events in this example. Now you can handle these events in the view and force the grid to stop/start listening for data updates before/after asynchronous data modifications.</p>
<p>Please note even though this approach requires several code lines in View's code-behind, ViewModel in this situation is completely independent from GridControl. Thus, this approach conforms the MVVM pattern.<br><br><u><strong><br>UPDATED:</strong></u><br><br></p>
<p>After we introduced <a href="https://documentation.devexpress.com/#WPF/CustomDocument9109">Services</a>, the same task can be implemented by creating a custom service (<a href="https://documentation.devexpress.com/#WPF/CustomDocument16920">How to create a Custom Service</a>). This service will have access to the GridControl in the View, and will contain the required BeginUpdate and EndUpdate methods. In these methods, GridControl's <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridDataControlBase_BeginDataUpdatetopic">BeginDataUpdate</a>  and <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridDataControlBase_EndDataUpdatetopic">EndDataUpdate</a>  methods will be called.</p>
<p><strong>Starting with v13.1.4, this example illustrates this approach.</strong></p>
<p> <br>See also:<br><br><a href="https://documentation.devexpress.com/WPF/CustomDocument17446.aspx">Services in ViewModelBase descendants</a> <br><a href="https://documentation.devexpress.com/WPF/CustomDocument17447.aspx">Services in POCO Objects</a>  <br><a href="https://documentation.devexpress.com/WPF/CustomDocument17450.aspx">Services in custom ViewModels</a> <br><br></p>

<br/>


