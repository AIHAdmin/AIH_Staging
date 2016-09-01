using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgingInHome.BLL.Repositories;
using AgingInHome.DAL;
using AutoMapper;

namespace AgingInHome.Models
{
    public class RatingModal
    {
        public int RatingId { get; set; }
        public int ProviderListingId { get; set; }
        public string ProviderId { get; set; }
        public string UserId { get; set; }
        public Nullable<int> Speedpoint { get; set; }
        public Nullable<int> QualityPoint { get; set; }
        public Nullable<int> ReliabilityPoint { get; set; }
        public string Comments { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }


        public bool AddRating(RatingModal _RatingModal)
        {
            var Status = Mapper.Map<RatingTable>(_RatingModal);            
            return new Rating().AddRating(Status);
        }
        public bool CheckEmailExits(RatingModal _RatingModal)
        {
            var Status = Mapper.Map<RatingTable>(_RatingModal);
           return new Rating().CheckEmailExist(Status);
           
        }
        public bool UserEmailExits(string Email)
        {
           
            return new Rating().UserEmailExits(Email);

        }
       

    }
}