﻿using Simple.EntityFrameworkCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Video.Domain.Videos
{
    public interface IVideoRepository : IRepository<Video>
    {
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="classifyId"></param>
        /// <param name="keywords"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        Task<List<Video>> GetListAsync(Guid? userId,Guid? classifyId, string? keywords, DateTime? startTime, DateTime? endTime, int skipCount,
            int maxResultCount);

        /// <summary>
        /// 获取视频列表数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="classifyId"></param>
        /// <param name="keywords"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Guid? userId, Guid? classifyId, string? keywords, DateTime? startTime, DateTime? endTime);

        /// <summary>
        /// 创建视频分类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task CreateClassifyAsync(string name);

        /// <summary>
        /// 删除视频分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteClassifyAsync(Guid id);

        /// <summary>
        /// 编辑视频分类
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>
        Task UpdateClassifyAsync(Classify classify);

        /// <summary>
        /// 获取视频分类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<Classify>> GetClassifyListAsync(string? name);

        /// <summary>
        /// 统计时间段关注数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<int> StatisticalConcernAsync(Guid userId,DateTime? startTime,DateTime? endTime);

        /// <summary>
        /// 统计时间段发布数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<int> ReleaseCountasync(Guid userId, DateTime? startTime, DateTime? endTime);
    }
}
