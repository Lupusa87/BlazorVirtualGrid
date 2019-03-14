using BlazorVirtualGridComponent.classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.businessLayer
{
    internal class CssGridHelper<TItem>
    {
        private string OldStyle_myGridArea = string.Empty;
        private string OldStyle_myContainerFrozen = string.Empty;
        private string OldStyle_myContainerNonFrozen = string.Empty;


        private BvgGrid<TItem> bvgGrid = new BvgGrid<TItem>();


        public CssGridHelper(BvgGrid<TItem> _bvgGrid)
        {
            bvgGrid = _bvgGrid;
           
        }

        internal void SaveCssStyles()
        {
            
                OldStyle_myGridArea = bvgGrid.cssHelper.GetStyleWithSelector(".myGridArea");
                OldStyle_myContainerFrozen = bvgGrid.cssHelper.GetStyleWithSelector(".myContainerFrozen");
                OldStyle_myContainerNonFrozen = bvgGrid.cssHelper.GetStyleWithSelector(".myContainerNonFrozen");
        }

        internal void RefreshCssStyles()
        {

            bvgGrid.cssHelper = new CssHelper<TItem>(bvgGrid);

           

                string[] updatePkg = new string[7];

                string NewStyle_myGridArea = bvgGrid.cssHelper.GetStyleWithSelector(".myGridArea");
                string NewStyle_myContainerFrozen = bvgGrid.cssHelper.GetStyleWithSelector(".myContainerFrozen");
                string NewStyle_myContainerNonFrozen = bvgGrid.cssHelper.GetStyleWithSelector(".myContainerNonFrozen");





            Console.WriteLine(OldStyle_myGridArea);
            Console.WriteLine(NewStyle_myGridArea);
            Console.WriteLine(OldStyle_myContainerFrozen);
            Console.WriteLine(NewStyle_myContainerFrozen);
            Console.WriteLine(OldStyle_myContainerNonFrozen);
            Console.WriteLine(NewStyle_myContainerNonFrozen);


            updatePkg[0] = "bvgStyle1";

                updatePkg[1] = OldStyle_myGridArea;
                updatePkg[2] = NewStyle_myGridArea;
                updatePkg[3] = OldStyle_myContainerFrozen;
                updatePkg[4] = NewStyle_myContainerFrozen;
                updatePkg[5] = OldStyle_myContainerNonFrozen;
                updatePkg[6] = NewStyle_myContainerNonFrozen;

                BvgJsInterop.UpdateStyleRule(updatePkg);
            

        }
    }
}
