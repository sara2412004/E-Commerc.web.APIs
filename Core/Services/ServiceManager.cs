using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {
        //1.momkn a3ml fun get service zy ma 3mlt fel uint of work 
        //2. manual implemention => ana ely a3ml el object w kda 
        //3. dependancy injection 

        //4.lazy implement 
        private readonly Lazy<IproductService> _LazyproductService=new Lazy<IproductService> (()=>new ProductService(unitOfWork,mapper));
        public IproductService productService => _LazyproductService.Value;


    }
}
