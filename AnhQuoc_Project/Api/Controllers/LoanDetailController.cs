using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanDetailController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public LoanDetailController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.LoanDetails.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.LoanDetails.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddLoanDetailDto addLoanDetailDto)
        {
            LoanDetail newItem = new LoanDetail();
            addLoanDetailDto.MapTo(ref newItem);

            quanLyThuVienContext.LoanDetails.Add(newItem);
            quanLyThuVienContext.SaveChanges();
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(string id, UpdateLoanDetailDto updateLoanDetailDto)
        {
            var getItem = quanLyThuVienContext.LoanDetails.Find(id);

            if (getItem is null)
                return NotFound();

            updateLoanDetailDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.LoanDetails.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
