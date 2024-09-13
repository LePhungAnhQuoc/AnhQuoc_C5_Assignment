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
        public IActionResult GetById(string iduser)
        {
            var getItem = quanLyThuVienContext.UserInfos.Find(iduser);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddUserInfoDto addUserInfoDto)
        {
            UserInfo newItem = new UserInfo();
            addUserInfoDto.MapTo(ref newItem);
            quanLyThuVienContext.UserInfos.Add(newItem);

            quanLyThuVienContext.SaveChanges();
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{iduser}")]
        public IActionResult Update(string iduser, UpdateUserInfoDto updateUserInfoDto)
        {
            var getItem = quanLyThuVienContext.UserInfos.Find(iduser);

            if (getItem is null)
                return NotFound();

            updateUserInfoDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{iduser}")]
        public IActionResult Delete(string iduser)
        {
            if (quanLyThuVienContext.UserInfos.Remove(iduser) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
