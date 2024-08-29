﻿using Api.Models.Dtos;
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
        public IActionResult GetById(string id)
        {
            var getItem = quanLyThuVienContext.Statisticals.Find(id);

            if (getItem is null)
                return NotFound();
            return Ok(getItem);
        }

        [HttpPost]
        public IActionResult Create(Statistical newItem)
        {
            quanLyThuVienContext.Statisticals.Add(newItem);

            quanLyThuVienContext.SaveChanges();
            return Ok(newItem);
        }

        [HttpPut]
        [Route("{DateTime}")]
        public IActionResult Update(string id, Statistical updateItem)
        {
            var getItem = quanLyThuVienContext.Statisticals.Find(id);

            if (getItem is null)
                return NotFound();

            Utilitys.Copy(getItem, updateItem);
            
            quanLyThuVienContext.SaveChanges();
            return Ok(getItem);
        }

        [HttpDelete]
        [Route("{DateTime}")]
        public IActionResult Delete(string id)
        {
            if (quanLyThuVienContext.Statisticals.Remove(id) == false)
                return NotFound();
            quanLyThuVienContext.SaveChanges();
            
            return Ok();
        }


        #region PrivateMethods
        #endregion
    }
}
