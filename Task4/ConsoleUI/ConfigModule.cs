using BookListService.Interfaces;
using BookRepositories;
using Ninject.Modules;
using XmlExporters;

namespace ConsoleUI
{
    public class ConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IXmlFormatExporter>().To<XmlWriterExporter>();
            Bind<IBookRepository>().To<BinaryFileRepository>();
        }
    }
}
