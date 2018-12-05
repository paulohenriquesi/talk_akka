using SimpleInjector;

namespace TalkAkka.GroupActors.Actors
{
    public static class Module
    {
        public static void AddActors(this Container container)
        {
            container.Register<GetAllCustomersActor>();
            container.Register<GetCustomerActor>();
            container.Register<GetCustomersOrderActor>();
            container.Register<WriteFileActor>();
        }
    }
}
