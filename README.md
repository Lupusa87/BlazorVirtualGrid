# Blazor Virtual Grid

![](https://placehold.it/15/4747d1/000000?text=+) 
If you like my blazor works and want to see more open sourced repos please support me with [paypal donation](https://www.paypal.me/VakhtangiAbashidze/10)
![](https://placehold.it/15/4747d1/000000?text=+) 

![](https://placehold.it/15/00e600/000000?text=+) 
Please send [email](mailto:VakhtangiAbashidze@gmail.com) if you consider to **hire me**.
![](https://placehold.it/15/00e600/000000?text=+)     


![](https://placehold.it/15/ffffff/000000?text=+)  


=================================================

Component is available on [nuget](https://www.nuget.org/packages/BlazorVirtualGridComponent/)

After installing package please add bellow script to your index.html

```
<script src="_content/BlazorVirtualGridComponent/BVirtualGridCJsInterop.js"></script>
```



Component is live [here](https://lupblazordemos.z13.web.core.windows.net/PageVirtualGrid)

In this repo there are component itself and consumer blazor app where you can see how component can be used.

Here I will also give some basic usage info.

[Component](https://www.nuget.org/packages/BlazorVirtualGridComponent/) can be downloaded from nuget, also we need four more nuget packages to install:
1. [BlazorSpitter](https://www.nuget.org/packages/BlazorSplitterComponent/);
2. [BlazorScrollbar](https://www.nuget.org/packages/BlazorScrollbarComponent/);
3. [Mono.WebAssembly.Interop](https://www.nuget.org/packages/Mono.WebAssembly.Interop);
4. [System.Linq.Dynamic.Core](https://www.nuget.org/packages/System.Linq.Dynamic.Core/).


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

Please also check [this youtube video](https://www.youtube.com/watch?v=UDylcERISeY) about BlazorVirtualGrid.

You can always open new issue or make PR, also ask questions at VakhtangiAbashidze@gmail.com or reach me out in twitter @lupusa1

# Thank you for your interest and happy coding with blazor.




          


