using AgingInHome.BLL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class CountryModel
    {
        public int Id { get; set; }
        public string abbreviation { get; set; }
        public string CountryName { get; set; }
        public List<CountryModel> GetAllCountry()
        {
            var contries= new Services().GetAllCountries();
            var _contries = Mapper.Map<List<CountryModel>>(contries);
            return _contries;
        }
    }
}