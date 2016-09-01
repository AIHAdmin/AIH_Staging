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
    public class ListingBlogModel
    {
        public int ListingBlogId { get; set; }
        public int ListingId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public string BlogImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsCommentOn { get; set; }
        public HttpPostedFileBase imageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public ListingBlogModel addBlog(ListingBlogModel listingBlogModel)
        {
            listingBlogModel.CreatedDate = DateTime.Now;
            var model = Mapper.Map<ListingBlog>(listingBlogModel);
            var result = new ListingBlogClass().AddBlog(model);
            return Mapper.Map<ListingBlogModel>(result);

        }
        public ListingBlogModel EditBlog(int blogid)
        {
            var Bolg = new ListingBlogClass().GetBlogById(blogid);
            var model = Mapper.Map<ListingBlogModel>(Bolg);
            return model;

        }
        public ProviderTeamModel EditTeam(Guid teamid)
        {
            ProviderTeamModel _ProviderTeamModel = new ProviderTeamModel();
            var team = new ListingBlogClass().GetTeamById(teamid);

            _ProviderTeamModel.Teamtitle = team.Teamtitle;
            _ProviderTeamModel.TeamPosition = team.TeamPosition;
            _ProviderTeamModel.TeamMiddleName = team.TeamMiddleName;
            _ProviderTeamModel.TeamLastName = team.TeamLastName;
            _ProviderTeamModel.TeamFirstName = team.TeamFirstName;
            _ProviderTeamModel.TeamBiography = team.TeamBiography;
            _ProviderTeamModel.ListingTeamId = team.ListingTeamId;
            _ProviderTeamModel.ListingId = team.ListingId;
            _ProviderTeamModel.IsActive = team.IsActive;
            _ProviderTeamModel.imageUrl = team.imageUrl;
            
            return _ProviderTeamModel;

        }
        public ListingBlogModel UpdateBlog(ListingBlogModel listingBlogModel)
        {
            var model = Mapper.Map<ListingBlog>(listingBlogModel);
            // return new ListingBlogClass().UpdateBlog(model);
            var result = new ListingBlogClass().UpdateBlog(model);
            return Mapper.Map<ListingBlogModel>(result);

        }
        public bool DeleteBlog(int blogid)
        {
            var blog = new ListingBlogClass().DelBlogById(blogid);
            return blog;

        }

        public bool BlogIsPublic(int blogid, bool IsActive)
        {
            var blog = new ListingBlogClass().BlogIsPublic(blogid, IsActive);
            return blog;

        }
        public bool ArticleIsPublic(int ArticleId, bool IsActive)
        {
            var Article = new ListingBlogClass().ArticleIsPublic(ArticleId, IsActive);
            return Article;
        }

        public bool PhotoIsPublic(int ArticleId, bool IsActive)
        {
            var Article = new ListingBlogClass().PhotoIsPublic(ArticleId, IsActive);
            return Article;
        }
        public bool TeamIsPublic(Guid TeamId, bool IsActive)
        {
            var Article = new ListingBlogClass().TeamIsPublic(TeamId, IsActive);
            return Article;
        }
        public bool VideoIsPublic(int VideoId, bool IsActive)
        {
            var Videos = new ListingBlogClass().VideoIsPublic(VideoId, IsActive);
            return Videos;
        }

    }
}