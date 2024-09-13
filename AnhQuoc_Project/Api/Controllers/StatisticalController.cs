using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public StatisticalController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.Statisticals.ToList());
        }

        [HttpGet]
        [Route("{DateTime}")]
        public IActionResult GetById(string DateTime)
        {
            var getItem = quanLyThuVienContext.Statisticals.Find(Convert.ToDateTime(DateTime));

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddStatisticalDto addStatisticalDto)
        {
            Statistical newItem = new Statistical();
            addStatisticalDto.MapTo(ref newItem);
            quanLyThuVienContext.Statisticals.Add(newItem);

            quanLyThuVienContext.SaveChanges();
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{DateTime}")]
        public IActionResult Update(string DateTime, UpdateStatisticalDto updateStatisticalDto)
        {
            var getItem = quanLyThuVienContext.Statisticals.Find(Convert.ToDateTime(DateTime));

            if (getItem is null)
                return NotFound();

            updateStatisticalDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{DateTime}")]
        public IActionResult Delete(string DateTime)
        {
            if (quanLyThuVienContext.Statisticals.Remove(Convert.ToDateTime(DateTime)) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
