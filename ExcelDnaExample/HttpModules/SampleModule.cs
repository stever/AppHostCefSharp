namespace SteveRGB.ExcelDnaExample.HttpModules
{
    public class SampleModule : Nancy.NancyModule
    {
        public SampleModule()
        {
            Get["/"] = _ => "Hello World!";
        }
    }
}
