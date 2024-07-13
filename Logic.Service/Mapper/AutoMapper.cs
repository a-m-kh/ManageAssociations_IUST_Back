using AutoMapper;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Logic.Service.Responses;
using Logic.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Mapper
{
	public class AutoMapper : Profile
	{
		public AutoMapper()
		{
			CreateMap<Event, EventCreateDto>().ReverseMap();
			CreateMap<Event, EventUpdateDto>().ReverseMap();
			CreateMap<Association, AssociationCreateDto>().ReverseMap();
			CreateMap<EventGetResponse, EventViewDto >().ReverseMap();
			CreateMap<EventCreateViewModel, EventCreateDto>();
			CreateMap<EventUpdateViewModel, EventUpdateDto>();
			CreateMap<EventUpdateDto, EventUpdateViewModel>();
			//CreateMap<EventCreateDto, Event>();
			//CreateMap<EventCreateDto, Event>();




			CreateMap<GetAssociationResponse, AssociationViewDto>().ReverseMap();
			CreateMap<BaseInfoDto, BaseInfoResponse>();
			CreateMap<BaseInfoResponse, BaseInfoDto>();
			CreateMap<AssociationUpdateDto, UpdateAssociationViewModel>().ReverseMap()
				.ForMember(res => res.LogoUrl, m => m
					.MapFrom(u => (string)null));

			CreateMap<CreateAssociationViewModel, AssociationCreateDto>()
				.ForMember(res => res.LogoUrl, m => m
					.MapFrom(u =>(string)null));


			CreateMap<GetAssociationResponseForUser, AssociationViewDto>();
			CreateMap<AssociationViewDto, GetAssociationResponseForUser>();


			CreateMap<Communication, CreateCommunicationDto>();
			CreateMap<CreateCommunicationDto, Communication>();
			CreateMap<CreateCommunicationDto, CreateCommunicationViewModel>();
			CreateMap<CreateCommunicationViewModel, CreateCommunicationDto>();
			CreateMap<UpdateCommunicationDto, UpdateCommunicationViewModel>();
			CreateMap<UpdateCommunicationViewModel, UpdateCommunicationDto>();
			CreateMap<GetCommunicationResponse, GetCommunicationDto>();
			CreateMap<GetCommunicationDto, GetCommunicationResponse>();

			CreateMap<Guest, CreateGuestDto>();
			CreateMap<CreateGuestDto, Guest>();
			CreateMap<CreateGuestDto, Communication>();
			CreateMap<CreateGuestDto, CreateGuestViewModel>();
			CreateMap<CreateGuestViewModel, CreateGuestDto>();
			CreateMap<UpdateGuestDto, UpdateGuestViewModel>();
			CreateMap<UpdateGuestViewModel, UpdateGuestDto>();
			CreateMap<GetGuestResponse, GetGuestDto>();
			CreateMap<GetGuestDto, GetGuestResponse>();

			CreateMap<AssociationMember, CreateAssociationMemberDto>();
			CreateMap<CreateAssociationMemberDto, AssociationMember>();
			CreateMap<CreateAssociationMemberDto, CreateAssociationMemberViewModel>();
			CreateMap<CreateAssociationMemberViewModel, CreateAssociationMemberDto>();
			CreateMap<UpdateAssociationMemberDto, UpdateAssociationMemberViewModel>();
			CreateMap<UpdateAssociationMemberViewModel, UpdateAssociationMemberDto>();
			CreateMap<GetAssociationMemberResponse, GetAssociationMemberDto>();
			CreateMap<GetAssociationMemberDto, GetAssociationMemberResponse>();

			CreateMap<Journal, CreateJournalDto>();
			CreateMap<CreateJournalDto, Journal>();
			CreateMap<CreateJournalDto, CreateJournalViewModel>();
			CreateMap<CreateJournalViewModel, CreateJournalDto>();
			CreateMap<UpdateJournalDto, UpdateJournalViewModel>();
			CreateMap<UpdateJournalViewModel, UpdateJournalDto>();
			CreateMap<GetJournalResponse, GetJournalDto>();
			CreateMap<GetJournalDto, GetJournalResponse>();

			CreateMap<Certification, CreateCertificationDto>();
			CreateMap<CreateCertificationDto, Certification>();
			CreateMap<CreateCertificationDto, CreateCertificationViewModel>();
			CreateMap<CreateCertificationViewModel, CreateCertificationDto>();
			CreateMap<UpdateCertificationDto, UpdateCertificationViewModel>();
			CreateMap<UpdateCertificationViewModel, UpdateCertificationDto>();
			CreateMap<GetCertificationResponse, GetCertificationDto>();
			CreateMap<GetCertificationDto, GetCertificationResponse>();


			CreateMap<Report, CreateReportDto>();
			CreateMap<CreateReportDto, Report>();
			CreateMap<CreateReportDto, CreateReportViewModel>();
			CreateMap<CreateReportViewModel, CreateReportDto>();
			CreateMap<UpdateReportDto, UpdateReportViewModel>();
			CreateMap<UpdateReportViewModel, UpdateReportDto>();
			//CreateMap<GetReportResponse, GetCertificationDto>();
			//CreateMap<GetCertificationDto, GetCertificationResponse>();


			CreateMap<News, CreateNewsDto>();
			CreateMap<CreateNewsDto, News>();
			CreateMap<CreateNewsDto, CreateNewsViewModel>();
			CreateMap<CreateNewsViewModel, CreateNewsDto>();
			CreateMap<UpdateNewsDto, UpdateNewsViewModel>();
			CreateMap<UpdateNewsViewModel, UpdateNewsDto>();
			CreateMap<GetNewsResponse, GetNewsDto>();
			CreateMap<GetNewsDto, GetNewsResponse>();
			CreateMap<GetNewsForAdminResponse, GetNewsDto>();
			CreateMap<GetNewsDto, GetNewsForAdminResponse>();
			CreateMap<GeneralPaginationModel<GetNewsForAdminResponse>, GeneralPaginationModel<GetNewsDto>>();
			CreateMap<GeneralPaginationModel<GetNewsDto>, GeneralPaginationModel<GetNewsForAdminResponse>>();



			CreateMap<GuestLicense, CreateGuestLicenseDto>();
			CreateMap<CreateGuestLicenseDto, GuestLicense>();
			CreateMap<CreateGuestLicenseDto, CreateGuestLicenseViewModel>();
			CreateMap<CreateGuestLicenseViewModel, CreateGuestLicenseDto>();
			CreateMap<UpdateGuestLicenseDto, UpdateGuestLicenseViewModel>();
			CreateMap<UpdateGuestLicenseViewModel, UpdateGuestLicenseDto>();
			CreateMap<GetGuestLicenseResponse, GetGuestLicenseDto>();
			CreateMap<GetGuestLicenseDto, GetGuestLicenseResponse>();
			CreateMap<GetListGuestLicenseResponse, GetGuestLicenseDto>();
			CreateMap<GetGuestLicenseDto, GetListGuestLicenseResponse>();
			CreateMap<GeneralPaginationModel<GetListGuestLicenseResponse>, GeneralPaginationModel<GetGuestLicenseDto>>();
			CreateMap<GeneralPaginationModel<GetGuestLicenseDto>, GeneralPaginationModel<GetListGuestLicenseResponse>>();



			CreateMap<CertificateOfAbsence, CreateCertificateOfAbsenceDto>();
			CreateMap<CreateCertificateOfAbsenceDto, CertificateOfAbsence>();
			CreateMap<CreateCertificateOfAbsenceDto, CreateCertificateOfAbsenceViewModel>();
			CreateMap<CreateCertificateOfAbsenceViewModel, CreateCertificateOfAbsenceDto>();
			CreateMap<UpdateCertificateOfAbsenceDto, UpdateCertificateOfAbsenceViewModel>();
			CreateMap<UpdateCertificateOfAbsenceViewModel, UpdateCertificateOfAbsenceDto>();
			CreateMap<GetCertificateOfAbsenceResponse, GetCertificateOfAbsenceDto>();
			CreateMap<GetCertificateOfAbsenceDto, GetCertificateOfAbsenceResponse>();
			CreateMap<GetListCertificateOfAbsenceResponse, GetCertificateOfAbsenceDto>();
			CreateMap<GetCertificateOfAbsenceDto, GetListCertificateOfAbsenceResponse>();

			CreateMap<GetForUserSliderImageResponse, SliderImage>();
			CreateMap<SliderImage, GetForUserSliderImageResponse>();
		}
	}
}
