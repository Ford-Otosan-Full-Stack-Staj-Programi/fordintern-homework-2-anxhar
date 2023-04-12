using OdevApi.Base;

namespace OdevApi.Service.Base;

public interface IBaseService<Dto, TEntity>
{
    BaseResponse<Dto> GetById(int id);
    BaseResponse<List<Dto>> GetAll();
    BaseResponse<bool> Insert(Dto insertResource);
    BaseResponse<bool> Update(int id, Dto updateResource);
    BaseResponse<bool> Remove(int id);
}
