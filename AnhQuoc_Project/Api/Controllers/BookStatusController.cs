﻿using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStatusController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public BookStatusController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.BookStatuses.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.BookStatuses.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(AddBookStatusDto addBookStatusDto)
        {
            BookStatus newItem = new BookStatus();
            addBookStatusDto.MapTo(ref newItem);

            quanLyThuVienContext.BookStatuses.Add(newItem);
            quanLyThuVienContext.SaveChanges();
            
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(string id, UpdateBookStatusDto updateBookStatusDto)
        {
            var getItem = quanLyThuVienContext.BookStatuses.Find(id);

            if (getItem is null)
                return NotFound();

            updateBookStatusDto.MapTo(ref getItem);
            quanLyThuVienContext.SaveChanges();
            
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.BookStatuses.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
