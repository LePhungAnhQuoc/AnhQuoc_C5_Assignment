using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanDetailHistoryController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public LoanDetailHistoryController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.LoanDetailHistories.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.LoanDetailHistories.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddLoanDetailHistoryDto addLoanDetailHistoryDto)
        {
            LoanDetailHistory newItem = new LoanDetailHistory();
            addLoanDetailHistoryDto.MapTo(ref newItem);
            quanLyThuVienContext.LoanDetailHistories.Add(newItem);

            quanLyThuVienContext.SaveChanges();
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(string id, UpdateLoanDetailHistoryDto updateLoanDetailHistoryDto)
        {
            var getItem = quanLyThuVienContext.LoanDetailHistories.Find(id);

            if (getItem is null)
                return NotFound();

            updateLoanDetailHistoryDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.LoanDetailHistories.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
