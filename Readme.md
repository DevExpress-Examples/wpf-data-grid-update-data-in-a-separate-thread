<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128650029/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3322)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# Data Grid for WPF - How to Update Data in a Separate Thread

This example demostrates how to update [GridControl](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl) data in a separate thread. To
[synchronize access to data](https://docs.devexpress.com/WPF/11765/controls-and-libraries/data-grid/performance-improvement/manage-multi-thread-data-updates#lock-gridcontrol-updates-to-synchronize-access-to-data), call the [BeginDataUpdate](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.BeginDataUpdate) and [EndDataUpdate](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.EndDataUpdate) methods. These methods allow you to lock updates within the GridControl, process data updates, and then apply all changes to the control.

Create a [custom service](https://docs.devexpress.com/WPF/16920/mvvm-framework/services/how-to-create-a-custom-service) that calls the GridControl methods. Use the [Dispatcher](https://docs.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcher) to invoke these methods in the UI thread.

```cs
...
public interface IGridUpdateService {
    void BeginUpdate();
    void EndUpdate();
}

public class GridUpdateService : ServiceBase, IGridUpdateService {
    //...

    public void BeginUpdate() {
        Dispatcher.Invoke(new Action(() => {
            if (GridControl != null) {
                GridControl.BeginDataUpdate();
            }
        }));
    }

    public void EndUpdate() {
       //...
    }
}
...
```

Add the service to your View and assosiate this service with the GridControl. 
```xaml
<dxg:GridControl>
    <mvvm:Interaction.Behaviors>
        <local:GridUpdateService />
    </mvvm:Interaction.Behaviors>
</dxg:GridControl>
...
```

Access the service at the View Model level. If you inherit your View Model class from ViewModelBase, you can access the service as follows:

```cs
public class ViewModel : ViewModelBase {
    //...
    
    public IGridUpdateService GridUpdateService { get { return GetService<IGridUpdateService>(); } }
    
    void TimerCallback(object state) {
        lock(SyncRoot) {
            if(GridUpdateService != null) {
                GridUpdateService.BeginUpdate();
                foreach(DataItem item in Source) {
                    item.Value = random.Next(100);
                }
                GridUpdateService.EndUpdate();
            }
        }
    }
}
```

## Files to Look At

* [GridUpdateService.cs](./CS/GridUpdateService.cs) (VB: [GridUpdateService.vb](./VB/GridUpdateService.vb))
* [MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
* [ViewModel.cs](./CS/ViewModel.cs) (VB: [ViewModel.vb](./VB/ViewModel.vb))

## Documentation

- [Managing Multi-Thread Data Updates](https://docs.devexpress.com/WPF/11765/controls-and-libraries/data-grid/binding-to-data/managing-multi-thread-data-updates)
- [How to Create a Custom Service](https://docs.devexpress.com/WPF/16920/mvvm-framework/services/how-to-create-a-custom-service)

Refer to the following topics for information on how to access a service in the ViewModel:
- [Services in ViewModelBase Descendants](https://docs.devexpress.com/WPF/17446/mvvm-framework/services/services-in-viewmodelbase-descendants)
- [Services in Generated ViewModels](https://docs.devexpress.com/WPF/17447/mvvm-framework/services/services-in-generated-view-model)
- [Services in Custom ViewModels](https://docs.devexpress.com/WPF/17450/mvvm-framework/services/services-in-custom-viewmodels)

## More Examples
- [How to Call the BeginDataUpdate and EndDataUpdate Methods at the View Model Level](https://github.com/DevExpress-Examples/how-to-call-data-grid-BeginDataUpdate-and-EndDataUpdate-at-the-view-model-level)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-update-data-in-a-separate-thread&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-update-data-in-a-separate-thread&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
