using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdultController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public AdultController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.Adults.ToList());
        }

        [HttpGet]
        [Route("{idreader}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.Adults.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddAdultDto addAdultDto)
        {
            Adult newItem = new Adult();
            addAdultDto.MapTo(ref newItem);

            quanLyThuVienContext.Adults.Add(newItem);
            quanLyThuVienContext.SaveChanges();

            return Ok(newItem);
        }

        [HttpPut]
        [Route("{idreader}")]
        public IActionResult Update(string id, UpdateAdultDto updateAdultDto)
        {
            var getItem = quanLyThuVienContext.Adults.Find(id);

            if (getItem is null)
                return NotFound();

            updateAdultDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();

            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{idreader}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.Adults.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
