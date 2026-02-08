using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> _userManager, IConfiguration _configuration,IMapper _mapper, IBasketRepository repository) //: IServiceManager
    {
        //1.momkn a3ml fun get service zy ma 3mlt fel uint of work 
        //2. manual implemention => ana ely a3ml el object w kda 
        //3. dependancy injection 

        //lazy implement 
        // it is lazy didnt need to add anything in program ( manual)
        private readonly Lazy<IproductService> _LazyproductService=new Lazy<IproductService> (()=>new ProductService(unitOfWork,mapper));
        public IproductService productService => _LazyproductService.Value;

        private readonly Lazy<IAuthenticationService> _LazyAuthenticationservic = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager,_configuration, _mapper));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationservic.Value;

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(repository, mapper));
        public IBasketService BasketService => _LazyBasketService.Value;

        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(repository, mapper,unitOfWork));
        public IOrderService OrderService => _LazyOrderService.Value;
    }
}
