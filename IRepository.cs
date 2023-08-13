namespace Todo_List_App
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(int id);
    }
}