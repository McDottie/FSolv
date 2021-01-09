using FSolv.model;
using Interfaces;
using System.Collections.Generic;

namespace FSolv.mapper.interfaces

{
    interface IProductMapper : IMapper<IProduto, int?, List<IProduto>>
    {
    }
}
