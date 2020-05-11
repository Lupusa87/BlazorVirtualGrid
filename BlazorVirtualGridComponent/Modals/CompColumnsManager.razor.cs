using BlazorVirtualGridComponent.classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorVirtualGridComponent.Modals
{
    public partial class CompColumnsManager<TItem>:IDisposable
    {
        [Parameter]
        public BvgGrid<TItem> bvgGrid { get; set; }

        protected List<MyDraggable> listDraggable = new List<MyDraggable>();
        protected List<MyDragTarget> listDragTarget = new List<MyDragTarget>();


        protected ClassForJS classForJS = new ClassForJS();

        
        protected override void OnInitialized()
        {

            classForJS.CustomOnDragStart = InvokeDragStartFromJS;
            classForJS.CustomOnDrop = InvokeDropFromJS;

            for (int i = 0; i < 3; i++)
            {
                listDragTarget.Add(new MyDragTarget
                {
                    ID = listDragTarget.Count,
                    ElementID = "DropTargetdDiv" + listDragTarget.Count,

                });
            }

            foreach (PropertyInfo item in bvgGrid.AllProps)
            {


                if (bvgGrid.bvgSettings.HiddenColumns.Values.Any(x => x.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    AddItem(2, item.Name);
                }
                else if(bvgGrid.bvgSettings.FrozenColumnsListOrdered.Values.Any(x => x.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    AddItem(1, item.Name);
                }
                else
                {
                    AddItem(0, item.Name);
                }



            }

            base.OnInitialized();
        }


        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                RegisterJsEvents();
            }


            base.OnAfterRender(firstRender);
        }

        public void RegisterJsEvents()
        {
            foreach (var item in listDragTarget)
            {
                BVirtualGridCJsInterop.HandleDrop(item.ElementID, item.ID, DotNetObjectReference.Create(classForJS));
            }
        }

        public void OnMouseDown(MouseEventArgs e, MyDraggable item)
        {
           
            BVirtualGridCJsInterop.HandleDrag(item.ElementID, item.ID, DotNetObjectReference.Create(classForJS));
           
        }

      
        public void InvokeDragStartFromJS(int id)
        {
            //if you need to know for some reason when drag is started this method will be invoked
        }

       
        public void InvokeDropFromJS(int parentID, int id)
        {
           
            if (listDraggable.Any(x => x.ID == id))
            {
                listDraggable.Single(x => x.ID == id).ParentID = parentID;

                StateHasChanged();
            }
        }

        private void AddItem(int parentID, string name)
        {
            int _id = listDraggable.Count + 1;
            listDraggable.Add(new MyDraggable
            {
                ID = _id,
                Name = name,
                ElementID = "draggableDiv" + _id,
                ParentID = parentID,
            });
        }


        public void SaveChanges()
        {
     

            bvgGrid.bvgSettings.HiddenColumns = new ValuesContainer<string>();
            if (listDraggable.Where(x => x.ParentID == 2).Any())
            {
                foreach (var item in listDraggable.Where(x => x.ParentID == 2).OrderBy(x => x.SequenceNumber))
                {

                    bvgGrid.bvgSettings.HiddenColumns.Add(item.Name);
                }
            }

            bvgGrid.bvgSettings.FrozenColumnsListOrdered = new ValuesContainer<string>();
            if (listDraggable.Where(x => x.ParentID == 1).Any())
            {
                foreach (var item in listDraggable.Where(x => x.ParentID == 1).OrderBy(x => x.SequenceNumber))
                {

                    bvgGrid.bvgSettings.FrozenColumnsListOrdered.Add(item.Name);
                }
            }

        }


        public void Dispose()
        {

        }

    }

    public class MyDraggable
    {

        public int ID { get; set; }

        public string ElementID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }

        public int SequenceNumber { get; set; }
    }

    public class MyDragTarget
    {

        public int ID { get; set; }

        public string ElementID { get; set; }
    }
}
