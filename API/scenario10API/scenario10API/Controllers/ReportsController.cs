using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scenario10API.models;
using scenario10API.models.DTOs;
using scenario10API.Repos.Reports;

namespace scenario10API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepo _ReportRepo;

        public ReportsController(IReportRepo ReportRepo)
        {
            _ReportRepo = ReportRepo;
        }

        // GET: api/Reports
        [HttpGet]
        public ActionResult<IEnumerable<ReportDTO>> GetReports()
        {
            var reports =  _ReportRepo.GetAll()
                .Select(report => new ReportDTO
                {
                    Id = report.Id,
                    Location = report.Location,
                    Img = report.Img,
                    Date = report.Date,
                    UserId = report.UserId,
                    SpeciesId = report.SpeciesId,
                })
                .ToList();

            if (!reports.Any())
            {
                return NotFound();
            }

            return reports;
        }

        [HttpGet("{id}")]
        public ActionResult<ReportDTO> GetReport(int id)
        {
            var report = _ReportRepo.GetById(id);

            if (report == null)
            {
                return NotFound();
            }

            var reportDTO = new ReportDTO
            {
                Id = report.Id,
                Location = report.Location,
                Img = report.Img,
                Date = report.Date,
                UserId = report.UserId,
                SpeciesId = report.SpeciesId,
            };

            return reportDTO;
        }

        [HttpPut("{id}")]
        public IActionResult PutReport(int id, ReportDTO reportDTO)
        {
            if (id != reportDTO.Id)
            {
                return BadRequest();
            }

            var report = _ReportRepo.GetById(id);

            if (report == null)
            {
                return NotFound();
            }

            report.Location = reportDTO.Location;
            report.Img = reportDTO.Img;
            report.Date = reportDTO.Date;
            report.UserId = reportDTO.UserId;
            report.SpeciesId = reportDTO.SpeciesId;

            _ReportRepo.Update(report);
            _ReportRepo.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<ReportDTO> PostReport(ReportDTO reportDTO)
        {
            var report = new Report
            {
                Location = reportDTO.Location,
                Img = reportDTO.Img,
                Date = reportDTO.Date,
                UserId = reportDTO.UserId,
                SpeciesId = reportDTO.SpeciesId,
            };

            _ReportRepo.Add(report);
            _ReportRepo.SaveChanges();

            return CreatedAtAction("GetReport", new { id = report.Id }, reportDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReport(int id)
        {
            var report = _ReportRepo.GetById(id);
            if (report == null)
            {
                return NotFound();
            }

            _ReportRepo.Delete(report);
            _ReportRepo.SaveChanges();

            return NoContent();
        }

        private bool ReportExists(int id)
        {
            return _ReportRepo.ReportExists(id);
        }

    }
}
