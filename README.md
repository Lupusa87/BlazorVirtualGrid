# Blazor Virtual Grid

![](https://placehold.it/15/4747d1/000000?text=+) 
If you like my work on blazor and want to see more open sourced demos please support me with donations.
![](https://placehold.it/15/4747d1/000000?text=+) 

[donate via paypal](https://www.paypal.me/VakhtangiAbashidze/50)


![](https://placehold.it/15/00e600/000000?text=+) 
Please send [email](VakhtangiAbashidze@gmail.com) if you consider to **hire me**.
![](https://placehold.it/15/00e600/000000?text=+) 


=================================================

Component is live [here](https://lupblazorvirtualgrid.z13.web.core.windows.net)

In this repo there are component itself and consumer blazor app where you can see how component can be used.

Here I will also give some basic usage info.

[Component](https://www.nuget.org/packages/BlazorVirtualGridComponent/) can be downloaded from nuget, also we need four more nuget packages to install:
1. [BlazorSpitter](https://www.nuget.org/packages/BlazorSplitterComponent/);
2. [BlazorScrollbar](https://www.nuget.org/packages/BlazorScrollbarComponent/);
3. [Mono.WebAssembly.Interop](https://www.nuget.org/packages/Mono.WebAssembly.Interop);
4. [System.Linq.Dynamic.Core](https://www.nuget.org/packages/System.Linq.Dynamic.Core/).

Client app csproj file should have configured linker
```
  <ItemGroup>
    <BlazorLinkerDescriptor Include="Linker.xml" />
  </ItemGroup>
```

and have linker file inside app folder with this content:

```
<?xml version="1.0" encoding="UTF-8" ?>
<linker>
  <assembly fullname="mscorlib">
    <type fullname="System.Threading.WasmRuntime" />
  </assembly>
  <assembly fullname="System.Core">
    <type fullname="System.Linq*" />
  </assembly>
  <assembly fullname="System.Linq.Dynamic.core">
  </assembly>
  <assembly fullname="BlazorVirtualGrid" />
</linker>
```
Last item - BlazorVirtualGrid should be replaced with your blazor app name.
This is necessary because linker can remove libraries which are used by component, for example without System.Linq.Dynamic.Core sorting will not work.

[Index.html](https://github.com/Lupusa87/BlazorVirtualGrid/blob/master/BlazorVirtualGrid/Pages/Index.cshtml) and [Indexbase.cs](https://github.com/Lupusa87/BlazorVirtualGrid/blob/master/BlazorVirtualGrid/Pages/IndexBase.cs) have example how component can be configured and consumed.

Component can receive any list object in parameter SourceList, table name and configuration settings.

`<CompBlazorVirtualGrid ref="Bvg1" SourceList="@list1" TableName="@TableName1" bvgSettings="@bvgSettings1"></CompBlazorVirtualGrid>`

For BvgSettings you will provide TItem and all desired configurations.

`public BvgSettings<MyItem> bvgSettings1 { get; set; } = new BvgSettings<MyItem>();`

You can configure styles and global properties:

```
  bvgSettings1.NonFrozenCellStyle = new BvgStyle
    {
        BackgroundColor = "#cccccc",
        ForeColor = "#00008B",
        BorderColor = "#000000",
        BorderWidth = 1,
    };

    bvgSettings1.RowHeight = 40;
    bvgSettings1.HeaderHeight = 50;
    bvgSettings1.ColWidthMin = 220;
    bvgSettings1.ColWidthMax = 400;

    bvgSettings1.VerticalScrollStyle = new BvgStyleScroll
    {
        ButtonColor = "#008000",
        ThumbColor = "#FF0000",
        ThumbWayColor = "#90EE90",
    };
```

Set frozen columns if any:
 ```
bvgSettings1.FrozenColumnsListOrdered = new ValuesContainer<string>();
    bvgSettings1.FrozenColumnsListOrdered
        .Add(nameof(MyItem.C3))
        .Add(nameof(MyItem.Date));
```

Hide columns:
```
    bvgSettings1.HiddenColumns
        .Add(nameof(MyItem.C1))
        .Add(nameof(MyItem.C2));
```

Set columns order, code will organize columns by provided order, after this by default:
```
bvgSettings1.NonFrozenColumnsListOrdered = new ValuesContainer<string>();
            bvgSettings1.NonFrozenColumnsListOrdered
                .Add(nameof(MyItem.C1))
                .Add(nameof(MyItem.C2));
```

Set column widths if you like any of them to have individual width:
```
  bvgSettings1.ColumnWidthsDictionary = new ValuesContainer<Tuple<string, ushort>>();
  bvgSettings1.ColumnWidthsDictionary
      .Add(Tuple.Create(nameof(MyItem.C3), (ushort)200))
      .Add(Tuple.Create(nameof(MyItem.Date), (ushort)200));
```

Please also check this youtube video about BlazorVirtualGrid.

You can always open new issue or make PR, also ask questions at VakhtangiAbashidze@gmail.com or reach me out in twitter @lupusa1

# Thank you for your interest and happy coding with blazor.




          


