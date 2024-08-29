using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public UserInfoController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.UserInfos.ToList());
        }

        [HttpGet]
        [Route("{iduser}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.UserInfos.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(UserInfo newItem)
        {
            quanLyThuVienContext.UserInfos.Add(newItem);

            quanLyThuVienContext.SaveChanges();
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{iduser}")]
        public IActionResult Update(string id, UserInfo updateItem)
        {
            var getItem = quanLyThuVienContext.UserInfos.Find(id);

            if (getItem is null)
                return NotFound();

            Utilitys.Copy(getItem, updateItem);
            
            quanLyThuVienContext.SaveChanges();
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{iduser}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.UserInfos.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
