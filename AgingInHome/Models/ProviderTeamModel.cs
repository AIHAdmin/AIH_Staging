using AgingInHome.BLL.Repositories;
using AgingInHome.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet;
using Microsoft.AspNet.Identity;

namespace AgingInHome.Models
{
    public class ProviderTeamModel
    {
        public System.Guid ListingTeamId { get; set; }
        public int ListingId { get; set; }
        public string Teamtitle { get; set; }
        public string TeamFirstName { get; set; }
        public string TeamMiddleName { get; set; }
        public string TeamLastName { get; set; }
        public string TeamPosition { get; set; }
        public string TeamBiography { get; set; }
        public HttpPostedFileBase imageUrlhttp { get; set; }
        public string imageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }

        public string Imagebytes { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> TotalRecords { get; set; }
        public ProviderTeamModel addTeam(ProviderTeamModel ProviderTeamModel)
        {
            ProviderTeamModel.CreatedDate = DateTime.Now;
            Mapper.Initialize(cnfg => { cnfg.CreateMap<ProviderTeamModel, ProviderTeam>(); });

            var model = Mapper.Map<ProviderTeamModel, ProviderTeam>(ProviderTeamModel);
            var result = new ListingTeamClass().AddTeam(model);
            ProviderTeamModel.ListingTeamId = result.ListingTeamId;
            return ProviderTeamModel;

        }
        public ProviderTeamModel EditTeam(int teamid)
        {
            var Team = new ListingTeamClass().GetTeamById(teamid);
            var model = Mapper.Map<ProviderTeamModel>(Team);
            return model;

        }
        public ProviderTeamModel UpdateTeam(ProviderTeamModel ProviderTeamModel)
        {
            //var model = Mapper.Map<ProviderTeam>(ProviderTeamModel);
            // return new ListingBlogClass().UpdateBlog(model);
            ProviderTeam _ProviderTeam = new ProviderTeam();
            _ProviderTeam.ListingId = ProviderTeamModel.ListingId;
            _ProviderTeam.ListingTeamId = ProviderTeamModel.ListingTeamId;
            _ProviderTeam.TeamBiography = ProviderTeamModel.TeamBiography;
            _ProviderTeam.TeamFirstName = ProviderTeamModel.TeamFirstName;
            _ProviderTeam.TeamLastName = ProviderTeamModel.TeamLastName;
            _ProviderTeam.TeamMiddleName = ProviderTeamModel.TeamMiddleName;
            _ProviderTeam.TeamPosition = ProviderTeamModel.TeamPosition;
            _ProviderTeam.Teamtitle = ProviderTeamModel.Teamtitle;
            if (ProviderTeamModel.imageUrlhttp != null)
                _ProviderTeam.imageUrl = ProviderTeamModel.imageUrl;

            _ProviderTeam.ModifiedBy = ProviderTeamModel.ModifiedBy;
            _ProviderTeam.ModifiedDate = DateTime.Now;
            var result = new ListingTeamClass().UpdateTeam(_ProviderTeam);
            if (result != null)
            {
                ProviderTeamModel.IsActive = result.IsActive;
                ProviderTeamModel.imageUrl = result.imageUrl;
                return ProviderTeamModel;
            }
            else
                return new ProviderTeamModel();

        }
        public bool DeleteTeam(int Teamid)
        {
            var team = new ListingTeamClass().DelTeamById(Teamid);
            return team;

        }
    }
}