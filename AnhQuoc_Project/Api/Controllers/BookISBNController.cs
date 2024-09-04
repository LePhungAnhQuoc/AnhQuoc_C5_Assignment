using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookISBNController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public BookISBNController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.BookIsbns.ToList());
        }

        [HttpGet]
        [Route("{isbn}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.BookIsbns.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddBookISBNDto addBookISBNDto)
        {
            BookIsbn newItem = new BookIsbn();
            addBookISBNDto.MapTo(ref newItem);

            quanLyThuVienContext.BookIsbns.Add(newItem);
            quanLyThuVienContext.SaveChanges();
            
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{isbn}")]
        public IActionResult Update(string id, UpdateBookISBNDto updateBookISBNDto)
        {
            var getItem = quanLyThuVienContext.BookIsbns.Find(id);

            if (getItem is null)
                return NotFound();

            updateBookISBNDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();
            
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{isbn}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.BookIsbns.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
