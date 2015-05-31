using System.Data.Entity;
using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Authentication;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.implementation;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Implementation;

namespace AllTheSame.Service
{
    /// <summary>
    ///     Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : GenericRepository<T> where T : class, IEntity<int>
    {
        /// <summary>
        ///     The _instance
        /// </summary>
        private static GenericRepository<T> _instance;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static GenericRepository<T> Instance
        {
            get
            {
                _instance = _instance ?? new GenericRepository<T>();
                return _instance;
            }
        }
    }

    /// <summary>
    ///     ServiceProxy
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    public class ServiceProxy<TEntity, TRepository> : EntityService<TEntity, TRepository>
        where TEntity : class, IEntity<int> where TRepository : class, IGenericRepository<TEntity>
    {
        /// <summary>
        ///     The _repository
        /// </summary>
        private IGenericRepository<TEntity> _repository;

        /// <summary>
        ///     The _unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        public ServiceProxy(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
            : base(unitOfWork, repository as TRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        /// <summary>
        ///     Gets the repository.
        /// </summary>
        /// <value>
        ///     The repository.
        /// </value>
        protected IGenericRepository<TEntity> CurrentRepository
        {
            get
            {
                if (_repository != null) return _repository;

                var allTheSameDbContext = new AllTheSameDbContext();
                _unitOfWork = new UnitOfWork(allTheSameDbContext);
                _repository = new Repository<TEntity>(allTheSameDbContext);

                return _repository;
            }
        }

        /// <summary>
        ///     Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                    _unitOfWork = null;
                }

                if (_repository != null)
                {
                    _repository.Dispose();
                    _repository = null;
                }
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    ///     ServiceProxy
    /// </summary>
    public class ServiceProxy : AllTheSameDbContext, IUnitOfWork
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ServiceProxy" /> class.
        /// </summary>
        public ServiceProxy()
        {
            _context = new AllTheSameDbContext();
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static ServiceProxy Instance
        {
            get
            {
                _instance = _instance ?? new ServiceProxy();
                return _instance;
            }
        }

        /// <summary>
        ///     Saves all pending changes
        /// </summary>
        /// <returns>
        ///     The number of objects in an Added, Modified, or Deleted state
        /// </returns>
        public int Commit()
        {
            return SaveChanges();
        }

        /// <summary>
        ///     Gets the entity service.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <returns></returns>
        public static EntityService<TEntity, TRepository> GetServiceRefernce<TEntity, TRepository>(
            TRepository repository)
            where TRepository : class, IGenericRepository<TEntity> where TEntity : class, IEntity<int>
        {
            var uow = new UnitOfWork(Instance._context);

            return new EntityService<TEntity, TRepository>(uow, repository);
        }

        /// <summary>
        ///     Gets the service refernce.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static EntityService<TEntity, IGenericRepository<TEntity>> GetServiceRefernce<TEntity>(DbContext context)
            where TEntity : class, IEntity<int>
        {
            var uow = new UnitOfWork(context);
            var respository = new GenericRepository<TEntity>(context);

            return new EntityService<TEntity, IGenericRepository<TEntity>>(uow, respository);
        }

        /// <summary>
        ///     Disposes the context. The underlying <see cref="T:System.Data.Entity.Core.Objects.ObjectContext" /> is also
        ///     disposed if it was created
        ///     is by this context or ownership was passed to this context when this context was created.
        ///     The connection to the database (<see cref="T:System.Data.Common.DbConnection" /> object) is also disposed if it was
        ///     created
        ///     is by this context or ownership was passed to this context when this context was created.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        // ReSharper disable once FunctionComplexityOverflow
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                #region cleanup

                //
                _authRepository?.Dispose();
                _authService?.Dispose();
                _personRepository?.Dispose();
                _personService?.Dispose();
                _userRepository?.Dispose();
                _userService?.Dispose();
                _userSessionRepository?.Dispose();
                _userSessionService?.Dispose();
                _vendorRepository?.Dispose();
                _vendorService?.Dispose();
                
                _context?.Dispose();
                //

                #endregion cleanup
            }
            base.Dispose(disposing);
        }

        #region Local Properties

        //
        /// <summary>
        ///     The _DB context
        /// </summary>
        private readonly AllTheSameDbContext _context;

        /// <summary>
        ///     The _instance
        /// </summary>
        private static ServiceProxy _instance;

        /// <summary>
        ///     The _auth repository
        /// </summary>
        private static IAuthRepository _authRepository;

        /// <summary>
        ///     The _auth service
        /// </summary>
        private static AuthService _authService;

        /// <summary>
        ///     The _user repository
        /// </summary>
        private static IUserRepository _userRepository;

        /// <summary>
        ///     The _user service
        /// </summary>
        private static UserService _userService;

        /// <summary>
        ///     The _person repository
        /// </summary>
        private static IPersonRepository _personRepository;

        /// <summary>
        ///     The _person service
        /// </summary>
        private static PersonService _personService;

        /// <summary>
        ///     The _user session repository
        /// </summary>
        private static IUserSessionRepository _userSessionRepository;

        /// <summary>
        ///     The _user session service
        /// </summary>
        private static UserSessionService _userSessionService;

        /// <summary>
        ///     The _vendor repository
        /// </summary>
        private static IVendorRepository _vendorRepository;

        /// <summary>
        ///     The _vendor service
        /// </summary>
        private static VendorService _vendorService;

        //

        #endregion Local Properties

        #region Common Repositories

        //
        //
        /// <summary>
        ///     Gets the authentication service proxy.
        /// </summary>
        /// <value>
        ///     The authentication service proxy.
        /// </value>
        public static AuthService AuthServiceProxy
        {
            get
            {
                _authRepository = new AuthRepository(Instance._context);

                _authService = _authService ?? new AuthService(Instance, _authRepository);
                return _authService;
            }
        }

        /// <summary>
        ///     Gets the user service proxy.
        /// </summary>
        /// <value>
        ///     The user service proxy.
        /// </value>
        public static UserService UserServiceProxy
        {
            get
            {
                _userRepository = new UserRepository(Instance._context);

                _userService = _userService ?? new UserService(Instance, _userRepository);
                return _userService;
            }
        }

        /// <summary>
        ///     Gets the user session service proxy.
        /// </summary>
        /// <value>
        ///     The user session service proxy.
        /// </value>
        public static UserSessionService UserSessionServiceProxy
        {
            get
            {
                _userSessionRepository = new UserSessionRepository(Instance._context);

                _userSessionService = _userSessionService ?? new UserSessionService(Instance, _userSessionRepository);
                return _userSessionService;
            }
        }

        /// <summary>
        ///     Gets the person service proxy.
        /// </summary>
        /// <value>
        ///     The person service proxy.
        /// </value>
        public static PersonService PersonServiceProxy
        {
            get
            {
                _personRepository = new PersonRepository(Instance._context);

                _personService = _personService ?? new PersonService(Instance, _personRepository);
                return _personService;
            }
        }

        /// <summary>
        ///     Gets the vendor service proxy.
        /// </summary>
        /// <value>
        ///     The vendor service proxy.
        /// </value>
        public static VendorService VendorServiceProxy
        {
            get
            {
                _vendorRepository = new VendorRepository(Instance._context);

                _vendorService = _vendorService ?? new VendorService(Instance, _vendorRepository);
                return _vendorService;
            }
        }

        //
        //

        #endregion Common Repositories
    }
}