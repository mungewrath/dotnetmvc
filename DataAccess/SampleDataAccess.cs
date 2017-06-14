using System;
using System.Linq;

namespace dotnetmvc.DataAccess
{
    public class SampleDataAccess : ISampleDataAccess
    {
        private readonly SamplePostgresDbContext dbContext;

        public SampleDataAccess(SamplePostgresDbContext context) {
            dbContext = context;
        }
        public string GetAttachmentName(int attachmentId)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                Attachment a = unitOfWork.GetRepository<Attachment>().Get(x => x.attachmentid == attachmentId).Single();
                return a.filename;
            }
        }
    }
}