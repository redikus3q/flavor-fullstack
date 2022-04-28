using Shisha.Models;
using Shisha.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _context;
        private IUserRepository _user;
        private ISessionTokenRepository _sessionToken;
        private IFlavorRepository _flavor;
        private ICommentRepository _comment;

        public RepositoryWrapper(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null) _user = new UserRepository(_context);
                return _user;
            }
        }

        public ISessionTokenRepository SessionToken
        {
            get
            {
                if (_sessionToken == null) _sessionToken = new SessionTokenRepository(_context);
                return _sessionToken;
            }
        }

        public IFlavorRepository Flavor
        {
            get
            {
                if (_flavor == null) _flavor = new FlavorRepository(_context);
                return _flavor;
            }
        }

        public ICommentRepository Comment
        {
            get
            {
                if (_comment == null) _comment = new CommentRepository(_context);
                return _comment;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
