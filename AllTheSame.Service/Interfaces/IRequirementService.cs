﻿using AllTheSame.Entity.Model;
using AllTheSame.Repository.UserData.interfaces;
using AllTheSame.Service.Common;

namespace AllTheSame.Service.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IRequirementService : IEntityService<Requirement, IRequirementRepository>
    {
    }
}