
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public interface IProductService : IBaseService<ProductDto, Product>
    {
        BaseResponse<ProductDto> UpdateOperation(int id, ProductDto dto);
    }
}
