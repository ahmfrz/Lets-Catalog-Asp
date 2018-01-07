using Catalog.Entities;
using StructureMap;

namespace LetsCatalog
{
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            //                x.For<IExample>().Use<Example>();

                            x.For<IUnitOfWork>().Use<UnitOfWork>();
                        });
            return ObjectFactory.Container;
        }
    }
}