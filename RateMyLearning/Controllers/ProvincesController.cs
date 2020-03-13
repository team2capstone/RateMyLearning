using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;

namespace RateMyLearning.Controllers
{
    public class ProvincesController : ControllerBase
    {
        private readonly rmldbContext _context;

        public ProvincesController(rmldbContext context)
        {
            _context = context;
        }

        public void CreateProvince(string provinceName) {
            var province = new Province { Name = provinceName };
            _context.Province.Add(province);
            _context.SaveChanges();
        }
    }
}
