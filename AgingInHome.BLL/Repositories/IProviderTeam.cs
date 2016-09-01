using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
    public interface IProviderTeamRepository
    {
        ProviderTeam AddTeam(ProviderTeam listingBlog);
        ProviderTeam EditTeam(int listingBlogId);
        ProviderTeam UpdateTeam(ProviderTeam _ListingBlog);
        ProviderTeam GetTeamById(int blogId);
        bool DelTeamById(int BlogId);

    }
    public class ListingTeamClass : IProviderTeamRepository
    {
        public ProviderTeam AddTeam(ProviderTeam TeamBlog)
        {
            using (var db = new AgingInHomeContext())
            {
                db.ProviderTeams.Add(TeamBlog);
                db.SaveChanges();
                return TeamBlog;
            }
        }

        public bool DelTeamById(int BlogId)
        {
            using (var db = new AgingInHomeContext())
            {
                var _Blog = db.ProviderTeams.Find(BlogId);
                db.ProviderTeams.Remove(_Blog);
                db.SaveChanges();
                return true;

            }
        }

        public ProviderTeam EditTeam(int listingBlogId)
        {
            using (var db = new AgingInHomeContext())
            {
                var _Blog = db.ProviderTeams.Find(listingBlogId);

                return _Blog;

            }
        }

        public ProviderTeam GetTeamById(int blogId)
        {
            using (var db = new AgingInHomeContext())
            {
                return db.ProviderTeams.Find(blogId);
            }
        }

        public ProviderTeam UpdateTeam(ProviderTeam _ListingTeam)
        {
            using (var db = new AgingInHomeContext())
            {
                var _Team = db.ProviderTeams.Find(_ListingTeam.ListingTeamId);
                _Team.TeamFirstName = _ListingTeam.TeamFirstName;
                if (_ListingTeam.imageUrl != null)
                {
                    _Team.imageUrl = _ListingTeam.imageUrl;
                }
                else {
                    _Team.imageUrl = _Team.imageUrl;
                }
                _Team.Teamtitle = _ListingTeam.Teamtitle;
                _Team.TeamMiddleName = _ListingTeam.TeamMiddleName;
                _Team.TeamLastName = _ListingTeam.TeamLastName;
                _Team.TeamPosition = _ListingTeam.TeamPosition;
                _Team.TeamBiography = _ListingTeam.TeamBiography;
                _Team.ModifiedBy = _ListingTeam.ModifiedBy;
                _Team.ModifiedDate = _ListingTeam.ModifiedDate;
                db.SaveChanges();
                return _Team;
            }
        }
    }
}
