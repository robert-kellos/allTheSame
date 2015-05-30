using System.Data.Entity;
using AllTheSame.Entity.Model;
using AllTheSame.Repository.Common;
using AllTheSame.Repository.UserData.interfaces;

namespace AllTheSame.Repository.UserData.implementation
{
    /// <summary>
    ///     ModuleRepository
    /// </summary>
    public class ModuleRepository : SyncRepository<Module>, IModuleRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ModuleRepository" /> class.
        /// </summary>
        /// <param name="context"></param>
        public ModuleRepository(DbContext context)
            : base(context)
        {
            //
        }
    }
}