using Microsoft.EntityFrameworkCore;
using RequestSender.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestSender.Services.Repositories
{
    public class RequestInfoRepository: IRequestInfoRepository
    {
        private readonly ApplicationContext _context;

        public RequestInfoRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public void Add(RequestInfo requestInfo)
        {
            _context.RequestInfoSet.Add(requestInfo);
            _context.SaveChanges();
        }

        public List<RequestInfo> Get(Func<RequestInfo, bool> predicate)
        {
            return _context.RequestInfoSet.Where(predicate).ToList();
        }

        public RequestInfo GetById(int id)
        {
            return _context.RequestInfoSet.SingleOrDefault(x => x.Id == id);
        }

        public void Update(RequestInfo requestInfo)
        {
            _context.RequestInfoSet.Update(requestInfo);
            _context.Entry(requestInfo).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(Func<RequestInfo, bool> predicate)
        {
            _context.RequestInfoSet.RemoveRange(_context.RequestInfoSet.Where(predicate));
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            RequestInfo requestInfo = new RequestInfo { Id = id };
            _context.RequestInfoSet.Remove(requestInfo);
            _context.SaveChanges();
        }
    }
}
