using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Video.Application.Contract.Base;
using Video.Application.Contract.Videos.Dtos;
using Video.Domain.Videos.Views;

namespace Video.Application.Contract.Videos
{
    public interface IVideoService
    {
        /// <summary>
        /// ������Ƶ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateVideoInput input);

        /// <summary>
        /// ɾ����Ƶ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ɾ����Ƶ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task AdminDeleteAsync(Guid id);

        /// <summary>
        /// ��ȡ��Ƶ�б�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<GetVideoListDto>> GetListAsync(GetVideoListInput input);

        /// <summary>
        /// ������Ƶ����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task CreateClassifyAsync(string name);

        /// <summary>
        /// ɾ����Ƶ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteClassifyAsync(Guid id);

        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task UpdateClassifyAsync(ClassifyDto dto);

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<ClassifyDto>> GetListClassifyDtoAsync(string? name);

        /// <summary>
        /// �Ƿ���ɾ����ǰ��Ƶ�ļ�
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<bool> WhetheritCanBeDeleted(string path);


        /// <summary>
        /// ͳ��ʱ��ι�ע����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> StatisticalConcernAsync(StatisticalConcernInput input);

        /// <summary>
        /// ͳ�Ʒ�������
        /// </summary>
        /// <returns></returns>
        Task<int> ReleaseCountasync(VideoInput input);
    }
}