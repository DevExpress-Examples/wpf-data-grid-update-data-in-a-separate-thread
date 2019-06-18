<!-- default file list -->
*Files to look at*:

* [CustomService.cs](./CS/CustomService.cs) (VB: [CustomService.vb](./VB/CustomService.vb))
* [DataItem.cs](./CS/DataItem.cs) (VB: [DataItem.vb](./VB/DataItem.vb))
* **[MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))**
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
* [ViewModel.cs](./CS/ViewModel.cs) (VB: [ViewModel.vb](./VB/ViewModel.vb))
<!-- default file list end -->
# How to Display Data which is Updated in Another Thread

> A recommended way to manage multi-thread updates is to [dispatch them to the main thread](https://docs.devexpress.com/WPF/11765/controls-and-libraries/data-grid/binding-to-data/managing-multi-thread-data-updates#dispatch-updates-to-the-main-thread). With this approach, you can perform time-consuming operations such as loading data in a separate thread.

> This approach does not work for the [TreeListView](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView).

This example invokes the [BeginDataUpdate](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.BeginDataUpdate) and [EndDataUpdate](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.EndDataUpdate) methods to temporarily disable internal data updates in the **GridControl**.

We used the following approaches in order not to call the GridControl's methods in the ViewModel.

In **v13.1.4** and later, we created a [custom service](https://docs.devexpress.com/WPF/16920/mvvm-framework/services/how-to-create-a-custom-service). This service implements the **ICustomService** interface and invokes **BeginDataUpdate** and **EndDataUpdate** in the **ICustomService.BeginUpdate** and **ICustomService.EndUpdate** methods.

```CS
public interface ICustomService {
    void BeginUpdate();
    void EndUpdate();
}
```

```VB
Public Interface ICustomService
    Sub BeginUpdate()
    Sub EndUpdate()
End Interface
```

Refer to the following topics for information on how to access a service in the ViewModel.
- [Services in ViewModelBase descendants](https://docs.devexpress.com/WPF/17446/mvvm-framework/services/services-in-viewmodelbase-descendants)
- [Services in POCO Objects](https://docs.devexpress.com/WPF/17447/mvvm-framework/services/services-in-poco-objects)
- [Services in custom ViewModels](https://docs.devexpress.com/WPF/17450/mvvm-framework/services/services-in-custom-viewmodels)

In the **previous versions**, the **ViewModel class** provides additional events and invokes them before and after the data update. The **MainWindow** subscribes to these events and invokes **BeginDataUpdate** and **EndDataUpdate** in the event handlers.

See also:

[Managing Multi-Thread Data Updates](https://docs.devexpress.com/WPF/11765/controls-and-libraries/data-grid/binding-to-data/managing-multi-thread-data-updates)
