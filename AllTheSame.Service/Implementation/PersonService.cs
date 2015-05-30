using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;
using AllTheSame.Service.Interfaces;

namespace AllTheSame.Service.Implementation
{
    /// <summary>
    ///     PersonService
    /// </summary>
    public class PersonService : EntityService<Person, IPersonRepository>, IPersonService
    {
        /// <summary>
        ///     The _person repository
        /// </summary>
        private readonly IPersonRepository _repository;

        /// <summary>
        ///     The _unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddressService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The person repository.</param>
        public PersonService(IUnitOfWork unitOfWork, IPersonRepository repository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Person GetById(long id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }

            if (_repository != null)
            {
                _repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}