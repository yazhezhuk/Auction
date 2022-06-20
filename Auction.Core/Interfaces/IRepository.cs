namespace Auction.Core.Interfaces;

public interface IRepository<TEntity, in TKey>
{ 
     Task<List<TEntity>> GetAll();
     List<TEntity> GetPart(int from,int to);
     Task<TEntity?> Get(TKey id);

     Task<int> Delete(TEntity entity);
     Task<int> Delete(TKey entityId);

     bool Add(TEntity entity);

     bool Update(TEntity entity);
}