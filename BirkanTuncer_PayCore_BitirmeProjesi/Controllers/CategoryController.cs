using AutoMapper;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using BirkanTuncer_PayCore_BitirmeProjesi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Collections.Generic;




namespace BirkanTuncer_PayCore_BitirmeProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ISession session;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// Only users with admin authority can make category changes.
        /// Normal users can only see categories.
        /// 
        /// </summary>
        /// <param name="categoryService"></param>
        /// <param name="mapper"></param>
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;

        }

        [Authorize]
        [HttpGet]
        public BaseResponse<IEnumerable<CategoryDto>> GetAll()
        {
            var response = categoryService.GetAll();
            return response;
        }

        [Authorize]
        [HttpGet("{id}")]
        public BaseResponse<CategoryDto> GetById(int id)
        {
            var response = categoryService.GetById(id);
            return response;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public BaseResponse<CategoryDto> Delete(int id)
        {
            var response = categoryService.Remove(id);
            return response;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public BaseResponse<CategoryDto> Create([FromBody] CategoryDto dto)
        {
            var response = categoryService.Insert(dto);
            return response;

        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public BaseResponse<CategoryDto> Update(int id, [FromBody] CategoryDto dto)
        {
            var response = categoryService.Update(id, dto);
            return response;
        }

    }
}
