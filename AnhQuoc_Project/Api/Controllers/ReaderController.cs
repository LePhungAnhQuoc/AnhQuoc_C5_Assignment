﻿using Api.Models.Dtos;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Api.Utilities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly string prefix = "U";
        private readonly int prefixLength = 1;

        private readonly QuanLyThuVienContext quanLyThuVienContext;

        public ReaderController(QuanLyThuVienContext quanLyThuVienContext)
        {
            this.quanLyThuVienContext = quanLyThuVienContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(quanLyThuVienContext.Readers.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.Readers.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(Reader newItem)
        {
            quanLyThuVienContext.Readers.Add(newItem);

            quanLyThuVienContext.SaveChanges();
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(string id, Reader updateItem)
        {
            var getItem = quanLyThuVienContext.Readers.Find(id);

            if (getItem is null)
                return NotFound();

            Utilitys.Copy(getItem, updateItem);
            getItem.ModifiedAt = DateTime.Now;
            
            quanLyThuVienContext.SaveChanges();
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.Readers.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}