using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public ChildController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.Children.ToList());
        }

        [HttpGet]
        [Route("{idReader}")]
        public IActionResult GetById(string idReader)
        {
            var getItem = quanLyThuVienContext.Children.Find(idReader);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddChildDto addItemDto)
        {
            Child newChild = new Child();
            addItemDto.MapTo(ref newChild);

            quanLyThuVienContext.Children.Add(newChild);
            quanLyThuVienContext.SaveChanges();

            return Ok(newChild);
        }

        [HttpPut]
        [Route("{idReader}")]
        public IActionResult Update(string idReader, UpdateChildDto updateChildDto)
        {
            var getItem = quanLyThuVienContext.Children.Find(idReader);

            if (getItem is null)
                return NotFound();

            updateChildDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();

            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{idReader}")]
        public IActionResult Delete(string idReader)
        {
            if (quanLyThuVienContext.Children.Remove(idReader) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
