﻿using Application.Mapping;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Comment
{
    public class CommentDto : IMapFrom<Domain.Model.Comment>
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string  Author { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Comment, CommentDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId))
                .ForMember(d => d.Author, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName))
                .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date));
        }
    }
}