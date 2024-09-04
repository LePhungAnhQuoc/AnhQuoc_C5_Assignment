using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;
using System.Security.Cryptography.Xml;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public BookController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.Books.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.Books.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddBookDto addBookDto)
        {
            Book newItem = new Book();
            addBookDto.MapTo(ref newItem);

            quanLyThuVienContext.Books.Add(newItem);
            quanLyThuVienContext.SaveChanges();

            return Ok(newItem);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(string id, UpdateBookDto updateBookDto)
        {
            var getItem = quanLyThuVienContext.Books.Find(id);

            if (getItem is null)
                return NotFound();

            updateBookDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();

            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.Books.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
