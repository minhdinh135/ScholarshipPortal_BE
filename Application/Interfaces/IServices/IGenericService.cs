namespace Application.Interfaces.IServices;
public interface IGenericService<T, AD, UD> where T : class
{
  Task<IEnumerable<T>> GetAll();
  Task<T> Get(params int[] keys);
  Task<T> Add(AD dto);
  Task<T> Update(UD dto);
  Task<T> Delete(params int[] keys);
}
