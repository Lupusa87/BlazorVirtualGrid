using BlazorVirtualGridComponent.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlazorVirtualGridComponent.businessLayer
{
    public static class ColumnsHelper<TItem>
    {
        public static ColProp[] GetColumnsOrderedList(BvgGrid<TItem> bvgGrid)
        {
            int ColsCount = bvgGrid.AllProps.Count() - bvgGrid.bvgSettings.HiddenColumns.Count();

            ColProp[] result = new ColProp[ColsCount];


            PropertyInfo[] AllPropsWithoutHidden = new PropertyInfo[ColsCount];


            int j = 0;
            foreach (var item in bvgGrid.AllProps)
            {
                if (!bvgGrid.bvgSettings.HiddenColumns.Values.Any(x=>x.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    AllPropsWithoutHidden[j] = item;
                    j++;
                }      
            }
          

            

            bool HasFrozenColumns = bvgGrid.bvgSettings.FrozenColumnsListOrdered.Count() > 0;

          
            bool HasNonFrozenColumnsOrdered = bvgGrid.bvgSettings.NonFrozenColumnsListOrdered.Count() > 0;
            if (!HasFrozenColumns && !HasNonFrozenColumnsOrdered)
            {
              
                j = 0;
                foreach (var item in AllPropsWithoutHidden)
                {
                    result[j] = new ColProp()
                    {
                        prop = item,
                        IsFrozen = false,
                    };
                    j++;
                }
            }
            else
            {
               
                List<OrderItem> list1 = new List<OrderItem>();
   
                foreach (var item in AllPropsWithoutHidden)
                {
                    list1.Add(new OrderItem
                    {
                        SequenceNumber = (ushort)(list1.Count+ AllPropsWithoutHidden.Count()+1),
                        Name = item.Name,
                        prop = item,
                        IsFrozen = false,
                        
                    });
                }

                if (HasFrozenColumns && !HasNonFrozenColumnsOrdered)
                {
                  
                    ushort k = 0;
                    foreach (var item in bvgGrid.bvgSettings.FrozenColumnsListOrdered.Values)
                    {

                      if (list1.Any(x=>x.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase)))
                      {
                            OrderItem i = list1.Single(x => x.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase));
                            i.SequenceNumber = k;
                            i.IsFrozen = true;
                            k++;
                      }
                    }
                    result = list1.OrderBy(x => x.SequenceNumber).Select(x => new ColProp { prop = x.prop, IsFrozen = x.IsFrozen }).ToArray();
                }
                if (!HasFrozenColumns && HasNonFrozenColumnsOrdered)
                {
                
                    ushort k = 0;
                    foreach (var item in bvgGrid.bvgSettings.NonFrozenColumnsListOrdered.Values)
                    {

                        if (list1.Any(x => x.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            list1.Single(x => x.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase)).SequenceNumber = k;
                            k++;
                        }
                    }
                    result = list1.OrderBy(x => x.SequenceNumber).Select(x => new ColProp { prop = x.prop, IsFrozen = x.IsFrozen }).ToArray();
                }
                if (HasFrozenColumns && HasNonFrozenColumnsOrdered)
                {
                   
                    ushort k = 0;
                    foreach (var item in bvgGrid.bvgSettings.NonFrozenColumnsListOrdered.Values)
                    {

                        if (list1.Any(x => x.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            list1.Single(x => x.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase)).SequenceNumber = k;
                            k++;
                        }
                    }

                    k = (ushort)(-bvgGrid.bvgSettings.FrozenColumnsListOrdered.Count());
                    foreach (var item in bvgGrid.bvgSettings.FrozenColumnsListOrdered.Values)
                    {

                        if (list1.Any(x => x.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            OrderItem i = list1.Single(x => x.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase));
                            i.SequenceNumber = k;
                            i.IsFrozen = true;
                            k++;
                        }
                    }

                    result = list1.OrderBy(x => x.SequenceNumber).Select(x => new ColProp { prop = x.prop, IsFrozen = x.IsFrozen }).ToArray();
                }
            }
            

            #region SetColWidths and seqNumber
            ushort seqN = 0;
            foreach (var item in result)
            {
                item.SequenceNumber = seqN++;
                if (bvgGrid.bvgSettings.ColumnWidthsDictionary.Values.Any(x=>x.Item1.Equals(item.prop.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    ushort colwidth = bvgGrid.bvgSettings.ColumnWidthsDictionary.Values.Single(x => x.Item1.Equals(item.prop.Name, StringComparison.InvariantCultureIgnoreCase)).Item2;

                    if (colwidth < bvgGrid.bvgSettings.ColWidthMin)
                    {
                        colwidth = bvgGrid.bvgSettings.ColWidthMin;
                    }

                    if (colwidth > bvgGrid.bvgSettings.ColWidthMax)
                    {
                        colwidth = bvgGrid.bvgSettings.ColWidthMax;
                    }

                    item.ColWidth = colwidth;
                }
                else
                {
                    item.ColWidth = bvgGrid.bvgSettings.ColWidthDefault;
                }
            }
            #endregion


          

            return result;
        }
    }


    public class OrderItem
    {
        public ushort SequenceNumber { get; set; }

        public PropertyInfo prop { get; set; }
        public string Name { get; set; }

        public bool IsFrozen { get; set; }
    }


    public class ColProp
    {

        public PropertyInfo prop { get; set; }

        public ushort ColWidth { get; set; }
        public bool IsFrozen { get; set; }

        public ushort SequenceNumber { get; set; }
    }




}
