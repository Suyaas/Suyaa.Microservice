﻿using Suyaa.DependencyInjection;
using Suyaa.Hosting.Jwt.ActionFilters;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Hosting.Jwt.Options;

namespace Suyaa.Hosting.Jwt.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {

        /// <summary>
        /// 添加Jwt数据支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="jwtOptionAction"></param>
        /// <returns></returns>
        public static IDependencyManager AddJwt(this IDependencyManager dependency, Action<JwtOption>? jwtOptionAction = null)
        {
            var option = new JwtOption();
            jwtOptionAction?.Invoke(option);
            dependency.AddJwt<JwtDataProvider, JwtData>(option);
            return dependency;
        }

        /// <summary>
        /// 添加Jwt数据支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IDependencyManager AddJwt<TProvider, TData>(this IDependencyManager dependency, JwtOption option)
            where TProvider : class, IJwtDataProvider<TData>
            where TData : class, IJwtData, new()
        {
            // 注册配置
            dependency.Register(option);
            // 注册管理器
            dependency.Register<IJwtManager<TData>, JwtManager<TData>>(Lifetimes.Transient);
            // 注册数据供应商
            dependency.Register<IJwtBuilder<TData>, JwtBuilder<TData>>(Lifetimes.Transient);
            // 注册数据供应商
            dependency.Register<IJwtDataProvider<TData>, TProvider>(Lifetimes.Transient);
            // 注册授权过滤器
            dependency.Register<JwtAuthorizeFilter<TData>, JwtAuthorizeFilter<TData>>(Lifetimes.Transient);
            return dependency;
        }
    }
}
