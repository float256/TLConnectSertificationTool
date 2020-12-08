using RequestSender.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestSender.Services.Repositories
{
    public interface IRequestInfoRepository
    {
        void Add(RequestInfo requestInfo);
        List<RequestInfo> Get(Func<RequestInfo, bool> predicate);
        RequestInfo GetById(int id);
        void Update(RequestInfo requestInfo);
        void Delete(Func<RequestInfo, bool> predicate);
        void Delete(int id);
    }
}
