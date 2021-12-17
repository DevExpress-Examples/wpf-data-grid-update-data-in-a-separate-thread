<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128650029/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3322)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Data Grid for WPF - How to Update Data in a Separate Thread

> A recommended way to manage multi-thread updates is to [dispatch them to the main thread](https://docs.devexpress.com/WPF/11765/controls-and-libraries/data-grid/binding-to-data/managing-multi-thread-data-updates#dispatch-updates-to-the-main-thread). With this approach, you can perform time-consuming operations such as loading data in a separate thread.

> This approach does not work for the [TreeListView](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView). [Dispatch data updates to the main thread](https://docs.devexpress.com/WPF/11765/controls-and-libraries/data-grid/binding-to-data/managing-multi-thread-data-updates#dispatch-updates-to-the-main-thread) to avoid the issue in this scenario.

This example invokes the [BeginDataUpdate](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.BeginDataUpdate) and [EndDataUpdate](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.EndDataUpdate) methods to temporarily disable internal data updates in the **GridControl**.

We used the following approaches in order not to call the GridControl's methods in the ViewModel.

In **v13.1.4** and later, we created a [custom service](https://docs.devexpress.com/WPF/16920/mvvm-framework/services/how-to-create-a-custom-service). This service implements the **IGridUpdateService** interface and invokes **BeginDataUpdate** and **EndDataUpdate** in the **IGridUpdateService.BeginUpdate** and **IGridUpdateService.EndUpdate** methods.

```CS
public interface IGridUpdateService {
    void BeginUpdate();
    void EndUpdate();
}
```

```vb
Public Interface IGridUpdateService
    Sub BeginUpdate()
    Sub EndUpdate()
End Interface
```

Refer to the following topics for information on how to access a service in the ViewModel.
- [Services in ViewModelBase descendants](https://docs.devexpress.com/WPF/17446/mvvm-framework/services/services-in-viewmodelbase-descendants)
- [Services in generated ViewModels](https://docs.devexpress.com/WPF/17447/mvvm-framework/services/services-in-generated-view-model)
- [Services in custom ViewModels](https://docs.devexpress.com/WPF/17450/mvvm-framework/services/services-in-custom-viewmodels)

In the **previous versions**, the **ViewModel class** provides additional events and invokes them before and after the data update. The **MainWindow** subscribes to these events and invokes **BeginDataUpdate** and **EndDataUpdate** in the event handlers.

## Files to Look At

* [GridUpdateService.cs](./CS/GridUpdateService.cs) (VB: [GridUpdateService.vb](./VB/GridUpdateService.vb))
* **[MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))**
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
* [ViewModel.cs](./CS/ViewModel.cs) (VB: [ViewModel.vb](./VB/ViewModel.vb))

## Documentation

[Managing Multi-Thread Data Updates](https://docs.devexpress.com/WPF/11765/controls-and-libraries/data-grid/binding-to-data/managing-multi-thread-data-updates)

## More Examples
[How to Call the BeginDataUpdate and EndDataUpdate Methods at the View Model Level](https://github.com/DevExpress-Examples/how-to-call-data-grid-BeginDataUpdate-and-EndDataUpdate-at-the-view-model-level)
