using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public class VendorAPIRepository : IDisposable
    {

        VendorAPIWebEntities context = new VendorAPIWebEntities();

        private UnitOfWork<ServiceProvider> serviceProviderRepository;
        private UnitOfWork<ServiceProviderIssue> serviceProviderIssueRepository;
        private UnitOfWork<Status> statusRepository;
        private UnitOfWork<Type> typeRepository;
        private UnitOfWork<Phase> phaseRepository;
        private UnitOfWork<User> userRepository;
        private UnitOfWork<UserRole> userRoleRepository;

        public UnitOfWork<ServiceProvider> ServiceProviderRepository
        {
            get
            {
                if (this.serviceProviderRepository == null)
                    this.serviceProviderRepository = new UnitOfWork<ServiceProvider>(context);
                return serviceProviderRepository;
            }
        }

        public UnitOfWork<ServiceProviderIssue> ServiceProviderIssueRepository
        {
            get
            {
                if (this.serviceProviderIssueRepository == null)
                    this.serviceProviderIssueRepository = new UnitOfWork<ServiceProviderIssue>(context);
                return serviceProviderIssueRepository;
            }
        }

        public UnitOfWork<Status> StatusRepository
        {
            get
            {
                if (this.statusRepository == null)
                    this.statusRepository = new UnitOfWork<Status>(context);
                return statusRepository;
            }
        }

        public UnitOfWork<Type> TypeRepository
        {
            get
            {
                if (this.typeRepository == null)
                    this.typeRepository = new UnitOfWork<Type>(context);
                return typeRepository;
            }
        }

        public UnitOfWork<Phase> PhaseRepository
        {
            get
            {
                if (this.phaseRepository == null)
                    this.phaseRepository = new UnitOfWork<Phase>(context);
                return phaseRepository;
            }
        }

        public UnitOfWork<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new UnitOfWork<User>(context);
                return userRepository;
            }
        }

        public UnitOfWork<UserRole> UserRoleRepository
        {
            get
            {
                if (this.userRoleRepository == null)
                    this.userRoleRepository = new UnitOfWork<UserRole>(context);
                return userRoleRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
