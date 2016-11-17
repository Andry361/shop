using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Logic
{
  public static class AssembliesMenager
  {
    private static List<Assembly> _loadedAssemblies = null;
    private static object _loker = new object();

    public static List<Assembly> GetAllAssemblies()
    {
      //L.Debug("");
      //L.Debug("Enter method:");
      try
      {
        if (_loadedAssemblies == null)
        {
          lock (_loker)
          {
            if (_loadedAssemblies == null)
            {
              Func<string, bool> isValidName = (string name) =>
              {
                return !name.StartsWith("System")
                    && !name.StartsWith("Microsoft")
                    && !name.StartsWith("mscorlib")
                    && !name.StartsWith("NLog")
                    && !name.StartsWith("NHibernate")
                    && !name.StartsWith("FluentNHibernate")
                    && !name.StartsWith("vshost32")
                    && !name.StartsWith("WindowsBase")
                    && !name.StartsWith("PresentationCore")
                    && !name.StartsWith("PresentationFramework")
                    && !name.StartsWith("UIAutomationTypes")
                    && !name.StartsWith("Accessibility")
                    && !name.StartsWith("UIAutomationProvider")
                    && !name.StartsWith("PresentationUI")
                    && !name.StartsWith("ReachFramework")
                    && !name.StartsWith("ClosedXML")
                    && !name.StartsWith("DocumentFormat.")
                    && !name.StartsWith("Newtonsoft.Json")
                    && !name.StartsWith("Caliburn.Micro")
                    && !name.StartsWith("Autofac")
                    && !name.StartsWith("WPFToolkit")
                    && !name.StartsWith("Google.")
                    && !name.StartsWith("HtmlAgilityPack")
                    && !name.StartsWith("Iesi.")
                    && !name.StartsWith("log4net")
                    && !name.StartsWith("office")
                    && !name.StartsWith("SetupCustomActions")
                    && !name.StartsWith("stdole")
                    && !name.StartsWith("Zlib.")
                    && !name.StartsWith("MySql.Data")
                    && !name.StartsWith("CsQuery")
                    && !name.StartsWith("DynamicProxyGenAssembly")
                    && !name.StartsWith("FakeItEasy");
              };

              var filterSystemAssemblies = new Func<List<Assembly>, List<Assembly>>((List<Assembly> input) =>
              {
                var result = input.Where(x => isValidName(x.FullName)).ToList();
                return result;
              });

              var allAssemblies = filterSystemAssemblies(AppDomain.CurrentDomain.GetAssemblies().ToList());
              bool allLoaded = false;
              while (!allLoaded)
              {
                allLoaded = true;
                foreach (var assm in allAssemblies.ToList())
                {
                  var referencedAssemblies = assm.GetReferencedAssemblies().Where(x => isValidName(x.FullName)).Select(x => Assembly.Load(x)).ToList();
                  foreach (var refAssm in referencedAssemblies)
                  {
                    if (allAssemblies.Contains(refAssm)) continue;
                    allLoaded = false;
                    allAssemblies.Add(refAssm);
                  }
                }
              }

              _loadedAssemblies = allAssemblies;
            }
          }
        }

        return _loadedAssemblies;
      }
      catch (Exception ex)
      {
        //L.Exception(ex, "General exception");
        throw;
      }
      finally
      {
        //L.Debug("Leave method");
        //L.Debug("");
      }
    }
  }
}
