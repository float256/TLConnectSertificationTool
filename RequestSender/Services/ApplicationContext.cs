using Microsoft.EntityFrameworkCore;
using RequestSender.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestSender.Services
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<RequestInfo> RequestInfoSet { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RequestInfo>().ToTable("request_info");
        }
    }
}
