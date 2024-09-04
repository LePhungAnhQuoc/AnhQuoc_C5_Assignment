﻿using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public UserController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.Users.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.Users.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddUserDto addUserDto)
        {
            User newItem = new User();
            addUserDto.MapTo(ref newItem);
            quanLyThuVienContext.Users.Add(newItem);
            quanLyThuVienContext.SaveChanges();

            return Ok(newItem);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(string id, UpdateUserDto updateUserDto)
        {
            var getItem = quanLyThuVienContext.Users.Find(id);

            if (getItem is null)
                return NotFound();

            updateUserDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.Users.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
