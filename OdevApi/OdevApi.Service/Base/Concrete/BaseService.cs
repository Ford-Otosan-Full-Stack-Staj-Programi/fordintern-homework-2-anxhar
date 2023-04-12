using AutoMapper;
using OdevApi.Base;
using OdevApi.Data;
using Serilog;

namespace OdevApi.Service.Base;

public abstract class BaseService<Dto, TEntity> : IBaseService<Dto, TEntity> where TEntity : class
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IGenericRepository<TEntity> genericRepository;

    public BaseService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<TEntity> genericRepository)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.genericRepository = genericRepository;
    }

    public virtual BaseResponse<List<Dto>> GetAll()
    {
        var entityList = genericRepository.GetAll();
        var mapped = mapper.Map<List<TEntity>, List<Dto>>(entityList);
        return new BaseResponse<List<Dto>>(mapped);
    }

    public virtual BaseResponse<Dto> GetById(int id)
    {
        var entity = genericRepository.GetById(id);
        var mapped = mapper.Map<TEntity, Dto>(entity);
        return new BaseResponse<Dto>(mapped);
    }

    public virtual BaseResponse<bool> Insert(Dto insertResource)
    {
        try
        {
            var entity = mapper.Map<Dto, TEntity>(insertResource);

            genericRepository.Insert(entity);
            unitOfWork.Complete();

            return new BaseResponse<bool>(true);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "BaseService_Insert");
            return new BaseResponse<bool>(ex.StackTrace);
        }
    }

    public virtual BaseResponse<bool> Remove(int id)
    {
        try
        {
            var entity = genericRepository.GetById(id);
            if (entity is null)
            {
                return new BaseResponse<bool>("No_Data");
            }

            genericRepository.Remove(entity);
            unitOfWork.Complete();

            return new BaseResponse<bool>(true);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "BaseService_Delete");
            return new BaseResponse<bool>(ex.StackTrace);
        }
    }

    public virtual BaseResponse<bool> Update(int id, Dto updateResource)
    {
        try
        {
            var entity = genericRepository.GetById(id);
            if (entity is null)
            {
                return new BaseResponse<bool>("No_Data");
            }

            var mappedEntity = mapper.Map<Dto, TEntity>(updateResource);
            mappedEntity.GetType().GetProperty("Id").SetValue(mappedEntity, id);

            genericRepository.Update(mappedEntity);
            unitOfWork.Complete();

            return new BaseResponse<bool>(true);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "BaseService_Update");
            return new BaseResponse<bool>(ex.StackTrace);
        }
    }
}
