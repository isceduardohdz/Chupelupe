using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chupelupe.Models;

namespace Chupelupe.Services
{
    public interface IWebServiceApi
    {
        Task<List<Promotion>> GetPromotions();
    }
}
