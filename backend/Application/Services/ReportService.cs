﻿using Application.DTO.Raport;
using Application.DTO.Report;
using Application.DTO.Service;
using Application.Interfaces.Reports;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class ReportService : IReportService
    {
        private readonly IReportRepository _reporteRepo;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reporteRepo, IMapper mapper)
        {
            _reporteRepo = reporteRepo;
            _mapper = mapper;
        }

        public int AddReport(NewReportDto report)
        {
            var rep = _mapper.Map<Report>(report);
            var id = _reporteRepo.AddReport(rep);
            return id;
        }

        public void DeleteReport(int reportId)
        {
            throw new NotImplementedException();
        }

        public object GetReport(int reportId)
        {
            var report = _reporteRepo.GetReport(reportId);
            var reportDto = _mapper.Map<ReportDetailsDto>(report);
            return reportDto;
        }

        public object GetReportForEdit(int reportId)
        {
            throw new NotImplementedException();
        }

        public List<ReportForListDto> GetReportsForList()
        {
            var reports = _reporteRepo.GetAllReports()
            .ProjectTo<ReportForListDto>(_mapper.ConfigurationProvider)
            .ToList();
            return reports;
        }

        public object UpdateReport()
        {
            throw new NotImplementedException();
        }
    }
}