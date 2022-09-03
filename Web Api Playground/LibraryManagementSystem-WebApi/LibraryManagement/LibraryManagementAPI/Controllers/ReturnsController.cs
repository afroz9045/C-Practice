using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class ReturnsController : ApiController
    {
        private readonly IReturnRepository _returnRepository;
        private readonly IMapper _mapper;

        public ReturnsController(IReturnRepository returnRepository, IMapper mapper)
        {
            _returnRepository = returnRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddReturn([FromBody] ReturnVm returnVm, short issueId)
        {
            var returnBook = _mapper.Map<ReturnVm, Return>(returnVm);
            return Ok(await _returnRepository.AddReturnAsync(returnBook,issueId));
        }

        [HttpGet]
        public async Task<ActionResult> GetBookReturn()
        {
            return Ok(await _returnRepository.GetReturnAsync());
        }
        [HttpGet("{bookReturnId}")]
        public async Task<ActionResult>  GetBookReturnById(int bookReturnId)
        {
            return Ok(await _returnRepository.GetReturnByIdAsync(bookReturnId));
        }
        [HttpPut("{bookReturnId}")]
        public async Task<ActionResult> UpdateReturnBookDetails(int bookReturnId, [FromBody] ReturnVm returnVm)
        {
            var returnBook = _mapper.Map<ReturnVm, Return>(returnVm);
            var result = Ok(await _returnRepository.UpdateReturnAsync(bookReturnId, returnBook));
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
        [HttpDelete]
    }
}