﻿using Suyaa.Data.Dependency;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 可查询
    /// </summary>
    public interface IDbQueryable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();
    }
}
