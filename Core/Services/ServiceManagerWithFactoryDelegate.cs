using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    // This is a placeholder implementation of IServiceManager using factory delegates.
    //shakl tany 8er el Lazy Implement ely fel serviceManager [Da a7san].....
    //kan 3ndy moshkala en ek CTOR by inject lia hagat kteer fe kol mara b5od object mn service fehom
    //b inject kolhom wana msh hst5dm kolo el '_mapper' msln aw ay haga mnhom fe object da....
    public class ServiceManagerWithFactoryDelegate(
        Func<IproductService> ProductFactory ,
        Func<IAuthenticationService> AuthenticationFactory ,
        Func<IBasketService> BasketFactory ,
        Func<IOrderService> OrderFactory,
        Func<ICacheService> CachFactory) : IServiceManager
    {
        //.Invoke() hena b invoke  bs ely ana 3ayzo mn el factory delegate
        public IproductService productService => ProductFactory.Invoke();

        public IAuthenticationService AuthenticationService => AuthenticationFactory.Invoke();

        public IBasketService BasketService => BasketFactory.Invoke();

        public IOrderService OrderService => OrderFactory.Invoke();
        public ICacheService CacheService => CachFactory.Invoke();
    }
}
