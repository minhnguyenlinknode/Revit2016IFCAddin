using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BIM.IFC.Export.UI.UPAR
{
    public class IFCExternalApplication : IExternalApplication
    {
        private static string _namespace_prefix;

        static IFCExternalApplication()
        {
            IFCExternalApplication._namespace_prefix = string.Concat(typeof(IFCExternalApplication).Namespace, ".");
        }

        public IFCExternalApplication()
        {
        }

        private void CreateRibbonPanel(UIControlledApplication a)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string location = executingAssembly.Location;
            this.GetType().FullName.Replace("App", "Command");
            RibbonPanel ribbonPanel = a.CreateRibbonPanel("UrbanPlanAR");
            PushButtonData pushButtonDatum = new PushButtonData("RevitIFCExport_Command", "IFC Export", location, "BIM.IFC.Export.UI.UPAR.IFCExternalCommand")
            {
                ToolTip = "Export a IFC file from a Revit Project",
                Image = this.NewBitmapImage(executingAssembly, "converter16.bmp"),
                LargeImage = this.NewBitmapImage(executingAssembly, "converter32.bmp"),
                LongDescription = "Export a IFC file from a Revit Project To Cloud Storage" //pushButtonDatum.ToolTip
            };
            ribbonPanel.AddItem(pushButtonDatum);
        }

        private BitmapImage NewBitmapImage(Assembly a, string imageName)
        {
            //var test = a.GetManifestResourceNames();
            Stream manifestResourceStream = a.GetManifestResourceStream(string.Concat(IFCExternalApplication._namespace_prefix, imageName));            
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = manifestResourceStream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication a)
        {
            this.CreateRibbonPanel(a);
            return Result.Succeeded;
        }
    }
}
